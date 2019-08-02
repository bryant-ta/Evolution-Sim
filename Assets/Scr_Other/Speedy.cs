using UnityEngine;

public class Speedy : Trability
{
    private void Start()
    {
        base.Setup();
        StatsContainer a = new StatsContainer { speed = -9 };
        st.UpdateStat(a);
    }

    public override void UpdateTrability()
    {
        
    }
}