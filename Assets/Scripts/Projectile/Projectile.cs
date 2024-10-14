using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Projectile : MonoBehaviour
{
    // 파티클 시스템 관련
    ParticleSystem explosion;

    // 인스턴스화 관련
    public GameObject projObject;   // 원본 오브젝트
    public GameObject projInstance; // 인스턴스

    // 투사체를 사용하는 무기 객체
    //public Weapon weapon;

    // 컴포넌트
    protected Rigidbody rb;

    // 발사 방향
    protected Vector3 forceDirection;


    protected virtual void Start()
    {
        explosion = transform.GetChild(0).GetChild(1).gameObject.GetComponent<ParticleSystem>();
        rb = projInstance.GetComponent<Rigidbody>();
    }


    public virtual void fire(Vector3 _from, Vector3 _to)
    {
        // 파티클 시스템 종료
        projInstance.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);

        // projectile 인스턴스화 및 setActive
        projInstance = Instantiate(projObject, _from, Quaternion.Euler(projObject.transform.forward));
        projInstance.SetActive(true);

        // 발사 방향 계산 및 발사
        forceDirection = Vector3.Normalize(_to - _from);
        projInstance.GetComponent<Rigidbody>().AddForce(forceDirection * 12.0f, ForceMode.Impulse);
    }


    // 충돌 시
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
        explosion.Play();
    }

}
