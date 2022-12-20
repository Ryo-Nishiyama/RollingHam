using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enhanceRock : MonoBehaviour
{
    //岩破壊成功でアイテム飛び散る
    //ダメージはぶつかってきたplayerの速度と同じ
    public float rockHP = 0.0f;
    AudioSource _AudioSource;
    AudioClip a_explosion;
    [SerializeField] bool enhance = true;
    [SerializeField] bool isDrop = true;
    float rockHP_first;
    GameObject item1, item2, item3, item4, item5, itemAll;
    GameObject _itemGene;
    GameObject exeffect,hitEffect;
    GameObject explosionSE_obj;
    Rigidbody rb;
    Renderer render;
    Material originalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        item1 = (GameObject)Resources.Load("items\\seed0");
        item2 = (GameObject)Resources.Load("items\\seed1");
        item3 = (GameObject)Resources.Load("items\\seed2");
        item4 = (GameObject)Resources.Load("items\\seed3");
        item5 = (GameObject)Resources.Load("items\\seed4");
        itemAll = (GameObject)Resources.Load("items\\seedAll");

        //resousの爆破オブジェクト読み込み
        _AudioSource = GetComponent<AudioSource>();
        a_explosion = (AudioClip)Resources.Load("SE\\se_explode");
        exeffect = (GameObject)Resources.Load("rock_BigExplosionEffect");
        hitEffect = (GameObject)Resources.Load("DustExplosion");
        explosionSE_obj = (GameObject)Resources.Load("explosionSE_obj");
        rockHP_first = rockHP;

        render = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrop)
        {
            rockPos();
        }
        if (rockHP <= 0.0f)
        {
            if (enhance)
            {
                rockBreak(item1);
                rockBreak(item2);
                rockBreak(item3);
                rockBreak(item4);
                rockBreak(item5);
                rockBreakAll(itemAll);
            }
            //音源再生用のオブジェクトを生成する
            GameObject exSE= Instantiate(explosionSE_obj, this.transform.position, Quaternion.identity);
            _AudioSource = exSE.GetComponent<AudioSource>();
            _AudioSource.PlayOneShot(a_explosion);
            Instantiate(exeffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        changeTexture();
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "player")
        {
            //突進の威力強化
            float rockDamage = Mathf.Abs(collision.relativeVelocity.x) + Mathf.Abs(collision.relativeVelocity.z);
            rockHP -= rockDamage * 1.2f;
            hitEffectInst(rockDamage * 1.2f, collision);
        }
        else if (collision.gameObject.tag == "mainAttack")
        {
            hitEffectInst(50, collision);
            //威力50で固定
            rockHP -= 50;
        }
    }
    void changeTexture()
    {
        if (rockHP <= rockHP_first / 3)
        {
            render.material = (Material)Resources.Load("rock\\rock_big_4")
        }
        else if (rockHP <= rockHP_first / 2)
        {
            render.material = (Material)Resources.Load("rock\\rock_big_3");
        }
        else if (rockHP <= rockHP_first / 1.5)
        {
            render.material = (Material)Resources.Load("rock\\rock_big_2");
        }

    }
    void hitEffectInst(float damage, Collision collision)
    {
        GameObject localEffect = hitEffect;
        //ダメージでエフェクトサイズを変動
        localEffect.transform.localScale = new Vector3(Random.Range(damage/50, damage/25), Random.Range(damage / 50, damage / 25), Random.Range(damage / 50, damage / 25));
        foreach (ContactPoint contact in collision.contacts)
        {
            Instantiate(localEffect, contact.point, Quaternion.identity);
        }
    }
    void rockBreak(GameObject _item)
    {
        int generate = Random.Range(2, 6 + 1);
        for (int i = 1; i < generate; i++)
        {
            float xpos = Random.Range(-0.5f, 0.5f);
            Vector3 pos = new Vector3(xpos, 5.0f, 0.0f) + this.transform.position;
            _itemGene = Instantiate(_item, pos, Quaternion.Euler(0f, Random.Range(0,360), 0f));
            rb = _itemGene.GetComponent<Rigidbody>();
            float randXpow = Random.Range(-150.0f, 150.0f);
            float randYpow = Random.Range(0f, 300.0f);
            float randZpow = Random.Range(-150.0f, 150.0f);
            rb.AddForce(new Vector3(randXpow, randYpow, randZpow));
        }
    }
    void rockBreakAll(GameObject _item)
    {
        //二分の一でall出現
        int generate = Random.Range(0, 2);
        if (generate == 0)
        {
            float xpos = Random.Range(-0.5f, 0.5f);
            Vector3 pos = new Vector3(xpos, 5.0f, 0.0f) + this.transform.localPosition;
            _itemGene = Instantiate(_item, pos, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            rb = _itemGene.GetComponent<Rigidbody>();
            float randXpow = Random.Range(-150.0f, 150.0f);
            float randYpow = Random.Range(0f, 300.0f);
            float randZpow = Random.Range(-150.0f, 150.0f);
            rb.AddForce(new Vector3(randXpow, randYpow, randZpow));
        }
    }
    void rockPos()
    {
        //落下が終了したとき位置と回転を止める
        if (this.GetComponent<Rigidbody>().velocity.y == 0)
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }
}
