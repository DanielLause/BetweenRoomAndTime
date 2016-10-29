using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
class DoorBehaviour : TimeShiftManager
{
    public bool DoorBlocked = false;
    public float TriggerRadius = 0;
    private SphereCollider sphereCollider;
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = TriggerRadius;
    }
    void Update()
    {
        if (!DoorBlocked)
        OnUpdate();
    }

}
