using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopwatch_hour : MonoBehaviour
{
    float timer = 0;
    int hourCount = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 60;
        //���Ƃ��Ƃ̌X������␳
        this.transform.localEulerAngles = new Vector3(0, 0, (timer + hourCount * 360) / 30);
        if (timer > 360)
        {
            timer = 0;
            hourCount += 1;
        }
        //��������猳�ɖ߂�
        if (hourCount > 30)
        {
            hourCount = 0;
        }
    }
}
