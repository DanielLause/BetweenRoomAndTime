using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectableObject : MonoBehaviour, IHighlightable
{
    private const float MAX_WAIT_TIME = 0.1f;

    private Material OutlineMat;

    private Coroutine highLightedCoroutine;
    private bool highlighted = false;

    private List<MeshRenderer> renderers = new List<MeshRenderer>();

    void Awake()
    {
        OutlineMat = (Material)Resources.Load("Materials/OutlineMat");
    }

    void Start()
    {
        renderers.Add(GetComponent<MeshRenderer>());
        MeshRenderer[] temp = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < temp.Length; i++)
            renderers.Add(temp[i]);
    }

    public void DeHightlight()
    {
        highlighted = false;

        for (int i = 0; i < renderers.Count; i++)
        {
            Utillitys.DeHightLightMeshrenderer(renderers[i]);
        }
    }   

    public void Highlight()
    {
        if (!highlighted)
        {
            for (int i = 0; i < renderers.Count; i++)
            {
                Utillitys.HightLightMeshrenderer(renderers[i], OutlineMat);
            }
        }

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
