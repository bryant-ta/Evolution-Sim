using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct StatsContainer
{
    public int size;
    public int endurance;
    public int efficiency;
    public int speed;
    public int agility;
    public int finesse;
    public int reasoning;
    public int memory;
    public int fertility;
    public int sense;
    public int special;
}

public class Host : MonoBehaviour
{
    [Header("Info")]
    public int gen = 1;

    [SerializeField] StatsContainer baseStats;
    [SerializeField] StatsContainer stats;

    [Header("State Stats")]
    public int hp = 10;
    public int energy = 10;

    [Label("Host Factory")] public HostFactory hf;

    [HideInInspector] public UnityEvent StatsUpdated;

    public void Init(int _gen, StatsContainer _baseStats)
    {
        gen = _gen;
        baseStats = _baseStats;
        stats = baseStats;

        // TODO: calculate other stats
    }

    float nextTick = 0;
    private void Update()
    {
        if (Time.time > nextTick)
        {
            // Tick Energy
            nextTick = Time.time + 1;
            energy--;
            if (energy <= 0)
            {
                Die();
            }
        }

        if (energy > Fertlity)
        {
            energy -= Mathf.RoundToInt(Fertlity / 4);
            Replicate();
        }
    }

    void Eat(GameObject food)
    {
        energy += food.GetComponent<Nourishment>().nourishment_value;
        Destroy(food.gameObject);
    }

    void Replicate()
    {
        hf.SpawnHost(transform.position, this);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Food")
        {
            Eat(col.gameObject);
        }
    }
    
    // Trabilities should UpdateStat() when initialized, passing direct stat changes (+ or -)
    public void UpdateStat(StatsContainer newStats)
    {
        stats.size += newStats.size;
        stats.endurance += newStats.endurance;
        stats.efficiency += newStats.efficiency;
        stats.speed += newStats.speed;
        stats.agility += newStats.agility;
        stats.finesse += newStats.finesse;
        stats.reasoning += newStats.reasoning;
        stats.memory += newStats.memory;
        stats.fertility += newStats.fertility;
        stats.sense += newStats.sense;
        stats.special += newStats.special;

        StatsUpdated.Invoke();
    }

    // Accessors for Base Stats
    public StatsContainer BaseStats => baseStats;

    // Accessors for Acquired Stats
    public int Size => stats.size;
    public int Endurance { get { return stats.endurance; } }
    public int Efficiency { get { return stats.efficiency; } }
    public int Speed { get { return stats.speed; } }
    public int Agility { get { return stats.agility; } }
    public int Finesse { get { return stats.finesse; } }
    public int Reasoning { get { return stats.reasoning; } }
    public int Memory { get { return stats.memory; } }
    public int Fertlity { get { return stats.fertility; } }
    public int Sense { get { return stats.sense; } }
    public int Special { get { return stats.special; } }
}