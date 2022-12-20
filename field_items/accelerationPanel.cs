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
            //動かなそうな色に変更
            _Renderer.material.color = new Color32(70, 70, 70, 150);
            _Renderer.material.SetColor("_EmissionColor",new Color32(0, 0, 0, 0));
            //タグを地面判定にすることで動作させなくする
            this.tag = "Ground";
        }
        //5秒後再起動
        if (stopCoolTime > 5)
        {
            isStopGap = false;
            _Renderer.material.color = new Color32(255, 255, 255, 255);
            stopCoolTime = 0;
            this.tag = "accelerationPanel";
        }
        //1f機能停止をずらす
        if (isStop)
        {
            isStop = false;
            isStopGap =true;
        }
    }
    void gradation()
    {
        colorTimer += Time.deltaTime * 0.25f;
        //パネルのemissionをグラデーション変化させる,12sで一周
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
