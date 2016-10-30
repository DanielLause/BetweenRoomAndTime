using UnityEngine;
using System.Collections;

public class ShowMessage : MonoBehaviour
{
    public string Message;
    public float delay;

    private PlayerGamemanger playerGamemanger;

    void Awake()
    {
        playerGamemanger = FindObjectOfType<PlayerGamemanger>();
    }

    void OnDisable()
    {
        //if (playerGamemanger != null)
            //playerGamemanger.ShowMessage(Message, delay);
    }
}
