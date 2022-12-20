using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectHam : MonoBehaviour
{
    Renderer _Renderer;
    [SerializeField] Material[] ballColor;
    [SerializeField] bool outdoorCheck = false;
    Color32[] ballColors = new Color32[] { new Color32(0, 255, 255, 40), new Color32(0, 255, 0, 40),
                                           new Color32(100, 30, 0, 40), new Color32(150, 0, 255, 40),
                                           new Color32(255, 255, 0, 40), new Color32(255, 0, 50, 40),
                                           new Color32(0, 0, 0, 40), new Color32(255, 255, 255, 40)};
    // Start is called before the first frame update
    void Start()
    {
        _Renderer = GetComponent<Renderer>();
        if (outdoorCheck)
        {
            int randNum = Random.Range(0, ballColors.Length);
            _Renderer.material.color = ballColors[randNum];
        }
        else
        {
            _Renderer.material = ballColor[selectBallSet.selectHam];
        }
    }
}
