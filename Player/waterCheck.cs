using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterCheck : MonoBehaviour
{
    bool isWater, isWaterEnter, isWaterStay, isWaterExit = false;

    //�ċz�̔���̂��߂ɐ��ڐG����𕪂���
    public bool IsWater()
    {
        if (isWaterEnter || isWaterStay)
        {
            isWater = true;
        }
        if (isWaterExit)
        {
            isWater = false;
        }
        isWaterEnter = false;
        isWaterStay = false;
        isWaterExit = false;
        return isWater;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "water")
        {
            isWaterEnter = true;
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "water")
        {
            isWaterStay = true;
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "water")
        {
            isWaterExit = true;
        }
    }
}