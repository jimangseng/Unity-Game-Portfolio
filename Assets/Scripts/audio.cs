using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public GameObject cubes;
    public GameObject cinemachineVC;
    public Material mat;

    public float[] spectrum;
    
    List<GameObject> gameObjects;
    GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        int width = 64;
        int depth = 64;
        float interval = 1.5f;

        spectrum = new float[width * depth];
        gameObjects = new List<GameObject>();
        cube = new GameObject();

        for (int i = 0; i < depth; i++)
        {
            for (int j = 0; j < width; j++)
            {
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.Translate(new Vector3(Random.value * width * interval, 0.0f, Random.value * depth * interval));
                cube.transform.localScale = new Vector3(1.5f + Random.value * 2.0f, 1.5f + Random.value * 2.0f, 1.5f + Random.value * 2.0f);

                cube.GetComponent<MeshRenderer>().material = mat;
                cube.transform.SetParent(cubes.transform);
                gameObjects.Add(cube);

            }
        }

        StartCoroutine(MoveCamera());

    }

    float sum;
    float average;
    float tmp;
    float deviation;

    IEnumerator MoveCamera()
    {
        float scale = 100.0f;
        float offset = 30.0f;
        //float duration = 1.5f;

        while(true)
        {
            float Loudness = 0.0f ;

            for(int i = 0; i<spectrum.Length; i++)
            {
                Loudness += spectrum[i];
            }

            Loudness = Mathf.Lerp(0, 1, spectrum[12]);

            yield return new WaitForSeconds(Loudness);
            cinemachineVC.transform.position = new Vector3(scale * (Random.value * 2.0f - 1.0f) + offset, scale * (Random.value * 2.0f - 1.0f) + offset, scale * (Random.value * 2.0f - 1.0f) + offset);
        }

        // stopCoroutine
    }

    // Update is called once per frame
    void Update()
    {

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);
        Vector3 position;

        for (int i = 0; i < spectrum.Length; i++)
        { 
            sum += spectrum[i];
        }

        average = sum / spectrum.Length;

        for (int i = 0; i < spectrum.Length; i++)
        {
            tmp += (spectrum[i] - average) * (spectrum[i] - average);
        }

        deviation = tmp / spectrum.Length;

        for (int i = 0; i < gameObjects.Count - 1; i++)
        {
            position = gameObjects[i].transform.localPosition;

            position.y = (spectrum[i] - average) / Mathf.Sqrt(deviation);

            //if(position.y < 0)
            //{
                position.y *= Mathf.Lerp(200.0f, 0.5f, position.y);
            //}

            gameObjects[i].transform.localPosition = position;
        }

        // 시네머신 : 카메라 거리, Wobble
        sum = 0.0f;
        tmp = 0.0f;
    }
}
