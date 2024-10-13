using System.Collections;
using System.Collections.Generic;
using UnityEditor.Recorder.Input;
using UnityEngine;
using UnityEngine.EventSystems;


public class BasicProjectile : ProjectileBase
{
    public override void fire(Vector3 _from, Vector3 _to)
    {
        base.fire(_from, _to);

        projInstance.GetComponent<Rigidbody>().AddForce(forceDirection * 12.0f, ForceMode.Impulse);
    }
}
