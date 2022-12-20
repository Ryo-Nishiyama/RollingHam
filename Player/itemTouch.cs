using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemTouch : MonoBehaviour
{
    public bool itemNumTouch = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "itemNum")
        {
            itemNumTouch = true;
            Destroy(collision.gameObject);
        }
    }
}
