using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainDrop : MonoBehaviour
{
    float x = 0.1f;
    float randValue;
    int randNum;
    int randRot;
    float acceleration = -0.02f;
    float speed = 75.0f;
    float pulmai;

    Vector2 pos;
    Vector2 defopos;

    // Start is called before the first frame update
    void Start()
    {
        randValue = Random.Range(1,5);
        randNum = Random.Range(0, 2);
        randRot = Random.Range(0, 2);
        if (randRot == 1)
        {
            pulmai = 1.0f;
        }
        else
        {
            pulmai = -1.0f;
        }
        defopos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        //指定した方向にゆっくり回転する場合
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 45f*pulmai), step);

        pos = this.transform.position;
        if (pos.x - defopos.x < randValue * x & randNum == 1)
        {
            //Space.Worldで移動の回転軸をワールド座標に合わせる
            this.transform.Translate((randValue * x) / 50, -0.01f, 0, Space.World);
        }
        else if(pos.x - defopos.x > randValue * x * -1 & randNum == 0)
        {
            this.transform.Translate((randValue * x) / -50 , -0.01f, 0, Space.World);
        }
        else
        {
            this.transform.Translate(0, acceleration, 0, Space.World);
        }
        acceleration *= 1.005f;
    }
}
