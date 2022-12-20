using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tips_generate : MonoBehaviour
{
    AudioSource _AudioSource;
    [SerializeField] AudioClip a_select;
    [SerializeField] GameObject canvas;
    [SerializeField] Button returnButton;
    [SerializeField] GameObject[] instantSet;
    [SerializeField] Image[] Images;
    [SerializeField] TextMeshProUGUI titleTxt;
    [SerializeField] TextMeshProUGUI descriptionTxt;
    //タイトルの背景色
    Color32[] titleColor = new Color32[] { new Color32(90, 235, 255, 255), new Color32(150, 255, 90, 255), new Color32(255, 60, 190, 255) };
    //それぞれのオブジェクトの位置
    List<Vector3> rectLocalPosList = new List<Vector3>() { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), 
                                                           new Vector3(-424.2f, -38, 0), new Vector3(185, -34, 0), new Vector3(-189, 123, 0) };
    //大枠の位置
    List<Vector3> rectFramePosList = new List<Vector3>() { new Vector3(910, 430, 0), new Vector3(2610, 430, 0), new Vector3(4310, 430, 0) };
    [SerializeField] Sprite[] playerImg;
    List<string> playerTitleTxtList = new List<string>() { "ボール：軽いボール","ボール：普通のボール","ボール：重いボール","ボール：チャージボール",
                                                           "ボール：基礎高ボール","ボール：滑るボール","ボール：直線ボール","ボール：超軽いボール",
                                                           "スキル：にんじん","スキル：赤にんじん","スキル：ひまわりの種","スキル：シールド","スキル：復活",
                                                           "スキル：加速","スキル：ジャンプ","スキル：氷攻撃"};

    List<string> playerDescriptionTxtList = new List<string>() { "空中性能が高く、スキルが溜まりやすい。\n代わりに地上での速度が遅く、耐久力が低い。\nジャンプスキルと相性抜群。",
                                                           "基本的な性能を持つ。\n初心者やノーマルな性能を好む人におすすめ。\n様々なスキルと\n組み合わせて好みのセットを見つけよう。",
                                                           "最高速度や耐久力が高い。\nチャージが遅く加速力が低い。\n止まらずにどんどん加速させよう。",
                                                           "最大チャージをしたときの速度が格段に速い。\n一方で普段の性能は低く、スキルも溜まりにくい。\n加速スキルと相性抜群。",
                                                           "全体的に能力が高い。\nしかし、チャージすることができず爆発力に欠ける。\n安定した走りがしたいときに選ぼう。",
                                                           "能力が高めでスキルが溜まりやすい。\n一方、ブレーキがほとんど効かず進み続けてしまう。\n滑る性能を活かして走れると強いかも？",
                                                           "速度が速く、ブレーキも強い。\nチャージ中以外は曲がることができず\n前にしか進めない。\nグリップ力を活かした走りをしてみよう。",
                                                           "全ての能力が高水準。\n耐久力が非常に低く、スキルも溜まりづらい。\n状態異常や攻撃に気を付けて走ろう。",
                                                           "必要ポイント：1\n最大HPの2割を回復することができる。\n必要ポイントが少ないため\n積極的に使って様々な場所に挑戦しよう。",
                                                           "必要ポイント：2\nすべての状態異常を直すことができる。\n状況に合わせて使おう。",
                                                           "必要ポイント：3\nランダムでステータスを\n一段階上昇させる。\nスキルポイントが余ったときにぴったり。",
                                                           "必要ポイント：1\n15秒の間\n飛び道具から身を守ることができる。\n攻撃を防いで安全に走ろう",
                                                           "必要ポイント：6\nHPが0になったとき時間経過を待たずに復活できる。\nやられてしまったときの保険としていかがだろうか。",
                                                           "必要ポイント：2\n最大チャージと同じ加速力を得ることができる。\nチャージによる加速強いボールを使うときは積極的に選ぼう。",
                                                           "必要ポイント：2\n地上にいるときジャンプをすることができる。\n空中機動力が高いボールを使うときは\nこのスキルでステージを走り回ろう。",
                                                           "氷ポイントを1つ消費することで\n1発氷を発射することができる。\n発射した攻撃はアイテムの回収や\n敵への攻撃、岩の破壊などを行うことができる。"};

    [SerializeField] Sprite[] systemImg;
    List<string> systemTitleTxtList = new List<string>() { "基本：呼吸","基本：強化バー","状態異常：火傷", "状態異常：混乱", "状態異常：しもやけ","麻痺", "状態異常：毒", "状態異常：眠り",
                                                           "状態異常：悪夢", "探索：草原", "探索：火山", "探索：雪原", "探索：海","探索：浮島", "探索：地下", "レース：道路","レース：八の字",
                                                           "ギミック：噴水","ギミック：竜巻","ギミック：ワープポイント","ギミック：氷生成器"};
    List<string> systemDescriptionTxtList = new List<string>() { "水中での活動時間を表す。\nゲージがなくなるとダメージを受ける。\n水から出たり、水中の泡に触れたりしてゲージを\n回復しながら気を付けて探索しよう。",
                                                                 "現在の強化状態を表す。\n同じ種を5つ集めるごとにいいことが...?",
                                                                 "火傷状態になり、ダメージを受ける。\nチャージが暴走して曲がりづらくなる。\n使いこなせば強い味方になるかも？",
                                                                 "混乱状態になり、旋回操作が反転する。\n混乱状態に翻弄されないよう気を付けて走ろう。",
                                                                 "しもやけ状態になり、チャージが遅くなる。\n最高速度が上昇する。\nチャージを使わなければ強い味方に。",
                                                                 "麻痺状態になり、\nチャージが最大の半分しかできなくなる。\n麻痺状態にならないように気を付けよう。",
                                                                 "毒状態になり、ダメージを受ける。\n曲がる力が上昇する。\n上がった機動力で回復アイテムを\n見つけて取りに行こう。",
                                                                 "眠り状態になり、動けなくなる。\n眠っている間はHPが回復する。\nHPが少ないときはひと眠りしてみよう。",
                                                                 "悪夢状態になり、動けなくなる。\n眠っている間ダメージを受ける。\n眠るときランダムで悪夢を見るため気を付けよう。",
                                                                 "草や木が生えた緑あふれるエリア。\n毒や眠りになりやすい。\n一定の場所にアイテムが発生しやすい。",
                                                                 "山と岩石でできたエリア。\nやけどになりやすい。\n岩を壊してハムスターを育てよう。",
                                                                 "雪の降る銀世界のエリア。\nしもやけになりやすい。\n雪の塊を壊してハムスタ－を育てよう。",
                                                                 "水で満たされた海のエリア。\n呼吸の残りに注意。\n土管の中を見てみよう。",
                                                                 "ステージに浮かぶなぞの島。\n島の上には良いアイテムがあることが多い。",
                                                                 "ステージの地下にあるコンクリートのエリア。\nボールを変えたいときに行くといいかも？",
                                                                 "水と道路でできたレース場。\nステージのほとんどが直線でできている。\n上のエリアほど通りやすい。",
                                                                 "空に浮かぶ八の字型のレース場。\nステージのほとんどがカーブでできている。\n曲線に自信があるボールにおすすめ。",
                                                                 "海から噴出される水。\n水に乗っている間上昇し続けることができる。",
                                                                 "常に回転している上向きの風。\n風に乗っている間上昇し続けることができる。",
                                                                 "各地に点在しているなぞの場所。\nしばらく上に乗っているとどこかへ飛ばされる。\n見かけたら乗ってみよう。",
                                                                 "氷の槍を発生させる怪しい霧。\n霧の中から発生する氷に当たるとダメージを受ける。\n安全なところに逃げよう。"};

    [SerializeField] Sprite[] itemImg;
    List<string> itemTitleTxtList = new List<string>() { "種：スピード種", "種：ブレーキ種", "種：せんかい種", "種：チャージ種", "種：かそく種", "種：オール種", "種：にせもの種",
                                                         "にんじん：ふつうのにんじん","にんじん：赤にんじん","にんじん：くさったにんじん","にんじん：なぞのにんじん","にんじん：金にんじん" ,
                                                         "時計：スピード時計","時計：ブレーキ時計","時計：せんかい時計","時計：チャージ時計","時計：かそく時計","時計：オール時計","ひまわり"};
    List<string> itemDescriptionTxtList = new List<string>() { "最大速度が上昇するひまわりの種。",
                                                               "ブレーキ力が上昇するひまわりの種。",
                                                               "曲がる力が上昇するひまわりの種。",
                                                               "チャージ速度が上昇するひまわりの種。",
                                                               "加速力が上昇するひまわりの種。",
                                                               "全ての能力が上昇するひまわりの種。",
                                                               "ダメージを受けるひまわりの種の偽物。",
                                                               "HPが回復する普通のにんじん。",
                                                               "状態異常が回復する赤いにんじん。\n状態異常ではないときは取っても何も起こらない。",
                                                               "毒状態になる腐ったにんじん。\nでもたまにいいことがあるかも...？",
                                                               "取ると混乱する怪しいにんじん。\n状態異常の時に取ると状態異常が回復する。",
                                                               "様々な効果を持つ金色のにんじん。\n良い効果が詰まっている。\n見かけたら取りに行こう。",
                                                               "最大速度が一時的に最大まで上昇する時計。\nしばらく経つと効果がなくなるため注意。",
                                                               "ブレーキ力が一時的に最大まで上昇する時計。\nしばらく経つと効果がなくなるため注意。",
                                                               "曲がる力が一時的に最大まで上昇する時計。\nしばらく経つと効果がなくなるため注意。",
                                                               "チャージ速度が一時的に最大まで上昇する時計。\nしばらく経つと効果がなくなるため注意。",
                                                               "加速力が一時的に最大まで上昇する時計。\nしばらく経つと効果がなくなるため注意。",
                                                               "全ての能力が一時的に最大まで上昇する時計。\nしばらく経つと効果がなくなるため注意。",
                                                               "スキルポイントと氷ポイントが\n早く溜まるようになるひまわり。\n一回の探索で5つまでしか出現しないため\n積極的に取りに行こう。"};

    int selectNum = 1;
    int selectNumNow = 1;
    int selectNumR;
    int selectNumL;
    //横移動の完了を確認
    bool changeCheck = true;

    List<string> mametishiki = new List<string>() { "システムまめちしき", "プレイヤーまめちしき", "アイテムまめちしき" };
    [SerializeField] TextMeshProUGUI mameTxt;

    List<GameObject> systemList;
    List<GameObject> itemList;
    List<List<GameObject>> allList=new List<List<GameObject>>();
    List<List<GameObject>> playerList = new List<List<GameObject>>();

    [SerializeField] GameObject[] parents;
    List<RectTransform> _rectTransforms = new List<RectTransform>();

    bool upArrow, downArrow, upArrow_down, downArrow_down, rightArrow_down, leftArrow_down;
    float moveLRTime = 0.75f;
    float moveLRTimer = 0;

    float marginX;

    float scrollNow = 0;
    float scrollSpeed = 1000;
    float scrollLimit;

    float coolTime = 0.3f;
    float coolTimer;
    bool returnCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        scrollLimit = 350 * (playerImg.Length - 3)+150;
        selectNumR = rectFramePosList.Count - 1;
        selectNumL = 0;
        coolTimer = coolTime;
        //間の広さを取得
        marginX = rectFramePosList[1].x - rectFramePosList[0].x;
        //要素数を各要素に合わせる
        systemList = new List<GameObject>(systemTitleTxtList.Count);
        itemList = new List<GameObject>(itemTitleTxtList.Count);
        allList.Add(new List<GameObject>());
        allList.Add(new List<GameObject>());
        allList.Add(new List<GameObject>());
        generateTips(systemTitleTxtList.Count, 0, titleColor[0], systemImg, systemTitleTxtList, systemDescriptionTxtList);
        generateTips(playerTitleTxtList.Count, 1, titleColor[1], playerImg, playerTitleTxtList, playerDescriptionTxtList);
        generateTips(itemTitleTxtList.Count, 2, titleColor[2], itemImg, itemTitleTxtList, itemDescriptionTxtList);
    }
    /// <summary>
    /// Tipsを自動生成する
    /// </summary>
    /// <param name="Num"></param>
    /// <param name="ListNum"></param>
    /// <param name="titlecolor"></param>
    /// <param name="thumbnails"></param>
    /// <param name="titleText"></param>
    /// <param name="descriptionText"></param>
    void generateTips(int Num,int ListNum ,Color32 titlecolor, Sprite[] thumbnails, List<string> titleText, List<string>descriptionText)
    {
        for (int i = 0; i < Num; i++)
        {
            List<GameObject> childs = new List<GameObject>();
            for (int j = 0; j < instantSet.Length; j++)
            {
                GameObject childObj = Instantiate(instantSet[j], instantSet[j].transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                //次の場所で処理できるように仮リストに格納
                childs.Add(childObj);
            }
            //色、画像、文の差し替え
            Image titleImg = childs[2].GetComponent<Image>();
            Image thumbImg = childs[3].GetComponent<Image>();
            TextMeshProUGUI descTxt = childs[4].GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI titleTxt = childs[5].GetComponent<TextMeshProUGUI>();
            titleImg.color = titlecolor;
            thumbImg.sprite = thumbnails[i];
            descTxt.text = descriptionText[i];
            titleTxt.text = titleText[i];

            //ひとまとめにする
            GameObject semiParent = new GameObject();
            for (int j = 0; j < instantSet.Length; j++)
            {
                RectTransform _rect = childs[j].GetComponent<RectTransform>();
                //それぞれの位置を調整
                Vector3 rectNowPos = new Vector3(/*45*/-1655 + rectLocalPosList[j].x, 350-350*i + rectLocalPosList[j].y, rectLocalPosList[j].z);
                _rect.anchoredPosition3D = rectNowPos;
                childs[j].transform.parent = semiParent.transform;
            }
            //ひとまとめずつ格納
            allList[ListNum].Add(semiParent);
            //それぞれの項目の大枠に格納
            allList[ListNum][i].transform.parent = parents[ListNum].transform;
        }
        //スクロール用にグローバルで保存
        _rectTransforms.Add( parents[ListNum].GetComponent<RectTransform>());
        _rectTransforms[ListNum].anchoredPosition3D = rectFramePosList[ListNum];
    }
    // Update is called once per frame
    void Update()
    {
        //遷移してないときのみ入力有効化
        if (changeCheck)
        {
            inputGet();
            //左右どちらかが入力されたとき
            if ((rightArrow_down || leftArrow_down)&&!returnCheck)
            {
                StartCoroutine(moveLR());
            }
        }
        changeButton();
        //returnボタン選択中はtipsの動きを停止
        if (!returnCheck && coolTimer <= 0)
        {
            moveUD();
        }
        //returnから戻ったときそのまま動き始めないように少し遅延させる
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
    void changeButton()
    {
        if (((scrollNow <= 0 && upArrow_down) || (scrollNow >= scrollLimit&&downArrow_down)) && !returnCheck)
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
                changeButton_tips(scrollLimit);
            }
            else if (downArrow_down)
            {
                changeButton_tips(0);
            }
        }
        else
        {
            //キーを押されたら即座に動かせるようにする
            if (downArrow_down || upArrow_down)
            {
                coolTimer = 0;
            }
        }
    }
    void changeButton_tips(float scrollPoint)
    {
        returnCheck = false;
        returnButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        scrollNow = scrollPoint;
        coolTimer = coolTime;
        Vector3 scrollPos = new Vector3(_rectTransforms[selectNumNow].anchoredPosition3D.x, rectFramePosList[selectNumNow].y + scrollNow, _rectTransforms[selectNumNow].anchoredPosition3D.z);
        _rectTransforms[selectNumNow].anchoredPosition3D = scrollPos;
        _AudioSource.PlayOneShot(a_select);
    }
    void moveUD()
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
        Vector3 scrollPos = new Vector3(_rectTransforms[selectNumNow].anchoredPosition3D.x, rectFramePosList[selectNumNow].y + scrollNow, _rectTransforms[selectNumNow].anchoredPosition3D.z);
        _rectTransforms[selectNumNow].anchoredPosition3D = scrollPos;
    }
    
    IEnumerator moveLR()
    {
        //タイマーリセット
        moveLRTimer = 0;
        changeCheck = false;

        _AudioSource.PlayOneShot(a_select);

        
        float moveDistance = 0;
        if (rightArrow_down)
        {
            //再配置するオブジェクトの値を保存
            selectNum -= 1;
            if (selectNum < 0)
            {
                selectNum = rectFramePosList.Count - 1;
            }
            //遷移後の中心の値を保存
            selectNumNow += 1;
            if(selectNumNow >= rectFramePosList.Count)
            {
                selectNumNow = 0;
            }
            moveDistance = -marginX;
        }
        else if (leftArrow_down)
        {
            selectNum += 1;
            if (selectNum >= rectFramePosList.Count)
            {
                selectNum = 0;
            }
            selectNumNow -=1;
            if (selectNumNow < 0)
            {
                selectNumNow = rectFramePosList.Count - 1;
            }
            moveDistance = marginX;
            
        }
        scrollLimit = 350 * (allList[selectNumNow].Count - 3)+150;
        List<Vector3> nowPos = new List<Vector3>();
        List<Vector3> targetPos = new List<Vector3>();
        for (int i = 0; i < _rectTransforms.Count; i++)
        {
            //初期位置と目的位置を保存
            nowPos.Add(new Vector3(_rectTransforms[i].anchoredPosition3D.x, _rectTransforms[i].anchoredPosition3D.y, 0));
            targetPos.Add(new Vector3(_rectTransforms[i].anchoredPosition3D.x + moveDistance, _rectTransforms[i].anchoredPosition3D.y, 0));
        }
        while (moveLRTimer <= moveLRTime)
        {
            moveLRTimer += Time.deltaTime;
            //指定時間で徐々に目的の位置までずらす
            for (int i=0;i< _rectTransforms.Count; i++)
            {
                _rectTransforms[i].anchoredPosition3D = Vector3.Lerp(nowPos[i], targetPos[i], moveLRTimer / moveLRTime);
            }
            
            //ループを抜けるタイミングで移動を許可,配置を修正
            if (moveLRTimer >= moveLRTime)
            {
                changeCheck = true;
                mameTxt.text = mametishiki[selectNumNow];
                if (leftArrow_down)
                {
                    //押し出された端の大枠を逆側に再配置する
                    Vector3 fixPos = new Vector3(rectFramePosList[0].x, rectFramePosList[rectFramePosList.Count - 1].y, _rectTransforms[selectNum].anchoredPosition3D.z);
                    _rectTransforms[selectNumR].anchoredPosition3D = fixPos;
                    selectNumR -= 1;
                    selectNumL -= 1;
                    if (selectNumR < 0)
                    {
                        selectNumR = rectFramePosList.Count - 1;
                    }
                    if (selectNumL < 0)
                    {
                        selectNumL = rectFramePosList.Count - 1;
                    }
                }
                else if (rightArrow_down)
                {
                    
                    Vector3 fixPos = new Vector3(rectFramePosList[rectFramePosList.Count - 1].x, rectFramePosList[rectFramePosList.Count - 1].y, _rectTransforms[selectNum].anchoredPosition3D.z);
                    _rectTransforms[selectNumL].anchoredPosition3D = fixPos;
                    selectNumR += 1;
                    selectNumL += 1;
                    if (selectNumR >= rectFramePosList.Count)
                    {
                        selectNumR = 0;
                    }
                    if (selectNumL >= rectFramePosList.Count)
                    {
                        selectNumL = 0;
                    }
                }
                //y軸を初期化
                for(int i=0;i< _rectTransforms.Count; i++)
                {
                    _rectTransforms[i].anchoredPosition3D = new Vector3(_rectTransforms[i].anchoredPosition3D.x, rectFramePosList[i].y, _rectTransforms[i].anchoredPosition3D.z);
                }
                //スクロール位置を初期化
                scrollNow = 0;
            }
            yield return null;
        }
        
    }
}
