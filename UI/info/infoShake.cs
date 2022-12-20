using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoShake : MonoBehaviour
{

    bool multiple = false;
    bool changeCheck = true;
    int nowPos = 0;
    int _framePos = 0;
    float rotateChange = -1;
    [SerializeField] frameLine _frameLine;
    [SerializeField] GameObject[] info_shadow;
    Vector3 infoActiveSize;

    // Start is called before the first frame update
    void Start()
    {
        //スケールは子にするなどしてシーン内で統一する
        infoActiveSize = info_shadow[0].transform.localScale;
        //複数あるとき、移動設定
        if (info_shadow.Length > 1)
        {
            multiple = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (multiple)
        {
            //複数の時だけframeLineを使う
            _framePos = _frameLine.framePos;
            if (nowPos != _framePos)
            {
                changeCheck = true;
                //移動したときパラメータを初期化
                if(nowPos!=2&& nowPos != 3)
                {
                    info_shadow[nowPos].transform.eulerAngles = new Vector3(0, 0, 0);
                    info_shadow[nowPos].transform.localScale = infoActiveSize;
                }
                nowPos = _framePos;
            }
        }
        //移動したとき新しく起動
        if (changeCheck)
        {
            //端は動かさない、マジックナンバー修正する
            if(_framePos != 2 && _framePos != 3)
            {
                StartCoroutine(backMove(nowPos));
            }
            changeCheck = false;
        }
    }

    IEnumerator backMove(int pos)
    {
        while (pos== nowPos)
        {
            float randSize = Random.Range(infoActiveSize.x, infoActiveSize.x * 1.05f);
            rotateChange *= -1;
            info_shadow[pos].transform.eulerAngles = new Vector3(0, 0, Random.Range(0.5f, 1.25f) * rotateChange);
            info_shadow[pos].transform.localScale = new Vector3(randSize, randSize, 1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
