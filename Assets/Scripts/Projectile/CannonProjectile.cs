
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;


public class CannonProjectile : ProjectileBase
{
    private void Update()
    {
        rb.velocity = weapon.GetComponent<Weapon>().trace.Velocity; // 발사할 때 뿐만이 아니라, 매 프레임 velocity를 갱신해줘야 하는데,,
    }

    public override void fire(Vector3 _from, Vector3 _to)
    {
        base.fire(_from, _to);
        weapon.trace.update(_from, _to);
    }


}