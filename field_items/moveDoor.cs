using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDoor : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float openMargin = 0;
    [SerializeField] float closeMargin = 0;
    [SerializeField] bool leftCheck = false;
    [SerializeField] Vector3 movePos = new Vector3(0, 0, 10);
    Vector3 defPos;
    Vector3 targetPos;
    float posTimer = Mathf.Sin(-1);
    bool openMarginCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        defPos = this.gameObject.transform.localPosition;
        if (leftCheck)
        {
            targetPos = defPos - movePos;
        }
        else
        {
            targetPos = defPos + movePos;
        }
        StartCoroutine(door(openMargin,closeMargin));
    }

    IEnumerator door(float openMargin,float closeMargin)
    {
        while (true)
        {
            posTimer += Time.deltaTime * moveSpeed;
            float nowTime = Mathf.Sin(posTimer) * 0.5f + 0.5f;
            this.transform.localPosition = Vector3.Lerp(defPos, targetPos, nowTime);
            if(posTimer > 1.570796f&&!openMarginCheck)
            {
                openMarginCheck = true;
                yield return new WaitForSeconds(openMargin);
            }
            if (posTimer > 4.712389f)
            {
                posTimer = -1.570796f;
                openMarginCheck = false;
                yield return new WaitForSeconds(closeMargin);
            }
            yield return null;
        }
        
    }
}
