using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

    public bool endOfAnimation;
    public GameObject NewLight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (endOfAnimation)
        {
            NewLight.SetActive(true);
        }
	
	}
}
