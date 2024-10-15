
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;


public class CannonProjectile : ProjectileBase
{
    Trace trace;

    public CannonProjectile (GameObject _projObj) : base(_projObj)
    {
        trace = new Trace ();
    }
    public override void fire(Vector3 _from, Vector3 _to)
    {
        base.fire(_from, _to);

        trace = new Trace(_from, _to);

    }

    public override void update()
    {
        trace.update();
        projInstance.GetComponent<Rigidbody>().velocity = trace.Velocity;
    }

    

}