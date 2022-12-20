using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM_menu : MonoBehaviour
{
    GameObject BGM;
    
    int _sceneNum;
    private void Start()
    {
        BGM = GameObject.Find("BGM");
        //����BGM�I�u�W�F�N�g������Ƃ��j������
        if (BGM != null && (BGM != this.gameObject))
        {
            Destroy(this.gameObject);
        }
        
        _sceneNum = ChangeScene.sceneNum;
        DontDestroyOnLoad(this.gameObject);
        
    }

    private void Update()
    {
        //�t�B�[���h�A���[�X�ɐ؂�ւ���Ƃ������~�߂�
        if ((_sceneNum == 0 || _sceneNum == 1|| _sceneNum == 4 || _sceneNum ==10) && (ChangeScene.sceneNum == 2 || ChangeScene.sceneNum == 6 || ChangeScene.sceneNum == 7|| ChangeScene.sceneNum == 9))
        {
            Destroy(this.gameObject);
        }
    }

}
