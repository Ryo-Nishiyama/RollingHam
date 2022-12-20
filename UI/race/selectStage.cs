using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectStage : MonoBehaviour
{
    static public int selectRaceNum = 1;
    GameObject[] childObj;
    Image[] backImg;
    [SerializeField] Image stageImg;
    [SerializeField] Sprite[] stageImgs;
    [SerializeField] bool returnCheck = true;
    [SerializeField] GameObject[] btnBack;
    [SerializeField] GameObject[] btnBackStripe;
    AudioSource _audioSource;
    [SerializeField] AudioClip a_select, a_cursor;
    Vector3[] stripePos;
    float stripeMove = 0;
    int selectNum = 1;
    Color32[] backColor = new Color32[] { new Color32(60, 60, 60, 255), new Color32(0, 150, 0, 255) };
    Color32[] backStripeColor = new Color32[] { new Color32(50, 50, 50, 255), new Color32(80, 180, 0, 255) };


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //全ての子オブジェクトを取得
        childObj = new GameObject[this.transform.childCount];
        backImg = new Image[this.transform.childCount];
        stripePos = new Vector3[btnBack.Length];
        for (int i = 0; i < childObj.Length; i++)
        {
            childObj[i] = this.transform.GetChild(i).gameObject;
            //それぞれの子オブジェクトの背景画像を取得
            backImg[i] = childObj[i].transform.GetChild(0).GetComponent<Image>();
        }
        for (int i = 0; i < btnBackStripe.Length; i++)
        {
            stripePos[i] = btnBackStripe[i].transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        getKey();
        pushButton();
        changeColor();
        changeImg();
    }
    void getKey()
    {
        //位置が変わったときstripeの位置を初期化
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectNum -= 1;
            stripeMove = 0;
            _audioSource.PlayOneShot(a_cursor);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectNum += 1;
            stripeMove = 0;
            _audioSource.PlayOneShot(a_cursor);
        }
        if (selectNum < 0)
        {
            selectNum = childObj.Length - 1;
        }
        else if (selectNum >= childObj.Length)
        {
            selectNum = 0;
        }
    }
    void pushButton()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {

            if (selectNum == 0 && returnCheck)
            {
                childObj[0].GetComponent<returnKey>().returnButton.onClick.Invoke();
            }
            //ランダムを押されたときリスト内のどれかを選ぶ
            else if (selectNum == childObj.Length - 1)
            {
                int randNum = Random.Range(0, childObj.Length - 1);
                if (returnCheck)
                {
                    randNum = Random.Range(1, childObj.Length - 1);
                }
                selectRaceNum = randNum;
                childObj[randNum].GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                selectRaceNum = selectNum;
                childObj[selectNum].GetComponent<Button>().onClick.Invoke();
            }
        }
    }
    void changeColor()
    {
        if (selectNum == 0 && returnCheck)
        {
            backImg[0].color = new Color32(182, 255, 255, 255);
        }
        else
        {
            backImg[0].color = new Color32(255, 255, 255, 255);
        }
    }
    void changeImg()
    {
        stripeMove += Time.deltaTime * 100f;
        if (stripeMove > 1202)
        {
            stripeMove = 0;
        }
        for (int i = 0; i < btnBack.Length; i++)
        {
            //位置、色を初期化
            btnBack[i].GetComponent<Image>().color = backColor[0];
            btnBackStripe[i].GetComponent<Image>().color = backStripeColor[0];
            btnBackStripe[i].transform.localPosition = stripePos[i];
        }
        int ImgNum = selectNum;
        //returnがあるときスキップ
        if (returnCheck)
        {
            ImgNum -= 1;
        }
        if (ImgNum >= 0)
        {
            stageImg.sprite = stageImgs[ImgNum];
            btnBack[ImgNum].GetComponent<Image>().color = backColor[1];
            btnBackStripe[ImgNum].GetComponent<Image>().color = backStripeColor[1];
            Vector3 stripeMovePos = new Vector3(stripePos[ImgNum].x - stripeMove, stripePos[ImgNum].y, stripePos[ImgNum].z);
            btnBackStripe[ImgNum].transform.localPosition = stripeMovePos;
        }
    }
}
