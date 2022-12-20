using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackShield : MonoBehaviour
{
    GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        shield = (GameObject)Resources.Load("shield_Eff");
    }
}
