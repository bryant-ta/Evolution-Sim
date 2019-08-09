using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIAnalysis : MonoBehaviour
{
    public Button buttonDisplayGraph;
    public Button buttonHideGraph;
    public WindowGraph wg;

    //Stat curDisplayStat;
    List<List<Host>> hostList = new List<List<Host>>();

    public void DisplayGraph(int curStat)
    {
        List<int> statAvgList = new List<int>();
        foreach(List<Host> hostsInGen in hostList)
        {
            int total = 0;
            foreach (Host host in hostsInGen)
            {
                switch (curStat)
                {
                    case 1:
                        total += host.Speed;
                        break;
                    default:
                        Debug.Log("Invalid Stat enum");
                        break;
                }
            }
            statAvgList.Add(Mathf.RoundToInt((float)total / hostsInGen.Count));
        }

        buttonDisplayGraph.gameObject.SetActive(false);
        buttonHideGraph.gameObject.SetActive(true);
        wg.gameObject.SetActive(true);
        wg.CreateGraph(statAvgList);
    }

    public void HideGraph()
    {
        buttonDisplayGraph.gameObject.SetActive(true);
        buttonHideGraph.gameObject.SetActive(false);
        wg.ClearGraph();
        wg.gameObject.SetActive(false);
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