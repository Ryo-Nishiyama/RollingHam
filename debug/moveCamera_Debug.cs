using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera_Debug : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    Vector3 positionOriginal;
    float moveSpeedOriginal;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeedOriginal = moveSpeed;
        positionOriginal = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = moveSpeedOriginal * 2;
        }
        else
        {
            moveSpeed = moveSpeedOriginal;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.forward * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition -= transform.forward * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition -= transform.right * moveSpeed;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                transform.localPosition -= transform.up * moveSpeed;
            }
            else
            {
                transform.localPosition += transform.up * moveSpeed;
            }
            
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y+rotateSpeed, transform.localEulerAngles.z);
        }
        if (Input.GetKey(KeyCode.J))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - rotateSpeed, transform.localEulerAngles.z);
        }
        if (Input.GetKey(KeyCode.K))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + rotateSpeed, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        if (Input.GetKey(KeyCode.I))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - rotateSpeed, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        if (Input.GetKey(KeyCode.Return))
        {
            transform.position = positionOriginal;
        }
        if (Input.GetKey(KeyCode.Backspace))
        {
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

    }
}
