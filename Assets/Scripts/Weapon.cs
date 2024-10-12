using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static Weapon;

public class Weapon : MonoBehaviour
{
    public enum AttackMode
    {
        Basic,
        Cannon
    }

    // 궤적 관련
    const int lineSegments = 10;

    public LineRenderer lineRenderer;

    public BasicProjectile basic;
    public CannonProjectile cannon;

    public void PreviewCannonballTrace(Vector3 _from, Vector3 _to)
    {
        lineRenderer.positionCount = lineSegments;
        lineRenderer.enabled = true;

        Vector3[] tPositions = new Vector3[lineSegments];

        cannon.trace = new CannonProjectile.Trace(_from, _to);

        for (int i = 0; i < lineSegments; i++)
        {
            tPositions[i] = cannon.trace.Position; // todo: 미리보기 표출되도록
        }

        lineRenderer.SetPositions(tPositions);
    }

    public void Fire(AttackMode _attackMode, Vector3 _from, Vector3 _to)
    {

        if(_attackMode == AttackMode.Basic)
        {
            basic.Fire(_from, _to);
        }
        else if(_attackMode == AttackMode.Cannon)
        {
            cannon.trace = new CannonProjectile.Trace(_from, _to);
            cannon.Fire(_from, _to);
        }
    }
}
