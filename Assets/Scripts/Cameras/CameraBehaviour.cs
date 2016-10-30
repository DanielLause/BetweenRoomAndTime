using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

   public enum doorState { Open,Blocked}
    [SerializeField]
    private DoorBehaviour door;
    


    void Start () {
	
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
}
