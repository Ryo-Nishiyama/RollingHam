using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class returnKey : MonoBehaviour
{
    public Button returnButton;
    public Image returnEscape;
    [SerializeField] ChangeScene _changeScene;
    float returnCounter = 0;

    // Update is called once per frame
    void Update()
    {
        if(raceSet.raceMode&& SceneManager.GetActiveScene().name == "selectBall")
        {
            raceBack();
        }
        else
        {
            clickButton();
        }
        buttonColor();
    }
    void clickButton()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetButton("Fire_1"))
        {
            returnCounter += Time.deltaTime;
        }
        else
        {
            returnCounter = 0;
        }
        if (returnCounter >= 3)
        {
            returnButton.onClick.Invoke();
        }
    }
    void buttonColor()
    {
        returnEscape.fillAmount = returnCounter / 3;
        returnEscape.color = new Color32(255, 100, 0, 255);
    }
    void raceBack()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetButton("Fire_1"))
        {
            returnCounter += Time.deltaTime;
        }
        else
        {
            returnCounter = 0;
        }
        if (returnCounter >= 3)
        {
            _changeScene.stageSelect_load();
            _changeScene.SE_cancel();
            _changeScene.fadeOut();
        }
        
    }
}
