using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accelerationPanel : MonoBehaviour
{
    //Material _Material;
    Renderer _Renderer;
    float colorTimer = 0;

    bool isStop,isStopEnter, isStopStay, isStopExit = false;
    bool isStopGap = false;
    float stopCoolTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        _Renderer = GetComponent<Renderer>();
        _Renderer.material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        IsStop();
        panelSwitch();
        if (!isStopGap)
        {
            gradation();
        }
    }

    void panelSwitch()
    {
        if (isStopGap)
        {
            stopCoolTime += Time.deltaTime;
            //�����Ȃ����ȐF�ɕύX
            _Renderer.material.color = new Color32(70, 70, 70, 150);
            _Renderer.material.SetColor("_EmissionColor",new Color32(0, 0, 0, 0));
            //�^�O��n�ʔ���ɂ��邱�Ƃœ��삳���Ȃ�����
            this.tag = "Ground";
        }
        //5�b��ċN��
        if (stopCoolTime > 5)
        {
            isStopGap = false;
            _Renderer.material.color = new Color32(255, 255, 255, 255);
            stopCoolTime = 0;
            this.tag = "accelerationPanel";
        }
        //1f�@�\��~�����炷
        if (isStop)
        {
            isStop = false;
            isStopGap =true;
        }
    }
    void gradation()
    {
        colorTimer += Time.deltaTime * 0.25f;
        //�p�l����emission���O���f�[�V�����ω�������,12s�ň��
        if (colorTimer <= 1)
        {
            _Renderer.material.SetColor("_EmissionColor", Color.Lerp(new Color32(255, 0, 0, 0), new Color32(0, 255, 0, 0), colorTimer));
        }
        else if (colorTimer <= 2)
        {
            _Renderer.material.SetColor("_EmissionColor", Color.Lerp(new Color32(0, 255, 0, 0), new Color32(0, 0, 255, 0), colorTimer - 1));
        }
        else if (colorTimer <= 3)
        {
            _Renderer.material.SetColor("_EmissionColor", Color.Lerp(new Color32(0, 0, 255, 0), new Color32(255, 0, 0, 0), colorTimer - 2));
        }
        else
        {
            colorTimer = 0;
        }
    }
    void IsStop()
    {
        if (isStopEnter || isStopStay)
        {
            isStop = true;
        }
        if (isStopExit)
        {
            isStop = false;
        }
        isStopEnter = false;
        isStopStay = false;
        isStopExit = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "player" || collision.tag == "GroundCheck")
        {
            isStopEnter = true;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "player" || collision.tag == "GroundCheck")
        {
            isStopStay = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "player" || collision.tag == "GroundCheck")
        {
            isStopExit = true;
        }
    }

}
