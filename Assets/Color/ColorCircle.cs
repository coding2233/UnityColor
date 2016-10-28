using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ColorCircle : MonoBehaviour, IDragHandler
{

    public delegate void RetureTextuePosition(Vector2 pos);
    public event RetureTextuePosition getPos;

    RectTransform rt;
    public int width = 256;
    public int height = 256;
    // Use this for initialization
    void Start () {
        rt = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 wordPos;
        //将UGUI的坐标转为世界坐标  
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out wordPos))
            rt.position = wordPos;
        
        if (rt.anchoredPosition.x <= 0)
            rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y);
        if (rt.anchoredPosition.y <= 0)
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, 0);
        if (rt.anchoredPosition.x >= width-1)
            rt.anchoredPosition = new Vector2(width-1,rt.anchoredPosition.y);
        if (rt.anchoredPosition.y >= height-1)
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, height-1);

        getPos(rt.anchoredPosition);
    }

    public void setShowColor()
    {
        getPos(rt.anchoredPosition);
    }

}
