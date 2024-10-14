
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;


public class CannonProjectile : ProjectileBase
{
    // 대포알의 궤적 계산을 위한 Trace 객체
    Trace trace;

    protected override void Start()
    {
        base.Start();

        trace = new Trace();
    }

    public override void fire(Vector3 _from, Vector3 _to)
    {
        base.fire(_from, _to);

        // Trace 객체의 Velocity값을 투사체 인스턴스의 velocity값으로 설정
        projInstance.GetComponent<Rigidbody>().velocity = trace.Velocity;   // trace가 null이다. 또 Start() 전에 fire()가 실행되어 생기는 문제인데, 어떻게 해결할 수 있을까?
    }

}