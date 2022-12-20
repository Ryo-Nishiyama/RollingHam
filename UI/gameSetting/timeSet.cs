using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timeSet : MonoBehaviour
{
    public TextMeshProUGUI minSet;
    public TextMeshProUGUI secSet;
    static public int gameTimer = 420;

    // Start is called before the first frame update
    void Start()
    {
        minSet.text = (gameTimer / 60).ToString("D2");
        secSet.text = (gameTimer % 60).ToString("D2");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void timeUP()
    {
        gameTimer = int.Parse(minSet.text) * 60 + int.Parse(secSet.text);
        if (gameTimer < 600)
        {
            gameTimer += 30;
        }
        else
        {
            gameTimer = 180;
        }
        minSet.text = (gameTimer / 60).ToString("D2");
        secSet.text = (gameTimer % 60).ToString("D2");
    }
    public void timeDOWN()
    {
        gameTimer = int.Parse(minSet.text) * 60 + int.Parse(secSet.text);
        if (gameTimer > 180)
        {
            gameTimer -= 30;
        }
        else
        {
            gameTimer = 600;
        }
        minSet.text = (gameTimer / 60).ToString("D2");
        secSet.text = (gameTimer % 60).ToString("D2");
    }

    public static float getTimer()
    {
        return (float)gameTimer;
    }
}
