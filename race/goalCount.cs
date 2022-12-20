using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalCount : MonoBehaviour
{
    [SerializeField] bool[] checkPoints = new bool[] { false, false, false, false };
    public bool goal = false;
    public float goalTime = 0.00001f;

    // Update is called once per frame
    void Update()
    {
        if (countdown.startGameCheck)
        {
            goalCheck();
        }
    }
    void goalCheck()
    {
        if (!goal)
        {
            goalTime += Time.deltaTime;
            if (goalTime > 5999.999f)
            {
                goalTime = 5999.999f;
            }
        }
        for(int i = 0; i < checkPoints.Length; i++)
        {
            if (checkPoints[i])
            {
                goal = true;
            }
            else
            {
                goal = false;
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "checkPoint1":
                checkPoints[0] = true;
                break;
            case "checkPoint2":
                //‹t‘–‚É‚æ‚éƒS[ƒ‹‚ð–hŽ~
                if (checkPoints[0])
                {
                    checkPoints[1] = true;
                }
                break;
            case "checkPoint3":
                if (checkPoints[0] && checkPoints[1])
                {
                    checkPoints[2] = true;
                }
                break;
            case "goal":
                if (checkPoints[0] && checkPoints[1] && checkPoints[2])
                {
                    checkPoints[3] = true;
                }
                break;
        }
    }
}
