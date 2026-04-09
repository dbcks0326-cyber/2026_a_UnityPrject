using System.Threading;
using UnityEngine;

public class cubemanager : MonoBehaviour
{
    public CubeGamerator[] generatedCubes = new CubeGamerator[5];

    public float Timer = 0.0f;
    public float interval = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= interval)
        {
            RandomizeCubeAcitivation();
            Timer = 0.0f;
        }
    }


    public void RandomizeCubeAcitivation()
    {
        for (int i = 0; i < generatedCubes.Length; i++)
        {

            int randomnum = Random.Range(0, 2);

            if (randomnum == 1)
            {
                generatedCubes[i].Gencube();
            }
        }
    }
}
