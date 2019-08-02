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

    //[SerializeField] int size;
    //[SerializeField] int endurance;
    //[SerializeField] int efficiency;
    //[SerializeField] int speed;
    //[SerializeField] int agility;
    //[SerializeField] int finesse;
    //[SerializeField] int reasoning;
    //[SerializeField] int memory;
    //[SerializeField] int fertility;
    //[SerializeField] int sense;
    //[SerializeField] int special;

    
}

public class Host : MonoBehaviour
{
    //// Possibly move to array representation later
    //// Non array form useful for showing values with labels in inspector
    //
    //const int numStats = 11;
    //enum Stat
    //{
    //    Size = 0,
    //    Endurance = 1,
    //    Efficency = 2,
    //    Speed = 3,
    //    Agility = 4,
    //    Finesse = 5,
    //    Reasoning = 6,
    //    Memory = 7,
    //    Fertility = 8,
    //    Sense = 9,
    //    Special = 10
    //}

    //[SerializeField] int[] baseStats = new int[numStats];
    //[SerializeField] int[] stats = new int[numStats];

    //[Header("Inherent Stat Values")]
    //[SerializeField] int baseSize = 10;
    //[SerializeField] int baseEndurance = 10;
    //[SerializeField] int baseEfficiency = 10;
    //[SerializeField] int baseSpeed = 10;
    //[SerializeField] int baseAgility = 10;
    //[SerializeField] int baseFinesse = 10;
    //[SerializeField] int baseReasoning = 10;
    //[SerializeField] int baseMemory = 10;
    //[SerializeField] int baseFertility = 10;
    //[SerializeField] int baseSense = 10;
    //[SerializeField] int baseSpecial = 10;

    //[Header("Acquired Stat Values")]    // The current values as affected by lifetime, disease, etc.
    //[SerializeField] int size;
    //[SerializeField] int endurance;
    //[SerializeField] int efficiency;
    //[SerializeField] int speed;
    //[SerializeField] int agility;
    //[SerializeField] int finesse;
    //[SerializeField] int reasoning;
    //[SerializeField] int memory;
    //[SerializeField] int fertility;
    //[SerializeField] int sense;
    //[SerializeField] int special;

    [SerializeField] StatsContainer baseStats;
    [SerializeField] StatsContainer stats;

    [Header("State Stats")]
    public int hp = 10;
    public int energy = 10;

    [HideInInspector] public UnityEvent StatsUpdated;

    private void Awake()
    {
        //size = baseSize;
        //endurance = baseEndurance;
        //efficiency = baseEfficiency;
        //speed = baseSpeed;
        //agility = baseAgility;
        //finesse = baseFinesse;
        //reasoning = baseReasoning;
        //memory = baseMemory;
        //fertility = baseFertility;
        //sense = baseSense;
        //special = baseSpecial;

        stats = baseStats;
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

    // External accessors for Acquired Stats
    public int Size { get { return stats.size; } }
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

    //// Event refers to Acquired stat value change
    //public UnityEvent sizeUpdate;
    //public UnityEvent enduranceUpdate;
    //public UnityEvent efficiencyUpdate;
    //public UnityEvent speedUpdate;
    //public UnityEvent agilityUpdate;
    //public UnityEvent finesseUpdate;
    //public UnityEvent reasoningUpdate;
    //public UnityEvent memoryUpdate;
    //public UnityEvent fertilityUpdate;
    //public UnityEvent senseUpdate;
    //public UnityEvent specialUpdate;

    //// Invoke on Acquired stat change
    //public void UpdateSize(int x)
    //{
    //    size = x;
    //    sizeUpdate.Invoke();
    //}
    //public void UpdateEndurance(int x)
    //{
    //    endurance = x;
    //    enduranceUpdate.Invoke();
    //}
    //public void UpdateEfficiency(int x)
    //{
    //    efficiency = x;
    //    efficiencyUpdate.Invoke();
    //}
    //public void UpdateSpeed(int x)
    //{
    //    speed = x;
    //    speedUpdate.Invoke();
    //}
    //public void UpdateAgility(int x)
    //{
    //    agility = x;
    //    agilityUpdate.Invoke();
    //}
    //public void UpdateFinesse(int x)
    //{
    //    finesse = x;
    //    finesseUpdate.Invoke();
    //}
    //public void UpdateReasoning(int x)
    //{
    //    reasoning = x;
    //    reasoningUpdate.Invoke();
    //}
    //public void UpdateMemory(int x)
    //{
    //    memory = x;
    //    memoryUpdate.Invoke();
    //}
    //public void UpdateFertility(int x)
    //{
    //    fertility = x;
    //    fertilityUpdate.Invoke();
    //}
    //public void UpdateSense(int x)
    //{
    //    sense = x;
    //    senseUpdate.Invoke();
    //}
    //public void UpdateSpecial(int x)
    //{
    //    special = x;
    //    specialUpdate.Invoke();
    //}
}