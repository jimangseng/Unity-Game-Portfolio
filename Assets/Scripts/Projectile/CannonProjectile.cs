
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;


public class CannonProjectile : ProjectileBase
{
    private void Update()
    {
        rb.velocity = weapon.GetComponent<Weapon>().trace.Velocity; // �߻��� �� �Ӹ��� �ƴ϶�, �� ������ velocity�� ��������� �ϴµ�,,
    }

    public override void fire(Vector3 _from, Vector3 _to)
    {
        base.fire(_from, _to);
        weapon.trace.update(_from, _to);
    }


}