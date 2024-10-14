using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Recorder.Input;
using UnityEngine;
using UnityEngine.EventSystems;


public class BasicProjectile : ProjectileBase
{
    protected virtual void Start()
    {
        // 241014 : 파티클 관련 주석 처리
        //explosion = transform.GetChild(0).GetChild(1).gameObject.GetComponent<ParticleSystem>();
    }


    public override void fire(Vector3 _from, Vector3 _to)
    {
        base.fire(_from, _to);

        // 기본 공격은 Rigidbody에 Addforce를 하여 움직인다.
        // 하지만 반면, 대포는 Velocity를 직접 계산한다. Trace 클래스를 사용하여.
        projInstance.GetComponent<Rigidbody>().AddForce(forceDirection * 12.0f, ForceMode.Impulse);
    }


    // 충돌 시
    // TODO: ProjectileBase 클래스로 옮길 것
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Obstacle"))
        {
            // 충돌한 물체 deactivate
            collision.gameObject.SetActive(false);
        }
        // prevent object from colliding(?)
        GetComponent<BoxCollider>().enabled = false;

        // clear missile particle system
        transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Clear(false);

        // stop smoke particle system
        ParticleSystem smoke = transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>();
        smoke.Stop();

        // play explosion particle system
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        //explosion.Play();
    }
}
