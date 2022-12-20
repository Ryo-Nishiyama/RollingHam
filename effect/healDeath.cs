using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healDeath : MonoBehaviour
{
    float deathTimer = 0;

    // Update is called once per frame
    void Update()
    {
        //oŒ»Œã1.5•bŒo‰ß‚ÅÁ–Å
        deathTimer += Time.deltaTime;
        if (deathTimer > 1.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
