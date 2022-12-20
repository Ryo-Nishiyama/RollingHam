using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopwatch : MonoBehaviour
{
    List<GameObject> parts = new List<GameObject>();
    List<Renderer> renderers = new List<Renderer>();
    List<Color32> color32s = new List<Color32>();
    GameObject gcd;

    float deathTimer = 0;
    float rotateTimer = 0;

    [SerializeField] bool deathCheck = true;
    // Start is called before the first frame update
    void Start()
    {
        //それぞれのオブジェクトの変化要素を取得
        for(int i = 0; i < 4; i++)
        {
            parts.Add(this.transform.GetChild(i).gameObject);
            renderers.Add(parts[i].GetComponent<Renderer>());
            color32s.Add(renderers[i].material.color);
        }
        gcd = this.transform.GetChild(4).gameObject;
        //ひ孫の取得
        parts.Add(gcd.GetComponent<Transform>().transform.GetChild(0).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject);
        parts.Add(gcd.GetComponent<Transform>().transform.GetChild(1).gameObject.GetComponent<Transform>().transform.GetChild(0).gameObject);
        renderers.Add(parts[4].GetComponent<Renderer>());
        color32s.Add(renderers[4].material.color);
        renderers.Add(parts[5].GetComponent<Renderer>());
        color32s.Add(renderers[5].material.color);
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトを回転させる
        rotateTimer += Time.deltaTime*30;
        this.transform.localEulerAngles = new Vector3(0, rotateTimer, 0);
        if (rotateTimer > 360)
        {
            rotateTimer = 0;
        }
        if (deathCheck)
        {
            //オブジェクトの破棄
            deathTimer += Time.deltaTime;
            if (deathTimer > 20)
            {
                //親を消滅
                Destroy(this.gameObject);
            }
            if (deathTimer > 20 * 0.7f)
            {
                float alpha_Sin = (Mathf.Sin(Time.time * 6) / 2 + 0.5f) * 255;
                //まとめて点滅
                for (int i = 0; i < parts.Count; i++)
                {
                    renderers[i].material.color = new Color32(color32s[i].r, color32s[i].g, color32s[i].b, (byte)alpha_Sin);
                }
            }
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "despone")
        {
            Destroy(this.gameObject);
        }
    }
}
