using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceLanceGenerator : MonoBehaviour
{
    public GameObject iceLance;
    [SerializeField] float cycle;
    [SerializeField] bool generateCheck = false;
    [SerializeField] float bulletBlur = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(generate());
    }

    IEnumerator generate()
    {
        while (generateCheck)
        {
            yield return new WaitForSeconds(cycle);
            Vector3 blur = new Vector3(0, Random.Range(-bulletBlur, bulletBlur), 0);
            Instantiate(iceLance, transform.position, Quaternion.Euler(transform.localEulerAngles + blur));
        }
    }
}
