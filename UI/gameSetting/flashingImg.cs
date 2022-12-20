using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashingImg : MonoBehaviour
{
    [SerializeField] Image flashImg;
    [SerializeField] Color32 flashColor = new Color32(255, 255, 255, 255);

    // Update is called once per frame
    void Update()
    {
        flashImg.color = new Color32(flashColor.r, flashColor.g, flashColor.b, (byte)Mathf.Ceil((Mathf.Sin(Time.realtimeSinceStartup * 4) / 4 + 0.75f) * 255));
    }
}
