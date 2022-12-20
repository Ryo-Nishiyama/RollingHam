using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crown_enable : MonoBehaviour
{
    [SerializeField] GameObject load_background;

    // Update is called once per frame
    void Update()
    {
        if (load_background.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
