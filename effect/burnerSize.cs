using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnerSize : MonoBehaviour
{
    [SerializeField] GameObject[] burnerObj;
    [SerializeField] ParticleSystem[] burners;
    [SerializeField] moveTPS _moveTPS;
    ParticleSystem.MainModule[] main = new ParticleSystem.MainModule[2];
    ParticleSystem.EmissionModule emission;
    Vector3 burnerRotateOriginal;
    float _radian, _radianPre;
    float burner_speed;
    float startSpeedNow = 0;
    int emissionRateNow = 0;
    // Start is called before the first frame update
    void Start()
    {
        main[0] = burners[0].main;
        main[1] = burners[1].main;
        emission = burners[0].emission;
        burnerRotateOriginal = burnerObj[0].transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        _radian = _moveTPS.radian;
        for (int i=0; i < burnerObj.Length; i++)
        {
            changeBurner(i);
            changeRotate(i);
        }
        _radianPre = _radian;
    }
    /// <summary>
    /// バーナーの長さを決める
    /// </summary>
    /// <param name="num">バーナーの個数</param>
    void changeBurner(int num)
    {
        if (!_moveTPS.R2 && !_moveTPS.R2_e && !_moveTPS._R2Bot)
        {
            burner_speed = _moveTPS.nowSpeed / _moveTPS.limitedSpeed;
        }
        else
        {
            burner_speed = 0;
        }
            startSpeedNow = Mathf.Lerp(0.0f, 9.0f, _moveTPS.ballVel_now / _moveTPS.ballVelocity / 4+burner_speed);
        if (_moveTPS.selectBall[3])
        {
            //limitedが高いほど伸びるよう変更
            //limitedじゃなくnowspeedをかける検討
            if (!_moveTPS.R2 && !_moveTPS.R2_e && !_moveTPS._R2Bot)
            {
                startSpeedNow = Mathf.Lerp(0.0f, 9.0f, (_moveTPS.addPower+_moveTPS.limitedSpeed) / 50);
            }
            else
            {
                startSpeedNow = 0;
            }
        }
        main[num].startSpeed = startSpeedNow;
    }
    void changeRotate(int num)
    {
        float nowRotate_x = Mathf.Lerp(-90, 90, _moveTPS.UDdir.y / 2 + 0.5f);
        float valueDifference = _radian - _radianPre;
        float nowRotate_y=0;
        //チャージ中,noturningはy軸回転させない
        if (!_moveTPS.R2 && !_moveTPS.R2_e && !_moveTPS._R2Bot)
        {
            //ゆっくり回転させる方法考える
            if (valueDifference > 0)
            {
                nowRotate_y = -10;
            }
            else if (valueDifference < 0)
            {
                nowRotate_y = 10;
            }
            else
            {
                nowRotate_y = 0;
            }
        }
        
        
        Vector3 nowRotate = new Vector3(nowRotate_x, nowRotate_y, burnerRotateOriginal.z);
        burnerObj[num].transform.localEulerAngles = nowRotate;
    }
}
