using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Recorder.Input;
using UnityEngine;
using UnityEngine.EventSystems;


public class BasicProjectile : ProjectileBase
{
    public BasicProjectile(GameObject _projObj) : base(_projObj)
    {

    }

    public override void fire(Vector3 _from, Vector3 _to)
    {
        base.fire(_from, _to);

        projInstance.GetComponent<Rigidbody>().AddForce(forceDirection * 10.0f, ForceMode.Impulse);
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
        projInstance.GetComponent<BoxCollider>().enabled = false;

        // clear missile particle system
        projInstance.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Clear(false);

        // stop smoke particle system
        ParticleSystem smoke = projInstance.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>();
        smoke.Stop();

        // play explosion particle system
        projInstance.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        //explosion.Play();
    }
}
