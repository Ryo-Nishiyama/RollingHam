using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOption : MonoBehaviour
{
    int resolution = 1;
    List<int> width = new List<int>() {1920, 1280, 720};
    List<int> height = new List<int>() { 1080, 720, 480};

    void resolutionUP()
    {
        Screen.SetResolution(width[resolution], height[resolution], true);
    }
}
