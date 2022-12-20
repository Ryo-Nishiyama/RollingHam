using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seedRotate : MonoBehaviour
{
    float rotateTimer = 0;
    float orizinalY;
    // Start is called before the first frame update
    void Start()
    {
        orizinalY = this.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        //10s‚Åˆê‰ñ“]
        rotateTimer += Time.deltaTime * 36;
        if (rotateTimer > 360)
        {
            rotateTimer = 0;
        }
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, orizinalY + rotateTimer, this.transform.eulerAngles.z);
    }
}
