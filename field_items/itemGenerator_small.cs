using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class itemGenerator_small : MonoBehaviour
{
    [SerializeField] lighting _lighting;
    [SerializeField] itemGenerator _itemGenerator;
    [SerializeField] GameObject[] items;
    [SerializeField] GameObject[] badItems;
    [SerializeField] GameObject[] rareItems;
    [SerializeField] GameObject[] rocks;
    Coroutine biasGenerate;
    [SerializeField] float cycle;
    [SerializeField] float badCycle;
    [SerializeField] float rareCycle;
    [SerializeField] float rockCycle;
    float[] cycleOriginalList = new float[4];
    bool biasCheck = false;
    int biasNum = 0;
    [SerializeField] Vector3 generateScope;

    // Start is called before the first frame update
    void Start()
    {
        //item��1�ȏ゠��Ƃ�
        if (items.Length > 0)
        {
            StartCoroutine(itemGenerate(cycle, items, 1));
        }
        //badItem��1�ȏ゠��Ƃ�
        if (badItems.Length > 0)
        {
            StartCoroutine(itemGenerate(badCycle, badItems, 1));
        }
        //rareItem��1�ȏ゠��Ƃ�
        if (rareItems.Length > 0)
        {
            StartCoroutine(itemGenerate(rareCycle, rareItems, 1));
            StartCoroutine(itemGenerateNight(rareCycle, rareItems, 1));
        }
        if (rocks.Length > 0)
        {
            StartCoroutine(itemGenerate(rockCycle, rocks, 1));
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (_lighting.isBias)
        {
            //�΂�A�C�e�������낦��
            biasNum = _itemGenerator.biasNum;
            if (!biasCheck)
            {
                biasCheck = true;
                biasGenerate = StartCoroutine(itemGenerateBias(rockCycle * 0.5f, _itemGenerator.AllItem, 1));
            }
        }
        else if(!_lighting.isBias && biasCheck)
        {
            biasCheck = false;
            StopCoroutine(biasGenerate);
        }
    }
    //num�����͈�x�ɐ������鐔�̗����Ɏg�p
    IEnumerator itemGenerate(float cycle, GameObject[] items, int num)
    {
        while (true)
        {
            //�΂肪�������Ă��Ȃ��Ƃ�
            if (!_lighting.isBias)
            {
                int counter = Random.Range(1, num);
                for (int i = 0; i <= counter; i++)
                {
                    float posRand_x = Random.Range(this.transform.position.x, this.transform.position.x + generateScope.x);
                    float posRand_z = Random.Range(this.transform.position.z, this.transform.position.z + generateScope.z);
                    Instantiate(items[Random.Range(0, items.Length)], new Vector3(posRand_x, this.transform.position.y, posRand_z), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
                }
            }
            //��ʃC�x���g�̎������T�C�N��2�{
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
    IEnumerator itemGenerateNight(float cycle, GameObject[] items, int num)
    {
        while (true)
        {
            //�郌�A�A�C�e���̔�����2�{
            if (!_lighting.isNight)
            {
                int counter = Random.Range(1, num);
                for (int i = 0; i <= counter; i++)
                {
                    float posRand_x = Random.Range(this.transform.position.x, this.transform.position.x + generateScope.x);
                    float posRand_z = Random.Range(this.transform.position.z, this.transform.position.z + generateScope.z);
                    Instantiate(items[Random.Range(0, items.Length)], new Vector3(posRand_x, this.transform.position.y, posRand_z), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
                }
            }
            yield return new WaitForSeconds(cycle);
        }
    }
    IEnumerator itemGenerateBias(float cycle, GameObject[] items, int num)
    {
        while (true)
        {
            int counter = Random.Range(1, num);
            for (int i = 0; i <= counter; i++)
            {
                float posRand_x = Random.Range(this.transform.position.x, this.transform.position.x + generateScope.x);
                float posRand_z = Random.Range(this.transform.position.z, this.transform.position.z + generateScope.z);
                Instantiate(items[biasNum], new Vector3(posRand_x, this.transform.position.y, posRand_z), Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            }
            yield return new WaitForSeconds(cycle);
        }
    }
}
