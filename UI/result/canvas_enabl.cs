using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas_enabl : MonoBehaviour
{
    public GameObject itemCount;
    // Start is called before the first frame update
    void Start()
    {
        itemCount.SetActive(true);
    }
}
