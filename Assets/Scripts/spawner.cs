
using UnityEngine;


public class spawner : MonoBehaviour
{

    public GameObject coinPrefabs;
    public GameObject MissilePrefabs;

    [Header("스폰 타이밍 설정")]
    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 2.0f;


    [Header("동전 스폰 확률 설정")]
    [Range(0, 100)]
    public int coinSpawnChanse = 50;



    public float timer = 0.0f;
    public float nextSpawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > nextSpawnTime)
        {
            SpwnObject();
            timer = 0.0f;
            SetNextSpawnTime();
        }


    }

    void SpwnObject()
    {
        Transform spawnTransform = transform;

        int randomValue = Random.Range(0, 100);

        if (randomValue < coinSpawnChanse)
        {
            Instantiate(coinPrefabs, spawnTransform.position, spawnTransform.rotation);
        }
        else
        {
            Instantiate(MissilePrefabs, spawnTransform.position, spawnTransform.rotation);
        }
    }



    void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

}
