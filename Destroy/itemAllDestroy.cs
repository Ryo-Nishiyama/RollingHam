using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemAllDestroy : MonoBehaviour
{
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
            _render.material.color = new Color(1.0f, 0.9f, 0.2f, alpha_Sin);
            _render.material.SetColor("_EmissionColor", Color.Lerp(new Color(0.85f, 0.85f, 0.35f), new Color(0f, 0f, 0f), alpha_Sin));
        }
    }
}
