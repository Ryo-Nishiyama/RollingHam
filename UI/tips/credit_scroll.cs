using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class credit_scroll : MonoBehaviour
{
    bool upArrow, downArrow, upArrow_down, downArrow_down, rightArrow_down, leftArrow_down;
    float scrollNow=0;
    float scrollSpeed = 1000;
    float scrollLimit=2100;
    float coolTimer;
    float coolTime = 0.3f;
    bool returnCheck=false;

    [SerializeField] GameObject creditObj;
    [SerializeField] Button returnButton;
    [SerializeField] AudioClip a_select;
    AudioSource _AudioSource;
    RectTransform _rectTransform;

    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        _rectTransform = creditObj.GetComponent<RectTransform>();
        originalPos = _rectTransform.anchoredPosition3D;
        coolTimer = coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        inputGet();
        changeButton();
        if (!returnCheck && coolTimer <= 0)
        {
            scroll();
        }
        if (coolTimer > 0)
        {
            coolTimer -= Time.deltaTime;
        }
    }
    void inputGet()
    {
        upArrow = Input.GetKey(KeyCode.UpArrow);
        downArrow = Input.GetKey(KeyCode.DownArrow);
        upArrow_down = Input.GetKeyDown(KeyCode.UpArrow);
        downArrow_down = Input.GetKeyDown(KeyCode.DownArrow);
        rightArrow_down = Input.GetKeyDown(KeyCode.RightArrow);
        leftArrow_down = Input.GetKeyDown(KeyCode.LeftArrow);
    }
    void scroll()
    {
        if (downArrow)
        {
            scrollNow += Time.deltaTime * scrollSpeed;
            if (scrollNow > scrollLimit)
            {
                scrollNow = scrollLimit;
            }
        }
        if (upArrow)
        {
            scrollNow -= Time.deltaTime * scrollSpeed;
            if (scrollNow < 0)
            {
                scrollNow = 0;
            }
        }
        Vector3 scrollPos = new Vector3(originalPos.x, originalPos.y + scrollNow, originalPos.z);
        _rectTransform.anchoredPosition3D = scrollPos;
    }
    void changeButton()
    {
        if (((scrollNow <= 0 && upArrow_down) || (scrollNow >= scrollLimit && downArrow_down)) && !returnCheck)
        {
            returnCheck = true;
            returnButton.GetComponent<Image>().color = new Color32(182, 255, 255, 255);
            _AudioSource.PlayOneShot(a_select);
            return;
        }
        if (returnCheck)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                returnButton.onClick.Invoke();
            }
            if (upArrow_down)
            {
                changeButton_credit(scrollLimit);
            }
            else if (downArrow_down)
            {
                changeButton_credit(0);
            }
        }
        else
        {
            //ÉLÅ[ÇâüÇ≥ÇÍÇΩÇÁë¶ç¿Ç…ìÆÇ©ÇπÇÈÇÊÇ§Ç…Ç∑ÇÈ
            if (downArrow_down || upArrow_down)
            {
                coolTimer = 0;
            }
        }
    }
    void changeButton_credit(float scrollPoint)
    {
        returnCheck = false;
        returnButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        scrollNow = scrollPoint;
        coolTimer = coolTime;
        Vector3 scrollPos = new Vector3(originalPos.x, originalPos.y + scrollNow, originalPos.z);
        _rectTransform.anchoredPosition3D = scrollPos;
        _AudioSource.PlayOneShot(a_select);
    }
}
