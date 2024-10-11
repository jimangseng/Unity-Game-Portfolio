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

    // ±ËÀû °ü·Ã
    const int lineSegments = 10;

    public GameObject targetCursor;

    public LineRenderer lineRenderer;

    // ???
    public BasicProjectile basic;
    public CannonProjectile cannon;
    //public ProjectileBase basic;
    //public ProjectileBase cannon;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = lineSegments;
    }

    public void PreviewCannonballTrace()
    {
        lineRenderer.enabled = true;

        Vector3[] tPositions = new Vector3[lineSegments];
        
        for (int i = 0; i < lineSegments; i++)
        {
            tPositions[i] = cannon.trace.currentPosition;
        }

        lineRenderer.SetPositions(tPositions);
    }

    public void Fire(AttackMode _attackMode)
    {
        Vector3 tFireFrom = gameObject.transform.position;
        tFireFrom.y += 1.5f;

        Vector3 tFIreTo = targetCursor.transform.position;

        if(_attackMode == AttackMode.Basic)
        {
            basic.Fire(tFireFrom, tFIreTo);
        }
        else if(_attackMode == AttackMode.Cannon)
        {
            cannon.Fire(tFireFrom, tFIreTo);
        }
    }
}
