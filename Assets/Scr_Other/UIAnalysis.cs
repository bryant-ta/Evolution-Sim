using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIAnalysis : MonoBehaviour
{
    public WindowGraph wg;
    public Button buttonHideGraph;
    public Button buttonAliveDistGraph;
    public List<Toggle> toggleStatGraph;
    
    List<List<Host>> hostList = new List<List<Host>>();

    // Should only be accessible if all other stat graph toggles are off
    public void ToggleAliveDistGraph()
    {
        GameObject[] aliveHosts = GameObject.FindGameObjectsWithTag("Host");
        List<int> statValues = new List<int>();
        if (wg.gameObject.activeSelf == false)
        {
            wg.gameObject.SetActive(true);
            wg.ShowGraph(Constants.NUM_STATS);
        }
        for (int i = 0; i < Constants.NUM_STATS; i++)
        {
            foreach (GameObject host in aliveHosts)
            {
                statValues.Add(StatNumToValue(i + 1, host.GetComponent<Host>()));
            }
            wg.ShowData(i, statValues, true);
            statValues.Clear();
        }
        buttonHideGraph.gameObject.SetActive(true);
    }

    public void ToggleStatGraph(int stat)
    {
        if (toggleStatGraph[stat-1].isOn)
        {
            DisplayStat(stat);
        }
        else
        {
            wg.ClearData(stat-1);
        }
    }

    void DisplayStat(int stat)
    {
        List<int> statAvgList = new List<int>();
        foreach(List<Host> hostsInGen in hostList)
        {
            int total = 0;
            foreach (Host host in hostsInGen)
            {
                total += StatNumToValue(stat, host);
            }
            statAvgList.Add(Mathf.RoundToInt((float)total / hostsInGen.Count));
        }

        if (wg.gameObject.activeSelf == false)
        {
            wg.gameObject.SetActive(true);
            wg.ShowGraph(statAvgList.Count);
        }
        buttonAliveDistGraph.gameObject.SetActive(false);
        buttonHideGraph.gameObject.SetActive(true);
        wg.ShowData(stat-1, statAvgList);
    }

    public void HideGraph()
    {
        buttonHideGraph.gameObject.SetActive(false);
        buttonAliveDistGraph.gameObject.SetActive(true);
        foreach (Toggle t in toggleStatGraph)
        {
            t.isOn = false;
        }
        wg.gameObject.SetActive(false);
        wg.ClearGraph();
    }

    public void RegisterHost(Host host, int gen)
    {
        if (gen <= 0) return;
        if (hostList.Count < gen)
        {
            hostList.Add(new List<Host> { host });
        }
        else
        {
            hostList[gen - 1].Add(host);
        }
    }

    // Return indicated host stat when given stat int
    int StatNumToValue(int statID, Host host)
    {
        switch (statID)
        {
            case 1:
                return host.Size;
            case 2:
                return host.Endurance;
            case 3:
                return host.Efficiency;
            case 4:
                return host.Speed;
            case 5:
                return host.Agility;
            case 6:
                return host.Finesse;
            case 7:
                return host.Reasoning;
            case 8:
                return host.Memory;
            case 9:
                return host.Fertility;
            case 10:
                return host.Sense;
            case 11:
                return host.Special;
            default:
                Debug.Log("Invalid Stat enum");
                return -1;
        }
    }

    //public List<Host> GetHostsInGen(int gen)
    //{
    //    return hostList[gen - 1];
    //}
}