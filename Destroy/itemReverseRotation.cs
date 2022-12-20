using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemReverseRotation : MonoBehaviour
{
    Vector3 rotate;
    // Start is called before the first frame update
    void Start()
    {
        rotate = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = rotate;
    }
}
