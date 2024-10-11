using System.Collections;
using System.Collections.Generic;
using UnityEditor.Recorder.Input;
using UnityEngine;
using UnityEngine.EventSystems;


public class BasicProjectile : ProjectileBase
{
 
    protected override void Start()
    {
        base.Start();
    }

    public override void Fire(Vector3 _from, Vector3 _to)
    {
        base.Fire(_from, _to);

        projInstance.GetComponent<Rigidbody>().AddForce(forceDirection * 12.0f, ForceMode.Impulse);
    }
}
