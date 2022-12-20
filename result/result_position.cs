using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result_position : MonoBehaviour
{
    [SerializeField] GameObject[] balls;
    [SerializeField] GameObject[] counts;
    [SerializeField] GameObject[] abilitys;
    [SerializeField] Image timeBack;
    [SerializeField] Image[] ballBacks;
    [SerializeField] Sprite[] backB;
    [SerializeField] Sprite[] backR;
    [SerializeField] Sprite[] backY;
    [SerializeField] Sprite[] backG;
    Sprite[][] backImgs = new Sprite[4][];
    Vector3[] ballPos = new Vector3[] { new Vector3 ( 0, 0, 0 ), new Vector3 ( 0, 0, 0 ), new Vector3 ( 0, 0, 0 ), new Vector3 ( 0, 0, 0 ) };
    Vector3[] ballScale = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Vector3[] abilityPos = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Vector3[] abilityScale = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Vector3[] countPos = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Vector3[] countScale = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
    Color32[] timeColors = new Color32[] { new Color32(180, 255, 255, 255), new Color32(255, 180, 255, 255), new Color32(255, 220, 180, 255), new Color32(255, 255, 180, 255) };

    // Start is called before the first frame update
    void Start()
    {
        backImgs[0] = backB;
        backImgs[1] = backR;
        backImgs[2] = backY;
        backImgs[3] = backG;
        for (int i = 0; i < balls.Length; i++)
        {
            ballPos[i] = balls[i].transform.position;
            ballScale[i] = balls[i].transform.localScale;
            abilityPos[i] = abilitys[i].transform.position;
            abilityScale[i] = abilitys[i].transform.localScale;
            countPos[i] = counts[i].transform.position;
            countScale[i] = counts[i].transform.localScale;
        }
        for (int i = 0; i < balls.Length; i++)
        {
            if (result_ranking.ranking[i] != 10)
            {
                //場所、スケールを変更
                int rank = result_ranking.ranking[i];
                balls[i].transform.position = ballPos[rank];
                balls[i].transform.localScale = ballScale[rank];
                abilitys[i].transform.position = abilityPos[rank];
                abilitys[i].transform.localScale = abilityScale[rank];
                //順位に応じて色を変更
                ballBacks[i].sprite = backImgs[i][rank];
                //1位のみ表示させるデータ
                if (rank == 0)
                {
                    counts[i].SetActive(true);
                    counts[i].transform.position = countPos[rank];
                    counts[i].transform.localScale = countScale[rank];
                    timeBack.color = timeColors[i];
                }
                else
                {
                    counts[i].SetActive(false);
                }
                
            }
            else
            {
                break;
            }
        }
    }
}
