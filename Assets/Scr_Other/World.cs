using UnityEngine;

public class World : MonoBehaviour
{
    public int maxFoodCount;
    public float foodSpawnInterval;

    public GameObject food;

    float nextFoodSpawn = 0;
    private void Update()
    {
        if (Time.time > nextFoodSpawn)
        {
            nextFoodSpawn = Time.time + foodSpawnInterval;
            SpawnFood();
        }
    }

    void SpawnFood()
    {
        Instantiate(food, Random.insideUnitCircle * gameObject.transform.localScale.x * 0.5f, Quaternion.identity);
    }
}