using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
class DoorBehaviour : TimeShiftManager
{
    public bool DoorBlocked = false;
    public float TriggerRadius;
    private SphereCollider collider;
    void Start()
    {
        collider = GetComponent<SphereCollider>();
        collider.radius = TriggerRadius;
    }
    void Update()
    {
        if (!DoorBlocked)
        OnUpdate();
    }

}
