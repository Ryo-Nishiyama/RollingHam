using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healDeath : MonoBehaviour
{
    float deathTimer = 0;

    // Update is called once per frame
    void Update()
    {
        //出現後1.5秒経過で消滅
        deathTimer += Time.deltaTime;
        if (deathTimer > 1.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
