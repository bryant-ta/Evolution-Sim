using UnityEngine;

[CreateAssetMenu(fileName = "HostFactory", menuName = "ScriptableObjects/HostFactory")]
public class HostFactory : ScriptableObject
{
    public GameObject host;
    public World wd;

    // Host creation function -  maybe move to factory later
    public void SpawnHost(Vector2 pos, Host parent1 = null, Host parent2 = null)
    {
        Stats childStats;

        // If host has no parents (A new species)
        if (parent1 == null)
        {
            parent1 = host.GetComponent<Host>();
            parent1.Init(0, new Stats(Constants.DEFAULT_STAT_VAL));
            childStats = parent1.BaseStats;
        }
        // If host has one parent (Replication)
        else if (parent2 == null)
        {
            childStats = parent1.BaseStats;
        }
        // If host has two parents (Normal Reproduction)
        else
        {
            // Get Base Stat from random parent, per stat
            int[] childStatsArr = new int[Constants.NUM_STATS];
            int[] parent1StatsArr = parent1.BaseStats.GetStatsIter();
            int[] parent2StatsArr = parent2.BaseStats.GetStatsIter();
            for (int i = 0; i < childStatsArr.Length; i++)
            {
                if (Random.Range(0,2) == 0)
                {
                    childStatsArr[i] = parent1StatsArr[i] + Random.Range(-Constants.ADAPTABILITY_VAL, Constants.ADAPTABILITY_VAL + 1);
                }
                else
                {
                    childStatsArr[i] = parent2StatsArr[i] + Random.Range(-Constants.ADAPTABILITY_VAL, Constants.ADAPTABILITY_VAL + 1);
                }
            }
            childStats = new Stats(childStatsArr[0], childStatsArr[1], childStatsArr[2], childStatsArr[3], childStatsArr[4],
                childStatsArr[5], childStatsArr[6], childStatsArr[7], childStatsArr[8], childStatsArr[9], childStatsArr[10]);
        }

        // Create child host
        GameObject obj = Instantiate(host, pos, Quaternion.Euler(0, 0, Random.Range(0, 359)));
        obj.GetComponent<Host>().Init(parent1.gen + 1, childStats);
    }
}