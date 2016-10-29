using UnityEngine;
using System.Collections;

public class PlayerGamemanger : MonoBehaviour
{
    private const int MAX_SHAKE_DELAY = 20;
    private const int MIN_SHAKE_DELAY = 13;

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

    void Start()
    {
        StartCoroutine(ShakeTimer());
    }

    private IEnumerator ShakeTimer()
    {
        yield return new WaitForSeconds(Random.Range(MIN_SHAKE_DELAY, MAX_SHAKE_DELAY));

        PlayerCameraController.EarthQuake(PlayerCameraController.SHAKE_INTENSITY, PlayerCameraController.SHAKE_DECAY);

        StartCoroutine(ShakeTimer());
    }
}
