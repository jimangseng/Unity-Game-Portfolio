using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class ProjectileBase : MonoBehaviour
{
    ParticleSystem explosion;

    public GameObject projObject;
    public GameObject projInstance;

    public Vector3 forceDirection;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        explosion = transform.GetChild(0).GetChild(1).gameObject.GetComponent<ParticleSystem>();

    }

    // when projectile collides with some object
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != LayerMask.NameToLayer("Obstacle"))
        {
            // deactive collded object
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

    public virtual void Fire(Vector3 _from, Vector3 _to)
    {
        // calculate force direction
        forceDirection = Vector3.Normalize(_to - _from);

        // instantiate projectile and set active
        projInstance = Instantiate(projObject, _from, Quaternion.Euler(projObject.transform.forward));
        projInstance.SetActive(true);


        // deactive explosion particle system
        projInstance.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }

}
