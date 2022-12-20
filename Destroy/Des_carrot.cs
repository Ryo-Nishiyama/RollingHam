using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Des_carrot : MonoBehaviour
{
    Renderer _Renderer_root, _Renderer_leaf;
    float alpha_Sin;
    float desTimer_set = 25f;
    Color32 originalColor_root, originalColor_leaf;
    // Start is called before the first frame update
    void Start()
    {
        _Renderer_root = transform.GetChild(0).gameObject.GetComponent<Renderer>();
        _Renderer_leaf = transform.GetChild(1).gameObject.GetComponent<Renderer>();
        originalColor_root = _Renderer_root.material.color;
        originalColor_leaf = _Renderer_leaf.material.color;
        StartCoroutine(flashing());
        StartCoroutine(destroySeed());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator flashing()
    {
        yield return new WaitForSeconds(desTimer_set * 0.8f);
        //è¡ñ≈Ç∑ÇÈÇ‹Ç≈ì_ñ≈Ç≥ÇπÇÈ
        while (true)
        {
            alpha_Sin = Mathf.Sin(Time.time * 6) / 4 + 0.5f;
            _Renderer_root.material.color = new Color32((byte)(_Renderer_root.material.color.r*255), (byte)(_Renderer_root.material.color.g*255), (byte)(_Renderer_root.material.color.b*255), (byte)(alpha_Sin * 255));
            _Renderer_leaf.material.color = new Color32((byte)(_Renderer_leaf.material.color.r * 255), (byte)(_Renderer_leaf.material.color.g * 255), (byte)(_Renderer_leaf.material.color.b * 255), (byte)(alpha_Sin * 255));
            yield return null;
        }
    }

    IEnumerator destroySeed()
    {
        yield return new WaitForSeconds(desTimer_set);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "despone")
        {
            Destroy(this.gameObject);
        }
    }
}
