using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class condition : MonoBehaviour
{
    //public Image conditionImg;
    public Sprite fire_sprite;
    public Sprite thunder_sprite;
    public Sprite ice_sprite;
    public Sprite sleep_sprite;
    public Sprite nightmare_sprite;
    public Sprite poison_sprite;
    public Sprite confusion_sprite;
    public Sprite none_sprite;
    public GameObject mainball;

    bool _burn, _frostbite, _paralysis, _sleep, _nightmare, _poison,_confusion;

    // Update is called once per frame
    void Update()
    {
        //èÛë‘àŸèÌÉAÉCÉRÉìêÿÇËë÷Ç¶
        _burn = mainball.GetComponent<moveTPS>().burn;
        _frostbite = mainball.GetComponent<moveTPS>().frostbite;
        _paralysis = mainball.GetComponent<moveTPS>().paralysis;
        _sleep = mainball.GetComponent<moveTPS>().sleep;
        _nightmare = mainball.GetComponent<moveTPS>().nightmare;
        _poison = mainball.GetComponent<moveTPS>().poison;
        _confusion = mainball.GetComponent<moveTPS>().reverseOp;
        if (_burn)
        {
            this.gameObject.GetComponent<Image>().sprite = fire_sprite;
        }
        else if (_frostbite)
        {
            this.gameObject.GetComponent<Image>().sprite = ice_sprite;
        }
        else if (_paralysis)
        {
            this.gameObject.GetComponent<Image>().sprite = thunder_sprite;
        }
        else if (_nightmare)
        {
            this.gameObject.GetComponent<Image>().sprite = nightmare_sprite;
        }
        else if (_sleep)
        {
            this.gameObject.GetComponent<Image>().sprite = sleep_sprite;
        }
        else if (_poison)
        {
            this.gameObject.GetComponent<Image>().sprite = poison_sprite;
        }
        else if (_confusion)
        {
            this.gameObject.GetComponent<Image>().sprite = confusion_sprite;
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = none_sprite;
        }
    }
}
