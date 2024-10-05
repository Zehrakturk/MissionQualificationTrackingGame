using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public class panelController : MonoBehaviour
{
    private List<TextMeshProUGUI> texts;
    private List<Image> images;
    private int currentIndex =0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText()
    {
        for (int i = 0; i < texts.Count; i++)
        {
            if (currentIndex == i)
            {
                texts[i].enabled = true;
            }
            else
            {
                texts[i].enabled = false;
            }

        }

    }

    public void AddText(TextMeshProUGUI text)
    {
        texts.Add(text);
    }
}
