
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;


public class CannonProjectile : Projectile
{
    //// 대포알이 그리는 궤적
    //public Trace trace;

    //// Projectile을 사용하고 있는 Weapon 객체
    //public Weapon weapon;

    //// Particle 관련
    //ParticleSystem explosion;


    //protected virtual void Start()
    //{
    //    explosion = transform.GetChild(0).GetChild(1).gameObject.GetComponent<ParticleSystem>();

    //    // trace 초기화
    //    trace = new Trace();
    //}


    //private void Update()
    //{
    //    trace.update();
    //    // 이로써 trace는 매 프레임 최신화된다

    //    //inst.GetComponent<Rigidbody>().velocity = trace.Velocity;
    //    // 매 프레임 velocity를 갱신
    //}


    //// 충돌 시
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer != LayerMask.NameToLayer("Obstacle"))
    //    {
    //        // 충돌한 물체 deactivate
    //        collision.gameObject.SetActive(false);
    //    }
    //    // prevent object from colliding(?)
    //    GetComponent<BoxCollider>().enabled = false;

    //    // clear missile particle system
    //    transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Clear(false);

    //    // stop smoke particle system
    //    ParticleSystem smoke = transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>();
    //    smoke.Stop();

    //    // play explosion particle system
    //    transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    //    explosion.Play();
    //}



}