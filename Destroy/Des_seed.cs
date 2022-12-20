using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Des_seed : MonoBehaviour
{
    Renderer _Renderer;
    float alpha_Sin;
    [SerializeField] float desTimer_set = 20f;
    Color32 originalColor;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        _Renderer = GetComponent<Renderer>();
        originalColor = _Renderer.material.color;
        StartCoroutine(flashing());
        StartCoroutine(destroySeed());
        rb = GetComponent<Rigidbody>();
    }
    IEnumerator flashing()
    {
        //�c��5�b�œ_�ŃX�^�[�g
        yield return new WaitForSeconds(desTimer_set-5f);
        //���ł���܂œ_�ł�����
        while (true)
        {
            alpha_Sin = Mathf.Sin(Time.time * 6) / 4 + 0.5f;
            _Renderer.material.color = new Color32(originalColor.r, originalColor.g, originalColor.b, (byte)(alpha_Sin * 255));
            yield return null;
        }
    }

    IEnumerator destroySeed()
    {
        yield return new WaitForSeconds(desTimer_set);
        Destroy(this.gameObject);
    }
    //�͈͊O�ɐ������ꂽ�Ƃ����ł�����
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "despone")
        {
            Destroy(this.gameObject);
        }
    }
}
