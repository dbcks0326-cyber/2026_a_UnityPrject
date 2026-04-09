using UnityEngine;

public class CubeGamerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int totalCubes = 10;
    public float cubeSpacing = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Gencube();
    }
    public void Gencube()
    {
        Vector3 myprosition = transform.position;

        for (int i = 0; i < totalCubes; i++)
        {
            Vector3 position = new Vector3(myprosition.x, myprosition.y, myprosition.z + (i * cubeSpacing));
            Instantiate(cubePrefab, position, Quaternion.identity);
        }
    }

}

 
