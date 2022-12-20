using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSpin : MonoBehaviour
{
    //空中の回転制限
    const float AIR_ATTENUATION_LIMIT = 0.3f;
    readonly List<Color> ALPHA_COLORS = new List<Color> { new Color(0, 0.21f, 0.39f), new Color(0.04f, 0.39f, 0),new Color(0.36f,0.16f,0),
                                                          new Color(0.14f,0,0.36f),new Color(0.36f,0.34f,0),new Color(0.36f,0,0.08f),
                                                          new Color(0,0,0),new Color(0.1f,0.1f,0.1f) };

    float _nowSpeed;
    float _limitedSpeed;
    float rotateSpeedDiff = 1;
    float nowAlpha;
    float limitAlpha;
    bool limitAlpha_flag = false;
    int selectBall_now;
    
    moveTPS _moveTPS;
    Renderer _renderer;
    Material _material_now, _material_limit;
    Rigidbody _rigidbody;
    [SerializeField] GroundCheck _GroundCheck;

    // Start is called before the first frame update
    void Start()
    {
        _moveTPS = transform.parent.gameObject.GetComponent<moveTPS>();
        _rigidbody = transform.parent.gameObject.GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _material_now = _renderer.materials[0];
        _material_limit = _renderer.materials[1];
        _material_now.EnableKeyword("_EMISSON");
        _material_limit.EnableKeyword("_EMISSON");
        if (!_moveTPS.selectBall[selectBall_now])
        {
            selectBall_now = selectBall_check(_moveTPS.selectBall);
            changeColor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _nowSpeed = _moveTPS.nowSpeed;
        _limitedSpeed = _moveTPS.limitedSpeed;
        spin(_nowSpeed);
        changeTexture(_nowSpeed,_limitedSpeed);
        if (!_moveTPS.selectBall[selectBall_now])
        {
            selectBall_now = selectBall_check(_moveTPS.selectBall);
            changeColor();
        }
    }
    int selectBall_check(bool[] ballList)
    {
        for (int i = 0; i < ballList.Length; i++)
        {
            if (ballList[i])
            {
                return i;
            }
        }
        return 0;
    }
    void spin(float nowSpeed, float attenuationSpeed = 0.02f)
    {
        float rotateSpeed = nowSpeed * 0.5f;
        //空中
        if (!_GroundCheck.IsGround())
        {
            //上りは徐々に減衰、一定まで下がったら倍率固定
            if (_rigidbody.velocity.y > 1 && rotateSpeedDiff > AIR_ATTENUATION_LIMIT)
            {
                rotateSpeedDiff -= attenuationSpeed;
            }
            //下りに変わったら徐々に減衰分を戻す
            else if(_rigidbody.velocity.y < 0 && rotateSpeedDiff < 1)
            {
                rotateSpeedDiff += attenuationSpeed;
            }
        }

        //地上に戻ったらリセット
        if (rotateSpeedDiff != 1 && _GroundCheck.IsGround())
        {
            rotateSpeedDiff = 1;
        }

        transform.Rotate(0, -rotateSpeed * rotateSpeedDiff,  0);
    }
    void changeColor()
    {
        _material_now.SetColor("_EmissionColor", ALPHA_COLORS[selectBall_now]);
    }
    void changeTexture(float nowSpeed,float limitedSpeed,float maxSpeed=70,float startSpeed=30)
    {
        nowAlpha = (nowSpeed / limitedSpeed)*255;
        //超えた時値を戻してbyteとの整合性をとる
        if (nowAlpha > 255)
        {
            nowAlpha = 255;
        }
        //スピードがstartSpeed超えたら
        if (nowSpeed > startSpeed)
        {
            limitAlpha = ((nowSpeed-startSpeed) / maxSpeed)*255;
            if (limitAlpha > 255)
            {
                limitAlpha = 255;
            }
            _material_limit.color = new Color32(255, 255, 255, (byte)limitAlpha);
            limitAlpha_flag = true;
        }
        else
        {
            limitAlpha_flag = false;
        }
        if (limitAlpha != 0 && !limitAlpha_flag)
        {
            limitAlpha = 0;
            _material_limit.color = new Color32(255, 255, 255, (byte)limitAlpha);
        }
        //現在の最大スピードに到達していることをわかりやすく
        _material_now.color = new Color32(255, 255, 255, (byte)nowAlpha);
    }
}
