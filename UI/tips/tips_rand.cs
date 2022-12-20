using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tips_rand : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image titleImg;
    [SerializeField] Image thumbnail;
    [SerializeField] TextMeshProUGUI titleTxt;
    [SerializeField] TextMeshProUGUI descriptionTxt;

    Color32[] titleColor = new Color32[] { new Color32(150, 255, 90, 255), new Color32(90, 235, 255, 255), new Color32(255, 60, 190, 255) };
    
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
                                                           "ギミック：噴水","ギミック：竜巻","ギミック：ワープポイント"};
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
                                                                 "各地に点在しているなぞの場所。\nしばらく上に乗っているとどこかへ飛ばされる。\n見かけたら乗ってみよう。"};

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

    // Start is called before the first frame update
    void Start()
    {
        int randNum = Random.Range(0, titleColor.Length);
        switch (randNum)
        {
            case 0:
                setTips(playerImg, 0, playerTitleTxtList, playerDescriptionTxtList);
                break;
            case 1:
                setTips(systemImg, 1, systemTitleTxtList, systemDescriptionTxtList);
                break;
            case 2:
                setTips(itemImg, 2, itemTitleTxtList, itemDescriptionTxtList);
                break;
        }
    }
    void setTips(Sprite[] Sprites,int ListNum,List<string> tipsTitleTxt, List<string> tipsDescriptionTxt)
    {
        titleImg.color = titleColor[ListNum];
        int selectNum = Random.Range(0, tipsTitleTxt.Count);
        thumbnail.sprite = Sprites[selectNum];
        titleTxt.text = tipsTitleTxt[selectNum];
        descriptionTxt.text = tipsDescriptionTxt[selectNum];

    }
    IEnumerator test()
    {
        while (true)
        {
            int randNum = Random.Range(0, titleColor.Length);
            switch (randNum)
            {
                case 0:
                    setTips(playerImg, 0, playerTitleTxtList, playerDescriptionTxtList);
                    break;
                case 1:
                    setTips(systemImg, 1, systemTitleTxtList, systemDescriptionTxtList);
                    break;
                case 2:
                    setTips(itemImg, 2, itemTitleTxtList, itemDescriptionTxtList);
                    break;
            }
            yield return new WaitForSeconds(2);
        }
        
    }
}
