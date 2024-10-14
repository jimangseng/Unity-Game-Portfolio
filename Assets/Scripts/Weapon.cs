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
    public BasicProjectile basicProjectile;
    public CannonProjectile cannonProjectile;

    // 궤적 관련
    const int lineSegments = 2;
    public LineRenderer lineRenderer;

    private void Start()
    {
        // TODO: 시간
        basicProjectile.projInstance = Instantiate(basicObj);
        cannonProjectile.projInstance = Instantiate(cannonObj);
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

    // 발사 시 호출. 왜? 발사의 방향을 알려야하니까.
    public void fire(AttackMode _attackMode, Vector3 _from, Vector3 _to)
    {
        if (_attackMode == AttackMode.Basic)
        {
            basicProjectile.fire(_from, _to);
        }

        else if (_attackMode == AttackMode.Cannon)
        {
            cannonProjectile.fire(_from, _to);
        }
    }

}