//------- K variables --------
float Ka;
float Kd;
float Ks;
float Shininess;

//------- Ambient light variables --------
float4 xAmbientColor;
float xAmbientIntensity;

//------- Point light variables --------
float4 xLightColors[4];
float3 xLightPositions[4];
float xLightIntensities[4];
int LightCount = 4;

//------- Spotlight variables --------
bool xSpotlights;
float4 xSpotlightColors[4];
float3 xSpotlightPositions[4];
float3 xSpotlightDirections[4];
float xSpotlightIntensities[4];
int SpotlightCount = 4;

//------- Camera variables --------
float3 xCameraPosition;

//------- Transformation matrices --------
float4x4 xWorld;
float4x4 xView;
float4x4 xProjection;

//------- Texture variables --------
bool xTextured;
float4 xDiffuseColor;

//------- Shader I/O structures --------
struct VertexShaderInput
{
    float4 Position          : POSITION0;
    float4 Normal            : NORMAL0;
    float2 TextureCoordinate : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position          : POSITION0;
    float4 PositionWorld     : TEXCOORD0;
    float2 TextureCoordinate : TEXCOORD1;
    float4 Intensity         : COLOR0;
    float3 Normal            : NORMAL0;
};

//------- Texture sampler --------
Texture2D xTexture;
sampler TextureSampler = sampler_state { texture = <xTexture>; magfilter = LINEAR; minfilter = LINEAR; mipfilter = LINEAR; AddressU = wrap; AddressV = wrap; };

//------- Lighting functions --------
float4 PhongLighting(float3 N, float3 L, float3 V, float3 R, float dist, float4 lightColor, float lightIntensity, float spotEffect)
{
    float4 Id = Kd * saturate(dot(N, L)) * spotEffect;
    float4 Is = Ks * pow(saturate(dot(-R, V)), Shininess);

    return (Id + Is) * lightColor * lightIntensity / (0.25 * dist * dist + 0.5 * dist);
}

float4 BlinnLighting(float3 N, float3 L, float3 H, float dist, float4 lightColor, float lightIntensity, float spotEffect)
{
    float4 Id = Kd * saturate(dot(N, L)) * spotEffect;
    float4 Is = Ks * pow(saturate(dot(N, H)), 2 * Shininess);

    return (Id + Is) * lightColor * lightIntensity / (0.25 * dist * dist + 0.5 * dist);
}

//------- Flat vertex shader --------
VertexShaderOutput FlatVertexShader(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;

    float4 worldPosition = mul(input.Position, xWorld);
    float4 viewPosition = mul(worldPosition, xView);
    output.Position = mul(viewPosition, xProjection);
    output.PositionWorld = worldPosition;

    output.TextureCoordinate = input.TextureCoordinate;

    return output;
}

//------- Flat shading pixel shaders --------
float4 FlatPhongPixelShader(VertexShaderOutput input) : COLOR0
{
    float3 N = normalize(cross(ddy(input.PositionWorld.xyz), ddx(input.PositionWorld.xyz)));
    float3 V = normalize(xCameraPosition - (float3) input.PositionWorld);

    float4 intensity = Ka * xAmbientColor * xAmbientIntensity;
    for (int i = 0; i < LightCount; i++)
    {
        float3 L = xLightPositions[i] - (float3) input.PositionWorld;
        float dist = length(L);
        L = normalize(L);
        float3 R = reflect(L, N);

        intensity += PhongLighting(N, L, V, R, dist, xLightColors[i], xLightIntensities[i], 1);
    }

    if (xSpotlights)
    {
        float cos_outer = 0.8f;
        float cos_inner = 0.9f;
        float cos_inner_minus_outer = cos_inner - cos_outer;
        float spotExponent = 20;

        for (int i = 0; i < SpotlightCount; i++)
        {
            float3 L = xSpotlightPositions[i] - (float3) input.PositionWorld;
            float dist = length(L);
            L = normalize(L);
            float3 R = reflect(L, N);
            float3 D = normalize(xSpotlightDirections[i]);

            float lambertTerm = max(dot(N, L), 0.0);
            if (lambertTerm > 0.0)
            {
                float cos_current = dot(-L, D);
                if (cos_current > cos_inner)
                {
                    intensity += PhongLighting(N, L, V, R, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
                else if (cos_current > cos_outer)
                {
                    float falloff = (cos_current - cos_outer) / cos_inner_minus_outer;
                    intensity += falloff * PhongLighting(N, L, V, R, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
            }
        }
    }
    intensity.a = 1;

    float4 textureColor = 0;
    if (xTextured)
    {
        textureColor = xTexture.Sample(TextureSampler, input.TextureCoordinate);
    }
    else
    {
        textureColor = xDiffuseColor;
    }

    return saturate(intensity * textureColor);
}

float4 FlatBlinnPixelShader(VertexShaderOutput input) : COLOR0
{
    float3 N = normalize(cross(ddy(input.PositionWorld.xyz), ddx(input.PositionWorld.xyz)));
    float3 V = normalize(xCameraPosition - (float3) input.PositionWorld);

    float4 intensity = Ka * xAmbientColor * xAmbientIntensity;
    for (int i = 0; i < LightCount; i++)
    {
        float3 L = xLightPositions[i] - (float3) input.PositionWorld;
        float dist = length(L);
        L = normalize(L);
        float3 H = normalize(L + V);

        intensity += BlinnLighting(N, L, H, dist, xLightColors[i], xLightIntensities[i], 1);
    }

    if (xSpotlights)
    {
        float cos_outer = 0.8f;
        float cos_inner = 0.9f;
        float cos_inner_minus_outer = cos_inner - cos_outer;
        float spotExponent = 20;

        for (int i = 0; i < SpotlightCount; i++)
        {
            float3 L = xSpotlightPositions[i] - (float3) input.PositionWorld;
            float dist = length(L);
            L = normalize(L);
            float3 H = normalize(L + V);
            float3 D = normalize(xSpotlightDirections[i]);

            float lambertTerm = max(dot(N, L), 0.0);
            if (lambertTerm > 0.0)
            {
                float cos_current = dot(-L, D);
                if (cos_current > cos_inner)
                {
                    intensity += BlinnLighting(N, L, H, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
                else if (cos_current > cos_outer)
                {
                    float falloff = (cos_current - cos_outer) / cos_inner_minus_outer;
                    intensity += falloff * BlinnLighting(N, L, H, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
            }
        }
    }
    intensity.a = 1;

    float4 textureColor = 0;
    if (xTextured)
    {
        textureColor = xTexture.Sample(TextureSampler, input.TextureCoordinate);
    }
    else
    {
        textureColor = xDiffuseColor;
    }

    return saturate(intensity * textureColor);
}

//------- Flat shading techniques --------
technique FlatPhong
{
    pass Pass0
    {
        VertexShader = compile vs_3_0 FlatVertexShader();
        PixelShader = compile ps_3_0 FlatPhongPixelShader();
    }
}

technique FlatBlinn
{
    pass Pass0
    {
        VertexShader = compile vs_3_0 FlatVertexShader();
        PixelShader = compile ps_3_0 FlatBlinnPixelShader();
    }
}

//------- Gouraud vertex shaders --------
VertexShaderOutput GouraudPhongVertexShader(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;

    input.Position = mul(input.Position, xWorld);
    output.Position = mul(input.Position, xView);
    output.Position = mul(output.Position, xProjection);

    output.TextureCoordinate = input.TextureCoordinate;

    float3 N = normalize(mul(input.Normal, (float3x3) xWorld));
    float3 V = normalize(xCameraPosition - (float3) input.Position);

    float4 intensity = Ka * xAmbientColor * xAmbientIntensity;
    for (int i = 0; i < LightCount; i++)
    {
        float3 L = xLightPositions[i] - (float3) input.Position;
        float dist = length(L);
        L = normalize(L);
        float3 R = reflect(L, N);

        intensity += PhongLighting(N, L, V, R, dist, xLightColors[i], xLightIntensities[i], 1);
    }

    if (xSpotlights)
    {
        float cos_outer = 0.8f;
        float cos_inner = 0.9f;
        float cos_inner_minus_outer = cos_inner - cos_outer;
        float spotExponent = 20;

        for (int i = 0; i < SpotlightCount; i++)
        {
            float3 L = xSpotlightPositions[i] - (float3) input.Position;
            float dist = length(L);
            L = normalize(L);
            float3 R = reflect(L, N);
            float3 D = normalize(xSpotlightDirections[i]);

            float lambertTerm = max(dot(N, L), 0.0);
            if (lambertTerm > 0.0)
            {
                float cos_current = dot(-L, D);
                if (cos_current > cos_inner)
                {
                    intensity += PhongLighting(N, L, V, R, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
                else if (cos_current > cos_outer)
                {
                    float falloff = (cos_current - cos_outer) / cos_inner_minus_outer;
                    intensity += falloff * PhongLighting(N, L, V, R, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
            }
        }
    }
    intensity.a = 1;

    output.Intensity = intensity;

    return output;
}

VertexShaderOutput GouraudBlinnVertexShader(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;

    input.Position = mul(input.Position, xWorld);
    output.Position = mul(input.Position, xView);
    output.Position = mul(output.Position, xProjection);

    output.TextureCoordinate = input.TextureCoordinate;

    float3 N = normalize(mul(input.Normal, (float3x3) xWorld));
    float3 V = normalize(xCameraPosition - (float3) input.Position);

    float4 intensity = Ka * xAmbientColor * xAmbientIntensity;
    for (int i = 0; i < LightCount; i++)
    {
        float3 L = xLightPositions[i] - (float3) input.Position;
        float dist = length(L);
        L = normalize(L);
        float3 H = normalize(L + V);

        intensity += BlinnLighting(N, L, H, dist, xLightColors[i], xLightIntensities[i], 1);
    }

    if (xSpotlights)
    {
        float cos_outer = 0.8f;
        float cos_inner = 0.9f;
        float cos_inner_minus_outer = cos_inner - cos_outer;
        float spotExponent = 20;

        for (int i = 0; i < SpotlightCount; i++)
        {
            float3 L = xSpotlightPositions[i] - (float3) input.Position;
            float dist = length(L);
            L = normalize(L);
            float3 H = normalize(L + V);
            float3 D = normalize(xSpotlightDirections[i]);

            float lambertTerm = max(dot(N, L), 0.0);
            if (lambertTerm > 0.0)
            {
                float cos_current = dot(-L, D);
                if (cos_current > cos_inner)
                {
                    intensity += BlinnLighting(N, L, H, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
                else if (cos_current > cos_outer)
                {
                    float falloff = (cos_current - cos_outer) / cos_inner_minus_outer;
                    intensity += falloff * BlinnLighting(N, L, H, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
            }
        }
    }
    intensity.a = 1;

    output.Intensity = intensity;

    return output;
}

//------- Gouraud pixel shader --------
float4 GouraudPixelShader(VertexShaderOutput input) : COLOR0
{
    if (xTextured)
    {
        return saturate(input.Intensity * xTexture.Sample(TextureSampler, input.TextureCoordinate));
    }
    else
    {
        return saturate(input.Intensity * xDiffuseColor);
    }
}

//------- Gouraud shading techniques --------
technique GouraudPhong
{
    pass Pass0
    {
        VertexShader = compile vs_3_0 GouraudPhongVertexShader();
        PixelShader = compile ps_3_0 GouraudPixelShader();
    }
}

technique GouraudBlinn
{
    pass Pass0
    {
        VertexShader = compile vs_3_0 GouraudBlinnVertexShader();
        PixelShader = compile ps_3_0 GouraudPixelShader();
    }
}

//------- Phong vertex shader --------
VertexShaderOutput PhongVertexShader(VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput)0;

    output.PositionWorld = mul(input.Position, xWorld);
    output.Position = mul(output.PositionWorld, xView);
    output.Position = mul(output.Position, xProjection);

    output.TextureCoordinate = input.TextureCoordinate;
    output.Normal = normalize(mul(input.Normal, (float3x3)xWorld));

    return output;
}

//------- Phong pixel shaders --------
float4 PhongPhongPixelShader(VertexShaderOutput input) : COLOR0
{
    float3 N = normalize(input.Normal);
    float3 V = normalize(xCameraPosition - (float3) input.PositionWorld);

    float4 intensity = Ka * xAmbientColor * xAmbientIntensity;
    for (int i = 0; i < LightCount; i++)
    {
        float3 L = xLightPositions[i] - (float3) input.PositionWorld;
        float dist = length(L);
        L = normalize(L);
        float3 R = reflect(L, N);

        intensity += PhongLighting(N, L, V, R, dist, xLightColors[i], xLightIntensities[i], 1);
    }

    if (xSpotlights)
    {
        float cos_outer = 0.8f;
        float cos_inner = 0.9f;
        float cos_inner_minus_outer = cos_inner - cos_outer;
        float spotExponent = 20;

        for (int i = 0; i < SpotlightCount; i++)
        {
            float3 L = xSpotlightPositions[i] - (float3) input.PositionWorld;
            float dist = length(L);
            L = normalize(L);
            float3 R = reflect(L, N);
            float3 D = normalize(xSpotlightDirections[i]);

            float lambertTerm = max(dot(N, L), 0.0);
            if (lambertTerm > 0.0)
            {
                float cos_current = dot(-L, D);
                if (cos_current > cos_inner)
                {
                    intensity += PhongLighting(N, L, V, R, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
                else if (cos_current > cos_outer)
                {
                    float falloff = (cos_current - cos_outer) / cos_inner_minus_outer;
                    intensity += falloff * PhongLighting(N, L, V, R, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
            }
        }
    }
    intensity.a = 1;

    if (xTextured)
    {
        return saturate(intensity * xTexture.Sample(TextureSampler, input.TextureCoordinate));
    }
    else
    {
        return saturate(intensity * xDiffuseColor);
    }
}

float4 PhongBlinnPixelShader(VertexShaderOutput input) : COLOR0
{
    float3 N = normalize(input.Normal);
    float3 V = normalize(xCameraPosition - (float3) input.PositionWorld);

    float4 intensity = Ka * xAmbientColor * xAmbientIntensity;
    for (int i = 0; i < LightCount; i++)
    {
        float3 L = xLightPositions[i] - (float3) input.PositionWorld;
        float dist = length(L);
        L = normalize(L);
        float3 H = normalize(L + V);

        intensity += BlinnLighting(N, L, H, dist, xLightColors[i], xLightIntensities[i], 1);
    }

    if (xSpotlights)
    {
        float cos_outer = 0.8f;
        float cos_inner = 0.9f;
        float cos_inner_minus_outer = cos_inner - cos_outer;
        float spotExponent = 20;

        for (int i = 0; i < SpotlightCount; i++)
        {
            float3 L = xSpotlightPositions[i] - (float3) input.PositionWorld;
            float dist = length(L);
            L = normalize(L);
            float3 H = normalize(L + V);
            float3 D = normalize(xSpotlightDirections[i]);

            float lambertTerm = max(dot(N, L), 0.0);
            if (lambertTerm > 0.0)
            {
                float cos_current = dot(-L, D);
                if (cos_current > cos_inner)
                {
                    intensity += BlinnLighting(N, L, H, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
                else if (cos_current > cos_outer)
                {
                    float falloff = (cos_current - cos_outer) / cos_inner_minus_outer;
                    intensity += falloff * BlinnLighting(N, L, H, dist, xSpotlightColors[i], xSpotlightIntensities[i], pow(saturate(cos_current), spotExponent));
                }
            }
        }
    }
    intensity.a = 1;

    if (xTextured)
    {
        return saturate(intensity * xTexture.Sample(TextureSampler, input.TextureCoordinate));
    }
    else
    {
        return saturate(intensity * xDiffuseColor);
    }
}

//------- Phong shading techniques --------
technique PhongPhong
{
    pass Pass0
    {
        VertexShader = compile vs_3_0 PhongVertexShader();
        PixelShader = compile ps_3_0 PhongPhongPixelShader();
    }
}

technique PhongBlinn
{
    pass Pass0
    {
        VertexShader = compile vs_3_0 PhongVertexShader();
        PixelShader = compile ps_3_0 PhongBlinnPixelShader();
    }
}
