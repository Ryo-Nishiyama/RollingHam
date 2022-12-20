using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class eventSet : MonoBehaviour
{
    public TextMeshProUGUI eventTxt;
    static public int eventFrecency = 1;
    List<string> level = new List<string>() { "è≠Ç»Ç¢", "Ç”Ç¬Ç§", "ëΩÇ¢", "Ç∆ÇƒÇ‡ëΩÇ¢" };
    // Start is called before the first frame update
    void Start()
    {
        eventTxt.text = level[eventFrecency];
    }

    public void eventUP()
    {
        if (eventFrecency < 3)
        {
            eventFrecency += 1;
        }
        else
        {
            eventFrecency = 0;
        }
        eventTxt.text = level[eventFrecency];
    }
    public void eventDOWN()
    {
        if (eventFrecency > 0)
        {
            eventFrecency -= 1;
        }
        else
        {
            eventFrecency = 3;
        }
        eventTxt.text = level[eventFrecency];
    }

    public static int EventFrecency()
    {
        return eventFrecency;
    }
}
