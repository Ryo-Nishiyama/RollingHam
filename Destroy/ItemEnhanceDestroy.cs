using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnhanceDestroy : MonoBehaviour
{
    bool isItemEnhance = false;
    float alpha_Sin;
    float desTimer = 0f;
    public float desTimer_set=20f;
    GameObject enhanceParticle;

    Renderer _render;
    Color _color;
    // Start is called before the first frame update
    void Start()
    {
        _render = gameObject.GetComponent<Renderer>();
        enhanceParticle = (GameObject)Resources.Load("particle\\enhanceParticle");
    }

    // Update is called once per frame
    void Update()
    {
        itemEnhanceDestroy();
    }
    void itemEnhanceDestroy()
    {
        desTimer += Time.deltaTime;
        if (desTimer > desTimer_set)
        {
            
            Destroy(this.gameObject);
        }
        //�A�C�e���̎������c��30%�܂Ō��������Ƃ��_�ŊJ�n
        if (desTimer > desTimer_set * 0.7)
        {
            //Time.time��6�b��1��
            alpha_Sin = Mathf.Sin(Time.time*6) / 2 + 0.5f;
            _render.material.color = new Color(0.55f, 0.8f, 0.9f, alpha_Sin);
        }
    }
}
