using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class buttonSelector : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip a_cursor;
    public Button[] changeButton;
    [SerializeField] Sprite[] startButtons;
    [SerializeField] GameObject startText;
    Vector3 textPos;
    public GameObject[] buttonPos;
    public Image seed;
    public Image[] stripe;
    public Image[] background;
    public int buttonCounter=1;
    int buttonCounterMax;

    float juziH, juziV = 0;
    float returnCounter = 0;
    public bool juziCheck_up, juziCheck_down, juziCheck_right, juziCheck_left = false;
    bool returnCheck = false;
    bool countStop = false;
    Vector3 seedPos;
    float margin;
    float stripePos1 = 251;
    float stripePos2 = -249;
    float stripeTime = 0;
    [SerializeField] bool startCheck = true;
    public bool backCheck = true;
    [SerializeField] bool seedCheck = true;
    public int stripeChange=800;

    // Start is called before the first frame update
    void Start()
    {
        textPos = startText.transform.localPosition;
        stripePos1 = stripe[0].transform.localPosition.x;
        stripePos2 = stripePos1 - stripeChange;
        if (seedCheck)
        {
            seedPos = seed.transform.position;
            margin = buttonPos[0].transform.position.y - buttonPos[1].transform.position.y;
        }
        
        audioSource = GetComponent<AudioSource>();
        buttonCounterMax = changeButton.Length;
        if(changeButton[0].name == "returnButton")
        {
            returnCheck = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (seedCheck)
        {
            moveSeed();
        }
        if (backCheck)
        {
            
            moveStripe(stripe[buttonCounter]);
        }
        else
        {
            if (changeButton.Length-1 > buttonCounter&&buttonCounter!=0)
            {
                
                moveStripe(stripe[buttonCounter - 1]);
            }
            
        }
        if (!countStop)
        {
            buttonSelect();
        }
        //スタートボタンがあるときテキスト位置をリセットしてスプライトを変更
        if (!startCheck)
        {
            startText.transform.localPosition = textPos;
            startButtonSet();
        }
        clickButton();
    }

    void moveSeed()
    {
        //キャンセル、決定の部分をスキップするようにする
        float sinPos = Mathf.Sin(Time.time * 4) / 2 + 0.5f;
        if (backCheck)
        {
            seed.transform.position = new Vector3(seedPos.x + sinPos * 10, seedPos.y - (buttonCounter ) * margin, seed.transform.position.z);
        }
        else
        {
            seed.transform.position = new Vector3(seedPos.x + sinPos * 10, seedPos.y - (buttonCounter - 1) * margin, seed.transform.position.z);
            if (changeButton.Length - 1 > buttonCounter && buttonCounter != 0)
            {
                seed.enabled = true;
            }
            else
            {
                seed.enabled = false;
            }
        }
            
    }
    void moveStripe(Image stripe)
    {
        //ストライプを移動
        stripeTime += Time.deltaTime*0.1f;
        float pos = Mathf.Lerp(stripePos1, stripePos2, stripeTime);
        stripe.rectTransform.localPosition = new Vector3(pos, stripe.transform.localPosition.y, stripe.transform.localPosition.z);
        if (stripeTime > 1)
        {
            stripeTime = 0;
        }
    }
    void buttonSelect()
    {
        changeButton[buttonCounter].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        if (backCheck&&startCheck)
        {
            background[buttonCounter].GetComponent<Image>().color = new Color32(60, 60, 60, 255);
            stripe[buttonCounter].GetComponent<Image>().color = new Color32(50, 50, 50, 255);
        }
        else
        {
            if (changeButton.Length - 1 > buttonCounter && buttonCounter != 0)
            {
                background[buttonCounter - 1].GetComponent<Image>().color = new Color32(60, 60, 60, 255);
                stripe[buttonCounter - 1].GetComponent<Image>().color = new Color32(50, 50, 50, 255);
            }
            
        }
        
        juziH = Input.GetAxis("Horizontal D-Pad");
        juziV = Input.GetAxis("Vertical D-Pad");
        if ((juziH > 0.5f && !juziCheck_up || Input.GetKeyDown(KeyCode.UpArrow))&&buttonCounterMax>1)
        {
            juziCheck_up = true;
            buttonCounter -= 1;
            audioSource.PlayOneShot(a_cursor);
        }
        else if (juziH <= 0.5f)
        {
            juziCheck_up = false;
        }
        if ((juziH < -0.5f && !juziCheck_down || Input.GetKeyDown(KeyCode.DownArrow)) && buttonCounterMax > 1)
        {
            juziCheck_down = true;
            buttonCounter += 1;
            audioSource.PlayOneShot(a_cursor);
        }
        else if (juziH >= -0.5f)
        {
            juziCheck_down = false;
        }
        if (buttonCounter >= buttonCounterMax)
        {
            buttonCounter = 0;

        }
        else if (buttonCounter < 0)
        {
            buttonCounter += buttonCounterMax;
        }
        if (buttonCounter == 0 && buttonCounterMax != 1&&!backCheck)
        {
            changeButton[buttonCounter].GetComponent<Image>().color = new Color32(182, 255, 255, 255);
        }
        
        if (backCheck&&startCheck)
        {
            background[buttonCounter].GetComponent<Image>().color = new Color32(0, 150, 0, 255);
            stripe[buttonCounter].GetComponent<Image>().color = new Color32(80, 180, 0, 255);
        }
        else
        {
            if (changeButton.Length - 1 > buttonCounter && buttonCounter != 0)
            {
                background[buttonCounter - 1].GetComponent<Image>().color = new Color32(0, 150, 0, 255);
                stripe[buttonCounter - 1].GetComponent<Image>().color = new Color32(80, 180, 0, 255);
            }
            
        }
    }
    void startButtonSet()
    {
        if(changeButton.Length - 1 == buttonCounter)
        {
            changeButton[changeButton.Length - 1].GetComponent<Image>().sprite = startButtons[1];
        }
        else
        {
            changeButton[changeButton.Length - 1].GetComponent<Image>().sprite = startButtons[0];
        }
    }
    void clickButton()
    {
        if ((Input.GetKey(KeyCode.Return) || Input.GetButton("Fire_2"))&& changeButton.Length - 1 == buttonCounter)
        {
            changeButton[buttonCounter].GetComponent<Image>().sprite = startButtons[2];
            startText.transform.localPosition = new Vector3(textPos.x - 13, textPos.y - 15, textPos.z);
        }
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetButtonUp("Fire_2"))
        {
            changeButton[buttonCounter].onClick.Invoke();
        }

        if (returnCheck)
        {
            //3秒以上押したとき戻る
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
                changeButton[0].onClick.Invoke();
            }
        }
    }
}
