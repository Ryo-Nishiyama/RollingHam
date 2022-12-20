using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemSet : MonoBehaviour
{
    public TextMeshProUGUI itemTxt;
    static public int itemAmount = 1;
    List<string> level = new List<string>() { "è≠Ç»Ç¢", "Ç”Ç¬Ç§", "ëΩÇ¢", "Ç∆ÇƒÇ‡ëΩÇ¢" };
    // Start is called before the first frame update
    void Start()
    {
        itemTxt.text = level[itemAmount];
    }

    public void itemUP()
    {
        if (itemAmount < 3)
        {
            itemAmount += 1;
        }
        else
        {
            itemAmount = 0;
        }
        itemTxt.text = level[itemAmount];
    }
    public void itemDOWN()
    {
        if (itemAmount > 0)
        {
            itemAmount -= 1;
        }
        else
        {
            itemAmount = 3;
        }
        itemTxt.text = level[itemAmount];
    }
    public static int ItemAmount()
    {
        return itemAmount;
    }
}
