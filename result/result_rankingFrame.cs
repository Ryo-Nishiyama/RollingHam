using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result_rankingFrame : MonoBehaviour
{
    [SerializeField] Image[] frames;
    [SerializeField] bool raceResult = false;
    [SerializeField] Color32 startColor = new Color32(255, 130, 0, 255);
    [SerializeField] Color32 endColor = new Color32(255, 200, 0, 255);

    // Start is called before the first frame update
    void Start()
    {
        if (raceResult)
        {
            for(int i = 0; i < frames.Length; i++)
            {
                frames[i].color = new Color32(50, 50, 50, 255);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Color32 nowColor = Color32.Lerp(startColor, endColor, (float)(Mathf.Sin(Time.time * 2) * 0.5 + 0.5f));
        if (raceResult)
        {
            frames[result_ranking.ranking[0]].color = nowColor;
        }
        else
        {
            frames[0].color = nowColor;
        }
    }
}
