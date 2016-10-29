using UnityEngine;
using System.Collections;
using System;

public class EffectableObject : MonoBehaviour, IHighlightable
{
    private const float MAX_WAIT_TIME = 0.1f;

    private Material OutlineMat;

    private Coroutine highLightedCoroutine;
    private bool highlighted = false;

    void Awake()
    {
        OutlineMat = (Material)Resources.Load("Materials/OutlineMat");
    }

    public void DeHightlight()
    {
        highlighted = false;
        Utillitys.DeHightLightMeshrenderer(GetComponent<MeshRenderer>());
    }   

    public void Highlight()
    {
        if (!highlighted)
            Utillitys.HightLightMeshrenderer(GetComponent<MeshRenderer>(), OutlineMat);

        if (highLightedCoroutine != null)
            StopCoroutine(highLightedCoroutine);

        highLightedCoroutine = StartCoroutine(HighLightUpdate());
        highlighted = true;

    }

    public IEnumerator HighLightUpdate()
    {
        yield return new WaitForSeconds(MAX_WAIT_TIME);

        DeHightlight();
    }
}
