using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainAttackControl : MonoBehaviour
{
    public float deathCount = 0f;
    Rigidbody rb;
    Collider mycollider;
    //Vector3 mainVel;

    public int itemEnhance, itemEnhance2, itemEnhance3, itemEnhance4, itemEnhance5, itemEnhanceAll = 0;
    public int[] tempEnhance = new int[] { 0, 0, 0, 0, 0, 0 };
    public bool elseEnter = false;
    public bool isItemDamageEnter, isEnhanceRockEnter = false;
    bool hit = false;
    List<GameObject> enhanceParticle = new List<GameObject>();
    List<GameObject> tempParticle = new List<GameObject>();
    GameObject shield;
    GameObject DustExplosion;
    GameObject enhanceParticle_speed, enhanceParticle_brake, enhanceParticle_charge, enhanceParticle_turning, enhanceParticle_accel, enhanceParticle_all;
    Vector3 enhanceParticleAngle = new Vector3(-90f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        mycollider = this.GetComponent<Collider>();
        shield = (GameObject)Resources.Load("particle\\shield_Eff");
        DustExplosion = (GameObject)Resources.Load("DustExplosion");
        DustExplosion.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_speed"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_brake"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_turning"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_charge"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_accel"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_all"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_speed"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_brake"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_turning"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_charge"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_accel"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_all"));
    }

    // Update is called once per frame
    void Update()
    {
        deathCount += Time.deltaTime;
        //加速しすぎた時は減速,最高速と同期させていいか
        rb.AddForce(transform.right * ((150 - rb.velocity.magnitude) * 150.0f), ForceMode.Force);
    }
    private void OnTriggerEnter(Collider collision)
    {
        //取得判定は次のフレームで行われるため取得後にアイテムを吹き飛ばさないためにtriggerをtrueにする
        if (collision.tag == "itemEnhance" && !hit)
        {
            hit = true;
            itemEnhance += 1;
            mycollider.isTrigger = true;
            Instantiate(enhanceParticle[0], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
            
        }
        else if (collision.tag == "itemEnhance2" && !hit)
        {
            hit = true;
            itemEnhance2 += 1;
            mycollider.isTrigger = true;
            Instantiate(enhanceParticle[1], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "itemEnhance3" && !hit)
        {
            hit = true;
            itemEnhance3 += 1;
            mycollider.isTrigger = true;
            Instantiate(enhanceParticle[2], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "itemEnhance4" && !hit)
        {
            hit = true;
            itemEnhance4 += 1;
            mycollider.isTrigger = true;
            Instantiate(enhanceParticle[3], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "itemEnhance5" && !hit)
        {
            hit = true;
            itemEnhance5 += 1;
            mycollider.isTrigger = true;
            Instantiate(enhanceParticle[4], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "itemAll" && !hit)
        {
            hit = true;
            itemEnhanceAll += 1;
            mycollider.isTrigger = true;
            Instantiate(enhanceParticle[5], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "itemDamage" && !hit)
        {
            isItemDamageEnter = true;
            mycollider.isTrigger = true;
            hit = true;
            
            Instantiate(DustExplosion, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "tempEnhance1" && !hit)
        {
            Debug.Log(collision.tag);
            hit = true;
            tempEnhance[0] += 1;
            mycollider.isTrigger = true;
            Instantiate(tempParticle[0], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);

        }
        else if (collision.tag == "tempEnhance2" && !hit)
        {
            hit = true;
            tempEnhance[1] += 1;
            mycollider.isTrigger = true;
            Instantiate(tempParticle[1], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "tempEnhance3" && !hit)
        {
            hit = true;
            tempEnhance[2] += 1;
            mycollider.isTrigger = true;
            Instantiate(tempParticle[2], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "tempEnhance4" && !hit)
        {
            hit = true;
            tempEnhance[3] += 1;
            mycollider.isTrigger = true;
            Instantiate(tempParticle[3], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "tempEnhance5" && !hit)
        {
            hit = true;
            tempEnhance[4] += 1;
            mycollider.isTrigger = true;
            Instantiate(tempParticle[4], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "tempEnhanceAll" && !hit)
        {
            hit = true;
            tempEnhance[5] += 1;
            mycollider.isTrigger = true;
            Instantiate(tempParticle[5], collision.transform.position, Quaternion.Euler(enhanceParticleAngle));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "shield" && !hit)
        {
            Vector3 hitPoint = collision.ClosestPointOnBounds(this.transform.position);
            Vector3 hitEuler = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y + 90, this.transform.eulerAngles.z);
            Instantiate(shield, hitPoint, Quaternion.Euler(hitEuler));
            elseEnter = true;
        }
        //その他オブジェクトに接触したとき消滅させる
        else
        {
            elseEnter = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enhanceRock" && !hit)
        {
            hit = true;
            mycollider.isTrigger = true;
            isEnhanceRockEnter = true;
        }
        else
        {
            elseEnter = true;
        }
    }
}
