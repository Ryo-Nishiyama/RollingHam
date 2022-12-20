using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverse : MonoBehaviour
{
    public float resetDir = 0f;
    float radian = 0f;
    float resetTime = 0f;
    float radianMemory = 0f;

    float radianCount = 0;
    bool reset = false;

    public GameObject unitychan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraReverse();
    }

    void cameraReverse()
    {
        float DualstickLhorizontal = Input.GetAxis("Horizontal Stick-L");
        float DualstickLvertical = Input.GetAxis("Vertical Stick-L");
        float DualstickRhorizontal = Input.GetAxis("Horizontal Stick-R");
        float DualstickRvertical = Input.GetAxis("Vertical Stick-R");

        bool buttonR2 = Input.GetButtonDown("Fire_R2");

        if (buttonR2)
        {
            reset = true;
            radianCount = 100;
            if (radian > 180)
            {
                radianMemory = -(360 - radian) / radianCount;
            }
            else
            {
                radianMemory = radian / radianCount;
            }
        }
        if (radianCount == 0 || DualstickRhorizontal != 0 || DualstickRvertical != 0)
        {
            reset = false;
            resetTime = 0f;
        }
        if (reset)
        {
            resetTime += 0.005f;
            if (resetDir > 360)
            {
                resetDir -= 360;
            }
            else if (resetDir < -360)
            {
                resetDir += 360;
            }
            if (radianCount > 0)
            {
                resetDir += radianMemory;
                radianCount -= 1;
                //ˆÚ“®‚µ‚Ä‚¢‚È‚¢‚Æ‚«‚¾‚¯”w–Ê‚ÉƒJƒƒ‰‚ğˆÚ“®‚·‚é
                if (DualstickLhorizontal == 0 && DualstickLvertical == 0)
                {
                    radian -= radianMemory;
                }
            }
        }
        

        if ((DualstickLhorizontal != 0 || DualstickLvertical != 0))
        {
            radian = Mathf.Atan2(DualstickLvertical, DualstickLhorizontal) * Mathf.Rad2Deg + 90;
        }
        
        if (radian < 0)
        {
            radian += 360;
        }
        else if(radian >= 360)
        {
            radian -= 360;
        }
        this.transform.localEulerAngles = new Vector3(0f, -radian, 0f);
    }
}
