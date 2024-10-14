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
    public BasicProjectile basic;
    public CannonProjectile cannon;

    // 궤적 관련
    public Trace trace;
    const int lineSegments = 10;
    public LineRenderer lineRenderer;
    Vector3 from;
    Vector3 to;

    private void Start()
    {
        trace = new Trace();
        trace.time = 0.0f;
    }

    private void Update()
    {
        trace.time += Time.deltaTime;
        trace.update(from, to);
        // 이로써 trace는 매 프레임 최신화된다.
    }

    // 궤적 미리보기
    public void previewTrace(Vector3 _from, Vector3 _to)
    {
        lineRenderer.positionCount = lineSegments;
        lineRenderer.enabled = true;

        Vector3[] tPositions = new Vector3[lineSegments];

        //trace = new Trace(_from, _to);
        //trace.update(_from, _to);

        for (int i = 0; i < lineSegments; i++)
        {
            tPositions[i] = trace.Position; // todo: 미리보기 표출되도록
        }

        lineRenderer.SetPositions(tPositions);
    }

    // 발사 시 호출
    public void fire(AttackMode _attackMode, Vector3 _from, Vector3 _to)
    {
        // 멤버 초기화
        from = _from;
        to = _to;

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
