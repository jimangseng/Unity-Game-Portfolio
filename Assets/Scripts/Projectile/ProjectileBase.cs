using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class ProjectileBase : MonoBehaviour
{
    // 인스턴스화 관련
    public GameObject projObject;   // 원본 오브젝트
    public GameObject projInstance; // 인스턴스

    // 발사 방향
    protected Vector3 forceDirection;

    // 파티클 시스템 관련 - 1014 임시로 주석 처리
    //protected ParticleSystem explosion;


    protected virtual void Start()
    {
        //explosion = transform.GetChild(0).GetChild(1).gameObject.GetComponent<ParticleSystem>();
    }


    public virtual void fire(Vector3 _from, Vector3 _to)
    {
        // 발사 방향 계산
        forceDirection = Vector3.Normalize(_to - _from);

        // *** 투사체가 인스턴스화되어야 하는 시점은 바로 발사될 때이다. ***
        // projectile 인스턴스화 및 setActive
        projInstance = Instantiate(projObject, _from, Quaternion.Euler(projObject.transform.forward));
        projInstance.SetActive(true);



        // 파티클 시스템 종료
        //projInstance.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        // 임시로 주석처리 - 1014



    }
}
