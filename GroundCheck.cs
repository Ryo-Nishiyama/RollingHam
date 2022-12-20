using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private string groundTag = "Ground";
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;

    private string accelTag = "accelerationPanel";
    private bool isAccel = false;
    private bool isAccelEnter, isAccelStay, isAccelExit;

    private string jumpTag = "jumpPanel";
    private bool isJump = false;
    private bool isJumpEnter, isJumpStay, isJumpExit;

    private string fireTag = "firePanel";
    private bool isFire = false;
    private bool isFireEnter, isFireStay, isFireExit;

    private string iceTag = "icePanel";
    private bool isIce = false;
    private bool isIceEnter, isIceStay, isIceExit;

    private string thunderTag = "thunderPanel";
    private bool isThunder = false;
    private bool isThunderEnter, isThunderStay, isThunderExit;

    private string sleepTag = "sleepPanel";
    private bool isSleep = false;
    private bool isSleepEnter, isSleepStay, isSleepExit;

    private string poisonTag = "poisonPanel";
    private bool isPoison = false;
    private bool isPoisonEnter, isPoisonStay, isPoisonExit;

    //接地判定を返すメソッド
    //物理判定の更新毎に呼ぶ必要がある
    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    public bool IsAccel()
    {
        if (isAccelEnter || isAccelStay)
        {
            isAccel = true;
        }
        //exitのみにするとtagが変わった瞬間を検知できない
        else if (isAccelExit || isGround)
        {
            isAccel = false;
        }
        
        isAccelEnter = false;
        isAccelStay = false;
        isAccelExit = false;
        return isAccel;
    }
    public bool IsJump()
    {
        if (isJumpEnter || isJumpStay)
        {
            isJump = true;
        }
        else if (isJumpExit)
        {
            isJump = false;
        }

        isJumpEnter = false;
        isJumpStay = false;
        isJumpExit = false;
        return isJump;
    }
    public bool IsFire()
    {
        if (isFireEnter || isFireStay)
        {
            isFire = true;
        }
        else if (isFireExit)
        {
            isFire = false;
        }

        isFireEnter = false;
        isFireStay = false;
        isFireExit = false;
        return isFire;
    }
    public bool IsThunder()
    {
        if (isThunderEnter || isThunderStay)
        {
            isThunder = true;
        }
        else if (isThunderExit)
        {
            isThunder = false;
        }

        isThunderEnter = false;
        isThunderStay = false;
        isThunderExit = false;
        return isThunder;
    }
    public bool IsIce()
    {
        if (isIceEnter || isIceStay)
        {
            isIce = true;
        }
        else if (isIceExit)
        {
            isIce = false;
        }

        isIceEnter = false;
        isIceStay = false;
        isIceExit = false;
        return isIce;
    }
    public bool IsSleep()
    {
        if (isSleepEnter || isSleepStay)
        {
            isSleep = true;
        }
        else if (isSleepExit)
        {
            isSleep = false;
        }

        isSleepEnter = false;
        isSleepStay = false;
        isSleepExit = false;
        return isSleep;
    }
    public bool IsPoison()
    {
        if (isPoisonEnter || isPoisonStay)
        {
            isPoison = true;
        }
        else if (isPoisonExit)
        {
            isPoison = false;
        }

        isPoisonEnter = false;
        isPoisonStay = false;
        isPoisonExit = false;
        return isPoison;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == groundTag || collision.tag == "move_floor")
        {
            isGroundEnter = true;
        }
        else if (collision.tag == accelTag)
        {
            isAccelEnter = true;
        }
        else if (collision.tag == jumpTag)
        {
            isJumpEnter = true;
        }
        else if (collision.tag == fireTag)
        {
            isFireEnter = true;
        }
        else if (collision.tag == thunderTag)
        {
            isThunderEnter = true;
        }
        else if (collision.tag == iceTag)
        {
            isIceEnter = true;
        }
        else if (collision.tag == sleepTag)
        {
            isSleepEnter = true;
        }
        else if (collision.tag == poisonTag)
        {
            isPoisonEnter = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        
        if (collision.tag == groundTag || collision.tag == "move_floor")
        {
            isGroundStay = true;
        }
        else if (collision.tag == accelTag)
        {
            isAccelStay = true;
        }
        else if (collision.tag == jumpTag)
        {
            isJumpStay = true;
        }
        else if (collision.tag == fireTag)
        {
            isFireStay = true;
        }
        else if (collision.tag == thunderTag)
        {
            isThunderStay = true;
        }
        else if (collision.tag == iceTag)
        {
            isIceStay = true;
        }
        else if (collision.tag == sleepTag)
        {
            isSleepStay = true;
        }
        else if (collision.tag == poisonTag)
        {
            isPoisonStay = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == groundTag||collision.tag=="move_floor")
        {
            isGroundExit = true;
        }
        else if (collision.tag == accelTag)
        {
            isAccelExit = true;
        }
        else if (collision.tag == jumpTag)
        {
            isJumpExit = true;
        }
        else if (collision.tag == fireTag)
        {
            isFireExit = true;
        }
        else if (collision.tag == thunderTag)
        {
            isThunderExit = true;
        }
        else if (collision.tag == iceTag)
        {
            isIceExit = true;
        }
        else if (collision.tag == sleepTag)
        {
            isSleepExit = true;
        }
        else if (collision.tag == poisonTag)
        {
            isPoisonExit = true;
        }
    }
}
