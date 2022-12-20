using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadResult : MonoBehaviour
{
    [SerializeField] ChangeScene _ChangeScene;
    [SerializeField] goalCount _goalCount;
    bool goalCheck = false;

    // Update is called once per frame
    void Update()
    {
        //�v���C���[���S�[���������J�ڂ��J�n
        if (_goalCount.goal && !goalCheck)
        {
            goalCheck = true;
            StartCoroutine(endRace());
        }
    }
    IEnumerator endRace()
    {
        yield return new WaitForSeconds(2);
        _ChangeScene.result_load();
        _ChangeScene.raceResult_load();
        _ChangeScene.fadeOut();
    }
}
