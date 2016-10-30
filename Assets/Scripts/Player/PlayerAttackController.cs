using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public float MaxDistance = 20;

    private PlayerGamemanger playerGamemanager;

    void Awake()
    {
        playerGamemanager = GetComponent<PlayerGamemanger>();
    }

    void Update()
    {
        CheckAttackInput();
        CheckHighLights();
    }

    private void CheckAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerGamemanager.TestTubeBehaviour.DeCreaseAmmo();
        }
    }

    private void CheckHighLights()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        RaycastHit[] hits = Physics.RaycastAll(ray, MaxDistance);

        for (int i = 0; i < hits.Length; i++)
        {

            Component[] rootComponents = hits[i].transform.GetComponents(typeof(Component));

            for (int z = 0; z < rootComponents.Length; z++)
            {
                if (rootComponents[z] is IHighlightable)
                    (rootComponents[z] as IHighlightable).Highlight();
            }

            for (int x = 0; x < hits[i].transform.childCount; x++)
            {
                Component[] components = hits[i].transform.GetChild(x).gameObject.GetComponents(typeof(Component));

                for (int y = 0; y < components.Length; y++)
                {
                    if (components[y] is IHighlightable)
                        (components[y] as IHighlightable).Highlight();
                }
            }
        }
    }
}
