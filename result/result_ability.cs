using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class result_ability : MonoBehaviour
{
    public GameObject[] skillSet;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < selectBallSet.selectAbiList.Length; i++)
        {
            skillSet[selectBallSet.selectAbiList[i]].SetActive(true);
            Vector3 abilityPos = skillSet[selectBallSet.selectAbiList[i]].transform.localPosition;
            skillSet[selectBallSet.selectAbiList[i]].transform.localPosition = new Vector3(abilityPos.x + i * 200, abilityPos.y, abilityPos.z);
        }

    }
}
