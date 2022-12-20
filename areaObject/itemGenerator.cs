using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class itemGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] seeds;
    [SerializeField] GameObject[] seedRare;
    [SerializeField] GameObject[] tempItems;
    [SerializeField] GameObject[] tempItemRare;
    [SerializeField] GameObject[] carrots;
    [SerializeField] GameObject[] carrotsRare;
    [SerializeField] GameObject[] enhanceBlock;
    public GameObject[] AllItem;
    Coroutine biasGenerate;
    public lighting _lighting;
    public bool debug = true;
    float timer,timerBlock,timerTemp = 0f;
    float cycle = 4f;
    float cycleTemp = 6f;
    float cycleCarrot = 4f;
    float cycleBlock = 3f;
    float cycleOrigin, cycleBlockOrigin;
    int randNum;
    public int biasNum = 0;
    bool biasCheck = false;
    float biasCycle = 0.5f;
    int[] generatePos = new int[] { -71, 321 };

    // Start is called before the first frame update
    void Start()
    {
        AllItem = seeds.Concat(seedRare).ToArray().Concat(tempItems).ToArray().Concat(tempItemRare).ToArray().Concat(carrots).ToArray().Concat(carrotsRare).ToArray().Concat(enhanceBlock).ToArray();
        if (!debug)
        {
            switch (itemSet.ItemAmount())
            {
                case 0:
                    cycle = 8f;
                    cycleBlock = 8f;
                    break;
                case 1:
                    cycle = 4f;
                    cycleBlock = 4f;
                    break;
                case 2:
                    cycle = 3f;
                    cycleBlock = 3f;
                    break;
                case 3:
                    cycle = 2f;
                    cycleBlock = 2f;
                    break;
            }
        }
        cycleOrigin = cycle;
        cycleBlockOrigin = cycleBlock;
        StartCoroutine(itemGenetate(cycle, seeds,1));
        StartCoroutine(itemGenetate(cycleTemp, tempItems,2));
        StartCoroutine(itemGenetate(cycleCarrot, carrots,2));
        StartCoroutine(itemGenetate(cycleBlock, enhanceBlock,3));
        StartCoroutine(itemRareGenetate(cycle * 2, seedRare));
        StartCoroutine(itemRareGenetate(cycleTemp * 2, tempItemRare));
        StartCoroutine(itemRareGenetate(cycleCarrot * 2, carrotsRare));
    }

    private void Update()
    {
        if (_lighting.isBias && !biasCheck)
        {
            biasCheck = true;
            biasNum = Random.Range(0, AllItem.Length);
            biasGenerate = StartCoroutine(itemBiasGenetate(biasCycle, AllItem));
        }
        else if (!_lighting.isBias && biasCheck)
        {
            biasCheck = false;
            StopCoroutine(biasGenerate);
        }
    }
    void tempItemGenerator()
    {
        timerTemp += Time.deltaTime;
        int counter = Random.Range(1, 3);
        if (timerTemp > cycleTemp)
        {
            timerTemp = 0;
            for (int i = 0; i <= counter; i++)
            {
                float posRand_x = Random.Range(generatePos[0], generatePos[1]);
                float posRand_y = Random.Range(generatePos[0], generatePos[1]);
                Instantiate(tempItems[Random.Range(0,tempItems.Length)], new Vector3(posRand_x, 50f, posRand_y), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            }
        }
    }
    //num部分は一度に生成する数の乱数に使用
    IEnumerator itemGenetate(float cycle, GameObject[] items,int num)
    {
        while (true)
        {
            //偏りイベントが発生していないとき
            if (!_lighting.isBias)
            {
                int counter = Random.Range(1, num);
                for (int i = 0; i <= counter; i++)
                {
                    float posRand_x = Random.Range(generatePos[0], generatePos[1]);
                    float posRand_y = Random.Range(generatePos[0], generatePos[1]);
                    Instantiate(items[Random.Range(0, items.Length)], new Vector3(posRand_x, 50f, posRand_y), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
                }
            }
            if (_lighting.isMass)
            {
                yield return new WaitForSeconds(cycle * 0.5f);
            }
            else
            {
                yield return new WaitForSeconds(cycle);
            }
        }
    }
    IEnumerator itemRareGenetate(float cycle, GameObject[] items)
    {
        while (true)
        {
            if (_lighting.isNight)
            {
                randNum = Random.Range(0, 20);
            }
            else
            {
                randNum = Random.Range(0, 50);
            }

            if (timerTemp > cycleTemp && 0 == randNum)
            {
                timerTemp = 0;
                float posRand_x = Random.Range(generatePos[0], generatePos[1]);
                float posRand_y = Random.Range(generatePos[0], generatePos[1]);
                Instantiate(items[0], new Vector3(posRand_x, 50f, posRand_y), Quaternion.Euler(0f, Random.Range(0, 360), 0f));

            }
            if (_lighting.isMass)
            {
                yield return new WaitForSeconds(cycle * 0.5f);
            }
            else
            {
                yield return new WaitForSeconds(cycle);
            }
        }
    }
    IEnumerator itemBiasGenetate(float cycle, GameObject[] items)
    {
        while (true)
        {
            timerTemp = 0;
            float posRand_x = Random.Range(generatePos[0], generatePos[1]);
            float posRand_y = Random.Range(generatePos[0], generatePos[1]);
            Instantiate(items[biasNum], new Vector3(posRand_x, 50f, posRand_y), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            if (_lighting.isMass)
            {
                yield return new WaitForSeconds(cycle * 0.5f);
            }
            else
            {
                yield return new WaitForSeconds(cycle);
            }
        }
    }
    void blockGenerator()
    {
        if (_lighting.isMass)
        {
            cycleBlock = cycleBlockOrigin * 0.25f;
        }
        else
        {
            cycleBlock = cycleBlockOrigin;
        }
        timerBlock += Time.deltaTime;
        int counter = Random.Range(0, 3);
        if (timerBlock > cycleBlock)
        {
            timerBlock = 0;
            for (int i = 0; i <= counter; i++)
            {
                float posRand_x = Random.Range(generatePos[0], generatePos[1]);
                float posRand_y = Random.Range(generatePos[0], generatePos[1]);
                Instantiate(enhanceBlock[0], new Vector3(posRand_x, 50f, posRand_y), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            }
        }
    }
}
