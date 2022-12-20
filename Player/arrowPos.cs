using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowPos : MonoBehaviour
{
    [SerializeField] moveTPS _moveTPS;
    Vector3 arrowPosBase = new Vector3(0, 0, 1.8f);
    Vector3 arrowRotateBase = new Vector3(90, 0, 0);
    Vector3 arrowPosUp = new Vector3(0, 1, 0);
    Vector3 arrowRotateUp = new Vector3(0, 0, 0);
    Vector3 arrowPosDown = new Vector3(0, 0, 1.4f);
    Vector3 arrowRotateDown = new Vector3(105, 0, 0);

    // Update is called once per frame
    void Update()
    {
        float _UDcounter = _moveTPS.UDcounter;
        if (_UDcounter >= 0)
        {
            this.transform.localPosition = Vector3.Lerp(arrowPosBase, arrowPosUp, _UDcounter);
            this.transform.localEulerAngles = Vector3.Lerp(arrowRotateBase, arrowRotateUp, _UDcounter);
        }
        else
        {
            this.transform.localPosition = Vector3.Lerp(arrowPosBase, arrowPosDown, -_UDcounter);
            this.transform.localEulerAngles = Vector3.Lerp(arrowRotateBase, arrowRotateDown, -_UDcounter);
        }

    }
}
