using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utillitys : MonoBehaviour
{
    public static void HightLightMeshrenderer(MeshRenderer renderer, Material outlineMat)
    {
        List<Material> tempMats = new List<Material>();

        for (int i = 0; i < renderer.materials.Length; i++)
            tempMats.Add(renderer.materials[i]);

        tempMats.Add(outlineMat);

        renderer.materials = tempMats.ToArray();
    }

    public static void DeHightLightMeshrenderer(MeshRenderer renderer)
    {
        List<Material> tempMats = new List<Material>();

        for (int i = 0; i < renderer.materials.Length - 1; i++)
            tempMats.Add(renderer.materials[i]);

        renderer.materials = tempMats.ToArray();
    }

    public static Vector2 Shake2D(float amplitude, float frequency, int octaves, float persistance, float lacunarity, float burstFrequency, int burstContrast, float time)
    {
        float valX = 0;
        float valY = 0;

        float iAmplitude = 1;
        float iFrequency = frequency;
        float maxAmplitude = 0;

        float burstCoord = time / (1 - burstFrequency);

        float burstMultiplier = Mathf.PerlinNoise(burstCoord, burstCoord);

        burstMultiplier = Mathf.Pow(burstMultiplier, burstContrast);

        for (int i = 0; i < octaves; i++) // Iterate trough octaves
        {
            float noiseFrequency = time / (1 - iFrequency) / 10;

            float perlinValueX = Mathf.PerlinNoise(noiseFrequency, 0.5f);
            float perlinValueY = Mathf.PerlinNoise(0.5f, noiseFrequency);

            perlinValueX = (perlinValueX + 0.0352f) * 2 - 1;
            perlinValueY = (perlinValueY + 0.0345f) * 2 - 1;

            valX += perlinValueX * iAmplitude;
            valY += perlinValueY * iAmplitude;

            maxAmplitude += iAmplitude;

            iAmplitude *= persistance;
            iFrequency *= lacunarity;
        }

        valX *= burstMultiplier;
        valY *= burstMultiplier;

        valX /= maxAmplitude;
        valY /= maxAmplitude;

        valX *= amplitude;
        valY *= amplitude;

        return new Vector2(valX, valY);
    }
}

public interface IHighlightable
{
    void Highlight();
    void DeHightlight();
    IEnumerator HighLightUpdate();
}
