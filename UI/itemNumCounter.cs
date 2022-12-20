using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemNumCounter : MonoBehaviour
{
    public moveTPS _moveTPS;
    public TextMeshProUGUI NumCounter;
    int counter;

    // Update is called once per frame
    void Update()
    {
        counter = GameFinish.NumCount;
        NumCounter.text = "*"+counter.ToString();
    }
}
