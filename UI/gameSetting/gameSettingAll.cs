using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameSettingAll : MonoBehaviour
{
    public timeSet _timeSet;
    public eventSet _eventSet;
    public itemSet _itemSet;
    public CPUSet _CPUSet;
    public buttonSelector _buttonSelector;
    public Image[] arrowImageR;
    public Image[] arrowImageL;

    AudioSource audioSource;
    [SerializeField] AudioClip a_select;

    bool juziCheck_right,juziCheck_left,juzi_right,juzi_left = false;
    float juziV = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //選んでない項目の色を元に戻す
        for(int i = 0; i < arrowImageR.Length; i++)
        {
            arrowImageR[i].enabled = false;
            arrowImageL[i].enabled = false;
        }
        
        juziV = Input.GetAxis("Horizontal D-Pad");
        juzi_left = false;
        juzi_right = false;
        if (juziV > 0.5f && !juziCheck_right)
        {
            juzi_right = true;
            juziCheck_right = true;
        }
        else if (juziV <= 0.5f)
        {
            juziCheck_right = false;
        }
        if (juziV < -0.5f && !juziCheck_left)
        {
            juzi_left = true;
            juziCheck_left = true;
        }
        else if (juziV >= -0.5f)
        {
            juziCheck_left = false;
        }

        //各項目の変化を実行
        switch (_buttonSelector.buttonCounter)
        {
            case 1:
                if (juzi_left ||Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    _timeSet.timeDOWN();
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    _timeSet.timeUP();
                }
                break;
            case 2:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    _eventSet.eventDOWN();
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    _eventSet.eventUP();
                }
                break;
            case 3:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    _itemSet.itemDOWN();
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    _itemSet.itemUP();
                }
                break;
            case 4:
                if (juzi_left || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    _CPUSet.cpuDOWN();
                }
                else if (juzi_right || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(a_select);
                    _CPUSet.cpuUP();
                }
                break;
        }
    }
    private void LateUpdate()
    {
        if(arrowImageR.Length >= _buttonSelector.buttonCounter&& _buttonSelector.buttonCounter != 0)
        {
            arrowImageR[_buttonSelector.buttonCounter - 1].enabled = true;
            arrowImageL[_buttonSelector.buttonCounter - 1].enabled = true;
        }
        
    }

}
