using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class flashing : MonoBehaviour
{
    public Button start;
    public TextMeshProUGUI start_t;
    
    [SerializeField] bool startScene = true;
    bool startCheck = false;
    [SerializeField] Image enterkey;

    // Update is called once per frame
    void Update()
    {
        start_t.color = new Color32(start_t.faceColor.r, start_t.faceColor.g, start_t.faceColor.b, (byte)Mathf.Ceil((Mathf.Sin(Time.time * 4) / 4 + 0.75f)*255));
        if (startScene)
        {
            enterkey.color = new Color32(255, 255, 255, (byte)Mathf.Ceil((Mathf.Sin(Time.time * 4) / 4 + 0.75f) * 255));
        }
        if ((Input.GetButton("Fire_2") || Input.GetKey(KeyCode.Return)) && startScene && !startCheck)
        {
            startCheck = true;
            start.onClick.Invoke();
        }
    }
}
