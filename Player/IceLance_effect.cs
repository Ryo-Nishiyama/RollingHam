using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLance_effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //0.5s‚Å©g‚ğ”jŠü
        StartCoroutine(remove());
    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
