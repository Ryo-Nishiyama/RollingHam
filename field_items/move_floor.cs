using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_floor : MonoBehaviour
{
    float timer = 0;
    int reverse = 1;
    [SerializeField] float cycle = 5;
    [SerializeField] Vector3 movePos = new Vector3(4, 0, 4);
    Vector3 defaultPos;

    // Start is called before the first frame update
    void Start()
    {
        //初期位置を保存
        defaultPos = this.transform.position;
        //相対距離に変更
        movePos += defaultPos;
    }

    // Update is called once per frame
    void Update()
    {
        //cycle分遅くする
        timer = Mathf.Sin(Time.time / cycle) / 2 + 0.5f;
        transform.position = Vector3.Lerp(defaultPos, movePos, timer);
    }
}
