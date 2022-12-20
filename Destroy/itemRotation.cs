using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemRotation : MonoBehaviour
{
    public float rotate_x = 5;
    public float rotate_y = 0;
    public float rotate_z = 5;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(rotate_x, rotate_y, rotate_z));
    }
}
