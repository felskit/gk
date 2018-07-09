using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectCars.Models;
using TomShane.Neoforce.Controls;
using EventArgs = TomShane.Neoforce.Controls.EventArgs;

namespace ProjectCars
{
    public enum CameraMode { StaticFixed, StaticFollowing, Attached }
    public enum CollisionType { None, Boundary }
    public enum GameState { Playing, Paused }

    public class ProjectCars : Game
    {
        private const float Acceleration = 0.0005f;
        private const float BrakePower = 3.0f;
        private const float Inertia = 4.0f;
        private const float MaxSpeed = 0.06f;
        private const float CameraStep = 0.033f;
        private const float WaypointTolerance = 0.1f;

        private readonly Random _random = new Random();
        private GameState _gameState = GameState.Playing;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private GraphicsDevice _device;
        private Effect _effect;

        private Matrix _viewMatrix;
        private Matrix _projectionMatrix;

        private int[,] _raceTrack;
        private Texture2D _raceTrackTexture;
        private BoundingBox _raceTrackBox;
        private VertexBuffer _vertexBuffer;

        private Model _skyboxModel;
        private Model[] _staticObjectModels;
        private Dictionary<int, List<Matrix>> _staticObjectMatrices;

        private int _currentWaypointIdx;
        private Vector3 _currentWaypointPosition;
        private List<Vector3> _waypointPositions;

        private Meter _meter;
        private Vehicle _ai;
        private Vehicle _car;
        private Camera _camera;

        private Vector4 _ambientColor;
        private Vector4[] _lightColors;
        private Vector3[] _lightPositions;
        private float[] _lightIntensities;

        private Vector4[] _spotlightColors;
        private Vector3[] _spotlightOffsets;
        private Vector3[] _spotlightPositions;
        private Vector3[] _spotlightDirections;
        private float[] _spotlightIntensities;

        private bool _menuOpening;
        private bool _cameraChanging;
        private bool _spotlightsChanging;
        private bool _spotlights;

        private Manager _ui;
        private Window _menuWindow;
        private Label _menuLabel;

        private Button _resumeButton;
        private Button _resetButton;
        private Button _settingsButton;
        private Button _exitButton;

        private Label _shadingLabel;
        private Label _lightingLabel;
        private Label _objectsLabel;
        private ComboBox _shadingComboBox;
        private ComboBox _lightingComboBox;
        private CheckBox _objectsCheckBox;
        private Button _backButton;

        private string CurrentModel => $"{_shadingComboBox.Text.Substring(1)}{_lightingComboBox.Text.Substring(1)}";

        #region Initialize

        public ProjectCars()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            Window.Title = "Project Cars";

            _ai = new Vehicle(new Vector3(9.7f, 0, -4.1f));
            _car = new Vehicle(new Vector3(9.3f, 0, -4.1f));

            InitControls();
            InitRaceTrack();
            InitWaypoints();
            InitLightData();

            _camera = new Camera(_raceTrack.GetLength(0) / 2.0f, -_raceTrack.GetLength(1) / 2.0f, new Vector3(0, 0.25f, 0.75f));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Fonts/CourierNew");
            _device = _graphics.GraphicsDevice;
            _effect = Content.Load<Effect>("effects");

            _raceTrackTexture = Content.Load<Texture2D>("Map/Racetrack/racetrack");
            _skyboxModel = LoadModel("Map/Skybox/skybox", 0.8f, 1.0f, 1.0f, 15, 1.0f);

            _staticObjectModels = new[]
            {
                LoadModel("Environment/Sphere/sphere", 0.4f, 0.8f, 0.6f, 15, 0.6f),
                LoadModel("Environment/Tunnel/tunnel", 0.4f, 0.8f, 0.6f, 15, 0.6f),
                LoadModel("Environment/Dumpster/dumpster", 0.4f, 0.8f, 0.6f, 15, 0.6f),
                LoadModel("Environment/Debris/debris", 0.4f, 0.8f, 0.6f, 15, 0.6f),
                LoadModel("Environment/Farmhouse/farmhouse", 0.4f, 0.8f, 0.4f, 15, 0.6f),
                LoadModel("Environment/Trees/tree1", 0.4f, 0.8f, 0.2f, 15, 0.6f),
                LoadModel("Environment/Trees/tree2", 0.4f, 0.8f, 0.2f, 15, 0.6f)
            };

            _meter = new Meter(new Vector2(
                    GraphicsDevice.Viewport.Width / 1.5f,
                    GraphicsDevice.Viewport.Height / 2.36f),
                Content.Load<Texture2D>("Meter/gauge"),
                Content.Load<Texture2D>("Meter/needle"),
                _spriteBatch, 0.4f);

            _ai.Model = LoadModel("Cars/Subaru/subaru", 0.3f, 0.8f, 0.8f, 15, 1.0f);
            _car.Model = LoadModel("Cars/Mercedes/mercedes", 0.3f, 0.8f, 0.8f, 15, 1.0f);

            SetUpCamera();
            SetUpRaceTrackVertices();
            SetUpRaceTrackBoundingBox();
        }

        #region Setup functions

        private Model LoadModel(string assetName, float ka, float kd, float ks, float shininess, float ambientIntensity)
        {
            var newModel = Content.Load<Model>(assetName);
            foreach (var mesh in newModel.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    if ((meshPart.Effect as BasicEffect)?.Texture == null)
                    {
                        _effect.Parameters["xDiffuseColor"].SetValue(new Vector4((meshPart.Effect as BasicEffect)?.DiffuseColor ?? new Vector3(1, 1, 1), 1));
                        _effect.Parameters["xTextured"].SetValue(false);
                    }
                    else
                    {
                        _effect.Parameters["xTexture"].SetValue(((BasicEffect)meshPart.Effect).Texture);
                        _effect.Parameters["xTextured"].SetValue(true);
                    }

                    _effect.Parameters["Ka"].SetValue(ka);
                    _effect.Parameters["Kd"].SetValue(kd);
                    _effect.Parameters["Ks"].SetValue(ks);
                    _effect.Parameters["Shininess"].SetValue(shininess);

                    _effect.Parameters["xAmbientColor"].SetValue(_ambientColor);
                    _effect.Parameters["xAmbientIntensity"].SetValue(ambientIntensity);

                    _effect.Parameters["xLightColors"].SetValue(_lightColors);
                    _effect.Parameters["xLightPositions"].SetValue(_lightPositions);
                    _effect.Parameters["xLightIntensities"].SetValue(_lightIntensities);

                    _effect.Parameters["xSpotlightColors"].SetValue(_spotlightColors);
                    _effect.Parameters["xSpotlightIntensities"].SetValue(_spotlightIntensities);

                    meshPart.Effect = _effect.Clone();
                }
            }
            return newModel;
        }

        private void InitRaceTrack()
        {
            _raceTrack = new[,]
            {
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,6,1,1,1,1,3,0,0},
                {0,0,2,0,0,0,0,2,0,0},
                {0,0,2,0,0,0,0,2,0,0},
                {0,0,5,1,1,3,0,2,0,0},
                {0,0,6,1,1,4,0,2,0,0},
                {0,0,2,0,0,0,0,2,0,0},
                {0,0,2,0,0,0,0,2,0,0},
                {0,0,5,1,7,3,0,2,0,0},
                {0,0,0,0,0,2,0,2,0,0},
                {0,0,0,6,1,4,0,2,0,0},
                {0,0,0,5,1,1,1,4,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0}
            };

            var sphereScale = Matrix.CreateScale(0.001f, 0.001f, 0.001f);
            var tunnelScale = Matrix.CreateScale(0.033f, 0.033f, 0.033f);
            var dumpsterScale = Matrix.CreateScale(0.0003f, 0.0003f, 0.0003f);
            var debrisScale = Matrix.CreateScale(0.02f, 0.02f, 0.02f);
            var farmhouseScale = Matrix.CreateScale(0.025f, 0.025f, 0.025f);
            var treeScale = Matrix.CreateScale(0.0004f, 0.0004f, 0.0004f);

            _staticObjectMatrices = new Dictionary<int, List<Matrix>>
            {
                {
                    0, new List<Matrix>
                    {
                        sphereScale * Matrix.CreateTranslation(new Vector3(10.5f, 0.11f, -3.5f))
                    }
                },
                {
                    1, new List<Matrix>
                    {
                        tunnelScale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(new Vector3(7.5f, -0.1f, -7.5f))
                    }
                },
                {
                    2, new List<Matrix>
                    {
                        dumpsterScale * Matrix.CreateRotationY(7.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(2.4f,0,-8.5f)),
                        dumpsterScale * Matrix.CreateRotationY(5.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(13.8f,0,-2.7f))
                    }
                },
                {
                    3, new List<Matrix>
                    {
                        debrisScale * Matrix.CreateRotationY(1 * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(2.9f,-0.0001f,-9.0f)),
                        debrisScale * Matrix.CreateRotationY(3 * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(1.6f,-0.0001f,-8.4f)),
                        debrisScale * Matrix.CreateRotationY(5 * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(13.4f,-0.0001f,-2.1f))
                    }
                },
                {
                    4, new List<Matrix>
                    {
                        farmhouseScale * Matrix.CreateRotationY(5.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(1.5f,0,-1.7f)),
                        farmhouseScale * Matrix.CreateRotationY(1.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(13.2f,0,-8.4f))
                    }
                },
                {
                    5, new List<Matrix>
                    {
                        treeScale * Matrix.CreateRotationY(1.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(1.2f,0,-7.0f)),
                        treeScale * Matrix.CreateRotationY(2.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(3.6f,0,-3.8f)),
                        treeScale * Matrix.CreateRotationY(3.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(3.7f,0,-5.6f)),
                        treeScale * Matrix.CreateRotationY(4.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(6.0f,0,-2.2f)),
                        treeScale * Matrix.CreateRotationY(5.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(6.0f,0,-9.0f)),
                        treeScale * Matrix.CreateRotationY(6.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(8.6f,0,-3.4f)),
                        treeScale * Matrix.CreateRotationY(7.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(9.1f,0,-6.5f)),
                        treeScale * Matrix.CreateRotationY(0.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(9.2f,0,-1.6f)),
                        treeScale * Matrix.CreateRotationY(1.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(12.6f,0,-9.3f)),
                        treeScale * Matrix.CreateRotationY(2.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(13.4f,0,-3.8f))
                    }
                },
                {
                    6, new List<Matrix>
                    {
                        treeScale * Matrix.CreateRotationY(1.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(1.2f,0,-5.0f)),
                        treeScale * Matrix.CreateRotationY(2.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(3.0f,0,-1.5f)),
                        treeScale * Matrix.CreateRotationY(3.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(4.8f,0,-4.4f)),
                        treeScale * Matrix.CreateRotationY(4.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(6.0f,0,-6.5f)),
                        treeScale * Matrix.CreateRotationY(5.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(8.0f,0,-5.2f)),
                        treeScale * Matrix.CreateRotationY(6.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(9.8f,0,-8.6f)),
                        treeScale * Matrix.CreateRotationY(7.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(10.5f,0,-4.3f)),
                        treeScale * Matrix.CreateRotationY(0.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(11.5f,0,-6.2f)),
                        treeScale * Matrix.CreateRotationY(1.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(11.8f,0,-2.2f)),
                        treeScale * Matrix.CreateRotationY(2.5f * MathHelper.PiOver4) * Matrix.CreateTranslation(new Vector3(14.2f,0,-7.2f))
                    }
                }
            };
        }

        private void InitWaypoints()
        {
            _waypointPositions = new List<Vector3>
            {
                new Vector3(9.7f,0,-5),
                new Vector3(10,0,-5.5f),
                new Vector3(11,0,-5.5f),
                new Vector3(11.5f,0,-5),
                new Vector3(11.5f,0,-4),
                new Vector3(12f,0,-3.5f),
                new Vector3(12.5f,0,-4),
                new Vector3(12.5f,0,-7),
                new Vector3(12,0,-7.5f),
                new Vector3(3,0,-7.5f),
                new Vector3(2.5f,0,-7),
                new Vector3(2.5f,0,-3),
                new Vector3(3,0,-2.5f),
                new Vector3(5,0,-2.5f),
                new Vector3(5.5f,0,-3),
                new Vector3(5.5f,0,-5),
                new Vector3(6,0,-5.5f),
                new Vector3(6.5f,0,-5),
                new Vector3(6.5f,0,-3),
                new Vector3(7,0,-2.5f),
                new Vector3(9,0,-2.5f),
                new Vector3(9.7f,0,-3),
                new Vector3(9.7f,0,-4.1f)
            };

            SetCurrentWaypoint(0);
        }

        private void InitLightData()
        {
            _ambientColor = new Vector4(1, 1, 1, 1);
            _lightColors = new[] {
                new Vector4(1, 1, 1, 1),
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 1, 0, 1),
                new Vector4(1, 0, 0, 1)
            };
            _lightPositions = new[] {
                new Vector3(10.5f, 1, -6),
                new Vector3(4, 1, -2.5f),
                new Vector3(2, 1, -6),
                new Vector3(12, 1, -3)
            };
            _lightIntensities = new[] { 1.0f, 1.0f, 1.0f, 1.0f };

            _spotlights = true;
            _spotlightColors = new[]
            {
                new Vector4(1, 1, 1, 1),
                new Vector4(1, 1, 1, 1),
                new Vector4(1, 1, 1, 1),
                new Vector4(1, 1, 1, 1)
            };
            _spotlightOffsets = new[]
            {
                new Vector3(-0.075f, 0.08f, -0.2f),
                new Vector3(0.075f, 0.08f, -0.2f),
                new Vector3(-0.07f, 0.08f, -0.2f),
                new Vector3(0.07f, 0.08f, -0.2f)
            };
            _spotlightPositions = new[]
            {
                _car.InitialPosition + _spotlightOffsets[0],
                _car.InitialPosition + _spotlightOffsets[1],
                _ai.InitialPosition + _spotlightOffsets[2],
                _ai.InitialPosition + _spotlightOffsets[3]
            };
            _spotlightIntensities = new[] { 0.8f, 0.8f, 0.8f, 0.8f };
            _spotlightDirections = new[]
            {
                new Vector3(0,-0.15f, -1),
                new Vector3(0,-0.15f, -1),
                new Vector3(0,-0.15f, -1),
                new Vector3(0,-0.15f, -1)
            };
        }

        private void SetUpCamera()
        {
            _viewMatrix = Matrix.CreateLookAt(new Vector3(0, 1, 0), new Vector3(1, 0, -1), new Vector3(0, 1, 0));
            _projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, _device.Viewport.AspectRatio, 0.2f, 500.0f);
        }

        private void SetUpRaceTrackVertices()
        {
            const float cols = 4.0f;
            const float rows = 2.0f;

            var raceTrackWidth = _raceTrack.GetLength(0);
            var raceTrackLength = _raceTrack.GetLength(1);

            var verticesList = new List<VertexPositionNormalTexture>();
            for (var x = 0; x < raceTrackWidth; x++)
            {
                for (var z = 0; z < raceTrackLength; z++)
                {
                    var tile = _raceTrack[x, z];

                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z), new Vector3(0, 1, 0), new Vector2(tile % cols / cols, ((int)(tile / cols) + 1) / rows)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z - 1), new Vector3(0, 1, 0), new Vector2(tile % cols / cols, (int)(tile / cols) / rows)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z), new Vector3(0, 1, 0), new Vector2((tile % cols + 1) / cols, ((int)(tile / cols) + 1) / rows)));

                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x, 0, -z - 1), new Vector3(0, 1, 0), new Vector2(tile % cols / cols, (int)(tile / cols) / rows)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z - 1), new Vector3(0, 1, 0), new Vector2((tile % cols + 1) / cols, (int)(tile / cols) / rows)));
                    verticesList.Add(new VertexPositionNormalTexture(new Vector3(x + 1, 0, -z), new Vector3(0, 1, 0), new Vector2((tile % cols + 1) / cols, ((int)(tile / cols) + 1) / rows)));
                }
            }

            _vertexBuffer = new VertexBuffer(_device, VertexPositionNormalTexture.VertexDeclaration, verticesList.Count, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(verticesList.ToArray());
        }

        private void SetUpRaceTrackBoundingBox()
        {
            var raceTrackWidth = _raceTrack.GetLength(0);
            var raceTrackLength = _raceTrack.GetLength(1);

            var boundaryPoints = new Vector3[2];
            boundaryPoints[0] = new Vector3(0, -20, 0);
            boundaryPoints[1] = new Vector3(raceTrackWidth, 20, -raceTrackLength);
            _raceTrackBox = BoundingBox.CreateFromPoints(boundaryPoints);
        }

        #endregion

        #endregion

        #region Update

        protected override void Update(GameTime gameTime)
        {
            ProcessKeyboard(gameTime);

            if (_gameState == GameState.Playing)
            {
                _car.Drive();

                var carSpere = new BoundingSphere(_car.Position, 0.22f);
                if (CheckCollision(carSpere) != CollisionType.None)
                {
                    ResetScene();
                }

                UpdateSpotlights();

                switch (_camera.Mode)
                {
                    case CameraMode.Attached:
                        UpdateAttachedCamera();
                        break;
                    case CameraMode.StaticFixed:
                        UpdateStaticFixedCamera();
                        break;
                    case CameraMode.StaticFollowing:
                        UpdateStaticFollowingCamera();
                        break;
                }
            }

            _ui.Update(gameTime);
            base.Update(gameTime);
        }

        #region Camera functions

        private void UpdateAttachedCamera()
        {
            _camera.Rotation = Quaternion.Lerp(_camera.Rotation, _car.Rotation, 0.08f);

            _camera.ResetPosition();
            _camera.Position = Vector3.Transform(_camera.Position, Matrix.CreateRotationY(_camera.Angle));
            _camera.Position = Vector3.Transform(_camera.Position, Matrix.CreateFromQuaternion(_camera.Rotation));
            _camera.Position += _car.Position;

            _camera.Up = new Vector3(0, 1, 0);
            _camera.Up = Vector3.Transform(_camera.Up, Matrix.CreateFromQuaternion(_camera.Rotation));

            _viewMatrix = Matrix.CreateLookAt(_camera.Position, _car.Position, _camera.Up);
        }

        private void UpdateStaticFixedCamera()
        {
            _camera.Rotation = Quaternion.Lerp(_camera.Rotation, _car.Rotation, 0.08f);

            _camera.ResetPosition();
            _camera.Position += _camera.Offset;

            var target = new Vector3(_camera.Position.X, 0, _camera.Position.Z);
            _camera.Up = new Vector3(0, 0, -1);
            _viewMatrix = Matrix.CreateLookAt(_camera.Position, target, _camera.Up);
        }

        private void UpdateStaticFollowingCamera()
        {
            _camera.Rotation = Quaternion.Lerp(_camera.Rotation, _car.Rotation, 0.08f);

            _camera.ResetPosition();
            _camera.Position += _camera.Offset;

            _camera.Up = new Vector3(0, 1, 0);
            _viewMatrix = Matrix.CreateLookAt(_camera.Position, _car.Position, _camera.Up);
        }

        #endregion

        #region Light functions

        private void UpdateSpotlights()
        {
            for (int i = 0; i < 2; i++)
            {
                _spotlightDirections[i] = Vector3.Transform(new Vector3(0, -0.15f, -1), Matrix.CreateFromQuaternion(_car.Rotation));
                _spotlightPositions[i] = Vector3.Transform(_spotlightOffsets[i], Matrix.CreateFromQuaternion(_car.Rotation));
                _spotlightPositions[i] += _car.Position;
            }
            for (int i = 2; i < 4; i++)
            {
                _spotlightDirections[i] = Vector3.Transform(new Vector3(0, -0.15f, -1), Matrix.CreateFromQuaternion(_ai.Rotation));
                _spotlightPositions[i] = Vector3.Transform(_spotlightOffsets[i], Matrix.CreateFromQuaternion(_ai.Rotation));
                _spotlightPositions[i] += _ai.Position;
            }
        }

        #endregion

        private void ProcessKeyboard(GameTime gameTime)
        {
            var keys = Keyboard.GetState();

            #region Menu

            if (keys.IsKeyDown(Keys.Escape) && _gameState == GameState.Playing && !_menuOpening)
            {
                _menuOpening = true;
                ShowMainMenu();
            }
            else if (keys.IsKeyDown(Keys.Escape) && _gameState == GameState.Paused && !_menuOpening)
            {
                _menuOpening = true;
                HideMenu();
            }

            if (keys.IsKeyUp(Keys.Escape) && _menuOpening)
            {
                _menuOpening = false;
            }

            #endregion

            if (_gameState == GameState.Paused) return;

            #region Turning

            var rotation = 0.0f;
            var turningSpeed = 1.6f * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;

            if (keys.IsKeyDown(Keys.D) && _car.DrivingSpeed > Inertia * Acceleration || keys.IsKeyDown(Keys.A) && _car.DrivingSpeed < Inertia * -Acceleration)
            {
                rotation += turningSpeed;
            }
            else if (keys.IsKeyDown(Keys.A) && _car.DrivingSpeed > Inertia * Acceleration || keys.IsKeyDown(Keys.D) && _car.DrivingSpeed < Inertia * -Acceleration)
            {
                rotation -= turningSpeed;
            }

            var additionalRotation = Quaternion.CreateFromAxisAngle(new Vector3(0, -1, 0), rotation);
            _car.Rotation *= additionalRotation;

            #endregion

            #region Driving speed

            if (keys.IsKeyDown(Keys.W) && _car.DrivingSpeed < MaxSpeed)
            {
                _car.DrivingSpeed += Acceleration;
                if (_car.DrivingSpeed < 0.0f)
                {
                    _car.DrivingSpeed += BrakePower * Acceleration;
                }
            }
            else if (keys.IsKeyDown(Keys.S) && _car.DrivingSpeed > 0.33 * -MaxSpeed)
            {
                _car.DrivingSpeed -= Acceleration;
                if (_car.DrivingSpeed > 0.0f)
                {
                    _car.DrivingSpeed -= BrakePower * Acceleration;
                }
            }
            else if (_car.DrivingSpeed > 0.0f)
            {
                _car.DrivingSpeed -= Acceleration;
            }
            else if (_car.DrivingSpeed < 0.0f)
            {
                _car.DrivingSpeed += Acceleration;
            }

            if (Math.Abs(_car.DrivingSpeed) < 0.5f * Acceleration)
            {
                _car.DrivingSpeed = 0f;
            }

            #endregion

            #region Light

            if (keys.IsKeyDown(Keys.L) && !_spotlightsChanging)
            {
                _spotlightsChanging = true;
                _spotlights = !_spotlights;
            }

            if (keys.IsKeyUp(Keys.L) && _spotlightsChanging)
            {
                _spotlightsChanging = false;
            }

            #endregion

            #region Camera

            if (keys.IsKeyDown(Keys.C) && !_cameraChanging)
            {
                _cameraChanging = true;
                var cameraMode = (int)_camera.Mode;
                _camera.Mode = (CameraMode)((cameraMode + 1) % 3);
                _camera.Reset();
            }

            if (keys.IsKeyUp(Keys.C) && _cameraChanging)
            {
                _cameraChanging = false;
            }

            if (keys.IsKeyDown(Keys.Up))
            {
                _camera.Offset.Z -= CameraStep;
            }
            else if (keys.IsKeyDown(Keys.Down))
            {
                _camera.Offset.Z += CameraStep;
            }

            if (keys.IsKeyDown(Keys.Left))
            {
                _camera.Offset.X -= CameraStep;
            }
            else if (keys.IsKeyDown(Keys.Right))
            {
                _camera.Offset.X += CameraStep;
            }

            if (keys.IsKeyDown(Keys.Q))
            {
                _camera.Angle += CameraStep;
            }
            else if (keys.IsKeyDown(Keys.E))
            {
                _camera.Angle -= CameraStep;
            }
            else if (keys.IsKeyDown(Keys.R))
            {
                _camera.Reset();
            }

            #endregion

            #region AI

            float currentSpeed;
            var distance = Vector3.Distance(_ai.Position, _currentWaypointPosition);

            if (distance < 0.5f)
            {
                currentSpeed = MaxSpeed / 5;
            }
            else if (distance < 2)
            {
                currentSpeed = MaxSpeed / 4;
            }
            else if (distance < 4)
            {
                currentSpeed = MaxSpeed / 3;
            }
            else
            {
                currentSpeed = MaxSpeed / 2;
            }

            _ai.Rotation = Quaternion.Lerp(_ai.Rotation, CalculateWaypointQuaternion(_ai.Position, _currentWaypointPosition), 0.05f);
            _ai.DrivingSpeed = MathHelper.Lerp(_ai.DrivingSpeed, currentSpeed, 0.01f);
            _ai.Drive();

            if (distance < 0.1f)
            {
                SetCurrentWaypoint((_currentWaypointIdx + 1) % _waypointPositions.Count);
            }

            #endregion

            #region Meter

            _meter.Update(Math.Abs(_car.DrivingSpeed) * 1000, 238);

            #endregion
        }

        #endregion

        #region Helper functions

        private void SetCurrentWaypoint(int idx)
        {
            _currentWaypointIdx = idx;
            _currentWaypointPosition = _waypointPositions[_currentWaypointIdx] + new Vector3((float)(_random.NextDouble() * WaypointTolerance), 0, (float)(_random.NextDouble() * WaypointTolerance));
        }

        private Quaternion CalculateWaypointQuaternion(Vector3 carPosition, Vector3 waypointPosition)
        {
            var reference = new Vector3(0, 0, -1);
            var referenceAngle = Math.Atan2(reference.Z, reference.X);
            var direction = waypointPosition - carPosition;
            var angle = (float)(Math.Atan2(direction.Z, direction.X) - referenceAngle);
            return Quaternion.CreateFromAxisAngle(new Vector3(0, -1, 0), angle);
        }

        private CollisionType CheckCollision(BoundingSphere sphere)
        {
            return _raceTrackBox.Contains(sphere) != ContainmentType.Contains ? CollisionType.Boundary : CollisionType.None;
        }

        private void ResetScene()
        {
            _camera.Reset(true);
            _car.Reset();
            _ai.Reset();
            SetCurrentWaypoint(0);
        }

        #endregion

        #region Draw

        protected override void Draw(GameTime gameTime)
        {
            _ui.BeginDraw(gameTime);
            _device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.DarkSlateBlue, 1.0f, 0);

            DrawSkybox();
            DrawRaceTrack();

            var aiMatrix = Matrix.CreateScale(0.09f, 0.09f, 0.09f) *
                           Matrix.CreateRotationY(MathHelper.Pi) *
                           Matrix.CreateFromQuaternion(_ai.Rotation) *
                           Matrix.CreateTranslation(_ai.Position);

            var carMatrix = Matrix.CreateScale(0.04f, 0.04f, 0.04f) *
                            Matrix.CreateRotationY(MathHelper.Pi) *
                            Matrix.CreateFromQuaternion(_car.Rotation) *
                            Matrix.CreateTranslation(_car.Position);

            DrawModel(_ai.Model, aiMatrix);
            DrawModel(_car.Model, carMatrix);

            if (_objectsCheckBox.Checked)
            {
                DrawStaticObjects();
            }

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, $"Shading model:{_shadingComboBox.Text}", new Vector2(12, 10), Color.Red, 0, Vector2.Zero, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);
            _spriteBatch.DrawString(_font, $"Lighting model:{_lightingComboBox.Text}", new Vector2(12, 27), Color.Red, 0, Vector2.Zero, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);
            _spriteBatch.DrawString(_font, $"Driving speed: {(int)(Math.Abs(_car.DrivingSpeed) * 1000)} km/h", new Vector2(12, 44), Color.Red, 0, Vector2.Zero, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);
            _spriteBatch.DrawString(_font, $"Position: {_car.Position.X:0.00} {_car.Position.Y} {_car.Position.Z:0.00}", new Vector2(12, 61), Color.Red, 0, Vector2.Zero, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);
            _spriteBatch.End();

            _meter.Draw();
            _ui.EndDraw();
            base.Draw(gameTime);
        }

        #region Drawing functions

        private void DrawModel(Model model, Matrix wMatrix)
        {
            var modelTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(modelTransforms);

            foreach (var mesh in model.Meshes)
            {
                foreach (var currentEffect in mesh.Effects)
                {
                    var worldMatrix = modelTransforms[mesh.ParentBone.Index] * wMatrix;
                    currentEffect.CurrentTechnique = currentEffect.Techniques[CurrentModel];
                    currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
                    currentEffect.Parameters["xView"].SetValue(_viewMatrix);
                    currentEffect.Parameters["xProjection"].SetValue(_projectionMatrix);
                    currentEffect.Parameters["xCameraPosition"].SetValue(_camera.Position);

                    currentEffect.Parameters["xSpotlights"].SetValue(_spotlights);
                    currentEffect.Parameters["xSpotlightPositions"].SetValue(_spotlightPositions);
                    currentEffect.Parameters["xSpotlightDirections"].SetValue(_spotlightDirections);
                }
                mesh.Draw();
            }
        }

        private void DrawSkybox()
        {
            var ss = new SamplerState { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp };
            _device.SamplerStates[0] = ss;

            var dss = new DepthStencilState { DepthBufferEnable = false };
            _device.DepthStencilState = dss;

            var skyboxTransforms = new Matrix[_skyboxModel.Bones.Count];
            _skyboxModel.CopyAbsoluteBoneTransformsTo(skyboxTransforms);

            foreach (var mesh in _skyboxModel.Meshes)
            {
                foreach (var currentEffect in mesh.Effects)
                {
                    var worldMatrix = skyboxTransforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(_car.Position);
                    currentEffect.CurrentTechnique = currentEffect.Techniques[CurrentModel];
                    currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
                    currentEffect.Parameters["xView"].SetValue(_viewMatrix);
                    currentEffect.Parameters["xProjection"].SetValue(_projectionMatrix);
                    currentEffect.Parameters["xCameraPosition"].SetValue(_camera.Position);
                    currentEffect.Parameters["xSpotlights"].SetValue(false);
                }
                mesh.Draw();
            }

            dss = new DepthStencilState { DepthBufferEnable = true };
            _device.DepthStencilState = dss;
        }

        private void DrawRaceTrack()
        {
            _effect.CurrentTechnique = _effect.Techniques[CurrentModel];
            _effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            _effect.Parameters["xView"].SetValue(_viewMatrix);
            _effect.Parameters["xProjection"].SetValue(_projectionMatrix);
            _effect.Parameters["xCameraPosition"].SetValue(_camera.Position);

            _effect.Parameters["xTexture"].SetValue(_raceTrackTexture);
            _effect.Parameters["xTextured"].SetValue(true);

            _effect.Parameters["Ka"].SetValue(0.4f);
            _effect.Parameters["Kd"].SetValue(0.6f);
            _effect.Parameters["Ks"].SetValue(0.2f);
            _effect.Parameters["Shininess"].SetValue(30);

            _effect.Parameters["xAmbientColor"].SetValue(_ambientColor);
            _effect.Parameters["xAmbientIntensity"].SetValue(1.0f);

            _effect.Parameters["xLightColors"].SetValue(_lightColors);
            _effect.Parameters["xLightPositions"].SetValue(_lightPositions);
            _effect.Parameters["xLightIntensities"].SetValue(_lightIntensities);

            _effect.Parameters["xSpotlights"].SetValue(_spotlights);
            _effect.Parameters["xSpotlightColors"].SetValue(_spotlightColors);
            _effect.Parameters["xSpotlightPositions"].SetValue(_spotlightPositions);
            _effect.Parameters["xSpotlightDirections"].SetValue(_spotlightDirections);
            _effect.Parameters["xSpotlightIntensities"].SetValue(_spotlightIntensities);

            foreach (var pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _device.SetVertexBuffer(_vertexBuffer);
                _device.DrawPrimitives(PrimitiveType.TriangleList, 0, _vertexBuffer.VertexCount / 3);
            }
        }

        private void DrawStaticObjects()
        {
            foreach (var kv in _staticObjectMatrices)
            {
                var model = _staticObjectModels[kv.Key];
                var modelTransforms = new Matrix[model.Bones.Count];
                model.CopyAbsoluteBoneTransformsTo(modelTransforms);

                foreach (var matrix in kv.Value)
                {
                    foreach (var mesh in model.Meshes)
                    {
                        foreach (var currentEffect in mesh.Effects)
                        {
                            var worldMatrix = kv.Key == 0 ? matrix : modelTransforms[mesh.ParentBone.Index] * matrix;
                            currentEffect.CurrentTechnique = currentEffect.Techniques[CurrentModel];
                            currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
                            currentEffect.Parameters["xView"].SetValue(_viewMatrix);
                            currentEffect.Parameters["xProjection"].SetValue(_projectionMatrix);
                            currentEffect.Parameters["xCameraPosition"].SetValue(_camera.Position);

                            currentEffect.Parameters["xSpotlights"].SetValue(_spotlights);
                            currentEffect.Parameters["xSpotlightPositions"].SetValue(_spotlightPositions);
                            currentEffect.Parameters["xSpotlightDirections"].SetValue(_spotlightDirections);
                        }
                        mesh.Draw();
                    }
                }
            }
        }

        #endregion

        #endregion

        #region User Interface

        private void InitControls()
        {
            _ui = new Manager(this, _graphics, "Green");
            _ui.Initialize();

            _menuWindow = new Window(_ui);
            _menuWindow.Init();
            _menuWindow.Text = "";
            _menuWindow.Width = 400;
            _menuWindow.Height = 300;
            _menuWindow.CloseButtonVisible = false;
            _menuWindow.DoubleClicks = false;
            _menuWindow.IconVisible = false;
            _menuWindow.Movable = false;
            _menuWindow.Resizable = false;
            _menuWindow.Center();

            _menuLabel = new Label(_ui);
            _menuLabel.Init();
            _menuLabel.Width = _menuWindow.ClientWidth / 4 + 12;
            _menuLabel.Height = 30;
            _menuLabel.Top = 15;
            _menuLabel.Left = (_menuWindow.ClientWidth - _menuLabel.Width) / 2;
            _menuLabel.TextColor = Color.White;
            _menuLabel.Text = "Main Menu";
            _menuLabel.Alignment = Alignment.MiddleCenter;
            _menuLabel.Parent = _menuWindow;

            _resumeButton = new Button(_ui);
            _resumeButton.Init();
            _resumeButton.Text = "Resume";
            _resumeButton.Width = 200;
            _resumeButton.Height = 40;
            _resumeButton.Top = 70;
            _resumeButton.Left = (_menuWindow.ClientWidth - _resumeButton.Width) / 2;
            _resumeButton.Click += ResumeButton_Click;
            _resumeButton.Parent = _menuWindow;

            _resetButton = new Button(_ui);
            _resetButton.Init();
            _resetButton.Text = "Reset";
            _resetButton.Width = 200;
            _resetButton.Height = 40;
            _resetButton.Top = 115;
            _resetButton.Left = (_menuWindow.ClientWidth - _resetButton.Width) / 2;
            _resetButton.Click += ResetButton_Click;
            _resetButton.Parent = _menuWindow;

            _settingsButton = new Button(_ui);
            _settingsButton.Init();
            _settingsButton.Text = "Settings";
            _settingsButton.Width = 200;
            _settingsButton.Height = 40;
            _settingsButton.Top = 160;
            _settingsButton.Left = (_menuWindow.ClientWidth - _settingsButton.Width) / 2;
            _settingsButton.Click += SettingsButton_Click;
            _settingsButton.Parent = _menuWindow;

            _exitButton = new Button(_ui);
            _exitButton.Init();
            _exitButton.Text = "Exit Game";
            _exitButton.Width = 200;
            _exitButton.Height = 40;
            _exitButton.Top = 205;
            _exitButton.Left = (_menuWindow.ClientWidth - _exitButton.Width) / 2;
            _exitButton.Click += ExitButton_Click;
            _exitButton.Parent = _menuWindow;

            _shadingLabel = new Label(_ui);
            _shadingLabel.Init();
            _shadingLabel.Width = 105;
            _shadingLabel.Height = 20;
            _shadingLabel.Top = 70;
            _shadingLabel.Left = _menuWindow.ClientWidth / 4 - _menuLabel.Width / 2;
            _shadingLabel.TextColor = Color.White;
            _shadingLabel.Text = " Shading model";
            _shadingLabel.Alignment = Alignment.MiddleLeft;
            _shadingLabel.Parent = _menuWindow;

            _lightingLabel = new Label(_ui);
            _lightingLabel.Init();
            _lightingLabel.Width = 105;
            _lightingLabel.Height = 20;
            _lightingLabel.Top = 95;
            _lightingLabel.Left = _menuWindow.ClientWidth / 4 - _menuLabel.Width / 2;
            _lightingLabel.TextColor = Color.White;
            _lightingLabel.Text = " Lighting model";
            _lightingLabel.Alignment = Alignment.MiddleLeft;
            _lightingLabel.Parent = _menuWindow;

            _objectsLabel = new Label(_ui);
            _objectsLabel.Init();
            _objectsLabel.Width = 105;
            _objectsLabel.Height = 20;
            _objectsLabel.Top = 120;
            _objectsLabel.Left = _menuWindow.ClientWidth / 4 - _menuLabel.Width / 2;
            _objectsLabel.TextColor = Color.White;
            _objectsLabel.Text = " Static objects";
            _objectsLabel.Alignment = Alignment.MiddleLeft;
            _objectsLabel.Parent = _menuWindow;

            _shadingComboBox = new ComboBox(_ui);
            _shadingComboBox.Init();
            _shadingComboBox.Width = 160;
            _shadingComboBox.Height = 20;
            _shadingComboBox.Top = 70;
            _shadingComboBox.Left = _menuWindow.ClientWidth / 4 + _shadingLabel.Width / 2 + 30;
            _shadingComboBox.TextColor = Color.White;
            _shadingComboBox.Items.Add(" Flat");
            _shadingComboBox.Items.Add(" Gouraud");
            _shadingComboBox.Items.Add(" Phong");
            _shadingComboBox.ItemIndex = _shadingComboBox.Items.IndexOf(" Phong");
            _shadingComboBox.Parent = _menuWindow;

            _lightingComboBox = new ComboBox(_ui);
            _lightingComboBox.Init();
            _lightingComboBox.Width = 160;
            _lightingComboBox.Height = 20;
            _lightingComboBox.Top = 95;
            _lightingComboBox.Left = _menuWindow.ClientWidth / 4 + _lightingLabel.Width / 2 + 30;
            _lightingComboBox.TextColor = Color.White;
            _lightingComboBox.Items.Add(" Phong");
            _lightingComboBox.Items.Add(" Blinn");
            _lightingComboBox.ItemIndex = _lightingComboBox.Items.IndexOf(" Phong");
            _lightingComboBox.Parent = _menuWindow;

            _objectsCheckBox = new CheckBox(_ui);
            _objectsCheckBox.Init();
            _objectsCheckBox.Left = _menuWindow.ClientWidth / 4 + _objectsLabel.Width / 2 + 30;
            _objectsCheckBox.Top = 124;
            _objectsCheckBox.Checked = true;
            _objectsCheckBox.Text = "";
            _objectsCheckBox.Parent = _menuWindow;

            _backButton = new Button(_ui);
            _backButton.Init();
            _backButton.Text = "Back";
            _backButton.Width = 200;
            _backButton.Height = 40;
            _backButton.Top = 160;
            _backButton.Left = (_menuWindow.ClientWidth - _backButton.Width) / 2;
            _backButton.Click += BackButton_Click;
            _backButton.Parent = _menuWindow;

            _menuWindow.Hide();
            _ui.Add(_menuWindow);
        }

        private void ShowMainMenu()
        {
            IsMouseVisible = true;
            _gameState = GameState.Paused;

            _menuLabel.Text = "Main Menu";
            _menuLabel.Show();

            new Control[] { _resumeButton, _resetButton, _settingsButton, _exitButton }.ToList().ForEach(x =>
            {
                x.Show();
                x.Focused = false;
            });
            new Control[] { _shadingLabel, _lightingLabel, _objectsLabel, _shadingComboBox, _lightingComboBox, _objectsCheckBox, _backButton }.ToList().ForEach(x => x.Hide());

            _menuWindow.Show();
        }

        private void ShowOptionsMenu()
        {
            IsMouseVisible = true;
            _gameState = GameState.Paused;

            _menuLabel.Text = "Settings";
            _menuLabel.Show();

            new Control[] { _resumeButton, _resetButton, _settingsButton, _exitButton }.ToList().ForEach(x => x.Hide());
            new Control[] { _shadingLabel, _lightingLabel, _objectsLabel, _shadingComboBox, _lightingComboBox, _objectsCheckBox, _backButton }.ToList().ForEach(x => x.Show());

            _menuWindow.Show();
        }

        private void HideMenu()
        {
            IsMouseVisible = false;
            _gameState = GameState.Playing;
            _menuWindow.Hide();
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            HideMenu();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetScene();
            HideMenu();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            ShowOptionsMenu();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ShowMainMenu();
        }

        #endregion
    }
}
