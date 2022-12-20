using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCheck : MonoBehaviour
{
    //Unnecessary scripts
    bool isItemNum = false;
    bool itemNumStay = false;
    bool itemNumExit = false;
    bool itemNumEnter = false;
    public Vector3 itemPoint;

    // Update is called once per frame
    public bool IsItemNum()
    {
        if (itemNumEnter || itemNumStay)
        {
            isItemNum = true;
        }
        else if (itemNumExit)
        {
            isItemNum = false;
        }
        itemNumStay = false;
        itemNumExit = false;
        itemNumExit = false;
        return isItemNum;
    }
    private void OnTriggerEnter(Collider collision)
    {
        itemPoint = collision.ClosestPointOnBounds(this.transform.position);
        if (collision.tag == "itemNum")
        {
            itemNumEnter = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "itemNum")
        {
            itemNumExit = true;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "itemNum")
        {
            itemNumStay = true;
        }
    }
}
