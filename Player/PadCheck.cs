using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadCheck : MonoBehaviour
{
    public bool PadMode = false;

    // Update is called once per frame
    void Update()
    {
        // �ڑ�����Ă���R���g���[���̖��O�𒲂ׂ�
        var controllerNames = Input.GetJoystickNames();
        // �����R���g���[�����ڑ�����Ă��Ȃ���΃G���[
        if (controllerNames.Length != 0)
        {
            PadMode = true;
        }
    }
}