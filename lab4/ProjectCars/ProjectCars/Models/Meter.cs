using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectCars.Models
{
	public class Meter
	{
		private readonly SpriteBatch _spriteBatch;

		private const float MaxMeterAngle = 230;

		private readonly float _scale;
		private float _lastAngle;

		private readonly Vector2 _meterPosition;
		private readonly Vector2 _meterOrigin;

		private readonly Texture2D _backgroundImage;
		private readonly Texture2D _needleImage;

		public float CurrentAngle;

		public Meter(Vector2 position, Texture2D backgroundImage, Texture2D needleImage, SpriteBatch spriteBatch, float scale)
		{
			_spriteBatch = spriteBatch;

			_backgroundImage = backgroundImage;
			_needleImage = needleImage;
			_scale = scale;

			_lastAngle = 0;

			_meterPosition = new Vector2(position.X + backgroundImage.Width / 2.0f, position.Y + backgroundImage.Height / 2.0f);
			_meterOrigin = new Vector2(250, 250);
		}

		public void Update(float currentValue, float maximumValue)
		{
		    CurrentAngle = currentValue < 0 ? 0 : MathHelper.SmoothStep(_lastAngle, (currentValue / maximumValue) * MaxMeterAngle, 0.2f);
		    _lastAngle = CurrentAngle;
		}

		public void Draw()
		{
			_spriteBatch.Begin();
			_spriteBatch.Draw(_backgroundImage, _meterPosition, null, Color.White, 0, new Vector2(_backgroundImage.Width / 2.0f, _backgroundImage.Height / 2.0f), _scale, SpriteEffects.None, 0); //Draw(backgroundImage, position, Color.White);
			_spriteBatch.Draw(_needleImage, _meterPosition, null, Color.White, MathHelper.ToRadians(CurrentAngle) - MathHelper.PiOver2 - MathHelper.PiOver4 - 0.07f, _meterOrigin, _scale, SpriteEffects.None, 0);
			_spriteBatch.End();
		}
	}
}
