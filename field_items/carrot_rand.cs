using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrot_rand : MonoBehaviour
{
    Renderer _Renderer_root, _Renderer_leaf;
    float emissionH = 1;
    // Start is called before the first frame update
    void Start()
    {
        _Renderer_root = transform.GetChild(0).gameObject.GetComponent<Renderer>();
        _Renderer_leaf = transform.GetChild(1).gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        emissionH += Time.deltaTime*0.25f;
        if (emissionH > 1)
        {
            emissionH = 0;
        }
        _Renderer_root.material.SetColor("_EmissionColor", Color.HSVToRGB(emissionH, 1, 0.7f));
        _Renderer_leaf.material.SetColor("_EmissionColor", Color.HSVToRGB(emissionH, 1, 0.7f));
    }
}
