using UnityEngine;
using System.Collections;

public class PlayerTriggerController : MonoBehaviour
{
    private const string DOOR_TAG = "Door";
    private const string AMMO_TAG = "Ammo";

    private PlayerGamemanger playerGamemanager;

    void Awake()
    {
        playerGamemanager = GetComponent<PlayerGamemanger>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == DOOR_TAG)
            other.GetComponent<DoorBehaviour>().AnimationSlider = 1;
        else if (other.tag == AMMO_TAG)
        {
            if (playerGamemanager.TestTubeBehaviour.ShootsLeft < playerGamemanager.TestTubeBehaviour.MaxShootAmount)
            {
                playerGamemanager.TestTubeBehaviour.IncreaseAmmo();
                Destroy(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == DOOR_TAG)
            other.GetComponent<DoorBehaviour>().AnimationSlider = -1;
    }
}
