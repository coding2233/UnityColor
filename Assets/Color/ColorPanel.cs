using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ColorPanel : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    Texture2D tex2d;
    public RawImage ri;

    int TexPixelLength = 256;
    Color[,] arrayColor;

    RectTransform rt;
    public RectTransform circleRect;
    // Use this for initialization
    void Start()
    {
        arrayColor = new Color[TexPixelLength, TexPixelLength];
        tex2d = new Texture2D(TexPixelLength, TexPixelLength, TextureFormat.RGB24, true);
        ri.texture = tex2d;

        rt = GetComponent<RectTransform>();

        SetColorPanel(Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Color end = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
            SetColorPanel(end);
        }
    }

    public void SetColorPanel(Color endColor)
    {
        Color[] CalcArray = CalcArrayColor(endColor);
        tex2d.SetPixels(CalcArray);
        tex2d.Apply();
    }



    Color[] CalcArrayColor(Color endColor)
    {
        Color value = (endColor - Color.white) / (TexPixelLength - 1);
        for (int i = 0; i < TexPixelLength; i++)
        {
            arrayColor[i, TexPixelLength - 1] = Color.white + value * i;
        }
        for (int i = 0; i < TexPixelLength; i++)
        {
            value = (arrayColor[i, TexPixelLength - 1] - Color.black) / (TexPixelLength - 1);
            for (int j = 0; j < TexPixelLength; j++)
            {
                arrayColor[i, j] = Color.black + value * j;
            }
        }
        List<Color> listColor = new List<Color>();
        for (int i = 0; i < TexPixelLength; i++)
        {
            for (int j = 0; j < TexPixelLength; j++)
            {
                listColor.Add(arrayColor[j, i]);
            }
        }

        return listColor.ToArray();
    }


    /// <summary>
    /// 获取颜色by坐标
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Color GetColorByPosition(Vector2 pos)
    {
        Texture2D tempTex2d = (Texture2D)ri.texture;
        Color getColor = tempTex2d.GetPixel((int)pos.x, (int)pos.y);

        return getColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 wordPos;
        //将UGUI的坐标转为世界坐标  
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out wordPos))
            circleRect.position = wordPos;

        circleRect.GetComponent<ColorCircle>().setShowColor();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 wordPos;
        //将UGUI的坐标转为世界坐标  
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out wordPos))
            circleRect.position = wordPos;

        circleRect.GetComponent<ColorCircle>().setShowColor();
    }
}
