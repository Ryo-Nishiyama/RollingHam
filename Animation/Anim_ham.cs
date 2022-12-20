using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anim_ham : MonoBehaviour
{
    Animator _Animator;
    public Rigidbody _Rigidbody;
    [SerializeField] moveTPS _moveTPS;
    [SerializeField] GroundCheck _GroundCheck;
    [SerializeField] bool selectMode = false;
    [SerializeField] int ballNum = 0;

    bool walk, dush, jump, charge, move = false;
    bool isResult = false;
    float groundSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "race_result")
        {
            isResult = true;
            SetResult();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isResult)
        {
            if (!selectMode)
            {
                groundSpeed = Mathf.Abs(_Rigidbody.velocity.x) + Mathf.Abs(_Rigidbody.velocity.z);
                animChange();
                hamPos();
                getParameter();
            }
            if (!move)
            {
                SetWait();
            }
        }
    }
    void animChange()
    {
        //移動オン
        if (_moveTPS._maruBot || _moveTPS.maru || _moveTPS.maru_w)
        {
            _Animator.SetBool("walk", true);
            //アニメーション速度を変化
            _Animator.SetFloat("walkSpeed", groundSpeed/3);
        }
        else
        {
            _Animator.SetBool("walk", false);
        }
        //チャージ
        if (_moveTPS._R2Bot || _moveTPS.R2 || _moveTPS.R2_e)
        {
            _Animator.SetBool("charge", true);
        }
        else
        {
            _Animator.SetBool("charge", false);
        }
        //接地せずある程度上昇力があるとき
        if (!_GroundCheck.IsGround() && _Rigidbody.velocity.y > 1)
        {
            _Animator.SetBool("jump", true);
        }
        else
        {
            _Animator.SetBool("jump", false);
        }
    }
    void hamPos()
    {
        float lerpSpeed = (groundSpeed / _moveTPS.limitedSpeed)/1.4f;
        if (lerpSpeed > 1)
        {
            lerpSpeed = 1;
        }
        else if (lerpSpeed < 0)
        {
            lerpSpeed = 0;
        }
        //nullエラー対策
        else if (lerpSpeed >= 0 || lerpSpeed <= 1)
        {
            lerpSpeed = lerpSpeed;
        }
        else
        {
            lerpSpeed = 0;
        }
        this.transform.localPosition = Vector3.Lerp(new Vector3(0, -0.424f, 0), new Vector3(0, -0.424f, -0.2f), lerpSpeed);
        this.transform.localRotation = Quaternion.Euler(Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(35, 0, 0), lerpSpeed));
    }
    void getParameter()
    {
        walk = _Animator.GetBool("walk");
        dush = _Animator.GetBool("dush");
        jump = _Animator.GetBool("jump");
        charge = _Animator.GetBool("charge");
        //どれかがtrueならmoveをtrue
        
        if (walk || dush || jump || charge)
        {
            move = true;
            _Animator.SetBool("move", true);
        }
        else
        {
            move = false;
            _Animator.SetBool("move", false);
        }
    }
    public void SetWait()
    {
        _Animator.SetInteger("waitRandom", Random.Range(0, 4));
    }
    void SetResult()
    {
        switch(result_ranking.ranking[ballNum])
        {
            case 0:
                _Animator.SetBool("victory", true);
                break;
            case 1:
                _Animator.SetBool("wait", true);
                break;
            case 3:
                _Animator.SetBool("lose", true);
                break;
        }
        
    }
}
