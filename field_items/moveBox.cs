using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBox : MonoBehaviour
{
    [SerializeField] Vector3 movePos = new Vector3(20, 20, 20);
    Vector3 defaultPos;

    bool isPlayer, isPlayerEnter, isPlayerStay, isPlayerExit = false;
    bool floorCheck,moveCheck,wallCheck = false;
    float floorCounter,wallCounter,moveCounter = 0;
    float moveTimer = 0;
    GameObject floor;
    List<GameObject> walls = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = this.transform.position;
        floor = gameObject.transform.Find("moveFloor").gameObject;
        walls.Add(gameObject.transform.Find("wall_transparent").gameObject);
        walls.Add(gameObject.transform.Find("wall_transparent.1").gameObject);
        walls.Add(gameObject.transform.Find("wall_transparent.2").gameObject);
        walls.Add(gameObject.transform.Find("wall_transparent.3").gameObject);
        walls.Add(gameObject.transform.Find("moveCeil").gameObject);
    }

    void FixedUpdate()
    {
        if (moveTimer<=1)
        {
            floorEmission();
        }

        if (floorCounter >= 1)
        {
            if (!walls[0].activeSelf)
            {
                wallSet();
            }
            if (wallCounter <= 1)
            {
                wallColor();
            }
        }
        if (moveTimer >= 1)
        {
            floorEmissionnRelease();
            if (wallCheck)
            {
                wallColor();
            }
            else
            {
                wallRelease();
            }
        }
        movePanel();
    }
    void floorEmission()
    {
        if (IsPlayer() && floorCounter < 1&&!moveCheck)
        {
            floorCounter += Time.deltaTime;
        }
        else if (!IsPlayer()&& floorCounter < 1)
        {
            floorCounter = 0;
        }
        if (floorCounter >= 1)
        {
            floorCounter = 1;
            floorCheck = true;
        }

        //–Ú“I’n‚É’…‚¢‚½‚Æ‚«
        Color32 emiColor = Color32.Lerp(new Color32(0, 0, 0, 255), new Color32(0, 255, 255, 255), floorCounter);
        floor.GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
    }
    void floorEmissionnRelease()
    {
        if (moveCheck && floorCounter > 0)
        {
            floorCounter -= Time.deltaTime;
            if (floorCounter <= 0)
            {
                floorCounter = 0;
                floorCheck = false;
                moveCheck = false;
            }
        }
        Color32 emiColor = Color32.Lerp(new Color32(0, 0, 0, 255), new Color32(0, 255, 255, 255), floorCounter);
        floor.GetComponent<Renderer>().material.SetColor("_EmissionColor", emiColor);
    }
    void wallSet()
    {
        for(int i = 0; i < walls.Count; i++)
        {
            walls[i].SetActive(true);
        }
    }
    void wallColor()
    {
        if (wallCheck)
        {
            wallCounter -= Time.deltaTime*2;
        }
        else
        {
            wallCounter += Time.deltaTime*2;
        }
        if (wallCounter >= 1)
        {
            wallCheck = true;
        }
        else if (wallCounter <= 0)
        {
            wallCheck = false;
        }
        float wall_a = Mathf.Lerp(0, 65, wallCounter);
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].GetComponent<Renderer>().material.color=(new Color32(200, 220, 255, (byte)wall_a));
        }
    }
    void wallRelease()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].SetActive(false);
        }
    }
    void movePanel()
    {
        if (moveTimer < 1 && wallCheck)
        {
            moveTimer += Time.deltaTime/5;
            moveCheck = true;
        }
        if (!floorCheck && moveTimer >= 0&&!IsPlayer())
        {
            moveTimer -= Time.deltaTime / 5;
        }
        transform.position = Vector3.Lerp(defaultPos, defaultPos+movePos, moveTimer);
    }
    bool IsPlayer()
    {
        if (isPlayerEnter || isPlayerStay)
        {
            isPlayer = true;
        }
        if (isPlayerExit)
        {
            isPlayer = false;
        }
        isPlayerEnter = false;
        isPlayerStay = false;
        isPlayerExit = false;
        return isPlayer;
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "player":
                isPlayerEnter = true;
                break;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "player":
                isPlayerStay = true;
                break;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "player":
                  isPlayerExit = true;
                
                break;
        }
    }
}
