using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class titleDescription : MonoBehaviour
{
    [SerializeField] buttonSelector _buttonSelector;
    public TextMeshProUGUI description;
    string[] textList = new string[] { "�n���X�^�[����Ă�\n���[�X�̗D����ڎw���܂�", "�D���Ȑݒ��\n���[�X��V�Ԃ��Ƃ��ł��܂�",
                                       "�Q�[���̗V�ѕ���\n�܂߂������ɂ���\n�l�X�ȏ������邱�Ƃ�\n�ł��܂��B",
                                       "�Q�[���Ɏg�p����\n�l�X�ȏ��\n�L�ڂ���Ă��܂��B", "�Q�[�����I�����܂��B" };

    // Update is called once per frame
    void Update()
    {
        description.text = textList[_buttonSelector.buttonCounter];
    }
}
