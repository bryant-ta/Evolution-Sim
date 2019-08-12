using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Stats
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

    public Stats(int _size = Constants.DEFAULT_STAT_VAL, int _endurance = Constants.DEFAULT_STAT_VAL, int _efficiency = Constants.DEFAULT_STAT_VAL, int _speed = Constants.DEFAULT_STAT_VAL, int _agility = Constants.DEFAULT_STAT_VAL, int _finesse = Constants.DEFAULT_STAT_VAL,
        int _reasoning = Constants.DEFAULT_STAT_VAL, int _memory = Constants.DEFAULT_STAT_VAL, int _fertility = Constants.DEFAULT_STAT_VAL, int _sense = Constants.DEFAULT_STAT_VAL, int _special = Constants.DEFAULT_STAT_VAL)
    {
        size = _size;
        endurance = _endurance;
        efficiency = _efficiency;
        speed = _speed;
        agility = _agility;
        finesse = _finesse;
        reasoning = _reasoning;
        memory = _memory;
        fertility = _fertility;
        sense = _sense;
        special = _special;
    }

    // Currently can't think of a better way to be able to iterate..
    // Note: I still want to have name and value pair display in inspector
    // Note: Unity currently doesn't serialize dictionary or struct
    public int[] GetStatsIter()
    {
        int[] statsArr = new int[Constants.NUM_STATS];
        statsArr[0] = size;
        statsArr[1] = size;
        statsArr[2] = size;
        statsArr[3] = size;
        statsArr[4] = size;
        statsArr[5] = size;
        statsArr[6] = size;
        statsArr[7] = size;
        statsArr[8] = size;
        statsArr[9] = size;
        statsArr[10] = size;
        return statsArr;
    }
}
public enum Stat
{
    Size = 1,
    Endurance = 2,
    Efficiency = 3,
    Speed = 4,
    Agility = 5,
    Finesse = 6,
    Reasoning = 7,
    Memory = 8,
    Fertility = 9,
    Sense = 10,
    Special = 11
}


public class Host : MonoBehaviour
{
    [Header("Info")]
    public int gen = 1;

    [SerializeField] Stats baseStats;
    [SerializeField] Stats stats;

    [Header("State Stats")]
    public int hp = 10;
    public int energy = 10;

    [Label("Host Factory")] public HostFactory hf;
    
    [HideInInspector] public UnityEvent StatsUpdated;

    bool isSpawningChild = false;
    UIAnalysis uia;

    public void Init(int _gen, Stats _baseStats)
    {
        gen = _gen;
        baseStats = _baseStats;
        AddStats(baseStats);

        // TODO: calculate other stats
        SetStateStats();

        // Breaking host modify self-contained rule bc Scriptable Object+Prefab nonsense in unity
        uia = GameObject.Find("LevelManager").GetComponent<UIAnalysis>();
        if (!uia)
        {
            print("Did not find 'LevelManager' in scene");
            return;
        }
        uia.RegisterHost(this, gen);

        
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
    }

    void Eat(GameObject food)
    {
        energy += food.GetComponent<Nourishment>().nourishment_value;
        Destroy(food.gameObject);
    }

    // Spawn potentially mutated copy of host
    // Requires 2 parents
    // Call on collision with same species
    void Reproduce(Host parent1, Host parent2)
    {
        hf.SpawnHost(transform.position, parent1, parent2);
    }

    // Spawn exact copy of host
    // Only requires 1 parent
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
        // temp defining species as same gen
        else if (col.gameObject.tag == "Host" && energy > 2*(Fertility + Efficiency) && col.gameObject.GetComponent<Host>().gen == gen)
        {
            energy /= 2;
            Host other = col.gameObject.GetComponent<Host>();
            if (!other.isSpawningChild)
            {
                isSpawningChild = true;
                Reproduce(this, other);
            }
            else    // Kinda hacky but works for now... dont like external obj responsible for this obj state
            {
                other.isSpawningChild = false;
            }
        }
    }

    void SetStateStats()
    {
        hp = Size * 2;
        energy = Endurance * 2 + Size;
    }
    
    // Trabilities should UpdateStat() when initialized, passing direct stat changes (+ or -)
    public void AddStats(Stats newStats)
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
    public Stats BaseStats => baseStats;

    // Accessors for Acquired Stats
    public int Size => stats.size;
    public int Endurance { get { return stats.endurance; } }
    public int Efficiency { get { return stats.efficiency; } }
    public int Speed { get { return stats.speed; } }
    public int Agility { get { return stats.agility; } }
    public int Finesse { get { return stats.finesse; } }
    public int Reasoning { get { return stats.reasoning; } }
    public int Memory { get { return stats.memory; } }
    public int Fertility { get { return stats.fertility; } }
    public int Sense { get { return stats.sense; } }
    public int Special { get { return stats.special; } }
}