using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

   public enum doorState { Open,Blocked}
    [SerializeField]
    private DoorBehaviour door;

    private Camera cam; 
    


    void Awake () {
        cam = GetComponent<Camera>();
	}
	
	void Update () {
	
	}
    public void SetDoorState(doorState state)
    {
        door.DoorBlocked = state == doorState.Open ? false : true;
    }
    public doorState GetDoorState()
    {
        return door.DoorBlocked ? doorState.Blocked : doorState.Open;
    }

    public Texture2D RTImage()
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = currentRT;
        return image;
    }
}
