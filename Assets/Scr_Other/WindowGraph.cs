using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class WindowGraph : MonoBehaviour {

    [SerializeField] Sprite circleSprite;
    [SerializeField] RectTransform graphContainer;
    [SerializeField] RectTransform labelTemplateX;
    [SerializeField] RectTransform labelTemplateY;
    [SerializeField] RectTransform dashTemplateX;
    [SerializeField] RectTransform dashTemplateY;

    List<GameObject> instObjs;

    private void Awake()
    {
        instObjs = new List<GameObject>();
    }

    public void CreateGraph(List<int> valueList)
    {
        ShowGraph(valueList, (int _i) => "" + (_i + 1), (float _f) => "" + Mathf.RoundToInt(_f));
    }

    public void ClearGraph()
    {
        foreach (GameObject obj in instObjs)
        {
            Destroy(obj);
        }
        instObjs.Clear();
    }

    private void ShowGraph(List<int> valueList, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null) {
        if (getAxisLabelX == null) {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null) {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;

        GameObject lastCirObj = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = xSize + i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleObj = CreateCircle(new Vector2(xPosition, yPosition));
            instObjs.Add(circleObj);
            GameObject lineObj;
            if (lastCirObj != null) {
                lineObj = CreateDotConnection(lastCirObj.GetComponent<RectTransform>().anchoredPosition, circleObj.GetComponent<RectTransform>().anchoredPosition);
                instObjs.Add(lineObj);
            }
            lastCirObj = circleObj;
            
            RectTransform labelX = Instantiate(labelTemplateX);
            instObjs.Add(labelX.gameObject);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -20f);
            labelX.GetComponent<Text>().text = getAxisLabelX(i);
            
            RectTransform dashX = Instantiate(dashTemplateX);
            instObjs.Add(dashX.gameObject);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, 0f);
        }

        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++) {
            RectTransform labelY = Instantiate(labelTemplateY);
            instObjs.Add(labelY.gameObject);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-30f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * yMaximum);
            
            RectTransform dashY = Instantiate(dashTemplateY);
            instObjs.Add(dashY.gameObject);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(0f, normalizedValue * graphHeight);
        }
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1, .5f);
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
