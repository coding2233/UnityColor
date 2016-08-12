using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ColorPanel : MonoBehaviour {
    Texture2D tex2d;
    public RawImage ri;

    int TexPixelLength = 256;
    Color[,] arrayColor;
	// Use this for initialization
	void Start () {
        arrayColor = new Color[TexPixelLength, TexPixelLength];
        tex2d = new Texture2D(TexPixelLength, TexPixelLength, TextureFormat.RGB24,true);
        ri.texture = tex2d;

        SetColorPanel(Color.red);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            Color end = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f),1.0f);
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

}
