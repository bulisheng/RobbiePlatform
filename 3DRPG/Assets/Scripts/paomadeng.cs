using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class paomadeng : MonoBehaviour
{
    private RectTransform thisRect;
    private RectTransform textRect;

    public List<string> texts = new List<string>();
    [SerializeField]
    private Text text;
    [Range(0, 10)]
    public float speed = 2;
    public int index = 0;
    private void Start()
    {
        thisRect = GetComponent<RectTransform>();
        textRect = text.GetComponent<RectTransform>();
        texts.Add("魏甲鑫许愿时光腰带还有冰柱,万世,石碑!!!");
        texts.Add("<color=#ffff00>祝魏甲鑫天天闪光,次次金牌!!!</color>");
    }
    // Update is called once per frame
    void Update()
    {
        if (texts.Count == 0)
            return;
        if (textRect.offsetMax.x <= 0)
        {
            if (texts.Count <= index)
            {
                index = 0;
            }
            text.text = texts[index];
        }
        if (textRect.offsetMax.x <= -10)
        {
            index++;
            textRect.offsetMax = new Vector2(thisRect.rect.width + textRect.rect.width, textRect.offsetMax.y);
        }
        else
            textRect.offsetMax = new Vector2(textRect.offsetMax.x - speed, textRect.offsetMax.y);
    }
}
}
