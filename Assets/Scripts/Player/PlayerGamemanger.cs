using UnityEngine;
using System.Collections;

public class PlayerGamemanger : MonoBehaviour
{
    [HideInInspector]
    public PlayerMovementController PlayerMovementController;
    [HideInInspector]
    public PlayerCameraController PlayerCameraController;
    [HideInInspector]
    public PlayerWakeUpController PlayerWakeUpController;

    void Awake()
    {
        PlayerMovementController = GetComponent<PlayerMovementController>();
        PlayerCameraController = GetComponent<PlayerCameraController>();
        PlayerWakeUpController = GetComponent<PlayerWakeUpController>();
    }
}
