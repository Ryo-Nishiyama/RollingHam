using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healDeath : MonoBehaviour
{
    float deathTimer = 0;

    // Update is called once per frame
    void Update()
    {
        //�o����1.5�b�o�߂ŏ���
        deathTimer += Time.deltaTime;
        if (deathTimer > 1.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
