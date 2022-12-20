using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randItemSet : MonoBehaviour
{
    [SerializeField] GameObject[] blocks;
    [SerializeField] Vector3 generateScope;
    [SerializeField] bool mountain,snow;
    [SerializeField] int generateNumMin;
    [SerializeField] int generateNumMax;
    // Start is called before the first frame update
    void Start()
    {
        generateRock(generateNumMin, generateNumMax);
    }

    void generateRock(int numMin,int numMax)
    {
        int counter = Random.Range(numMin, numMax);
        for (int i = 0; i <= counter; i++)
        {
            float posRand_x = Random.Range(this.transform.position.x, this.transform.position.x + generateScope.x);
            float posRand_z = Random.Range(this.transform.position.z, this.transform.position.z + generateScope.z);
            
            //�����ꏊ���R�̋��ɂȂ�����ēx���I
            if (mountain)
            {
                while ((posRand_x- this.transform.position.x > 40 && posRand_x- this.transform.position.x < 102) && (posRand_z- this.transform.position.z > 47 && posRand_z- this.transform.position.z < 102))
                {
                    posRand_x = Random.Range(this.transform.position.x, this.transform.position.x + generateScope.x);
                    posRand_z = Random.Range(this.transform.position.z, this.transform.position.z + generateScope.z);
                }
            }
            else if (snow)
            {
                while ((posRand_x- this.transform.position.x > 58 && posRand_x- this.transform.position.x < 118) && (posRand_z- this.transform.position.z > 120 && posRand_z- this.transform.position.x < 180))
                {
                    posRand_x = Random.Range(this.transform.position.x, this.transform.position.x + generateScope.x);
                    posRand_z = Random.Range(this.transform.position.z, this.transform.position.z + generateScope.z);
                }
            }
            GameObject _block = blocks[Random.Range(0, blocks.Length)];
            //�T�C�Y�������_���ɕω�
            _block.transform.localScale = new Vector3(Random.Range(0.8f, 2.0f), Random.Range(0.8f, 2.0f), Random.Range(0.8f, 2.0f));
            //�p�x�������_���ɔz�u
            Instantiate(_block, new Vector3(posRand_x, this.transform.position.y, posRand_z), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        }
    }
}
