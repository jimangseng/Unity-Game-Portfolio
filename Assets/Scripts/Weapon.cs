using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    // ���� ����
    const int lineSegments = 10;

    public LineRenderer lineRenderer;

    // ??? ĳ���ÿ� ���Ͽ�..
    public BasicProjectile basic;
    public CannonProjectile cannon;
    //public ProjectileBase basic;
    //public ProjectileBase cannon;

    public void PreviewCannonballTrace()
    {
        lineRenderer.positionCount = lineSegments;
        lineRenderer.enabled = true;

        Vector3[] tPositions = new Vector3[lineSegments];

        for (int i = 0; i < lineSegments; i++)
        {
            tPositions[i] = cannon.trace.currentPosition; // todo: �̸����� ǥ��ǵ���
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
            cannon.Fire(_from, _to);
        }
    }
}
