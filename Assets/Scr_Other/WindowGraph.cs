using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

/// Modified Graph Object, credit to https://unitycodemonkey.com
/// 
/// Features:
///     - Overlay graph data on a fixed X and Y axes, with Unity Toggles
///     - Customize axes label formatting
///     
public class WindowGraph : MonoBehaviour {

    [SerializeField] Sprite circleSprite;
    [SerializeField] RectTransform graphContainer;
    [SerializeField] RectTransform labelTemplateX;
    [SerializeField] RectTransform labelTemplateY;
    [SerializeField] RectTransform dashTemplateX;
    [SerializeField] RectTransform dashTemplateY;
    
    public float yMaximum = 100f;
    public float xSize = 50f;
    public int separatorCount = 10;
    public string titleX;

    public GameObject dataTogglesCont;     // Holds buttons for toggling data

    List<GameObject>[] dataObjs = new List<GameObject>[Constants.NUM_STATS];    // Each list holds a stat's plot (circles, lines)
    List<GameObject> graphObjs = new List<GameObject>();        // Holds axes, titles, label lines, etc.
    List<Color> dataColors = new List<Color>();                 // Holds stat plot colors

    float graphWidth;
    float graphHeight;

    private void Awake()
    {
        graphWidth = graphContainer.sizeDelta.x;
        graphHeight = graphContainer.sizeDelta.y;
        for (int i = 0; i < dataObjs.Length; i++)
        {
            dataObjs[i] = new List<GameObject>();
        }

        // Match plot color to toggle "graphic" color, chosen in inspector
        foreach (Toggle t in dataTogglesCont.transform.GetComponentsInChildren<Toggle>())
        {
            dataColors.Add(t.graphic.color);
        }
    }

    // Creates data points and connections
    // id: index of data in INTERNAl arrays, groupVertical: valueList data represents data points of one X (i.e. Point Dist graph)
    public void ShowData(int id, List<int> valueList, bool groupVertical = false)
    {
        if (groupVertical)
        {
            float xPosition = xSize * (id + 1);
            for (int i = 0; i < valueList.Count; i++)
            {
                float yPosition = (valueList[i] / yMaximum) * graphHeight;
                GameObject circleObj = CreateCircle(id, new Vector2(xPosition, yPosition));
                dataObjs[id].Add(circleObj);
            }
        }
        else
        {
            GameObject lastCirObj = null;        // Current data group to do actions on
            for (int i = 0; i < valueList.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = (valueList[i] / yMaximum) * graphHeight;
                GameObject circleObj = CreateCircle(id, new Vector2(xPosition, yPosition));
                dataObjs[id].Add(circleObj);
                GameObject lineObj;
                if (lastCirObj != null)
                {
                    lineObj = CreateDotConnection(id, lastCirObj.GetComponent<RectTransform>().anchoredPosition, circleObj.GetComponent<RectTransform>().anchoredPosition);
                    dataObjs[id].Add(lineObj);
                }
                lastCirObj = circleObj;
            }
        }
    }

    // Creates graph axes, axes labels, increment lines
    // Option to specify axes label format
    public void ShowGraph(int numXElements, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null) {
        if (getAxisLabelX == null) {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null) {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }
        
        GameObject axesTitleX = new GameObject("AxesTitleX", typeof(Text));
        graphObjs.Add(axesTitleX.gameObject);
        axesTitleX.transform.SetParent(graphContainer, false);
        axesTitleX.gameObject.SetActive(true);
        axesTitleX.GetComponent<Text>().text = titleX;
        axesTitleX.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        RectTransform rectTransform = axesTitleX.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphWidth / 2, -30f);
        rectTransform.sizeDelta = new Vector2(110, 20);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        for (int i = 0; i < numXElements; i++) {
            float xPosition = xSize + i * xSize;
            
            RectTransform labelX = Instantiate(labelTemplateX);
            graphObjs.Add(labelX.gameObject);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -20f);
            labelX.GetComponent<Text>().text = getAxisLabelX(i);
            
            RectTransform dashX = Instantiate(dashTemplateX);
            graphObjs.Add(dashX.gameObject);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, 0f);
        }
        
        for (int i = 0; i <= separatorCount; i++) {
            RectTransform labelY = Instantiate(labelTemplateY);
            graphObjs.Add(labelY.gameObject);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-30f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * yMaximum);
            
            RectTransform dashY = Instantiate(dashTemplateY);
            graphObjs.Add(dashY.gameObject);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(0f, normalizedValue * graphHeight);
        }
    }

    public void ClearData(int id)
    {
        foreach (GameObject obj in dataObjs[id])
        {
            Destroy(obj);
        }
        dataObjs[id].Clear();
    }

    public void ClearGraph(bool dataOnly = false)   // Don't use dataOnly option in this project (Evolution_Sim)
    {
        if (!dataOnly)
        {
            foreach (GameObject obj in graphObjs)
            {
                Destroy(obj);
            }
        }
        foreach (List<GameObject> dataPoints in dataObjs)
        {
            foreach (GameObject obj in dataPoints)
            {
                Destroy(obj);
            }
            dataPoints.Clear();
        }
        graphObjs.Clear();
    }

    private GameObject CreateCircle(int id, Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.GetComponent<Image>().color = dataColors[id];
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private GameObject CreateDotConnection(int id, Vector2 dotPositionA, Vector2 dotPositionB) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = dataColors[id] * 0.75f;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        return gameObject;
    }

}
