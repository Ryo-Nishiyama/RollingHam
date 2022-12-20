using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnhanceDestroy3 : MonoBehaviour
{
    bool isItemEnhance = false;

    float alpha_Sin;
    float desTimer = 0f;
    public float desTimer_set = 20f;

    Renderer _render;
    // Start is called before the first frame update
    void Start()
    {
        _render = gameObject.GetComponent<Renderer>();
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
        if (desTimer > desTimer_set * 0.7)
        {
            alpha_Sin = Mathf.Sin(Time.time * 6) / 2 + 0.5f;
            _render.material.color = new Color(0.6f, 1.0f, 0.5f, alpha_Sin);
        }
    }
}
