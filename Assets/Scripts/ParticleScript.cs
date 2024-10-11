using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public audio audiovisualScript;
    public GameObject cinemachineVC;
    ParticleSystem ps;

    
    float[] currZ;

    float Loudness;

    ParticleSystem.Particle[] particles;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");

        ps = gameObject.GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
        currZ = new float[particles.Length];

        float[] spectrum = new float[4096];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);

        Loudness = 0.5f;

        for (int i = 0; i < spectrum.Length; i++)
        {
            Loudness += spectrum[i];
        }


        StopAllCoroutines();

        StartCoroutine(MoveCamera());
        StartCoroutine(UpdateParticles());
        
    }

    float a = 0.0f;

    // Update is called once per frame
    void Update()
    {

        ps.GetParticles(particles);

        for (int i = 0; i < particles.Length; i++)
        {
            var pos = particles[i].position;
            pos.z = Mathf.SmoothDamp(particles[i].position.z, currZ[i], ref a, 0.03f);

            particles[i].position = pos;

        }
        ps.SetParticles(particles);
    }

    IEnumerator UpdateParticles()
    {
        Debug.Log("Update Particles");

        while (true)
        {
            float[] spectrum = new float[4096];

//#if UNITY_WEBGL
//            SSWebInteract.GetSpectrumData(spectrum);
//#else
            AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
//#endif

            ps.GetParticles(particles);

            for (int i = 0; i < ps.main.maxParticles; i++)
            {
                float clampedData = 0.0f;
                clampedData = Mathf.Lerp(0.0f, 10.0f, spectrum[i]);
                clampedData = Mathf.Clamp(clampedData, 0.0f, 2f);
                clampedData = Mathf.Lerp(0.0f, 5.0f, clampedData);

                currZ[i] = Mathf.Lerp(0.0f, 20.0f, clampedData) * Mathf.Lerp(1.0f, 3.0f, Loudness);

                particles[i].startSize = (Mathf.Lerp(0.8f, 0.05f, currZ[i]));
            }

            ps.SetParticles(particles);

            yield return new WaitForSeconds(0.1f);

        }
    }

    IEnumerator MoveCamera()
    {
        float scale = 50.0f;
        float offset = 15.0f;
        float step = 0.2f;

        while (true)
        {
            float[] spectrum = new float[4096];

//#if UNITY_WEBGL
//            SSWebInteract.GetSpectrumData(spectrum);
//#else
            AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
//#endif

            Loudness = 0.0f;

            for (int i = 0; i < spectrum.Length; i++)
            {
                Loudness += spectrum[i];
            }

            float time;
            time = Mathf.Lerp(30.0f, 1.5f, Loudness);

            //var em = ps.emission;
            //em.enabled = true;
            //em.SetBursts(new ParticleSystem.Burst[] {
            //new ParticleSystem.Burst(0.0f, 300, 300, Mathf.RoundToInt(Mathf.Lerp(10.0f, 2.0f, Loudness)), Mathf.Lerp(0.1f, 1.0f, Loudness))
            //});

            cinemachineVC.transform.position = new Vector3(scale * (Random.Range(-1.0f, 1.0f)) + offset * (Random.Range(-1.0f, 1.0f)),
                scale * (Random.Range(-1.0f, 1.0f)) + offset * (Random.Range(-1.0f, 1.0f)),
                scale * (Random.Range(-1.0f, 1.0f)) + offset * (Random.Range(-1.0f, 1.0f)));

            yield return new WaitForSeconds(step * time);
        }
    }

    void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}