using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resolution : MonoBehaviour
{
    bool full = true;
    int resolutionW = 1920;
    int resolutionH = 1080;

    public void resolutionFull()
    {
        full = true;
        resolutionW = 1920;
        resolutionH = 1080;
    }
    public void resolution2()
    {
        full = false;
        resolutionW = 1680;
        resolutionH = 1050;
    }
    public void resolution3()
    {
        full = false;
        resolutionW = 1600;
        resolutionH = 900;
    }
    public void resolution4()
    {
        full = false;
        resolutionW = 1600;
        resolutionH = 1024;
    }
    public void resolution5()
    {
        full = false;
        resolutionW = 1440;
        resolutionH = 900;
    }
    public void resolution6()
    {
        full = false;
        resolutionW = 1280;
        resolutionH = 720;
    }
    public void SettingDecision()
    {
        if(resolutionW!=Screen.width && resolutionH != Screen.height)
        {
            Screen.SetResolution(resolutionW, resolutionH, full);
        }
    }
}
