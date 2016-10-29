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
}

public interface IHighlightable
{
    void Highlight();
    void DeHightlight();
    IEnumerator HighLightUpdate();
}
