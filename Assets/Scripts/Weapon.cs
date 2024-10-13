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
    }

    public void previewTrace(Vector3 _from, Vector3 _to)
    {
        lineRenderer.positionCount = lineSegments;
        lineRenderer.enabled = true;

        Vector3[] tPositions = new Vector3[lineSegments];

        //trace = new Trace(_from, _to);
        //trace.update(_from, _to);

        for (int i = 0; i < lineSegments; i++)
        {
            //tPositions[i] = cannon.trace.Position; // todo: 미리보기 표출되도록
        }

        lineRenderer.SetPositions(tPositions);
    }

    public void fire(AttackMode _attackMode, Vector3 _from, Vector3 _to)
    {
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
