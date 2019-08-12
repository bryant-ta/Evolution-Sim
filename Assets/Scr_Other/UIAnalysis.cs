using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIAnalysis : MonoBehaviour
{
    public WindowGraph wg;
    public Button buttonHideGraph;
    public List<Toggle> toggleStatGraph;
    
    List<List<Host>> hostList = new List<List<Host>>();

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
                switch (stat)
                {
                    case 1:
                        total += host.Size;
                        break;
                    case 2:
                        total += host.Endurance;
                        break;
                    case 3:
                        total += host.Efficiency;
                        break;
                    case 4:
                        total += host.Speed;
                        break;
                    case 5:
                        total += host.Agility;
                        break;
                    case 6:
                        total += host.Finesse;
                        break;
                    case 7:
                        total += host.Reasoning;
                        break;
                    case 8:
                        total += host.Memory;
                        break;
                    case 9:
                        total += host.Fertility;
                        break;
                    case 10:
                        total += host.Sense;
                        break;
                    case 11:
                        total += host.Special;
                        break;
                    default:
                        Debug.Log("Invalid Stat enum");
                        break;
                }
            }
            statAvgList.Add(Mathf.RoundToInt((float)total / hostsInGen.Count));
        }

        if (wg.gameObject.activeSelf == false)
        {
            wg.gameObject.SetActive(true);
            wg.ShowGraph(statAvgList.Count);
        }
        buttonHideGraph.gameObject.SetActive(true);
        wg.ShowData(stat-1, statAvgList);
    }

    public void HideGraph()
    {
        buttonHideGraph.gameObject.SetActive(false);
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

    //public List<Host> GetHostsInGen(int gen)
    //{
    //    return hostList[gen - 1];
    //}
}