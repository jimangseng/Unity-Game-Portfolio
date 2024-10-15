using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Weapon: MonoBehaviour
{
    public enum AttackMode
    {
        Basic,
        Cannon
    }

    // 투사체 관련
    public GameObject basicObj;
    public GameObject cannonObj;
    ProjectileBase basic;
    ProjectileBase cannon;

    // 궤적 관련
    const int lineSegments = 2;
    public LineRenderer lineRenderer;

    private void Start()
    {
        basic = new BasicProjectile(basicObj);
        cannon = new CannonProjectile(cannonObj);
    }

    private void Update()
    {
        cannon.update();
    }

    // 궤적 미리보기
    public void previewTrace(Vector3 _from, Vector3 _to)
    {
        lineRenderer.positionCount = lineSegments;
        lineRenderer.enabled = true;

        Vector3[] tPositions = new Vector3[lineSegments];

        for (int i = 0; i < lineSegments; i++)
        {
            //tPositions[i] = cannon.trace.Position; // todo: 미리보기 표출되도록
        }

        lineRenderer.SetPositions(tPositions);
    }


    public void fire(AttackMode _attackMode, Vector3 _from, Vector3 _to)
    {
        if (_attackMode == AttackMode.Basic)
        {
            basic.fire(_from, _to);
        }

        else if (_attackMode == AttackMode.Cannon)
        {
            cannon.fire(_from, _to);
        }
    }

}