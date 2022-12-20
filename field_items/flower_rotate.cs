using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flower_rotate : MonoBehaviour
{
    float rotateY = 0;
    float rotateZ = 0;
    float originalX, originalY;

    // Start is called before the first frame update
    void Start()
    {
        originalX = this.transform.localEulerAngles.x;
        rotateY = this.transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        rotateZ += Time.deltaTime * 10;
        rotateY += Time.deltaTime * 20;
        if (rotateZ > 360)
        {
            rotateZ = 0;
        }
        if (rotateY > 360)
        {
            rotateY = 0;
        }
        this.transform.eulerAngles = new Vector3(originalX, rotateY, rotateZ);
    }
}
