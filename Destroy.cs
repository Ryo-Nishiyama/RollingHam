using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    float time = 0.0f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1.4f)
        {
            Destroy(gameObject);
        }
    }
    
}
