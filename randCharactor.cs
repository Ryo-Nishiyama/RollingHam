using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randCharactor : MonoBehaviour
{
    public GameObject[] rain;
    float time;
    float z = 0;
    float xNum = 1;

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if(time > 0.025f)
        {
            int randNum = Random.Range(0, rain.Length);
            float randValue = Random.value;
            float x = 8.5f * xNum * randValue;
            float y = 5.2f;
            Instantiate(rain[randNum], new Vector2(x, y), rain[randNum].transform.rotation);
            time = 0;
            xNum *= -1;
        }
        
    }
}
