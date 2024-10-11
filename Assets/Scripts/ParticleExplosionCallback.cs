using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCallback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleSystemStopped()
    {
        //Debug.Log("ps stopped.");
        Destroy(gameObject.transform.parent.parent.gameObject);
    }
}
