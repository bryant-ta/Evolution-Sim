using UnityEngine;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    [Header("Hosts")]
    public int initNumHost;

    public GameObject host;

    [Header("Foods")]
    public int initNumFood;
    public int maxNumFood;
    public float foodSpawnInterval;

    public GameObject food;

    [Label("Host Factory")] public HostFactory hf;

    public int numFood = 0;

    private void Awake()
    {
        SpawnFood(initNumFood);
        Populate(initNumHost);
    }

    float nextFoodSpawn = 0;
    private void Update()
    {
        if (numFood < maxNumFood && Time.time > nextFoodSpawn)
        {
            nextFoodSpawn = Time.time + foodSpawnInterval;
            SpawnFood(1);
        }
    }

    void Populate(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            hf.SpawnHost(Random.insideUnitCircle * gameObject.transform.localScale.x * 0.5f);
        }
    }

    void SpawnFood(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            GameObject obj = Instantiate(food, Random.insideUnitCircle * gameObject.transform.localScale.x * 0.5f, Quaternion.identity, gameObject.transform);
            obj.GetComponent<Nourishment>().wd = this;
        }
        numFood += numToSpawn;
    }
}