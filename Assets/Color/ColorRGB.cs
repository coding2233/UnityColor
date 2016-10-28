using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ColorRGB : MonoBehaviour {
    Texture2D tex2d;
    public RawImage ri;
    int TexPixelWdith = 16;
    int TexPixelHeight = 256;
    Color[,] arrayColor;
    // Use this for initialization
    void Start () {
        arrayColor = new Color[TexPixelWdith, TexPixelHeight];
        tex2d = new Texture2D(TexPixelWdith, TexPixelHeight, TextureFormat.RGB24,true);

        Color[] calcArray = CalcArrayColor();
        tex2d.SetPixels(calcArray);
        tex2d.Apply();

        ri.texture = tex2d;
    }
	
    
    Color[] CalcArrayColor()
    {
        int addValue = (TexPixelHeight - 1) / 3;
        for (int i = 0; i < TexPixelWdith; i++)
        {
            arrayColor[i, 0] = Color.red;
            arrayColor[i, addValue] = Color.green;
            arrayColor[i, addValue+addValue] = Color.blue;
            arrayColor[i, TexPixelHeight - 1] = Color.red;
        }
        Color value = (Color.green - Color.red)/addValue;
        for (int i = 0; i < TexPixelWdith; i++)
        {
            for (int j = 0; j < addValue; j++)
            {
                arrayColor[i, j] = Color.red + value * j;
            }
        }
        value = (Color.blue - Color.green)/ addValue;
        for (int i = 0; i < TexPixelWdith; i++)
        {
            for (int j = addValue; j < addValue*2; j++)
            {
                arrayColor[i, j] = Color.green + value * (j-addValue);
            }
        }

        value = (Color.red - Color.blue) / ((TexPixelHeight - 1)-addValue-addValue);
        for (int i = 0; i < TexPixelWdith; i++)
        {
            for (int j = addValue*2; j < TexPixelHeight - 1; j++)
            {
                arrayColor[i, j] = Color.blue + value * (j- addValue * 2);
            }
        }

        List<Color> listColor = new List<Color>();
        for (int i = 0; i < TexPixelHeight; i++)
        {
            for (int j = 0; j < TexPixelWdith; j++)
            {
                listColor.Add(arrayColor[j, i]);
            }
        }

        return listColor.ToArray();
    }

    /// <summary>
    /// 获取颜色 根据高度
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public Color GetColorBySliderValue(float value)
    {
        Color getColor=tex2d.GetPixel(0,(int)((TexPixelHeight-1)*(1.0f-value)));
        return getColor;
    }
}



