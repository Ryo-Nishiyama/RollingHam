using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunflowerGenerator : MonoBehaviour
{
    [SerializeField] GameObject sunflower;

    List<Vector3> sponPosList = new List<Vector3> {new Vector3(7,1.25f,176.5f),new Vector3(-7.5f,55,4),new Vector3(38.5f,44,169),
                                        new Vector3(150.5f,-23.5f,255), new Vector3(215,5,276.5f), new Vector3(266.5f,30,27.5f),
                                        new Vector3(300,-15.5f,-40), new Vector3(-68,9.25f,-70),new Vector3(312,50.25f,312.5f) };
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            //ランダムで5個まで座標を選ぶ
            int randNum = Random.Range(0, sponPosList.Count);
            Instantiate(sunflower, sponPosList[randNum], Quaternion.Euler(sunflower.transform.localEulerAngles));
            //選んだ座標を削除
            sponPosList.RemoveAt(randNum);
        }
    }
}
