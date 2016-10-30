using UnityEngine;
using System.Collections;

public class CameraPanelBehaviour : MonoBehaviour {

    public RenderTexture rTexture;
    CameraBehaviour[] camArray;
    CameraBehaviour currentCam;
    int nextCamIndex = 0;
    //public void OnButtonCameraClicked()
    //{
    //    if (camArray.Length >0)
    //    {

    //    nextCamIndex = nextCamIndex > camArray.Length ? 0 : nextCamIndex;

    //    if (nextCamIndex > 0)
    //    camArray[0].GetComponent<Camera>().targetTexture = null;
    //    else
    //    camArray[nextCamIndex].GetComponent<Camera>().targetTexture = null;

    //        camArray[nextCamIndex].SetDoorState()
    //    }
    //}
}
