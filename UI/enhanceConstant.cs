using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enhanceConstant : MonoBehaviour
{
    Color32 originalColor;
    // Start is called before the first frame update
    void Start()
    {
        originalColor = this.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //constant‚ð“_–Å‚³‚¹‚Ä‹­’²
        int alphaColor = (int)Mathf.Ceil((Mathf.Sin(Time.time * 3) * 0.25f + 0.75f) * 255);
        this.GetComponent<Image>().color = new Color32(originalColor.r, originalColor.g, originalColor.b, (byte)alphaColor);
    }
}
