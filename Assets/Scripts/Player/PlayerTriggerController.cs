using UnityEngine;
using System.Collections;

public class PlayerTriggerController : MonoBehaviour
{
    private const string DOOR_TAG = "Door";

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == DOOR_TAG)
        {
            other.GetComponent<DoorBehaviour>().AnimationSlider = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == DOOR_TAG)
        {
            other.GetComponent<DoorBehaviour>().AnimationSlider = -1;
        }
    }
}
