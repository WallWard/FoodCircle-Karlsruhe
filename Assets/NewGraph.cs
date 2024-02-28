using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGraph : MonoBehaviour
{

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("grapcontainer").GetComponent<RectTransform>();

        CreateCircle(new Vector2(200, 200));
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameobject = new GameObject("circle", typeof(Image));
        gameobject.transform.SetParent(graphContainer, false);
        gameobject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameobject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.anchorMin = new Vector2(0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
