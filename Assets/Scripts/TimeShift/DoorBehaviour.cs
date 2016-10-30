using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
class DoorBehaviour : TimeShiftManager
{
    public bool DoorBlocked = false;
    private SphereCollider sphereCollider;
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    void Update()
    {
        if (!DoorBlocked)
        OnUpdate();
    }

}
