using UnityEngine;

[CreateAssetMenu(fileName = "HostFactory", menuName = "ScriptableObjects/HostFactory")]
public class HostFactory : ScriptableObject
{
    public GameObject host;
    public World wd;

    // Host creation function -  maybe move to factory later
    public void SpawnHost(Vector2 pos, Host parent = null)
    {
        if (parent == null)
        {
            parent = host.GetComponent<Host>();
            StatsContainer temp = new StatsContainer() { size = 10, endurance = 10, efficiency = 10, speed = 10,
                agility = 10, finesse = 10, reasoning = 10, memory = 10, fertility = 10, sense = 10, special = 10 };
            parent.Init(0, temp);
        }
        GameObject obj = Instantiate(host, pos, Quaternion.Euler(0, 0, Random.Range(0, 359)));
        obj.GetComponent<Host>().Init(parent.gen, parent.BaseStats);
    }
}