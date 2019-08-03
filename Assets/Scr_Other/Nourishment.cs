using UnityEngine;

public class Nourishment : MonoBehaviour
{
    public int nourishment_value;

    [HideInInspector] public World wd;

    private void OnDestroy()
    {
        wd.numFood--; 
    }
}