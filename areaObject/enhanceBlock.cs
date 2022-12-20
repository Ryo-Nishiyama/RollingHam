using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enhanceBlock : MonoBehaviour
{
    public float rockHP = 15.0f;
    AudioSource _AudioSource;
    AudioClip a_explosion;
    float rockHP_first;
    float liveTime = 0;
    float alpha_Sin;
    float desTimer = 0f;
    List<GameObject> itemList = new List<GameObject>();
    GameObject _itemGene;
    GameObject exeffect;
    Rigidbody rb;
    Renderer _render;
    Material originalMaterial;
    [SerializeField] bool deathCheck = true;
    [SerializeField] bool instantiateCheck = true;

    // Start is called before the first frame update
    void Start()
    {
        
        _AudioSource = GetComponent<AudioSource>();
        a_explosion = (AudioClip)Resources.Load("SE\\block_break");
        itemList.Add((GameObject)Resources.Load("items\\seed0"));
        itemList.Add((GameObject)Resources.Load("items\\seed1"));
        itemList.Add((GameObject)Resources.Load("items\\seed2"));
        itemList.Add((GameObject)Resources.Load("items\\seed3"));
        itemList.Add((GameObject)Resources.Load("items\\seed4"));
        itemList.Add((GameObject)Resources.Load("items\\seed_Damage"));
        //resousの爆破オブジェクト読み込み
        exeffect = (GameObject)Resources.Load("DustExplosion");
        rockHP_first = rockHP;
        
        _render = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rockHP <= 0.0f)
        {
            AudioSource.PlayClipAtPoint(a_explosion,this.transform.position);
            Instantiate(exeffect, this.transform.localPosition, Quaternion.identity);
            if (instantiateCheck)
            {
                blockBreak();
            }
            Destroy(this.gameObject);
        }
        changeTexture();
        if (deathCheck)
        {
            deathBlock();
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //突進の威力強化
            float rockDamage = Mathf.Abs(collision.relativeVelocity.x) + Mathf.Abs(collision.relativeVelocity.z);
            rockHP -= rockDamage * 1.2f;
        }
        else if (collision.gameObject.tag == "mainAttack")
        {
            rockHP -= 50;
        }
        else if (collision.gameObject.tag == "despone")
        {
            Destroy(this.gameObject);
        }
    }
    void changeTexture()
    {
        if (rockHP <= rockHP_first / 3)
        {
            _render.material = (Material)Resources.Load("rock\\rock_small_3");
        }
        else if (rockHP <= rockHP_first / 1.5)
        {
            _render.material = (Material)Resources.Load("rock\\rock_small_2");
        }

    }
    void blockBreak()
    {
        int generate = Random.Range(0, 6);
        int generateCounter = 0;
        do
        {
            Vector3 pos = new Vector3(0.0f, 2.0f, 0.0f) + this.transform.position;
            _itemGene = Instantiate(itemList[generate], pos, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            rb = _itemGene.GetComponent<Rigidbody>();
            float randXpow = Random.Range(-20.0f, 20.0f);
            float randYpow = Random.Range(0f, 40.0f);
            float randZpow = Random.Range(-20.0f, 20.0f);
            rb.AddForce(new Vector3(randXpow, randYpow, randZpow));
            generateCounter += 1;
            generate = Random.Range(0, 12);
        }
        //最大4つ生成
        while (generate < 6&&generateCounter<4);
    }
    void deathBlock()
    {
        liveTime += Time.deltaTime;
        //20sで消滅
        if (liveTime > 20)
        {
            Destroy(this.gameObject);
        }
        if (liveTime > 20 * 0.7f)
        {
            //半透明点滅
            alpha_Sin = (Mathf.Sin(Time.time * 6) / 4 + 0.75f) * 255;
            Color32 blockColor = _render.material.color;
            _render.material.color = new Color32(blockColor.r, blockColor.g, blockColor.b, (byte)alpha_Sin);
        }
        
    }
    void hitEffectInst(float damage, Collision collision)
    {
        GameObject localEffect = exeffect;
        //ダメージでエフェクトサイズを変動
        localEffect.transform.localScale = new Vector3(Random.Range(damage / 100, damage / 50), Random.Range(damage / 50, damage / 25), Random.Range(damage / 50, damage / 25));
        foreach (ContactPoint contact in collision.contacts)
        {
            Instantiate(localEffect, contact.point, Quaternion.identity);
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
