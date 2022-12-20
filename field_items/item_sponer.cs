using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_sponer : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    //�������鐔
    [SerializeField] int sizeX, sizeZ;
    int sizeRandX, sizeRandZ;
    //�A�C�e���̊Ԋu
    [SerializeField] float marginX, marginZ;
    //�p�����邩
    [SerializeField] bool roop = false;
    //������鎞��
    [SerializeField] float cycleTime = 10;
    //�������������_���ɂ��邩
    [SerializeField] bool randCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        if (randCheck)
        {
            sizeRandX = Random.Range(1, sizeX+1);
            sizeRandZ = Random.Range(1, sizeZ+1);
        }
        else
        {
            sizeRandX = sizeX;
            sizeRandZ = sizeZ;
        }
        
        for (int i = 0; i < sizeRandX; i++)
        {
            for(int j = 0; j < sizeRandZ; j++)
            {
                int randNum = Random.Range(0, items.Length);
                Vector3 sponPos = new Vector3(this.transform.position.x + i * marginX, this.transform.position.y, this.transform.position.z + j * marginZ);
                Instantiate(items[randNum], sponPos, Quaternion.Euler(items[randNum].transform.eulerAngles));
            }
        }
        if (roop)
        {
            StartCoroutine(roopSpon(cycleTime));
        }
    }
    IEnumerator roopSpon(float cycle)
    {
        while (true)
        {
            if (randCheck)
            {
                sizeRandX = Random.Range(1, sizeX+1);
                sizeRandZ = Random.Range(1, sizeZ+1);
            }
            yield return new WaitForSeconds(cycle);
            for (int i = 0; i < sizeRandX; i++)
            {
                for (int j = 0; j < sizeRandZ; j++)
                {
                    int randNum = Random.Range(0, items.Length);
                    Vector3 sponPos = new Vector3(this.transform.position.x + i * marginX, this.transform.position.y, this.transform.position.z + j * marginZ);
                    Instantiate(items[randNum], sponPos, Quaternion.Euler(items[randNum].transform.eulerAngles));
                }
            }
        }
        
    }
}
