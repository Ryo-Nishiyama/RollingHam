using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CPUSet : MonoBehaviour
{
    public TextMeshProUGUI cpuTxt;
    static public int cpuCount = 3;
    List<string> level = new List<string>() {"0", "1" , "2" , "3" };

    // Start is called before the first frame update
    void Start()
    {
        cpuTxt.text = level[cpuCount];
    }

    public void cpuUP()
    {
        if (cpuCount < 3)
        {
            cpuCount += 1;
        }
        else
        {
            cpuCount = 0;
        }
        cpuTxt.text = level[cpuCount];
    }
    public void cpuDOWN()
    {
        if (cpuCount > 0)
        {
            cpuCount -= 1;
        }
        else
        {
            cpuCount = 3;
        }
        cpuTxt.text = level[cpuCount];
    }
}
