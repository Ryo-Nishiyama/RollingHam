using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadCheck : MonoBehaviour
{
    public bool PadMode = false;

    // Update is called once per frame
    void Update()
    {
        // 接続されているコントローラの名前を調べる
        var controllerNames = Input.GetJoystickNames();
        // 一台もコントローラが接続されていなければエラー
        if (controllerNames.Length != 0)
        {
            PadMode = true;
        }
    }
}