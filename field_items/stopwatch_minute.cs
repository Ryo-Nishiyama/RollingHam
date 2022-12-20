using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopwatch_minute : MonoBehaviour
{
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 60;
        //‚à‚Æ‚à‚Æ‚ÌŒX‚«•ª‚ð•â³
        this.transform.localEulerAngles = new Vector3(0, 0, timer);
        if (timer > 360)
        {
            timer = 0;
        }
    }
}
