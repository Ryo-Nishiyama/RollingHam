using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class result_enviroment : MonoBehaviour
{
    [SerializeField] ChangeScene _changeScene;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        resultChange();
    }

    void resultChange()
    {
        bool maru = Input.GetButtonDown("Fire_2");
        if (maru)
        {
            SceneManager.LoadScene("rainEffectScene");
        }
    }
}
