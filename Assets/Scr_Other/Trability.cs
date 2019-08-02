using UnityEngine;

public abstract class Trability : MonoBehaviour
{
    public Stats st;

    public virtual void Setup()
    {
        st = GetComponent<Stats>();
    }

    // Recalculate applicable values in trability
    public abstract void UpdateTrability();
}