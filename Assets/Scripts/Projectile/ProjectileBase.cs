using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class ProjectileBase
{
    protected GameObject projObject;
    protected GameObject projInstance;

    protected Vector3 forceDirection;

    public ProjectileBase(GameObject _projObject)
    {
        projObject = _projObject;

    }

    public virtual void fire(Vector3 _from, Vector3 _to)
    {
        projInstance = MonoBehaviour.Instantiate(projObject, _from, Quaternion.Euler(projObject.transform.forward));
        projInstance.SetActive(true);

        forceDirection = Vector3.Normalize(_to - _from);
    }

    public virtual void update()
    {

    }

    
}
