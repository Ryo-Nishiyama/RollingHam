using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class frameLine : MonoBehaviour
{
    LineRenderer _LineRenderer;
    [SerializeField] selectBallSet _selectBallSet;
    public Image[] backgroundFrame;
    public Image[] background;
    public Image[] stripe;

    Vector3[] linePos_ball = new Vector3[]{new Vector3(0, 4.5f, 0), new Vector3(4.65f, 4.5f, 0), new Vector3(4.65f, 1.5f, 0), new Vector3(0, 1.5f, 0)};
    Vector3[] linePos_abi = new Vector3[] {new Vector3(0, 1.2f, 0), new Vector3(4.65f, 1.2f, 0), new Vector3(4.65f, -3.1f, 0), new Vector3(0, -3.1f, 0) };
    new Color32[] frameColor = new Color32[] { new Color32(0, 208, 255, 255), new Color32(255, 214, 0, 255) };

    public int framePos = 0;

    float timeCounter = 0;
    float juziH,juziV = 0;
    public bool juziCheck_up, juziCheck_down, juziCheck_right, juziCheck_left = false;
    public bool juzi_up, juzi_down, juzi_right, juzi_left = false;
    public bool selectCheck = false;

    AudioSource audioSource;
    public AudioClip a_cancel,a_start,a_select,a_cursor;

    float stripePos1, stripePos2;
    float stripeTime=0;

    // Start is called before the first frame update
    void Start()
    {
        stripePos1 = stripe[0].transform.localPosition.x;
        stripePos2 = stripePos1 - 867;
        _LineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        juzi_up = false;
        juzi_down = false;
        juzi_right = false;
        juzi_left = false;
        juziH = Input.GetAxis("Vertical D-Pad");
        juziV = Input.GetAxis("Horizontal D-Pad");
        //スキル未選択で押したとき、startボタン無効化
        if (framePos == 2)
        {
            selectCheck = false;
        }
        if ((juziH > 0.5f && !juziCheck_up) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            juzi_up = true;
            juziCheck_up = true;
            if (!selectCheck)
            {
                audioSource.PlayOneShot(a_cursor);
                framePos -= 1;
                if (framePos < 0)
                {
                    framePos = 3;
                }
            }
        }
        else if (juziH <= 0.5f)
        {
            juziCheck_up = false;
        }
        if ((juziH < -0.5f && !juziCheck_down) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            juzi_down = true;
            juziCheck_down = true;
            if (!selectCheck) 
            {
                audioSource.PlayOneShot(a_cursor);
                framePos += 1;
                if (framePos > 3)
                {
                    framePos = 0;
                }
            }
        }
        else if (juziH >= -0.5f)
        {
            juziCheck_down = false;
        }
        
        if (juziV > 0.5f && !juziCheck_right)
        {
            juzi_right = true;
            juziCheck_right = true;
        }
        else if (juziV <= 0.5f)
        {
            juziCheck_right = false;
        }
        if (juziV < -0.5f && !juziCheck_left)
        {
            juzi_left = true;
            juziCheck_left = true;
        }
        else if (juziV >= -0.5f)
        {
            juziCheck_left = false;
        }
        if (framePos == 0)
        {
            backgroundFrame[0].color = new Color32(0, 255, 230, 255);
        }
        else
        {
            backgroundFrame[0].color = new Color32(255, 255, 255, 255);
        }
        if (framePos == 1)
        {
            backgroundFrame[1].color = new Color32(0, 255, 230, 255);
        }
        else
        {
            backgroundFrame[1].color = new Color32(255, 255, 255, 255);
        }

        if (Input.GetButtonDown("Fire_2") || Input.GetKeyDown(KeyCode.Return))
        {
            //選択前から選択されたときのみ再生
            if (!selectCheck)
            {
                audioSource.PlayOneShot(a_select);
            }
            
            selectCheck = true;
            if (framePos == 0|| framePos == 1)
            {
                background[framePos].color = new Color32(0, 150, 0, 255);
                stripe[framePos].color = new Color32(80, 180, 0, 255);
            }
        }
        if (Input.GetButtonDown("Fire_1")|| Input.GetKeyDown(KeyCode.Escape))
        {
            if (selectCheck)
            {
                audioSource.PlayOneShot(a_cancel);
            }
            selectCheck = false;
            if (framePos == 0 || framePos == 1)
            {
                background[framePos].color = new Color32(60, 60, 60, 255);
                stripe[framePos].color = new Color32(50, 50, 50, 255);
            }
        }
        if (selectCheck && (framePos == 0 || framePos == 1))
        {
            moveStripe(stripe[framePos]);
        }
    }
    void moveStripe(Image stripe)
    {
        //ストライプを移動
        stripeTime += Time.deltaTime * 0.1f;
        float pos = Mathf.Lerp(stripePos1, stripePos2, stripeTime);
        stripe.rectTransform.localPosition = new Vector3(pos, stripe.transform.localPosition.y, stripe.transform.localPosition.z);
        if (stripeTime > 1)
        {
            stripeTime = 0;
        }
    }
}
