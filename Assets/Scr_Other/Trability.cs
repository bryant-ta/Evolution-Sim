using UnityEngine;

public abstract class Trability : MonoBehaviour
{
    [HideInInspector] public Host ho;

    public virtual void Setup()
    {
        ho = GetComponent<Host>();
        ho.StatsUpdated.AddListener(UpdateTrability);
    }

    // Recalculate applicable values in trability
    public abstract void UpdateTrability();
}