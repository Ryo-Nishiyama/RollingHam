using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionTPS : MonoBehaviour
{
    float frontbackSpeed = 0.2f;
    Vector3 frontbackPos = new Vector3(0,1,-2.5f);
    float frontbackAngle = 5;
    Vector3 frontbackPosOriginal = new Vector3(0,1.75f,-3);
    float frontbackAngleOriginal = 15;
    public float directionSpeedY = 30.0f;
    public float directionSpeedX = 30.0f;
    public float limitAngle = 90.0f;
    public float limitAngleHori = 135.0f;
    public Camera PlayerCamera;
    public Camera UICamera;
    public GameObject unitychan;
    public moveTPS _moveTPS;
    public float overDir = 0.0f;

    float turningPos = 0;
    float turningPosLimited = 0.75f;
    List<float> turningPosLimiteds = new List<float>() { 0.45f, 0.45f, 0.25f, 0.45f, 0.35f, 0.45f, 0.05f, 0.45f };
    float turningTime = 0;
    float turningSpeed = 2.5f;
    float _radian = 0;
    float _radianPre = 0;
    float _ballTurningInitial;
    float stickXTime = 0f;
    float stickRTimeY = 0f;
    float stickYTime = 0f;
    float MouseTimeX = 0f;
    float MouseTimeY = 0f;
    float MouseVerticalTime = 0f;
    float MouseHorizontalTime = 0f;

    bool resetDirection = false;
    bool PadCheck = false;

    float resetTime = 0f;
    float resetSide = 0f;
    float NoMouseSec = 0;

    float DualstickRvertical;
    float DualstickRhorizontal;
    float DualstickLhorizontal = 0;
    float DualstickLvertical = 0;
    float MouseHorizontal;
    float MouseVertical;
    float KeyHorizontal;
    float KeyVertical;
    bool MouseWheel = false;
    bool up_I, down_K, right_L, left_J = false;

    bool chargeCheck = false;
    float chargeCountPre = 0;

    float _nowHP;
    Vector3 cameraPos;

    bool backCheck, backCheckR, backCheckL = false;
    float localPosR, localPosL, localPosZ = 5;
    float localPosY = 1.75f;
    Vector3 backPos = new Vector3(0, 0, 0);
    Vector3 upPos = new Vector3(0, 0, 0);
    RaycastHit hit;
    int obstacleMask;

    private void Start()
    {
        obstacleMask = LayerMask.GetMask(new string[] { "ground", "wall" });
        _nowHP = _moveTPS.nowHP;
        _ballTurningInitial = _moveTPS.ballTurningInitial;
        //カメラの初期位置を保存
        cameraPos = this.transform.localPosition;
        frontbackPosOriginal = PlayerCamera.transform.localPosition;
        frontbackAngleOriginal = PlayerCamera.transform.localEulerAngles.x;

    }
    void FixedUpdate()
    {
        _ballTurningInitial = _moveTPS.ballTurningInitial;
        _radian = _moveTPS.radian;
        touchWall();
        //開始前のHP変更で揺れないよう制限
        if (countdown.startGameCheck)
        {
            //ダメージ揺れ
            Shake(0.2f, 0.1f);
        }
        
        //チャージ揺れ
        ShakeCharge(0.25f, 0.025f);
        turningPosChange();
        if (PadCheck)
        {
            PadInput();
            DualChangeDirection(DualstickRvertical, DualstickRhorizontal, stickYTime, stickXTime);
        }
        else
        {

            KeyInput();
            DualChangeDirection(DualstickRvertical, DualstickRhorizontal, KeyVertical, KeyHorizontal);

        }
        //前フレームのradianを保存
        _radianPre = _radian;
    }
    public void Shake(float duration, float magnitude)
    {
        //HPが減少したとき、撃墜されたとき
        if ((_nowHP > _moveTPS.nowHP || (_nowHP > 0 && _moveTPS.nowHP == 0))&&!_moveTPS.changeBall && Time.timeScale==1)
        {
            StartCoroutine(Vibration(duration, magnitude));
        }
        else if (_moveTPS.changeBall)
        {
            _moveTPS.changeBall = false;
        }
        _nowHP = _moveTPS.nowHP;
    }
    void ShakeCharge(float duration, float magnitude)
    {
        if (!_moveTPS.R2 && !_moveTPS._R2Bot && !_moveTPS.R2_e && chargeCheck)
        {
            StartCoroutine(Vibration(duration * (chargeCountPre / _moveTPS.maxCharge) * _moveTPS.ballChargePowerTime, magnitude));
            chargeCheck = false;
        }
        //一定のチャージがされているとき揺れok
        if (_moveTPS.chargeCount > _moveTPS.maxCharge * 0.7f && !_moveTPS.selectBall[3])
        {
            chargeCheck = true;
        }
        else
        {
            chargeCheck = false;
        }
        chargeCountPre = _moveTPS.chargeCount;
    }
    IEnumerator Vibration(float duration, float magnitude)
    {
        
        float elapsed = 0;
        while (elapsed < duration)
        {
            float xPos = cameraPos.x + Random.Range(-1f, 1f) * magnitude;
            float yPos = cameraPos.y + Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(xPos, yPos, cameraPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        this.transform.localPosition = cameraPos;
    }
    
    void PadInput()
    {
        DualstickRvertical = Input.GetAxis("Vertical Stick-R");
        DualstickRhorizontal = Input.GetAxis("Horizontal Stick-R");
        DualstickLhorizontal = Input.GetAxis("Horizontal Stick-L");
        DualstickLvertical = Input.GetAxis("Vertical Stick-L");
        Vector2 stickR = new Vector2(DualstickRhorizontal, DualstickRvertical);
        stickYTime += stickR.y * directionSpeedY * Time.deltaTime;
        stickXTime += stickR.x * directionSpeedX * Time.deltaTime;
    }
    void MouseInput()
    {
        MouseVertical = Input.GetAxis("Mouse Y");
        MouseHorizontal = Input.GetAxis("Mouse X");
        MouseWheel = Input.GetMouseButtonDown(2);
        MouseVerticalTime = MouseVertical * directionSpeedY * Time.deltaTime * 10;
        MouseHorizontalTime += MouseHorizontal * directionSpeedX * Time.deltaTime * 10;
    }
    void KeyInput()
    {
        up_I = Input.GetKey(KeyCode.I);
        down_K = Input.GetKey(KeyCode.K);
        right_L = Input.GetKey(KeyCode.L);
        left_J = Input.GetKey(KeyCode.J);
        if (up_I)
        {
            KeyVertical += directionSpeedY * Time.deltaTime;
        }
        if (down_K)
        {
            KeyVertical += -directionSpeedY * Time.deltaTime;
        }
        if (right_L)
        {
            KeyHorizontal += directionSpeedX * Time.deltaTime;
        }
        if (left_J)
        {
            KeyHorizontal += -directionSpeedX * Time.deltaTime;
        }

    }
    void turningPosChange()
    {
        float changeRadian = (Mathf.Abs(_radian) - Mathf.Abs(_radianPre));
        if (changeRadian > 0 && turningTime >= 0&& (!_moveTPS.R2 && !_moveTPS.R2_e && !_moveTPS._R2Bot))
        {
            if (turningTime < 1)
            {
                turningTime += Time.deltaTime * turningSpeed;
            }
            turningPos = Mathf.Lerp(0, turningPosLimiteds[selectBallSet.selectHam], Mathf.Abs(turningTime));
            
        }
        else if (changeRadian < 0&& turningTime<=0&& (!_moveTPS.R2 && !_moveTPS.R2_e && !_moveTPS._R2Bot))
        {
            
            if (turningTime > -1)
            {
                turningTime -= Time.deltaTime * turningSpeed;
            }
            turningPos = Mathf.Lerp(0, -turningPosLimiteds[selectBallSet.selectHam], Mathf.Abs(turningTime));
        }
        else
        {
            if (turningTime > 0)
            {
                turningTime -= Time.deltaTime * turningSpeed;
                turningPos = Mathf.Lerp(0, turningPosLimiteds[selectBallSet.selectHam], Mathf.Abs(turningTime));
                if (turningTime < 0)
                {
                    turningTime = 0;
                }
            }
            else if (turningTime < 0)
            {
                turningTime += Time.deltaTime * turningSpeed;
                turningPos = Mathf.Lerp(0, -turningPosLimiteds[selectBallSet.selectHam], Mathf.Abs(turningTime));
                if (turningTime > 0)
                {
                    turningTime = 0;
                }
            }
            if (turningTime == 0)
            {
                turningPos = 0;
            }
        }
    }
    void DualChangeDirection(float Rvertical, float Rhorizontal, float Lvertical, float Lhorizontal)
    {

        //Y軸最大まで傾ける
        if (Lvertical > limitAngle)
        {
            Lvertical = limitAngle;
        }
        else if (Lvertical < -limitAngle)
        {
            Lvertical = -limitAngle;
        }

        //X軸最大まで傾ける
        if (Lhorizontal > limitAngleHori)
        {
            Lhorizontal = limitAngleHori;
        }
        else if (Lhorizontal < -limitAngleHori)
        {
            Lhorizontal = -limitAngleHori;
        }
        if ((Rvertical == 0 && Rhorizontal == 0 && PadCheck) || MouseWheel || (!up_I && !down_K && !right_L && !left_J && !PadCheck))
        {
            resetDirection = true;
        }
        else if (((Rvertical != 0 || Rhorizontal != 0) && PadCheck) || (MouseHorizontal != 0 || MouseVertical != 0) || (!PadCheck && (up_I || down_K || right_L || left_J)))
        {
            resetDirection = false;
            resetTime = 0f;
        }

        if (resetDirection)
        {
            resetTime += 0.005f;
            Lvertical = Mathf.Lerp(Lvertical, 0, resetTime);
            Lhorizontal = Mathf.Lerp(Lhorizontal, 0, resetTime);
        }
        //両方0でないとき視点をもとに戻す
        if (DualstickLhorizontal != 0 || DualstickLvertical != 0 || MouseWheel)
        {
            overDir += Lhorizontal;
            if (overDir > 360)
            {
                overDir = 0;
            }
            if (overDir < -360)
            {
                overDir = 0;
            }
        }
        Vector3 changeRotate = new Vector3(Lvertical, Lhorizontal, 0f);
        this.transform.localEulerAngles = changeRotate;
        //stickと同期させて値の保存を防ぐ
        stickYTime = Lvertical;
        KeyVertical = Lvertical;
        stickXTime = Lhorizontal;
        MouseHorizontalTime = Lhorizontal;
        KeyHorizontal = Lhorizontal;
    }
    //カメラがオブジェクトにめり込まないように移動させる
    void touchWall()
    {
        Debug.DrawRay(gameObject.transform.position, (-transform.forward + transform.up / 3 -transform.right / 3) * 5, Color.green, 1f, false);
        Debug.DrawRay(gameObject.transform.position, (-transform.forward + transform.up / 3 + transform.right / 3) * 5, Color.green, 1f, false);
        Debug.DrawRay(new Vector3(PlayerCamera.transform.position.x,gameObject.transform.position.y,PlayerCamera.transform.position.z), new Vector3(0,1.75f,0) , Color.green, 1f, false);

        float RotateY = this.gameObject.transform.eulerAngles.y * Mathf.Deg2Rad;

        //斜め後ろ二方向でカメラがめり込まないかチェック
        if (Physics.Raycast(gameObject.transform.position, -transform.forward + transform.up / 3 - transform.right / 3, out hit, 5, obstacleMask))
        {
            backPos = hit.point - this.gameObject.transform.position;
            //z軸の距離を自分から見た向きに回転,角の距離から直線距離に
            localPosL = Mathf.Abs(Mathf.Sin(RotateY) * backPos.x + Mathf.Cos(RotateY) * backPos.z);
            backCheckL = true;
        }
        else
        {
            backCheckL = false;
        }
        if (Physics.Raycast(gameObject.transform.position, -transform.forward + transform.up / 3 + transform.right / 3, out hit, 5, obstacleMask))
        {
            backPos = hit.point - this.gameObject.transform.position;
            localPosR = Mathf.Abs(Mathf.Sin(RotateY) * backPos.x + Mathf.Cos(RotateY) * backPos.z);
            backCheckR = true;
        }
        else
        {
            backCheckR = false;
        }
        //両方当たっているとき、より障害物まで近いほうの値を参照
        if (backCheckL && backCheckR)
        {
            if (localPosL < localPosR)
            {
                localPosZ = localPosL;
            }
            else
            {
                localPosZ = localPosR;
            }
        }
        else if (backCheckL)
        {
            localPosZ = localPosL;
        }
        else if (backCheckR)
        {
            localPosZ = localPosR;
        }
        //障害物から離れた時滑らかに元に戻す
        if (localPosZ < 5)
        {
            localPosZ += 0.03f;
            if (localPosZ > 5)
            {
                localPosZ = 5;
            }
        }
        if (Physics.Raycast(new Vector3(PlayerCamera.transform.position.x, gameObject.transform.position.y, PlayerCamera.transform.position.z), transform.up, out hit,5f, obstacleMask))
        {
            upPos = hit.point - this.gameObject.transform.position;
            localPosY = Mathf.Lerp(0.25f, frontbackPosOriginal.y, (upPos.y-0.5f)/2.5f);
        }
        else if (localPosY < frontbackPosOriginal.y)
        {
            localPosY += 0.03f;
            if (localPosY < frontbackPosOriginal.y)
            {
                localPosY = frontbackPosOriginal.y;
            }
        }
        //最大5のlocalPosZを割って逆数にすることでカメラ位置を調整
        PlayerCamera.transform.localPosition = Vector3.Lerp(new Vector3(turningPos, localPosY, frontbackPosOriginal.z), new Vector3(0, localPosY, 0), 1 - localPosZ / 5);
        UICamera.transform.localPosition = Vector3.Lerp(new Vector3(turningPos,localPosY, -3), new Vector3(0, localPosY, 0), 1 - localPosZ / 3);
    }
}
