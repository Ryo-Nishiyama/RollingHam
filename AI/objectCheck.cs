using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectCheck : MonoBehaviour
{
    public string enterScopeObjectTag;
    public Vector3 enterScopeObjectPos;

    private void OnTriggerEnter(Collider collision)
    {
        enterScopeObjectTag = collision.transform.tag;
        enterScopeObjectPos = collision.transform.position;
    }
}
