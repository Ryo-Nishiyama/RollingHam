using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class polkaDots : MonoBehaviour
{
    Transform polkaSheetL, polkaSheetR;
    [SerializeField] bool reverseCheck = false;
    [SerializeField] float speed = 1;
    Vector3 defPosL, defPosR;
    float changePos = 0;

    // Start is called before the first frame update
    void Start()
    {
        polkaSheetL = this.transform.GetChild(0);
        polkaSheetR = this.transform.GetChild(1);
        defPosL = polkaSheetL.localPosition;
        defPosR = polkaSheetR.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //ˆÚ“®‘¬“x
        changePos = Time.deltaTime * speed;
        if (reverseCheck)
        {
            polkaSheetL.localPosition = new Vector3(polkaSheetL.localPosition.x + changePos, 0, 0);
            polkaSheetR.localPosition = new Vector3(polkaSheetR.localPosition.x + changePos, 0, 0);
            //’[‚ÌˆÊ’u‚Ü‚Å‚«‚½‚ç‘Š•û‚ÌŒã‚ë‚ÉˆÚ“®‚³‚¹‚é
            if (polkaSheetR.localPosition.x > defPosR.x)
            {
                polkaSheetR.localPosition = polkaSheetL.localPosition - defPosR;
            }
            else if(polkaSheetL.localPosition.x> defPosR.x)
            {
                polkaSheetL.localPosition = polkaSheetR.localPosition - defPosR;
            }
        }
        else
        {
            polkaSheetL.localPosition = new Vector3(polkaSheetL.localPosition.x - changePos, 0, 0);
            polkaSheetR.localPosition = new Vector3(polkaSheetR.localPosition.x - changePos, 0, 0);
            if (polkaSheetR.localPosition.x < -defPosR.x)
            {
                polkaSheetR.localPosition = polkaSheetL.localPosition + defPosR;
            }
            else if (polkaSheetL.localPosition.x < -defPosR.x)
            {
                polkaSheetL.localPosition = polkaSheetR.localPosition + defPosR;
            }
        }
    }
}
