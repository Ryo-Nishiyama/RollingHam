using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class moveTPS : MonoBehaviour
{
    public bool BotMode = false;
    public bool DebugMode = false;
    public bool fullEnhance = false;
    public float limitedSpeedInitial = 20f;
    public float limitedSpeed;
    float limitedSpeedUp;
    float limitedSpeedRatio = 1;
    float limitedSpeedAirRatio = 1;
    \
    public GroundCheck ground;
    public waterCheck _waterCheck;\
    public GameObject CameraVertical;
    public GameObject reverse;
    public lighting _lighting;
    public special _special;
    public mainAttack _mainAttack;
    public autoPlayer _autoPlayer;
    public Slider velocity_slider;
    public Image speedImg, speedImg2;
    [SerializeField] Image speedImg_back;
    public Image hp_bar, condition_bar;
    public Image count1, count2, count3, count4, count5;
    [SerializeField] Image[] countConstants;
    public Image waterGauge;
    [SerializeField] Image shield_img;
    public float overDir = 0f;
    //チャージ限界
    public float maxChargeInitial = 60f;
    public float maxCharge;
    public bool itemNumDes = false;
    //0:light,1:nomal,2:heavey,3:no turning,4:super charge
    public bool[] selectBall = new bool[] { false, false, false, false, false, false, false, false };\

    //sp移植
    public float spChargeTime = 3f;
    float timeCharge = 0f;
    public int spCount, lanceCount = 0;
    public float special_lerp = 0;
    public int abiCount = 0;
    public int useSpecial = 1;
    bool juziLcheck, juziRcheck = false;
    bool juziLcheckC, juziRcheckC = false;
    bool sp1, sp2, sp3, sp4, sp5, sp6, sp7 = false;
    float juziH = 0.0f;
    int[] _selectAbiList;
    int[] useSpecialList = new int[] { 1, 2, 3, 1, 6, 2, 2 };

    bool startFrame = true;

    float maxChargeTime = 0;
    float chargeCoolTime = 0;
    bool chargeCoolTime_flag = false;
    float maxChargeUp;
    public float addPower = 0;\
    public int shortJumpTime = 4;
    public float color_lerp_set;
    public TextMeshProUGUI enhanceText;
    bool[] enhanceDown = new bool[] { false, false, false, false, false };
    public Image[] enhanceImage;
    public Image[] tempImage;
    public Image[] enhanceTempImg;
    public float nowSpeed, nowHP, nowHP_pre;

    //最大HP
    public float maxHP = 100f;
    //上限を超えた時のブレーキ
    public float limitBrake = 1.25f;
    //ブレーキの効きやすさ
    public float ballBrakeInitial = 1.1f;
    float ballBrake;
    float ballBrakeTime = 10;
    float ballBrakeTimeInitial = 10;
    float ballBrakeTimeCount = 0;
    //自然停止
    public float ballBrake_nature = 1.03f;
    //加速
    public float ballVelocityInitial = 20f;
    public float ballVelocity;
    float ballVelocityUp;
    //空中加速
    public float ballVelocityAirInitial = 18f;
    float ballVelocityAir;
    float ballVelocityAirUp;
    //飛行上昇量
    public float flyingRate = 2.0f;
    //旋回
    public float ballTurningInitial = 100f;
    float ballTurning;
    float ballTurningUp;
    //チャージ、停止中の旋回
    public float ballTurningC = 2f;
    //ジャンプ力
    public float ballJumpPower = 7.0f;
    //重力
    public float ballGravity = 9.81f;

    //チャージ速度
    public float ballChargeInitial = 45.0f;
    float ballCharge;
    float ballChargePower = 0;
    //チャージの効果が完全になくなるまでの時間
    public float ballChargePowerTime = 3;
    float ballChargePowerTimeCount = 0;
    float ballChargeUp;
    //衝突ダメージ倍率
    float clashMitigation, crashIncrement = 1;
    //効果音
    public AudioClip a_enhanceGet, a_damage, a_recover, a_specialUsed, a_tempEnhance, a_changeBall, a_warp, a_move, a_inWater, a_down, a_carrot, a_itemNum, a_death, a_clash,a_chargeFire,a_bubble;
    [SerializeField] AudioClip[] a_chargeList;
    [SerializeField] AudioClip[] a_specialSE;
    [SerializeField] AudioClip[] a_abnormality;
    [SerializeField] AudioClip[] a_constant;

    public Vector3 UDdir;
    public float ballVel_now = 0;
    public float UDcounter = 0f;
    float dir, dir2 = 0f;
    public float radian = 0f;
    public float chargeCount = 0f;
    float textCounter = 0f;
    float deathTime, deathSin = 0f;
    float burnCount, frostbiteCount, paralysisCount, sleepCount, poisonCount, reverseCount = 0f;
    float burnRecover, frostbiteRecover, paralysisRecover, sleepRecover, poisonRecover, reverseRecover = 0f;
    [SerializeField] float[] conditionRecoverTime = new float[] { 10, 10, 10, 7.5f, 10, 15 };
    float natureRecover = 0f;
    bool isRecoverCheck;
    GameObject isRecover_eff;
    float burnDamage, sleepDamage, poisonDamage = 0f;
    float poisonDamageCounter = 1f;
    float randSizeTime = 30;
    int randSize = 1;
    float nowSize = 1f;
    float sizeChangeTimeSet = 5;
    float randSizeChange = 1;

    int jumpCount = 0;
    int jumpdelay = 0;
    int buttonJumpCount = 0;
    int groundPassTime = 1000;
    int itemDamage = 0;
    int itemEnhance, itemEnhance2, itemEnhance3, itemEnhance4, itemEnhance5, itemCollect = 0;
    //取得した各能力上昇の個数
    public int[] itemEnhanceCount = new int[] { 0, 0, 0, 0, 0 };
    float[] enhanceUp = new float[] { 0, 0, 0, 0, 0 };
    float[] bonusEnhance = new float[] { 0, 0, 0, 0, 0, 0 };
    float[] bonusEnhanceRate = new float[] { 0, 2.5f, 5.0f, 7.5f, 10.0f };
    float[] bonusEnhanceLerp = new float[] { 0.0f, 0.273f, 0.504f, 0.733f, 1.0f };
    int[] bonusEnhanceCheck = new int[] { 0, 0, 0, 0, 0 };
    int[] bonusEnhanceCheck_pre = new int[] { 0, 0, 0, 0, 0 };
    int[] itemEnhanceCount_pre = new int[] { 0, 0, 0, 0, 0 };
    bool[] enhanceTemps = new bool[] { false, false, false, false, false };
    float[] enhanceTempsTimer = new float[] { 0, 0, 0, 0, 0 };
    float enhanceTempsTime = 10;


    bool buttonOne, buttonOneUp = false;
    bool buttonOneKey, buttonOneKeyUp = false;
    bool isGround, isAccel, isJump = false;
    bool _isFire, _isThunder, _isIce, _isSleep, _isPoison = false;
    public bool nowCondition = false;
    bool conditionClear = false;
    bool upCheck = true;
    int GroundAwayTime = 0;
    bool groundPass = true;
    bool deathTimeCheck, deathEffCheck = false;
    bool[] deathDownCheck = new bool[] { false, false, false, false, false };
    bool carrotEnter, carrot_fakeEnter, carrot_maxEnter, carrot_randEnter, carrot_conditionEnter = false;

    public int itemNumCount = 0;
    bool isItemEnhanceEnter, isItemEnhance2Enter, isItemEnhance3Enter, isItemEnhance4Enter, isItemEnhance5Enter, isItemEnhanceAllEnter, isItemCollectEnter = false;

    bool isWarp, isWarpEnter, isWarpStay, isWarpExit = false;
    bool warpCheck = false;
    float warpCool = 0;
    float warpTime = 0;
    Vector3[] warpPos = new Vector3[] { new Vector3(226.25f, 65, -8), new Vector3(317.5f, -10, -63), new Vector3(150, -18, 215),
                                        new Vector3(288,10,255),new Vector3(-68,20,-65),new Vector3(-44,1,293.5f),
                                        new Vector3(7,34,176),new Vector3(0,50,0),new Vector3(120,150,120)};
    public bool isWater = false;
    bool isWaterEnter, isWaterStay, isWaterExit = false;
    bool isBubble, isBubbleEnter, isBubbleStay, isBubbleExit = false;
    float waterTime = 0;
    float waterTimeLimit = 15;
    float waterTimeCounter = 0;
    float waterSpeedRate = 1;
    float waterGravityRate = 1;
    float waterAccelRate = 1;
    float waterTurnRate = 1;
    float swimmingRate = 1;

    public bool shieldCheck;
    bool shieldStart;
    float shieldTime = 0;
    float shieldLimit = 15;
    GameObject shield_eff;

    public bool detectionItemNum, detectionItemNum2, detectionItemNum3 = false;
    bool isDespone, isHitStop = false;
    float elapsedTime;
    public bool burn, frostbite, paralysis, sleep, nightmare, poison, reverseOp = false;

    float mouseWheel = 0;
    float mouseWheel_old = 0;
    public bool maru, maru_w = false;
    bool batu, batu_s = false;
    public bool _maruBot = false;
    public bool _batuBot = false;
    bool R1, R1_r = false;
    public bool overR1 = false;
    public bool R2, R2_e = false;
    bool chargeCheck = false;

    bool left_a, right_d, inpur_w, input_s = false;
    public bool _R2Bot = false;
    float _UDcounterBot = 0;
    public float _axis_y = 0.0f;
    float UDstick = 0;
    float hp_lerp = 1;
    float speedSet = 0;

    bool airCheck = false;
    [System.NonSerialized]
    public int botNum = 0;

    Material originalMaterial;
    Renderer render;
    Rigidbody rb;
    public AudioSource audioSource;
    public AudioSource audioSource_move;
    public AudioSource audioSource_dush;
    public AudioSource audioSource_charge;
    public AudioSource audioSource_water;
    float moveVolume, dushVolume = 0;
    List<GameObject> enhanceParticle = new List<GameObject>();
    List<GameObject> tempParticle = new List<GameObject>();
    List<GameObject> carrotParticle = new List<GameObject>();
    [SerializeField] GameObject sunflowerParticle;
    [SerializeField] GameObject[] e_Heal;
    [SerializeField] GameObject e_changeBall;
    [SerializeField] GameObject e_shield, shieldObj;
    [SerializeField] GameObject e_death;
    [SerializeField] GameObject e_hitDamage;
    public bool changeBall;
    public GameObject e_warp;
    GameObject _e_warp;
    float warpCount = 1;
    Vector3 enhanceParticleAngle = new Vector3(-90f, 0f, 0f);
    [SerializeField] ParticleSystem[] constantEffs;
    ParticleSystem.MainModule[] constantEffsMain = new ParticleSystem.MainModule[5]; 

    Color color_1 = new Color32(40, 180, 50, 255);
    Color color_2 = new Color32(230, 220, 40, 255);
    //Color color_3 = new Color32(240, 190, 50, 255);
    Color color_4 = new Color32(220, 20, 20, 255);

    //ボールの色を保存
    Color32 ballColor;
    Color32[] ballColors = new Color32[] { new Color32(0, 255, 255, 40), new Color32(0, 255, 0, 40),
                                           new Color32(100, 30, 0, 40), new Color32(150, 0, 255, 40),
                                           new Color32(255, 255, 0, 40), new Color32(255, 0, 50, 40),
                                           new Color32(0, 0, 0, 40), new Color32(255, 255, 255, 40)};

    bool lanceAttackCheck = false;
    bool targetCheck = false;
    Vector3 targetPoint;

    bool pause = false;
    LayerMask layerMaskWall;

    List<Vector3> sponePosList = new List<Vector3>() { new Vector3(58, 8.5f, 30),new Vector3(-4, 54f, 8.5f),new Vector3(65, 0.5f, 199.25f),
                                                     new Vector3(260,0.5f,245)};
    List<Vector3> sponeRotateList = new List<Vector3>() { new Vector3(0, 0, 0), new Vector3(0, -90, 0), new Vector3(0, 90, 0),
                                                          new Vector3(0, 90, 0)};
    [SerializeField] bool randStartPos = false;

    Coroutine col_speedImgChange = null;
    Coroutine col_chargeImgChange = null;
    private void OnDestroy()
    {
        _lighting=null;
        _special=null;
        _mainAttack=null;
        _autoPlayer=null;
    }
    private void Start()
    {
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_speed"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_brake"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_turning"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_charge"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_accel"));
        enhanceParticle.Add((GameObject)Resources.Load("particle\\enhanceParticle_all"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_speed"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_brake"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_turning"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_charge"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_accel"));
        tempParticle.Add((GameObject)Resources.Load("particle\\temp\\tempParticle_all"));
        carrotParticle.Add((GameObject)Resources.Load("particle\\carrot\\carrot_CFX4"));
        carrotParticle.Add((GameObject)Resources.Load("particle\\carrot\\carrotFake_CFX4"));
        carrotParticle.Add((GameObject)Resources.Load("particle\\carrot\\carrotMax_CFX4"));
        carrotParticle.Add((GameObject)Resources.Load("particle\\carrot\\carrotRand_CFX4"));
        carrotParticle.Add((GameObject)Resources.Load("particle\\carrot\\carrotCondition_CFX4"));
        //メインモジュールの取り出し
        for(int i=0;  i< constantEffs.Length; i++)
        {
            constantEffsMain[i] = constantEffs[i].main;
        }
        
        //ゲーム開始時位置と向きをランダムに設定する
        if (randStartPos)
        {
            int randPosNum = Random.Range(0, sponePosList.Count);
            this.transform.position = sponePosList[randPosNum];
            this.transform.eulerAngles = sponeRotateList[randPosNum];
        }
        if (transform.name == "mainBall_Bot.0")
        {
            botNum = 0;
            if (CPUSet.cpuCount < 1)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (transform.name == "mainBall_Bot.1")
        {
            botNum = 1;
            if (CPUSet.cpuCount < 2)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (transform.name == "mainBall_Bot.2")
        {
            botNum = 2;
            if (CPUSet.cpuCount < 3)
            {
                this.gameObject.SetActive(false);
            }
        }
        e_changeBall.transform.localScale = new Vector3(3, 3, 3);
        //他シーンで得た強化量を代入
        if (!DebugMode)
        {
            //レースモードの時セットした値を代入
            //それ以外の時は他シーンで得た値をセット
            if("game"!=SceneManager.GetActiveScene().name)
            {
                if (!BotMode)
                {
                    SetEnhance(GameFinish.EnhanceCount);
                    itemNumCount = GameFinish.NumCount;
                }
                else
                {
                    SetEnhance(GameFinish.EnhanceBotCount[botNum]);
                    itemNumCount = GameFinish.NumBotCount[botNum];
                }
            }
            Debug.Log(raceSet.raceMode);
            if(raceSet.raceMode&&("race" == SceneManager.GetActiveScene().name|| "race2" == SceneManager.GetActiveScene().name))
            {
                if (!BotMode)
                {
                    SetEnhance(GameFinish.EnhanceCountInitial);
                }
                else
                {
                    SetEnhance(GameFinish.EnhanceBotCountInitial[botNum]);
                }
            }
        }
        Debug.Log(string.Join(", ", GameFinish.EnhanceCountInitial));
        if (fullEnhance)
        {
            debug_enhanceSet();
        }
        //ビューで設定した向きを向くようにする
        radian = this.gameObject.transform.localEulerAngles.y;
        //正面を向かせる
        UDdir = transform.forward;

        BallSelect();
        
        render = this.GetComponent<Renderer>();
        rb = this.GetComponent<Rigidbody>();
        originalMaterial = new Material(render.material);
        rb.useGravity = false;
        audioSource = this.GetComponent<AudioSource>();
        //HPを同期
        nowHP = maxHP;
        
        limitedSpeedUp = limitedSpeedInitial / 20;
        maxChargeUp = maxChargeInitial / 20;
        ballVelocityUp = ballVelocityInitial / 20;
        ballVelocityAirUp = ballVelocityAirInitial / 20;
        ballTurningUp = -ballTurningInitial / 50;
        ballChargeUp = ballChargeInitial / 10

        //マスクするレイヤーの選択
        layerMaskWall = 1 << LayerMask.NameToLayer("wall");

        _selectAbiList = selectBallSet.selectAbiList;

        useSpecial = useSpecialList[_selectAbiList[abiCount]];
        if (!DebugMode)
        {
            for (int i = 0; i < selectBall.Length; i++)
            {
                selectBall[i] = false;
            }
            selectBall[selectBallSet.selectHam] = true;
            BallSelect();
        }
        if (BotMode&&!DebugMode)
        {
            for (int i = 0; i < selectBall.Length; i++)
            {
                selectBall[i] = false;
            }
            selectBall[selectBallSet.selectBotHam[botNum]] = true;
            BallSelect();
            //探索モードの時自動強化
            if (!raceSet.raceMode)
            {
                StartCoroutine(bot_autoEnhance(Random.Range(1.0f, 5.0f), Random.Range(30, 100)));
            }
            
        }
        for (int i = 0; i < selectBall.Length; i++)
        {
            if (selectBall[i])
            {
                ballColor = ballColors[i];
                render.material.color = ballColor;
                break;
            }
        }
        //一定強化エフェクトを初期化
        for(int i = 0; i < constantEffs.Length; i++)
        {
            SetBonusEff(i, 0);
        }
        EnhanceCheck();
        startFrame = false;
    }
    RaycastHit hit;
    Vector3 alwaysRotateR, alwaysRotateL, alwaysRotateR2, alwaysRotateL2 = new Vector3(999, 999, 999);
    float roadWidth;
    void SetEnhance(int[] enhances)
    {
        for(int i = 0; i < enhances.Length; i++)
        {
            itemEnhanceCount[i] = enhances[i];
        }
    }
    void FixedUpdate()
    {
        if (countdown.startGameCheck)
        {
            //前フレームのHP,強化状態を保存
            nowHP_pre = nowHP;

            itemEnhanceCount_pre[0] = itemEnhanceCount[0];
            itemEnhanceCount_pre[1] = itemEnhanceCount[1];
            itemEnhanceCount_pre[2] = itemEnhanceCount[2];
            itemEnhanceCount_pre[3] = itemEnhanceCount[3];
            itemEnhanceCount_pre[4] = itemEnhanceCount[4];
            if (isHitStop)
            {
                elapsedTime += Time.unscaledDeltaTime;
                if (elapsedTime > 0.05f)
                {
                    hitStopEnd();
                }
            }
            if (!BotMode)
            {
                playSE();
            }
            isGround = ground.IsGround();
            IsWater();
            IsBubble();
            carrot();
            shieldControl();
            if (Physics.Raycast(gameObject.transform.position, transform.right, out hit, Mathf.Infinity, layerMaskWall))
            {
                alwaysRotateR = hit.point - this.gameObject.transform.position;
            }
            if (Physics.Raycast(gameObject.transform.position, -transform.right, out hit, Mathf.Infinity, layerMaskWall))
            {
                alwaysRotateL = hit.point - this.gameObject.transform.position;
            }
            if (Physics.Raycast(gameObject.transform.position, transform.forward + transform.right, out hit, Mathf.Infinity, layerMaskWall))
            {
                alwaysRotateR2 = hit.point - this.gameObject.transform.position;
            }
            if (Physics.Raycast(gameObject.transform.position, transform.forward - transform.right, out hit, Mathf.Infinity, layerMaskWall))
            {
                alwaysRotateL2 = hit.point - this.gameObject.transform.position;
            }
            //道幅取得
            roadWidth = Mathf.Abs(alwaysRotateR.x) + Mathf.Abs(alwaysRotateR.z) + Mathf.Abs(alwaysRotateL.x) + Mathf.Abs(alwaysRotateL.z);
            if (changeBall)
            {
                BallSelect();
                //ボーナス強化値も補正し直す
                BonusEnhanceSet_speed();
                BonusEnhanceSet_brake();
                BonusEnhanceSet_turning();
                BonusEnhanceSet_charge();
                BonusEnhanceSet_accel();
            }

            SetLocalGravity();
            EnhanceCheck();
            spCharge();

            if (DebugMode)
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    nowHP -= 10;
                }
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    nowHP += 10;
                }
                if (Input.GetKey(KeyCode.U))
                {
                    maxHP += 1;
                }
                if (Input.GetKeyDown("1") && Input.GetKey("left shift") && itemEnhanceCount[0] > 0)
                {
                    itemEnhanceCount[0] -= 1;
                }
                else if (Input.GetKeyDown("1") && itemEnhanceCount[0] < 20)
                {
                    itemEnhanceCount[0] += 1;
                }
                if (Input.GetKeyDown("2") && Input.GetKey("left shift") && itemEnhanceCount[1] > 0)
                {
                    itemEnhanceCount[1] -= 1;
                }
                else if (Input.GetKeyDown("2") && itemEnhanceCount[1] < 20)
                {
                    itemEnhanceCount[1] += 1;
                }
                if (Input.GetKeyDown("3") && Input.GetKey("left shift") && itemEnhanceCount[2] > 0)
                {
                    itemEnhanceCount[2] -= 1;
                }
                else if (Input.GetKeyDown("3") && itemEnhanceCount[2] < 20)
                {
                    itemEnhanceCount[2] += 1;
                }
                if (Input.GetKeyDown("4") && Input.GetKey("left shift") && itemEnhanceCount[3] > 0)
                {
                    itemEnhanceCount[3] -= 1;
                }
                else if (Input.GetKeyDown("4") && itemEnhanceCount[3] < 20)
                {
                    itemEnhanceCount[3] += 1;
                }
                if (Input.GetKeyDown("5") && Input.GetKey("left shift") && itemEnhanceCount[4] > 0)
                {
                    itemEnhanceCount[4] -= 1;
                }
                else if (Input.GetKeyDown("5") && itemEnhanceCount[4] < 20)
                {
                    itemEnhanceCount[4] += 1;
                }
                buttonOne = Input.GetButton("Fire_3");
                buttonOneUp = Input.GetButtonUp("Fire_3");
                buttonOneKey = Input.GetKey(KeyCode.Space);
                buttonOneKeyUp = Input.GetKeyUp(KeyCode.Space);
            }
            if (nowHP > 0)
            {
                if (!sleep)
                {
                    ball_hit();
                }
                condition();
            }
            if (!isWater)
            {
                Flying();
            }
            if (isWater)
            {
                Swimming();
            }

            despone();
            gameEvent();


            if (BotMode)
            {
                moveBot();
            }
            else
            {
                _autoPlayer.enabled = false;
            }
            if (nowHP > 0)
            {
                IsWarp();
                warp();
                if (!sleep)
                {
                    MoveFoward();
                    eyeDirection();
                    Jump();
                }
            }
            //moveforward計算後常に更新
            nowSpeed = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) + Mathf.Abs(rb.velocity.z);
            damage();
            effect();
            damageDown();

            if (!BotMode)
            {
                damageEffect();
            }
            HealEffect();
        }
            
    }
    void Update()
    {
        if (countdown.startGameCheck)
        {
            SPconsum();
            avirityChange();
            //itemNum();

            enhanceTemp();
            if (!BotMode)
            {
                inputGet();
                //ポーズモードの時SEを一時停止する
                if (gamePause.pause && !pause)
                {
                    audioPause();
                    pause = true;
                }
                else if (!gamePause.pause && pause)
                {
                    audioPlay();
                    pause = false;
                }
                //ゲームが止まったとき効果音も止める
                if (Time.timeScale == 0)
                {
                    audioPause();
                }
            }
            testScript();
        }
    }
    private void LateUpdate()
    {
        
        //velocityRotate();
        if (!BotMode)
        {
            enhanceCounter();
            gaugeFill();
        }
    }
    void inputGet()
    {
        R2 = Input.GetButton("Fire_R2");
        R2_e = Input.GetKey(KeyCode.E);
        R1 = Input.GetButtonDown("Fire_R1");
        R1_r = Input.GetKeyDown(KeyCode.R);
        
        left_a = Input.GetKey(KeyCode.A);
        right_d = Input.GetKey(KeyCode.D);
        inpur_w = Input.GetKey(KeyCode.W);
        input_s = Input.GetKey(KeyCode.S);
        UDstick = Input.GetAxis("Vertical Stick-L");

        maru = true;
        //maru = Input.GetButton("Fire_2");
        maru_w = true;
        //maru_w = Input.GetKey(KeyCode.W);
    }
    void audioPause()
    {
        audioSource.Pause();
        audioSource_move.Pause();
        audioSource_dush.Pause();
        audioSource_charge.Pause();
        audioSource_water.Pause();
    }
    void audioPlay()
    {
        audioSource.Play();
        audioSource_move.Play();
        audioSource_dush.Play();
        //チャージ最大状態で再開したときチャージ音は鳴らさない
        if (maxChargeTime <= 0)
        {
            audioSource_charge.Play();
        }
        else
        {
            audioSource_charge.Stop();
        }
        if (isWater)
        {
            audioSource_water.Play();
        }
        else
        {
            audioSource_water.Stop();
        }
        
    }
    void testScript()
    {
        if (targetCheck)
        {
            //自オブジェクトとターゲットとの角度を計算,マイナスの時右旋回、プラスの時左旋回させる、値はおそらくずれるため都度修正
            Quaternion targetQrot = Quaternion.LookRotation(targetPoint - this.transform.position);
            Vector3 targetEuler = targetQrot.eulerAngles;
        }
    }

    /// <summary>
    /// HP,condition gauge change
    /// </summary>
    void gaugeFill()
    {
        float condition_lerp;
        //状態異常ゲージの管理
        if (conditionClear)
        {
            condition_bar.fillAmount = 0;
            conditionClear = false;
        }
        else if (burn)
        {
            condition_lerp = burnRecover / conditionRecoverTime[0];
            condition_bar.fillAmount = Mathf.Lerp(1, 0, condition_lerp);
        }
        else if (paralysis)
        {
            condition_lerp = paralysisRecover / conditionRecoverTime[1];
            condition_bar.fillAmount = Mathf.Lerp(1, 0, condition_lerp);
        }
        else if (frostbite)
        {
            condition_lerp = frostbiteRecover / conditionRecoverTime[2];
            condition_bar.fillAmount = Mathf.Lerp(1, 0, condition_lerp);
        }
        else if (sleep)
        {
            condition_lerp = sleepRecover / conditionRecoverTime[3];
            condition_bar.fillAmount = Mathf.Lerp(1, 0, condition_lerp);
        }
        else if (poison)
        {
            condition_lerp = poisonRecover / conditionRecoverTime[4];
            condition_bar.fillAmount = Mathf.Lerp(1, 0, condition_lerp);
        }
        else if (reverseOp)
        {
            condition_lerp = reverseRecover / conditionRecoverTime[5];
            condition_bar.fillAmount = Mathf.Lerp(1, 0, condition_lerp);
        }
        hp_bar.fillAmount = Mathf.Lerp(0, 1, hp_lerp);
        //HPbar色変更
        if (hp_lerp > 0.65f)
        {
            hp_bar.GetComponent<Image>().color = Color.Lerp(color_2, color_1, (hp_lerp - 0.65f) * 4f);
        }
        else if (hp_lerp > 0.2f)
        {
            hp_bar.GetComponent<Image>().color = Color.Lerp(color_4, color_2, (hp_lerp - 0.2f) * 4f);
        }
        speedImg2.fillAmount = Mathf.Lerp(0, 1, speedSet);
        moveVolume = Mathf.Lerp(0, 1, speedSet);
        if (color_lerp_set > 1f)
        {
            color_lerp_set = 1f;
        }

        //スピードメーター系が最大値のとき点滅させる
        if (color_lerp_set >= 1f && col_chargeImgChange==null)
        {
            col_chargeImgChange = StartCoroutine(chargeImgChangeColor(speedImg.color, new Color32(255, 255, 255, 255), new Color32(255, 255, 150, 200)));
        }
        else if (color_lerp_set < 1 && col_chargeImgChange != null)
        {
            col_chargeImgChange = null;
        }
        if (speedSet >= 1 && col_speedImgChange == null)
        {
            col_speedImgChange = StartCoroutine(speedImgChangeColor(speedImg2.color, new Color32(0, 120, 255, 153)));
        }
        if (speedSet < 1 && col_speedImgChange != null)
        {
            col_speedImgChange = null;
        }
        //チャージのクールタイム中は背景を暗くする
        if (chargeCoolTime > 0 && !chargeCoolTime_flag)
        {
            chargeCoolTime_flag = true;
            StartCoroutine(coolTimeFlash(new Color32(255, 255, 255, 255), 0.5f, 0.7f));
        }
        else if (chargeCoolTime <= 0 && chargeCoolTime_flag)
        {
            chargeCoolTime_flag = false;
        }
        if ((_lighting.isCharge||burn) && nowHP > 1)
        {
            speedImg.fillAmount = color_lerp_set;
        }
        else if (chargeCount > 0f)
        {
            speedImg.fillAmount = Mathf.Lerp(0, 1, color_lerp_set);
        }
        else
        {
            speedImg.fillAmount = 0;
        }
        if (waterTime > 0)
        {
            waterGauge.fillAmount = Mathf.Lerp(1, 0, waterTime/waterTimeLimit);
        }
    }
    IEnumerator coolTimeFlash(Color32 originalColor,float startLuminance,float endLuminance)
    {
        float colorLevel = 0;
        float colorTimer = 0;
        while (chargeCoolTime_flag)
        {
            colorTimer += Time.deltaTime * 12;
            colorLevel = Mathf.Cos(colorTimer) * 0.5f + 0.5f;
            speedImg_back.color = Color.HSVToRGB(0, 0, Mathf.Lerp(startLuminance, endLuminance, colorLevel));
            Debug.Log(colorLevel);
            yield return null;
        }
        //色を戻して終了
        speedImg_back.color = originalColor;
        yield break;
    }
    IEnumerator speedImgChangeColor(Color32 originalColor,Color32 nextColor,int flashCount = 2)
    {
        float colorLevel = 0;
        float colorTimer = 0;
        float marginTime = 0.5f;
        while (speedSet >= 1)
        {
            colorTimer += Time.deltaTime * 12;
            colorLevel = Mathf.Cos(colorTimer) * 0.5f + 0.5f;
            speedImg2.color = Color32.Lerp(nextColor, originalColor, colorLevel);
            if (colorTimer >= Mathf.PI * flashCount * 2)
            {
                colorTimer = 0;
                yield return new WaitForSeconds(marginTime);
            }
            yield return null;
        }
        speedImg2.color = originalColor;
        yield break;
    }
    IEnumerator chargeImgChangeColor(Color32 originalColor_main, Color32 originalColor_back,Color32 nextColor_main)
    {
        //255,255,150,200
        float colorLevel = 0;
        float colorTimer = 0;
        float marginTime = 0.15f;
        speedImg.color = nextColor_main;
        while (color_lerp_set >= 1)
        {
            colorTimer += Time.deltaTime * 6;
            colorLevel = Mathf.Cos(colorTimer) * 0.5f + 0.5f;
            speedImg_back.color = Color.HSVToRGB(colorLevel, 0.25f, 1);
            yield return new WaitForSeconds(marginTime);
        }
        speedImg.color = originalColor_main;
        if (!chargeCoolTime_flag)
        {
            speedImg_back.color = originalColor_back;
        }
        yield break;
    }
    void debug_enhanceSet()
    {
        itemEnhanceCount[0] = 20;
        itemEnhanceCount[1] = 20;
        itemEnhanceCount[2] = 20;
        itemEnhanceCount[3] = 20;
        itemEnhanceCount[4] = 20;
    }
    /// <summary>
    /// 強化値を含むボールの性能を選んだもの、変化したものに更新
    /// </summary>
    void BallSelect()
    {
        //軽量空中型
        if (selectBall[0])
        {
            spChargeTime = 2;
            limitedSpeedInitial =9f;
            limitedSpeedRatio = 2;
            limitedSpeedAirRatio = 4;
            //それぞれのHP割合でnowHPを更新
            float nowHPRatio = nowHP / maxHP;
            maxHP = 50;
            nowHP = Mathf.Floor(maxHP * nowHPRatio);
            nowHP_pre = nowHP;
            limitBrake = 1.1f;
            //ブレーキ力
            ballBrakeInitial = 1.025f;
            ballBrake_nature = 1.05f;
            //止まるときマイナスする時間
            ballBrakeTimeInitial = 0.35f;
            ballVelocityInitial = 25;
            ballVelocityAirInitial = 20;
            flyingRate = 1;
            ballTurningInitial = 150;
            ballTurningC = 2.5f;
            ballJumpPower = 22.5f;
            ballGravity = 3.31f;
            ballChargeInitial = 15;
            maxCharge = 30;
            ballChargePowerTime = 3;

            clashMitigation = 1.2f;
            crashIncrement = 1.2f;

            waterTimeLimit = 20;
            waterSpeedRate = 1.5f;
            waterGravityRate = 0.25f;
            waterAccelRate = 1.5f;
            waterTurnRate = 0.5f;
            swimmingRate = 1;

            //強化アイテム上昇幅補正
            limitedSpeedUp = limitedSpeedInitial / 20;
            ballVelocityUp = ballVelocityInitial / 20;
            ballVelocityAirUp = ballVelocityAirInitial / 20;
            ballTurningUp = -ballTurningInitial / 50;
            ballChargeUp = ballChargeInitial / 10;
        }
        //基準値
        else if (selectBall[1])
        {
            spChargeTime = 3;
            limitedSpeedInitial = 15f;
            limitedSpeedRatio = 2;
            limitedSpeedAirRatio = 2;
            float nowHPRatio = nowHP / maxHP;
            maxHP = 100;
            nowHP = Mathf.Floor(maxHP * nowHPRatio);
            nowHP_pre = nowHP;
            ballBrakeInitial = 2f;
            ballBrake_nature = 1.025f;
            ballBrakeTimeInitial = 0.5f;
            ballVelocityInitial = 17.5f;
            ballVelocityAirInitial = 10;
            flyingRate = 2;
            ballTurningInitial = 100;
            ballTurningC = 1.25f;
            ballJumpPower = 15;
            ballGravity = 9.81f;
            ballChargeInitial = 12.5f;
            maxCharge = 35;
            ballChargePowerTime = 3;

            limitedSpeedUp = limitedSpeedInitial / 20;
            ballVelocityUp = ballVelocityInitial / 20;
            ballVelocityAirUp = ballVelocityAirInitial / 20;
            ballTurningUp = -ballTurningInitial / 50;
            ballChargeUp = ballChargeInitial / 10;

            waterSpeedRate = 1;
            waterGravityRate = 0.25f;
            waterAccelRate = 0.8f;
            waterTurnRate = 1f;
            swimmingRate = 1.5f;
        }
        //重量級地上型
        else if (selectBall[2])
        {
            spChargeTime = 4;
            limitedSpeedInitial = 22.5f;
            limitedSpeedRatio = 3;
            limitedSpeedAirRatio = 1;
            float nowHPRatio = nowHP / maxHP;
            maxHP = 200;
            nowHP = Mathf.Floor(maxHP * nowHPRatio);
            nowHP_pre = nowHP;
            ballBrakeInitial = 1.5f;
            ballBrake_nature = 1.01f;
            ballBrakeTimeInitial = 1;
            ballVelocityInitial = 12;
            ballVelocityAirInitial = 5;
            flyingRate = 2.5f;
            ballTurningInitial = 400;
            ballTurningC = 1.25f;
            ballJumpPower = 7.5f;
            ballGravity = 9.31f;
            ballChargeInitial = 1;
            maxCharge = 10;
            ballChargePowerTime = 2;

            limitedSpeedUp = limitedSpeedInitial / 20;
            ballVelocityUp = ballVelocityInitial / 20;
            ballVelocityAirUp = ballVelocityAirInitial / 20;
            ballTurningUp = -ballTurningInitial / 50;
            ballChargeUp = ballChargeInitial / 10;

            waterSpeedRate = 0.5f;
            waterGravityRate = 0.75f;
            waterAccelRate = 0.8f;
            waterTurnRate = 1f;
            swimmingRate = 2;
        }
        //チャージ型
        else if (selectBall[3])
        {
            //チャージカウントをリセット
            chargeCount = 0;

            spChargeTime = 5;
            limitedSpeedInitial = 25f;
            limitedSpeedRatio = 1.5f;
            limitedSpeedAirRatio = 1.5f;
            float nowHPRatio = nowHP / maxHP;
            maxHP = 100;
            nowHP = Mathf.Floor(maxHP * nowHPRatio);
            nowHP_pre = nowHP;
            limitBrake = 1.05f;
            ballBrakeInitial = 0.75f;
            ballBrake_nature = 1.3f;
            ballBrakeTimeInitial = 0.25f;
            ballVelocityInitial = 15f;
            ballVelocityAirInitial = 2.5f;
            flyingRate = 2;
            ballTurningInitial = 150;
            ballTurningC = 2;
            ballJumpPower = 10;
            ballGravity = 9.81f;
            ballChargeInitial = 10;
            maxCharge = 100;
            ballChargePowerTime = 6;

            limitedSpeedUp = limitedSpeedInitial / 20;
            ballVelocityUp = ballVelocityInitial / 20;
            ballVelocityAirUp = ballVelocityAirInitial / 20;
            ballTurningUp = -ballTurningInitial / 50;
            ballChargeUp = ballChargeInitial / 5;

            waterSpeedRate = 1;
            waterGravityRate = 0.25f;
            waterAccelRate = 0.8f;
            waterTurnRate = 1f;
            swimmingRate = 2;
        }
        //チャージ不可基礎高め
        else if (selectBall[4])
        {
            spChargeTime = 3;
            limitedSpeedRatio = 2;
            limitedSpeedAirRatio = 2;
            limitedSpeedInitial = 17.5f;
            float nowHPRatio = nowHP / maxHP;
            maxHP = 150;
            nowHP = Mathf.Floor(maxHP * nowHPRatio);
            nowHP_pre = nowHP;
            limitBrake = 1.35f;
            ballBrakeInitial = 3f;
            ballBrake_nature = 1.005f;
            ballBrakeTimeInitial = 0.8f;
            ballVelocityInitial = 20f;
            ballVelocityAirInitial = 10;
            flyingRate = 2;
            ballTurningInitial = 150;
            ballTurningC = 2;
            ballJumpPower = 15;
            ballGravity = 9.81f;
            ballChargeInitial = 10;
            maxCharge =0;
            ballChargePowerTime = 0;

            limitedSpeedUp = limitedSpeedInitial / 20;
            ballVelocityUp = ballVelocityInitial / 20;
            ballVelocityAirUp = ballVelocityAirInitial / 20;
            ballTurningUp = -ballTurningInitial / 50;
            ballChargeUp = ballChargeInitial / 10;

            waterSpeedRate = 1;
            waterGravityRate = 0.5f;
            waterAccelRate = 0.8f;
            waterTurnRate = 1f;
            swimmingRate = 2;
        }
        //ブレーキ滑る
        else if (selectBall[5])
        {
            spChargeTime = 2;
            limitedSpeedInitial = 20f;
            limitedSpeedRatio = 2;
            limitedSpeedAirRatio = 2;
            float nowHPRatio = nowHP / maxHP;
            maxHP = 120;
            nowHP = Mathf.Floor(maxHP * nowHPRatio);
            nowHP_pre = nowHP;
            ballBrakeInitial = 0.005f;
            ballBrake_nature = 1.025f;
            ballBrakeTimeInitial = 0.01f;
            ballVelocityInitial = 25f;
            ballVelocityAirInitial = 15;
            flyingRate = 2;
            ballTurningInitial = 150;
            ballTurningC = 2;
            ballJumpPower = 20;
            ballGravity = 9.81f;
            ballChargeInitial = 12.5f;
            maxCharge = 30;
            ballChargePowerTime = 2.5f;

            limitedSpeedUp = limitedSpeedInitial / 20;
            ballVelocityUp = ballVelocityInitial / 20;
            ballVelocityAirUp = ballVelocityAirInitial / 20;
            ballTurningUp = -ballTurningInitial / 50;
            ballChargeUp = ballChargeInitial / 10;

            waterSpeedRate = 1;
            waterGravityRate = 0.25f;
            waterAccelRate = 0.8f;
            waterTurnRate = 1f;
            swimmingRate = 1.5f;
        }
        //走行中旋回ほぼ無し、ビタビタに止まる
        else if (selectBall[6])
        {
            spChargeTime = 3;
            limitedSpeedInitial = 17.5f;
            limitedSpeedRatio = 2;
            limitedSpeedAirRatio = 2;
            float nowHPRatio = nowHP / maxHP;
            maxHP = 80;
            nowHP = Mathf.Floor(maxHP * nowHPRatio);
            nowHP_pre = nowHP;
            ballBrakeInitial = 50f;
            ballBrake_nature = 1.025f;
            ballBrakeTimeInitial = 50;
            ballVelocityInitial = 50f;
            ballVelocityAirInitial = 50f;
            flyingRate = 2;
            ballTurningInitial = 75;
            //高くして普段曲がらないように
            ballTurningC = 50000;
            ballJumpPower = 17.5f;
            ballGravity = 9.81f;
            ballChargeInitial = 12.5f;
            maxCharge = 10;
            ballChargePowerTime = 0.1f;

            limitedSpeedUp = limitedSpeedInitial / 20;
            ballVelocityUp = ballVelocityInitial / 20;
            ballVelocityAirUp = ballVelocityAirInitial / 20;
            ballTurningUp = -ballTurningInitial / 50;
            ballChargeUp = ballChargeInitial / 10;

            waterSpeedRate = 1;
            waterGravityRate = 0.25f;
            waterAccelRate = 0.8f;
            waterTurnRate = 1f;
            swimmingRate = 1.25f;
        }
        //つよい、紙耐久、スキル溜まらない
        else if (selectBall[7])
        {
            spChargeTime = 5;
            limitedSpeedInitial = 20f;
            limitedSpeedRatio = 2;
            limitedSpeedAirRatio = 2.5f;
            float nowHPRatio = nowHP / maxHP;
            maxHP = 10;
            nowHP = Mathf.Floor(maxHP * nowHPRatio);
            nowHP_pre = nowHP;
            ballBrakeInitial = 3f;
            ballBrake_nature = 1.025f;
            ballBrakeTimeInitial = 0.5f;
            ballVelocityInitial = 30f;
            ballVelocityAirInitial = 20;
            flyingRate = 2;
            ballTurningInitial = 100;
            ballTurningC = 2;
            ballJumpPower = 25;
            ballGravity = 9.81f;
            ballChargeInitial = 25f;
            maxCharge = 25;
            ballChargePowerTime = 3;

            limitedSpeedUp = limitedSpeedInitial / 20;
            ballVelocityUp = ballVelocityInitial / 20;
            ballVelocityAirUp = ballVelocityAirInitial / 20;
            ballTurningUp = -ballTurningInitial / 50;
            ballChargeUp = ballChargeInitial / 10;

            waterSpeedRate = 1;
            waterGravityRate = 0.25f;
            waterAccelRate = 0.8f;
            waterTurnRate = 1f;
            swimmingRate = 1.5f;
        }
    }
    void SetLocalGravity()
    {
        if (!isGround && (R2 || _R2Bot || R2_e)&& !isJump)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
        }
        float localGravity = ballGravity;
        if ((Input.GetButton("Fire_R2") || _R2Bot || R2_e) && !isGround && !isJump)
        {
            localGravity *= 3;
            UDdir.y = 0;
        }
        if (isWater)
        {
            localGravity *= waterGravityRate;
        }
        rb.AddForce(new Vector3(0f, -localGravity*2, 0f), ForceMode.Acceleration);
    }
    /// <summary>
    /// 強化ボーナスを示すエフェクトをセット
    /// </summary>
    /// <param name="Num"></param>
    /// <param name="particleLevel"></param>
    void SetBonusEff(int Num, int particleLevel)
    {
        ParticleSystem.EmissionModule constantEffEmission = constantEffs[Num].emission;
        //5未満の時表示しない
        if (particleLevel == 0)
        {
            constantEffsMain[Num].startLifetime = 0;
        }
        else if (particleLevel == 1)
        {
            constantEffsMain[Num].startLifetime = 0.4f;
            constantEffsMain[Num].startSpeed = 0.2f;
            constantEffEmission.rateOverTime = 2;
        }
        else if (particleLevel == 2)
        {
            constantEffsMain[Num].startLifetime = 0.6f;
            constantEffsMain[Num].startSpeed = 0.2f;
            constantEffEmission.rateOverTime = 3;
        }
        else if (particleLevel == 3)
        {
            constantEffsMain[Num].startLifetime = 0.8f;
            constantEffsMain[Num].startSpeed = 0.3f;
            constantEffEmission.rateOverTime = 4;
        }
        else if (particleLevel == 4)
        {
            constantEffsMain[Num].startLifetime = 1.0f;
            constantEffsMain[Num].startSpeed = 0.4f;
            constantEffEmission.rateOverTime = 5;
        }

    }
    void a_EnhanceConstant(int Num)
    {
        //開始時から強化状態の時1フレーム目では音を鳴らさない
        if (!BotMode && !startFrame)
        {
            //前回より強化値が増えたとき
            if (bonusEnhanceCheck[Num] > bonusEnhanceCheck_pre[Num])
            {
                audioSource.PlayOneShot(a_constant[0]);
            }
            //減ったとき
            else if (bonusEnhanceCheck[Num] < bonusEnhanceCheck_pre[Num])
            {
                audioSource.PlayOneShot(a_constant[1]);
            }
        }
    }
    void BonusEnhanceSet_speed()
    {
        a_EnhanceConstant(0);
        if (itemEnhanceCount[0] >= 20)
        {
            SetBonusEff(0, 4);
            bonusEnhance[0] = limitedSpeedUp * bonusEnhanceRate[4];
        }
        else if (itemEnhanceCount[0] >= 15)
        {
            SetBonusEff(0, 3);
            bonusEnhance[0] = limitedSpeedUp * bonusEnhanceRate[3];
        }
        else if (itemEnhanceCount[0] >= 10)
        {
            SetBonusEff(0, 2);
            bonusEnhance[0] = limitedSpeedUp * bonusEnhanceRate[2];
        }
        else if (itemEnhanceCount[0] >= 5)
        {
            SetBonusEff(0, 1);
            bonusEnhance[0] = limitedSpeedUp * bonusEnhanceRate[1];
        }
        else
        {
            SetBonusEff(0, 0);
            bonusEnhance[0] = bonusEnhanceRate[0];
        }
    }
    void BonusEnhanceSet_brake()
    {
        a_EnhanceConstant(1);
        if (itemEnhanceCount[1] >= 20)
        {
            SetBonusEff(1, 4);
            bonusEnhance[1] = (itemEnhanceCount[1] * ballBrakeInitial / 10) * bonusEnhanceRate[4];
        }
        else if (itemEnhanceCount[0] >= 15)
        {
            SetBonusEff(1, 3);
            bonusEnhance[1] = (itemEnhanceCount[1] * ballBrakeInitial / 10) * bonusEnhanceRate[3];
        }
        else if (itemEnhanceCount[0] >= 10)
        {
            SetBonusEff(1, 2);
            bonusEnhance[1] = (itemEnhanceCount[1] * ballBrakeInitial / 10) * bonusEnhanceRate[2];
        }
        else if (itemEnhanceCount[0] >= 5)
        {
            SetBonusEff(1, 1);
            bonusEnhance[1] = (itemEnhanceCount[1] * ballBrakeInitial / 10) * bonusEnhanceRate[1];
        }
        else
        {
            SetBonusEff(1, 0);
            bonusEnhance[1] = bonusEnhanceRate[0];
        }
    }
    void BonusEnhanceSet_turning()
    {
        a_EnhanceConstant(2);
        if (itemEnhanceCount[2] >= 20)
        {
            SetBonusEff(2, 4);
            bonusEnhance[2] = ballTurningUp * bonusEnhanceRate[4];
        }
        else if (itemEnhanceCount[2] >= 15)
        {
            SetBonusEff(2, 3);
            bonusEnhance[2] = ballTurningUp * bonusEnhanceRate[3];
        }
        else if (itemEnhanceCount[2] >= 10)
        {
            SetBonusEff(2, 2);
            bonusEnhance[2] = ballTurningUp * bonusEnhanceRate[2];
        }
        else if (itemEnhanceCount[2] >= 5)
        {
            SetBonusEff(2, 1);
            bonusEnhance[2] = ballTurningUp * bonusEnhanceRate[1];
        }
        else
        {
            SetBonusEff(2, 0);
            bonusEnhance[2] = bonusEnhanceRate[0];
        }
    }
    void BonusEnhanceSet_charge()
    {
        a_EnhanceConstant(3);
        if (itemEnhanceCount[3] >= 20)
        {
            SetBonusEff(3, 4);
            bonusEnhance[3] = ballChargeUp * bonusEnhanceRate[4];
        }
        else if (itemEnhanceCount[3] >= 15)
        {
            SetBonusEff(3, 3);
            bonusEnhance[3] = ballChargeUp * bonusEnhanceRate[3];
        }
        else if (itemEnhanceCount[3] >= 10)
        {
            SetBonusEff(3, 2);
            bonusEnhance[3] = ballChargeUp * bonusEnhanceRate[2];
        }
        else if (itemEnhanceCount[3] >= 5)
        {
            SetBonusEff(3, 1);
            bonusEnhance[3] = ballChargeUp * bonusEnhanceRate[1];
        }
        else
        {
            SetBonusEff(3, 0);
            bonusEnhance[3] = bonusEnhanceRate[0];
        }
    }
    
    void BonusEnhanceSet_accel()
    {
        a_EnhanceConstant(4);
        if (itemEnhanceCount[4] >= 20)
        {
            SetBonusEff(4, 4);
            bonusEnhance[4] = ballVelocityUp * bonusEnhanceRate[4];
            bonusEnhance[5] = ballVelocityAirUp * bonusEnhanceRate[4];
        }
        else if (itemEnhanceCount[4] >= 15)
        {
            SetBonusEff(4, 3);
            bonusEnhance[4] = ballVelocityUp * bonusEnhanceRate[3];
            bonusEnhance[5] = ballVelocityAirUp * bonusEnhanceRate[3];
        }
        else if (itemEnhanceCount[4] >= 10)
        {
            SetBonusEff(4, 2);
            bonusEnhance[4] = ballVelocityUp * bonusEnhanceRate[2];
            bonusEnhance[5] = ballVelocityAirUp * bonusEnhanceRate[2];
        }
        else if (itemEnhanceCount[4] >= 5)
        {
            SetBonusEff(4, 1);
            bonusEnhance[4] = ballVelocityUp * bonusEnhanceRate[1];
            bonusEnhance[5] = ballVelocityAirUp * bonusEnhanceRate[1];
        }
        else
        {
            SetBonusEff(4, 0);
            bonusEnhance[4] = bonusEnhanceRate[0];
            bonusEnhance[5] = bonusEnhanceRate[0];
        }
    }
    void EnhanceCheck()
    {
        //ボーナス分の強化量をセット
        {
            for(int i = 0; i < itemEnhanceCount.Length; i++)
            {
                if (itemEnhanceCount[i] >= 20)
                {
                    bonusEnhanceCheck[i] = 4;
                }
                else if (itemEnhanceCount[i] >= 15)
                {
                    bonusEnhanceCheck[i] = 3;
                }
                else if (itemEnhanceCount[i] >= 10)
                {
                    bonusEnhanceCheck[i] = 2;
                }
                else if (itemEnhanceCount[i] >= 5)
                {
                    bonusEnhanceCheck[i] = 1;
                }
                else
                {
                    bonusEnhanceCheck[i] = 0;
                }
            }
            //5個取るたびに一定の追加上昇
            if (bonusEnhanceCheck[0] != bonusEnhanceCheck_pre[0])
            {
                BonusEnhanceSet_speed();
            }
            if (bonusEnhanceCheck[1] != bonusEnhanceCheck_pre[1])
            {
                BonusEnhanceSet_brake();
            }
            if (bonusEnhanceCheck[2] != bonusEnhanceCheck_pre[2])
            {
                BonusEnhanceSet_turning();
            }
            if (bonusEnhanceCheck[3] != bonusEnhanceCheck_pre[3])
            {
                BonusEnhanceSet_charge();
            }
            if (bonusEnhanceCheck[4] != bonusEnhanceCheck_pre[4])
            {
                BonusEnhanceSet_accel();
            }
            bonusEnhanceCheck_pre = new int[] { bonusEnhanceCheck[0], bonusEnhanceCheck[1], bonusEnhanceCheck[2], bonusEnhanceCheck[3], bonusEnhanceCheck[4] };

        }
        float nowLimitedSpeed = limitedSpeedInitial + bonusEnhance[0];
        float nowBrake = ballBrakeInitial + bonusEnhance[1] * 5;
        float nowBrakeTimee = ballBrakeTimeInitial + bonusEnhance[1];
        float NowBallTurning = ballTurningInitial + bonusEnhance[2];
        float nowBallCharge = ballChargeInitial + bonusEnhance[3];
        float nowBallVelocity = ballVelocityInitial + bonusEnhance[4];
        float nowBallVelocityAir = ballVelocityAirInitial + bonusEnhance[5];
        //水中ステータス
        if (isWater)
        {
            //一時強化中かどうかで分岐
            if (enhanceTemps[0])
            {
                limitedSpeed = (nowLimitedSpeed + limitedSpeedUp * 20) * waterSpeedRate;
            }
            else
            {
                limitedSpeed = (nowLimitedSpeed + limitedSpeedUp * itemEnhanceCount[0]) * waterSpeedRate;
            }
            if (enhanceTemps[1])
            {
                ballBrake = nowBrake + (20*0.5f* ballBrakeInitial);
                ballBrakeTime = nowBrakeTimee + (20 * ballBrakeInitial *0.1f);
            }
            else
            {
                ballBrake = nowBrake + (itemEnhanceCount[1] * 0.5f*ballBrakeInitial);
                ballBrakeTime = nowBrakeTimee + (itemEnhanceCount[1] * ballBrakeInitial *0.1f);
            }
            if (enhanceTemps[2])
            {
                ballTurning = (NowBallTurning + ballTurningUp * 20) * waterTurnRate;
            }
            else
            {
                ballTurning = (NowBallTurning + ballTurningUp * itemEnhanceCount[2]) * waterTurnRate;
            }
            if (enhanceTemps[3])
            {
                ballCharge = nowBallCharge + ballChargeUp * 20;
            }
            else
            {
                ballCharge = nowBallCharge + ballChargeUp * itemEnhanceCount[3];
            }
            if (enhanceTemps[4])
            {
                ballVelocity = (nowBallVelocity + ballVelocityUp * 20) * waterAccelRate;
                ballVelocityAir = nowBallVelocityAir + ballVelocityAirUp * 20;
            }
            else
            {
                ballVelocity = (nowBallVelocity + ballVelocityUp * itemEnhanceCount[4]) * waterAccelRate;
                ballVelocityAir = nowBallVelocityAir + ballVelocityAirUp * itemEnhanceCount[4];
            }
            
        }
        //地上,空中ステータス
        else
        {
            if (enhanceTemps[0])
            {
                limitedSpeed = nowLimitedSpeed + limitedSpeedUp * 20;
            }
            else
            {
                limitedSpeed = nowLimitedSpeed + limitedSpeedUp * itemEnhanceCount[0];
            }
            if (enhanceTemps[1])
            {
                ballBrake = nowBrake + (20 * 0.5f * ballBrakeInitial);
                ballBrakeTime = nowBrakeTimee + (20 * ballBrakeInitial / 10);
            }
            else
            {
                ballBrake = nowBrake + (itemEnhanceCount[1] * 0.5f*ballBrakeInitial);
                ballBrakeTime = nowBrakeTimee + (itemEnhanceCount[1] * ballBrakeInitial / 10);
            }
            if (enhanceTemps[2])
            {
                ballTurning = NowBallTurning + ballTurningUp * 20;
            }
            else
            {
                ballTurning = NowBallTurning + ballTurningUp * itemEnhanceCount[2];
            }
            if (enhanceTemps[3])
            {
                ballCharge = nowBallCharge + ballChargeUp * 20;
            }
            else
            {
                ballCharge = nowBallCharge + ballChargeUp * itemEnhanceCount[3];
            }
            if (enhanceTemps[4])
            {
                ballVelocity = nowBallVelocity + ballVelocityUp * 20;
                ballVelocityAir = nowBallVelocityAir + ballVelocityAirUp * 20;
            }
            else
            {
                ballVelocity = nowBallVelocity + ballVelocityUp * itemEnhanceCount[4];
                ballVelocityAir = nowBallVelocityAir + ballVelocityAirUp * itemEnhanceCount[4];
            }
            
        }
        //暴走中最高速度上昇
        if (_lighting.isCharge||burn)
        {
            limitedSpeed *= 3f;
        }
        //凍傷のとき最高速度上昇
        if (frostbite)
        {
            limitedSpeed *= 1.5f;
        }
        if (selectBall[3])
        {
            //チャージ力がないとき最高速度を落とす,add30ぐらいから徐々に,要調整
            float chargeBallLimit = Mathf.Lerp(0.15f, 1, addPower / 30);
            limitedSpeed *= chargeBallLimit;
        }
    }

    void MoveFoward()
    {
        if (sp3)
        {
            sp3 = false;
            int randEnhance = Random.Range(1, 6);
            List<int> fullEnhance = new List<int>();
            if (!BotMode)
            {
                audioSource.PlayOneShot(a_specialUsed);
            }
            
            if (itemEnhanceCount[0] >= 20)
            {
                fullEnhance.Add(1);
            }
            if (itemEnhanceCount[1] >= 20)
            {
                fullEnhance.Add(2);
            }
            if (itemEnhanceCount[2] >= 20)
            {
                fullEnhance.Add(3);
            }
            if (itemEnhanceCount[3] >= 20)
            {
                fullEnhance.Add(4);
            }
            if (itemEnhanceCount[4] >= 20)
            {
                fullEnhance.Add(5);
            }
            while (fullEnhance.IndexOf(randEnhance) != -1)
            {
                randEnhance = Random.Range(1, 6);
            }
            switch (randEnhance)
            {
                case 1:
                    isItemEnhanceEnter = true;
                    break;
                case 2:
                    isItemEnhance2Enter = true;
                    break;
                case 3:
                    isItemEnhance3Enter = true;
                    break;
                case 4:
                    isItemEnhance4Enter = true;
                    break;
                case 5:
                    isItemEnhance5Enter = true;
                    break;
            }
        }
        //加速板踏んだorsp6使用時。最大チャージと同じ加速を与える
        isAccel = ground.IsAccel();
        if (isAccel || sp6)
        {
            chargeCount = maxCharge;
            sp6 = false;

        }
        if (_lighting.isCharge||burn)
        {
            chargeCount = maxCharge;
        }
        isJump = ground.IsJump();
        //ジャンプ板を踏んだ時上向きに加速
        if (isJump)
        {
            rb.AddForce(0, 30.0f, 0, ForceMode.Force);
        }
        addPower = Mathf.Lerp(ballChargePower, 0f, ballChargePowerTimeCount / ballChargePowerTime);
        //チャージ不可ボール
        if (selectBall[4])
        {
            addPower = 0;
        }
        if(!R2 && !_R2Bot && !R2_e)
        {
            ballBrakeTimeCount = 0;
        }
        else
        {
            ballBrakeTimeCount += ballBrakeTime;
            if (!isGround)
            {
                ballBrakeTimeCount += ballBrakeTime * 0.5f;
            }
        }
        if ((maru || _maruBot || maru_w)/* && (!R2 && !_R2Bot && !R2_e)*/)
        {
            dushVolume = addPower / ballChargePower;
            ballVel_now = ballVelocity + addPower - ballBrakeTimeCount;
            //下に下がって↑入力の時上方向強化
            if (ballVel_now < 0)
            {
                ballVel_now = 0;
            }
            if (isGround||isWater)
            {
                rb.AddForce(UDdir * ballVel_now, ForceMode.Force);
            }
            else
            {
                rb.AddForce(UDdir * ballVel_now * 0.5f, ForceMode.Force);
            }
        }
        //チャージ加速力が0になったとき減少時間をリセット
        if (addPower <= 0)
        {
            ballChargePower = 0;
            ballChargePowerTimeCount = 0;
        }
        else
        {
            ballChargePowerTimeCount += Time.deltaTime;
        }
        //凍傷でチャージ不可
        //チャージ速度
        if ((R2 || _R2Bot || R2_e))
        {
            chargeCheck = true;
            //麻痺で最大チャージ半減
            if (paralysis)
            {
                if (chargeCount < maxCharge*0.5f)
                {
                    chargeCount += Time.deltaTime * ballCharge;
                }
                else
                {
                    audioSource_charge.Stop();
                }
            }
            else
            {
                if (chargeCount < maxCharge && chargeCoolTime <= 0f)
                {
                    if (frostbite)
                    {
                        //凍傷でチャージ1/4
                        chargeCount += Time.deltaTime * ballCharge * 0.25f;
                    }
                    else
                    {
                        chargeCount += Time.deltaTime * ballCharge;
                    }
                    maxChargeTime = 0;
                }
                if(chargeCount >= maxCharge||(paralysis&& chargeCount >= maxCharge*0.5f))
                {
                    if (maxChargeTime == 0 && !selectBall[4])
                    {
                        audioSource_charge.Stop();
                        if (!BotMode)
                        {
                            audioSource.PlayOneShot(a_chargeList[0]);
                        }
                        
                    }
                    maxChargeTime += Time.deltaTime;
                }
                //3秒以上最大チャージ継続でリセット,1秒チャージできない
                if (maxChargeTime > 3.0f)
                {
                    if (!BotMode)
                    {
                        audioSource.PlayOneShot(a_chargeList[3]);
                    }
                    
                    maxChargeTime = 0f;
                    chargeCount = 0f;
                    chargeCoolTime = 1f;
                }
            }
        }
        //R2を放したとき超過時間をリセット
        else
        {
            maxChargeTime = 0f;
        }
        //クールタイムの消費
        if (chargeCoolTime > 0f)
        {
            chargeCoolTime -= Time.deltaTime;
        }
        
        if (batu || batu_s)
        {
            chargeCount = 0f;
        }
        //チャージ開放
        if(chargeCount > 0f && (!R2 && !_R2Bot && !R2_e))
        {
            //割合の2乗を掛けることで少量チャージの威力減
            ballChargePower = Mathf.Pow(chargeCount / maxCharge, 2f)* chargeCount;
            chargeCount = 0f;
        }
        //上限速度を超えた時速度制限
        //麻痺で最高速半減
        if (paralysis)
        {
            //地上
            if (rb.velocity.magnitude >= Mathf.Abs(limitedSpeed * 0.5f) && isGround/* || rb.velocity.magnitude <= -limitedSpeed*/)
            {
                rb.velocity = new Vector3(rb.velocity.x / limitBrake, rb.velocity.y, rb.velocity.z / limitBrake);
            }
            //空中
            else if (rb.velocity.magnitude >= Mathf.Abs(limitedSpeed * 0.5f) && isGround == false)
            {
                rb.velocity = new Vector3(rb.velocity.x / (limitBrake - limitBrake * 0.16f), rb.velocity.y, rb.velocity.z / (limitBrake - limitBrake * 0.16f));
            }
        }
        else
        {
            if (rb.velocity.magnitude >= Mathf.Abs(limitedSpeed) && isGround/* || rb.velocity.magnitude <= -limitedSpeed*/)
            {
                rb.velocity = new Vector3(rb.velocity.x *0.99f, rb.velocity.y, rb.velocity.z *0.99f);
            }
            else if (rb.velocity.magnitude >= Mathf.Abs(limitedSpeed) && isGround == false)
            {
                rb.velocity = new Vector3(rb.velocity.x / 0.95f, rb.velocity.y, rb.velocity.z / 0.95f);
            }
        }

        if (rb.velocity.magnitude >= Mathf.Abs(limitedSpeed) * limitedSpeedRatio && isGround)
        {
            
            float limSpeedMax = limitedSpeed * limitedSpeedRatio / rb.velocity.magnitude;
            rb.velocity = new Vector3(rb.velocity.x * limSpeedMax, rb.velocity.y, rb.velocity.z * limSpeedMax);
        }
        //制限の"limitedSpeedAirRatio"倍を超えた時、強制的にx,zベクトルに割合減算
        else if (rb.velocity.magnitude >= Mathf.Abs(limitedSpeed) * limitedSpeedAirRatio && !isGround)
        {
            
            float limSpeedMax = limitedSpeed * limitedSpeedAirRatio / rb.velocity.magnitude;
            rb.velocity = new Vector3(rb.velocity.x * limSpeedMax, rb.velocity.y, rb.velocity.z * limSpeedMax);
        }
        


        if (frostbite)
        {
            //ブレーキ
            //凍傷のときブレーキが効きづらくなる
            if (batu || batu_s || Input.GetButton("Fire_R2") || _R2Bot || R2_e)
            {
                ballChargePower = 0;
                rb.velocity = new Vector3(rb.velocity.x / (1 + ballBrake / 500), rb.velocity.y, rb.velocity.z / (1 + ballBrake / 500));
            }
            //自然ブレーキ
            else if (maru == false && _maruBot == false && maru_w == false)
            {
                rb.velocity = new Vector3(rb.velocity.x / (1 + ballBrake_nature / 500), rb.velocity.y, rb.velocity.z / (1 + ballBrake_nature / 500));
            }
        }
        else
        {
            if (batu || batu_s || Input.GetButton("Fire_R2") || _R2Bot || R2_e)
            {
                //チャージの加速をリセット
                ballChargePower = 0;
                rb.velocity = new Vector3(rb.velocity.x / (1 + ballBrake / 1000), rb.velocity.y, rb.velocity.z / (1 + ballBrake / 1000));
            }
            else if (maru == false && _maruBot == false && maru_w == false)
            {
                rb.velocity = new Vector3(rb.velocity.x / ballBrake_nature, rb.velocity.y, rb.velocity.z / ballBrake_nature);
            }
        }

        
    }
    void velocityRotate()
    {
        //玉の向きから見た座標に回転
        float localRotateY = this.gameObject.transform.localEulerAngles.y * Mathf.Deg2Rad;
        float rotateX = Mathf.Cos(localRotateY) * rb.velocity.x - Mathf.Sin(localRotateY) * rb.velocity.z;
        float rotateZ = Mathf.Sin(localRotateY) * rb.velocity.x - Mathf.Cos(localRotateY) * rb.velocity.z;
        rb.velocity = new Vector3(rotateX, rb.velocity.y, rotateZ);
        Debug.Log(rotateX);
        Debug.Log(rotateZ);
    }

    void Jump()
    {
        //空中でスタートするとジャンプが使えなくなる
        if (!isGround)
        {
            GroundAwayTime += 1;
        }
        else
        {
            GroundAwayTime = 0;
        }
        //unCheckで押しっぱなし2段以上連続ジャンプを阻止
        if (buttonOneUp || buttonOneKeyUp || sp7)
        {
            upCheck = true;
        }
        if ((buttonOne || buttonOneKey || sp7) && buttonJumpCount < shortJumpTime && jumpCount > 0 && upCheck)
        {
            buttonJumpCount += 1;
            sp7 = false;
        }
        if (buttonJumpCount > 0)
        {
            jumpdelay += 1;
        }
        if (isGround)
        {
            //passtimeで通り抜けた後を計算して接地判定再開
            jumpCount = 1;
            groundPass = false;
            groundPassTime = 0;
        }
        if (jumpCount > 0 && GroundAwayTime == 1)
        {
            jumpCount -= 1;
        }
        if (jumpCount > 0 && jumpdelay > shortJumpTime)
        {
            rb.velocity = new Vector3(0.0f, ballJumpPower, 0.0f);
            jumpCount -= 1;
            buttonJumpCount = 0;
            jumpdelay = 0;
            groundPass = true;
            if (buttonOne || buttonOneKey)
            {
                upCheck = false;
            }
            else
            {
                upCheck = true;
            }
        }
        if (groundPass)
        {
            groundPassTime += 1;
        }
    }

    void Flying()
    {
        if (!ground.IsGround())
        {
            //飛行補助用
            rb.AddForce(0, 1, 0);

            rb.velocity = new Vector3(rb.velocity.x / (1+UDcounter/10),rb.velocity.y, rb.velocity.z / (1 + UDcounter / 10));
            if ((UDstick > 0 || inpur_w) && UDcounter < 1.0f)
            {
                //傾いていく速度
                if (UDstick != 0)
                {
                    UDcounter += UDstick * 0.025f;
                }
                else
                {
                    UDcounter += 0.025f;
                }
            }
            if ((UDstick < 0 || input_s) && UDcounter > -1.0f)
            {
                if (UDstick != 0)
                {
                    UDcounter += UDstick * 0.025f;
                }
                else
                {
                    UDcounter -= 0.025f;
                }
            }
            if (UDcounter > 0)
            {
                UDdir = Vector3.Lerp(transform.forward, transform.up, UDcounter);
                //下方向へ加速していて上方向に入力しているとき一律で上昇力補正(ささやか)
                if (rb.velocity.y < 0)
                {
                    rb.AddForce(0,10, 0,ForceMode.Acceleration);
                }
                //上昇力が強くなりすぎた時制限をかける
                if (rb.velocity.y > 40)
                {
                    rb.velocity = rb.velocity / 1.1f;
                }
            }
            else if (UDcounter < 0)
            {
                UDdir = Vector3.Lerp(transform.forward, -transform.up * 0.75f, Mathf.Abs(UDcounter));
            }
            else
            {
                
                UDdir = transform.forward;
            }
        }
        else
        {
            UDcounter = 0;
            UDdir = transform.forward;
        }

    }
    void Swimming()
    {
        if ((UDstick > 0 || inpur_w) && UDcounter < 1.0f)
        {
            //傾いていく速度
            if (UDstick != 0)
            {
                UDcounter += UDstick * 0.01f;
            }
            else
            {
                UDcounter += 0.01f;
            }

        }
        if ((UDstick < 0 || input_s) && UDcounter > -1.0f)
        {
            if (UDstick != 0)
            {
                UDcounter += UDstick * 0.01f;
            }
            else
            {
                UDcounter -= 0.01f;
            }
        }
        
        if (UDcounter > 0)
        {
            //速度が上がるほど上昇量に制限をかける
            float UPsetting = rb.velocity.magnitude / 10;
            UDdir = Vector3.Lerp(transform.forward, new Vector3(0, 1.8f, 0), UDcounter);
            if (!_lighting.isCharge&&!burn)
            {
                rb.AddForce(new Vector3(0f, -swimmingRate * UDdir.y * 5*UPsetting, 0f), ForceMode.Acceleration);
            }
            else
            {
                //チャージ暴走イベントの時落下を強力にする
                rb.AddForce(new Vector3(0f, -swimmingRate * UDdir.y * 100, 0f), ForceMode.Acceleration);
            }
        }
        else if (UDcounter < 0)
        {
            UDdir = Vector3.Lerp(transform.forward, new Vector3(0, -0.8f, 0), Mathf.Abs(UDcounter));
        }
        else
        {
            UDdir = transform.forward;
        }
        //接地した中に下に刺さらないように角度を初期化
        if (isGround && UDcounter<0)
        {
            isGround = false;
            UDcounter = 0;
            UDdir = transform.forward;
        }
    }
    void eyeDirection()
    {
        dir = CameraVertical.GetComponent<DirectionTPS>().overDir;
        dir2 = reverse.GetComponent<reverse>().resetDir;
        float radihozo = 0f;
        float DualstickLhorizontal = Input.GetAxis("Horizontal Stick-L");
        float DualstickLvertical = Input.GetAxis("Vertical Stick-L");

        if (DualstickLhorizontal != 0 || DualstickLvertical != 0 || left_a || right_d)
        {
            float radian2 = Mathf.Atan2(DualstickLvertical, DualstickLhorizontal) * Mathf.Rad2Deg + 90;
            if (left_a)
            {
                radian2 = 270;
            }
            if (right_d)
            {
                radian2 = 90;
            }
            if (right_d && left_a)
            {
                radian2 = 0;
            }
            if (radian2 > 180)
            {
                radian2 -= 360;
            }
            if (radian2 > 90)
            {
                radian2 = 180 - radian2;
            }
            if (radian2 < -90)
            {
                radian2 = -(180 - Mathf.Abs(radian2));
            }
            //操作反転
            if (reverseOp)
            {
                radian2 *= -1;
            }
            float turningAmount = radian2 / (ballTurning * ballTurningC);
            //旋回力上昇系の行動をとっているとき
            if (batu || R2 || _R2Bot || batu_s || R2_e)
            {
                turningAmount = radian2 / ballTurning;
            }
            //火傷中旋回半減
            if (burn)
            {
                turningAmount *= 0.5f;
            }
            //毒中旋回2倍
            else if (poison)
            {
                turningAmount *= 2;
            }
            
            radian += turningAmount + dir2;
        }
        if(_axis_y != 0)
        {
            if (burn)
            {
                if (batu || R2 || _R2Bot || batu_s || R2_e)
                {
                    radihozo = _axis_y / (ballTurning * ballTurningC);
                }
                else
                {
                    radihozo = _axis_y / (ballTurning * ballTurningC * 2);
                }
            }
            else
            {
                if (batu || R2 || _R2Bot || batu_s || R2_e)
                {
                    radihozo =  _axis_y / ballTurning;
                }
                else
                {
                    radihozo = _axis_y / (ballTurning * ballTurningC);
                }
            }
            radian += radihozo;
        }
        
        if (radian < 0)
        {
            radian += 360;
        }
        else if (radian > 360)
        {
            radian = 0;
        }
        
        //方向転換中の速度を下げる
        float curveDeceleration = Mathf.Abs(radihozo * (1 - Mathf.Abs(UDcounter)) * (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z)))*ballBrakeTime;
        if (radihozo!=0)
        {
            if (isGround && !R2 && !R2_e && !_R2Bot)
            {
                rb.velocity = new Vector3(rb.velocity.x / (1 + curveDeceleration / 10000), rb.velocity.y, rb.velocity.z / (1 + curveDeceleration / 10000));
            }
            else if(!isGround)
            {
                rb.velocity = new Vector3(rb.velocity.x / (1 + curveDeceleration / 7500), rb.velocity.y, rb.velocity.z / (1 + curveDeceleration / 7500));
            }
            
        }

        //resetDirを追加してプラス元の値は0に戻す
        this.transform.eulerAngles = new Vector3(0f, radian, 0f);
    }

    void damage()
    {
        if (nowHP > maxHP)
        {
            nowHP = maxHP;
        }
        bool death = false;
        //sp1でHP回復
        if (sp1)
        {
            nowHP += Mathf.Floor(maxHP * 0.2f);
            sp1 = false;
        }
        if (itemDamage > 0)
        {
            nowHP -= 10;
            itemDamage -= 1;
            int randDown = Random.Range(1, 6);
            if (itemEnhanceCount[0] > 0 &&randDown==1)
            {
                itemEnhanceCount[0]--;
                enhanceImageChange(6);
            }
            else if (itemEnhanceCount[1] > 0 && randDown == 2)
            {
                itemEnhanceCount[1]--;
                enhanceImageChange(7);
            }
            else if (itemEnhanceCount[2] > 0 && randDown == 3)
            {
                itemEnhanceCount[2]--;
                enhanceImageChange(8);
            }
            else if (itemEnhanceCount[3] > 0 && randDown == 4)
            {
                itemEnhanceCount[3]--;
                enhanceDown[3] = true;
                enhanceImageChange(9);
            }
            else if (itemEnhanceCount[4] > 0 && randDown == 5)
            {
                itemEnhanceCount[4]--;
                enhanceImageChange(10);
            }
        }
        //sp2で状態異常回復
        if (sp2 || nowHP <= 0)
        {
            HealCondition();
        }
        if (sp2)
        {
            HealConditionEffect();
            sp2 = false;
        }
        //自然回復イベントで定数回復
        if (_lighting.isRecover && nowHP > 0)
        {
            if (!isRecoverCheck)
            {
                isRecover_eff = Instantiate(e_Heal[3], transform.position, Quaternion.Euler(e_Heal[3].transform.localEulerAngles));
                isRecover_eff.transform.parent = this.transform;
                isRecoverCheck = true;
            }
            natureRecover += Time.deltaTime;
            if (natureRecover > 1f)
            {
                natureRecover = 0f;
                nowHP += Mathf.Ceil(maxHP / 32);
            }
        }
        else if((!_lighting.isRecover || nowHP <= 0)&&isRecoverCheck)
        {
            //発動していないときオブジェクトを止めて破棄
            isRecover_eff.transform.GetChild(0).GetComponent<ParticleSystem>().loop = false;
            isRecover_eff.GetComponent<ParticleSystem>().loop = false;
            isRecoverCheck = false;
        }
        
        if (nowHP < 0)
        {
            nowHP = 0;
        }
        //撃破されたとき操作を制限
        if (nowHP <= 0 && !deathTimeCheck)
        {
            Instantiate(e_death, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.2f), Quaternion.Euler(transform.localEulerAngles));
            audioSource.PlayOneShot(a_death);
            chargeCount = 0f;
            death = true;
            deathTimeCheck = true;
            deathSin = 0f;
            render.material = originalMaterial;
            rb.velocity = new Vector3(0f, 0f, 0f);
        }
        if (nowHP > maxHP)
        {
            nowHP = maxHP;
        }
        hp_lerp = nowHP / maxHP;
        if (death)
        {
            deathDown(0);
            deathDown(1);
            deathDown(2);
            deathDown(3);
            deathDown(4);
            for(int i = 0; i < deathDownCheck.Length; i++)
            {
                if (deathDownCheck[i])
                {
                    enhanceImageChange(11);
                    break;
                }
            }
            deathDownCheck = new bool[] { false, false, false, false, false };
        }

        
        if (deathTimeCheck)
        {
            if (deathTime == 0)
            {
                deathEffCheck = false;
            }
            //透明化点滅
            deathSin = Mathf.Sin(Time.time * 6) / 2;
            //アルファ値のみ書き換え
            render.material.color = new Color32(ballColor.r, ballColor.g, ballColor.b, (byte)Mathf.Ceil((Mathf.Sin(Time.time * 6) / 2 + 0.5f) * 100)); ;
            //タイマー
            deathTime += Time.deltaTime;
        }
        //sp5使用時蘇生
        if (sp5)
        {
            deathTime = 4.8f;
            sp5 = false;
        }
        //4.8秒経過で復活モーション開始
        if (deathTime > 4.8f && !deathEffCheck)
        {
            deathEffCheck = true;
            GameObject recover = Instantiate(e_Heal[0], transform.position+e_Heal[0].transform.position, Quaternion.Euler(e_Heal[0].transform.eulerAngles));
            recover.transform.parent = this.transform;
            if (!BotMode)
            {
                audioSource.PlayOneShot(a_recover);
            }
            
        }
        //5秒経過で操作可能
        if (deathTime > 5.0f)
        {
            deathTimeCheck = false;
            //最大HPの7割の状態で復活
            nowHP = Mathf.Floor(maxHP * 0.7f);
            deathTime = 0f;
            //色を元に戻す
            render.material.color = ballColor;
        }
    }
    void deathDown(int itemEnhanceNum)
    {
        //ステータスをランダムで3～6割切り捨てで減少させる
        int deathPenalty = (int)Mathf.Floor(Random.Range(3, 6 + 1) * itemEnhanceCount[itemEnhanceNum] * 0.1f);
        //強化値が1以上のとき最低1は減少させる
        if (deathPenalty == 0 && itemEnhanceCount[itemEnhanceNum] > 0)
        {
            deathPenalty = 1;
        }
        itemEnhanceCount[itemEnhanceNum] -= deathPenalty;
        if (deathPenalty > 0)
        {
            deathDownCheck[itemEnhanceNum] = true;
        }
    }
    void damageDown()
    {
        if (nowHP < nowHP_pre)
        {
            for (int i = 0; i < itemEnhanceCount.Length; i++)
            {
                int randDown = (int)(Mathf.Ceil(nowHP_pre) - Mathf.Ceil(nowHP))*2;
                
                //1ダメージで各2％ずつ抽選
                if (itemEnhanceCount[i] > 1&&randDown > Random.Range(0,100))
                {
                    itemEnhanceCount[i] -= 1;
                    enhanceImageChange(i+6);
                }
            }
        }
    }
    void damageEffect()
    {
        if (nowHP < nowHP_pre)
        {
            audioSource.PlayOneShot(a_damage);
        }
        else if (nowHP > nowHP_pre)
        {
            audioSource.PlayOneShot(a_specialSE[0]);
        }
        for(int i = 0; i < itemEnhanceCount.Length; i++)
        {
            if (itemEnhanceCount[i] < itemEnhanceCount_pre[i])
            {
                audioSource.PlayOneShot(a_down);
                break;
            }
        }
    }
    void HealEffect()
    {
        if (nowHP > nowHP_pre)
        {
            GameObject heal = Instantiate(e_Heal[1], transform.position, Quaternion.Euler(e_Heal[1].transform.eulerAngles));
            heal.transform.parent = this.transform;
            if (!BotMode)
            {
                audioSource.PlayOneShot(a_specialSE[1]);
            }
            
        }
    }
    void shieldControl()
    {
        if (shieldCheck)
        {
            if (!shieldStart)
            {
                //起動SE入れる
                if (!BotMode)
                {
                    audioSource.PlayOneShot(a_specialSE[3]);
                }
                shield_img.color = new Color32(255, 255, 255, 255);
                shieldObj.SetActive(true);
                shield_img.enabled = true;
                shieldStart = true;
                shield_eff = Instantiate(e_shield, transform.position, Quaternion.Euler(e_shield.transform.eulerAngles));
                shield_eff.transform.parent = this.transform;
            }
            shieldTime += Time.deltaTime;
        }
        if (shieldTime > shieldLimit)
        {
            shieldTime = 0;
            shieldObj.SetActive(false);
            shield_img.enabled = false;
            shieldStart = false;
            shieldCheck = false;
            shield_eff.transform.GetChild(0).GetComponent<ParticleSystem>().loop = false;
            shield_eff.GetComponent<ParticleSystem>().loop = false;
        }
        else if(shieldTime > shieldLimit * 0.8&&!BotMode)
        {
            shield_img.color = new Color32(255, 255, 255, (byte)((Mathf.Sin(Time.time * 6) / 2 + 0.5f)*255));
        }

    }
    /// <summary>
    /// にんじんアイテムの処理
    /// </summary>
    void carrot()
    {
        //最大の2分の1回復
        if (carrotEnter)
        {
            carrotEnter = false;
            nowHP += maxHP / 2;
            if (nowHP > maxHP)
            {
                nowHP = maxHP;
            }
        }
        //10分の1でmax,それ以外毒
        if (carrot_fakeEnter)
        {
            carrot_fakeEnter = false;
            if (Random.Range(0, 10) == 0)
            {
                carrot_maxEnter = true;
            }
            else if(!nowCondition)
            {
                poisonCount = 999;
                poisonRecover = 0f;
                poisonDamageCounter = 1;
            }
        }
        if (carrot_maxEnter)
        {
            nowHP = maxHP;
            HealCondition();
            if (nowCondition)
            {
                HealConditionEffect();
            }
            tempImageChange(5);
            shieldCheck = true;
            for(int i = 0; i < enhanceTemps.Length; i++)
            {
                enhanceTemps[i] = true;
            }
            carrot_maxEnter = false;

        }
        //歩くきのこ
        if (carrot_randEnter)
        {
            carrot_randEnter = false;
            HealCondition();
            if (!nowCondition)
            {
                nowCondition = true;
                reverseCount = 999;
            }
        }
        //状態異常回復
        if (carrot_conditionEnter)
        {
            carrot_conditionEnter = false;
            HealCondition();
            if (nowCondition)
            {
                HealConditionEffect();
            }
        }
    }
    void HealCondition()
    {
        burnRecover = 999f;
        frostbiteRecover = 999f;
        paralysisRecover = 999f;
        poisonRecover = 999f;
        sleepRecover = 999f;
        reverseRecover = 999f;
        waterTime = 0;
        waterGauge.fillAmount = Mathf.Lerp(1, 0, waterTime / waterTimeLimit);
        conditionClear = true;
        burn = false;
        poison = false;
    }
    void HealConditionEffect()
    {
        GameObject heal = Instantiate(e_Heal[2], transform.position, Quaternion.Euler(e_Heal[2].transform.eulerAngles));
        heal.transform.parent = this.transform;
        audioSource.PlayOneShot(a_specialSE[1]);
    }
    void despone()
    {
        if (isDespone)
        {
            nowHP = 0f;
            this.transform.position = new Vector3(0f, 3f, 0f);
            isDespone = false;
        }
    }
    /// <summary>
    /// 状態異常の処理
    /// </summary>
    void condition()
    {
        //火傷
        _isFire = ground.IsFire();
        
        {
            if (_isFire && !nowCondition)
            {
                burnCount += Time.deltaTime;
            }
            else
            {
                burnCount = 0;
            }
            if (burnCount > 1.0f)
            {
                burnRecover = 0f;
                burn = true;
                nowCondition = true;
            }
            if (burn == true)
            {
                burnDamage += Time.deltaTime;
            }
            if (_isFire == false && burn)
            {
                burnRecover += Time.deltaTime;
            }
            if (burnRecover > conditionRecoverTime[0])
            {
                burn = false;
                nowCondition = false;
                burnDamage = 0f;
                burnRecover = 0f;
                condition_bar.fillAmount = 0;
            }
            if (burnDamage > 1.0f)
            {
                burnDamage = 0f;
                float burnDamageNow = Mathf.Floor(maxHP / 32);
                if (burnDamageNow < 1)
                {
                    burnDamageNow = 1;
                }
                nowHP -= burnDamageNow;
            }
        }
        //凍傷
        _isIce = ground.IsIce();
        {
            if (_isIce && !nowCondition)
            {
                frostbiteCount += Time.deltaTime;
            }
            else
            {
                frostbiteCount = 0;
            }
            if (frostbiteCount > 1.0f)
            {
                frostbiteRecover = 0f;
                frostbite = true;
                nowCondition = true;
            }
            if (_isIce == false && frostbite)
            {
                frostbiteRecover += Time.deltaTime;
            }
            if (frostbiteRecover > conditionRecoverTime[1])
            {
                frostbite = false;
                nowCondition = false;
                frostbiteRecover = 0f;
                condition_bar.fillAmount = 0;
            }
        }
        //麻痺
        _isThunder = ground.IsThunder();
        {
            if (_isThunder && !nowCondition)
            {
                paralysisCount += Time.deltaTime;
            }
            else
            {
                paralysisCount = 0;
            }
            if (paralysisCount > 1.0f)
            {
                paralysisRecover = 0f;
                paralysis = true;
                nowCondition = true;
            }
            if (_isThunder == false && paralysis)
            {
                paralysisRecover += Time.deltaTime;
            }
            if (paralysisRecover > conditionRecoverTime[2])
            {
                paralysis = false;
                nowCondition = false;
                paralysisRecover = 0f;
                condition_bar.fillAmount = 0;
            }
        }
        //眠り
        _isSleep = ground.IsSleep();
        {
            if (_isSleep && !nowCondition)
            {
                sleepCount += Time.deltaTime;
            }
            else
            {
                sleepCount = 0;
            }
            if (sleepCount > 1.5f)
            {
                sleepRecover = 0f;
                sleep = true;
                //チャージ系をリセット
                R2 = false;
                R2_e = false;
                _R2Bot = false;
                chargeCount = 0;
                nowCondition = true;
                int randNightmare = Random.Range(0, 5);
                //20%で悪夢状態
                if (randNightmare == 0)
                {
                    nightmare = true;
                }
            }
            if (sleep == true)
            {
                sleepDamage += Time.deltaTime;
            }
            if (_isThunder == false && sleep)
            {
                sleepRecover += Time.deltaTime;
            }
            if (sleepRecover > conditionRecoverTime[3])
            {
                sleep = false;
                nightmare = false;
                nowCondition = false;
                sleepRecover = 0f;
                condition_bar.fillAmount = 0;
            }
            if (sleepDamage > 1.0f)
            {
                if (nightmare)
                {
                    sleepDamage = 0f;
                    nowHP -= Mathf.Floor(maxHP / 16);
                }
                else
                {
                    sleepDamage = 0f;
                    if(nowHP + Mathf.Floor(maxHP / 16) >= maxHP)
                    {
                        nowHP = maxHP;
                    }
                    else
                    {
                        nowHP += Mathf.Floor(maxHP / 16);
                    }
                    
                }
                
            }

        }
        //毒
        _isPoison = ground.IsPoison();
        {
            if (_isPoison && !nowCondition)
            {
                poisonCount += Time.deltaTime;
            }
            else if (poisonCount != 999)
            {
                poisonCount = 0;
            }
            if (poisonCount > 1.0f)
            {
                if (poisonCount != 999)
                {
                    poisonRecover = 0f;
                    poisonDamageCounter = 1;
                }
                poison = true;
                nowCondition = true;
                
            }
            if (poison)
            {
                poisonDamage += Time.deltaTime;
            }
            if (!_isPoison && poison)
            {
                poisonRecover += Time.deltaTime;
            }
            
            if (poisonRecover > conditionRecoverTime[4])
            {
                poison = false;
                nowCondition = false;
                poisonRecover = 0f;
                condition_bar.fillAmount = 0;
                poisonCount = 0;
            }
            if (poisonDamage > 0.75f)
            {
                poisonDamage = 0;
                poisonDamageCounter += 0.1f;
                float poisonDamageNow = Mathf.Floor(maxHP / (32 / poisonDamageCounter));
                //最大HPが低く0になったときは最低ダメージ1を与える
                if (poisonDamageNow <= 0)
                {
                    poisonDamageNow = 1;
                }
                nowHP -= poisonDamageNow;
            }
        }
        if (reverseCount > 3.0f)
        {
            reverseRecover = 0;
            reverseOp = true;
            nowCondition = true;
        }
        if (reverseOp)
        {
            reverseRecover += Time.deltaTime;
        }
        if (reverseRecover > conditionRecoverTime[5])
        {
            reverseOp = false;
            nowCondition = false;
            reverseRecover = 0f;
            condition_bar.fillAmount = 0;
        }
        //水中
        if (_waterCheck.IsWater()&&!isBubble)
        {
            waterTime += Time.deltaTime;
            //limitを超えた時ダメージ
            if (waterTime > waterTimeLimit)
            {
                waterTimeCounter += Time.deltaTime;
                if (waterTimeCounter > 1.0f)
                {
                    nowHP -= Mathf.Floor(maxHP/30);
                    waterTimeCounter = 0;
                }
            }
        }
        //泡に触れた時呼吸全快
        else if (isBubble)
        {
            if (waterTime > 0 && !BotMode)
            {
                audioSource.PlayOneShot(a_bubble);
            }
            waterTime = 0;
        }
        else if(waterTime > 0)
        {
            //limitを代入して時間の回復を始める
            if (waterTime > waterTimeLimit)
            {
                waterTime = waterTimeLimit;
            }
            waterTime -= Time.deltaTime*3;
            if (waterTime < 0)
            {
                waterTime = 0;
            }
        }
        //iceLance
        if (lanceAttackCheck)
        {
            nowHP -= 25;
            lanceAttackCheck = false;
        }
    }
    void effect()
    {
        //Rendererコンポーネントを取得（Material取得のため）
        render.material.EnableKeyword("_EMISSION");
        color_lerp_set = chargeCount / maxCharge;
        if ((_lighting.isCharge || burn) && nowHP > 1)
        {
            color_lerp_set = 1;
        }
        speedSet = nowSpeed*1.3f / (limitedSpeed);
        if ((_lighting.isCharge||burn) && nowHP > 1)
        {
            render.material.SetColor("_EmissionColor", new Color(0.96f, 0.625f, 0.3125f));
        }
        else if (chargeCount > 0f)
        {
            render.material.SetColor("_EmissionColor", Color.Lerp(new Color(0f, 0f, 0f), new Color(0.96f, 0.625f, 0.3125f), color_lerp_set));
        }
        else
        {
            if (deathTimeCheck == false)
            {
                render.material = originalMaterial;
            }
        }
        if (nowHP <= maxHP / 4 && nowHP > 0)
        {
            render.material.color = new Color32(255, 0, 0, (byte)Mathf.Ceil((Mathf.Sin(Time.time * 6) / 2 + 0.5f) * 100));
            if (chargeCount <= 0f&& render.material.color!=originalMaterial.color)
            {
                render.material.SetColor("_EmissionColor", new Color(0, 0, 0));
            }
        }
        else
        {
            if (deathTimeCheck == false)
            {
                render.material.color = ballColor;
            }
        }


        // 強化アイテムを取ったとき頭上に表示,最大強化時は表示しない
        if (isItemEnhanceEnter && itemEnhanceCount[0] < 20)
        {
            itemEnhanceCount[0]++;
            enhanceImageChange(0);
            isItemEnhanceEnter = false;
        }
        else if (isItemEnhance2Enter && itemEnhanceCount[1] < 20)
        {
            itemEnhanceCount[1]++;
            enhanceImageChange(1);
            isItemEnhance2Enter = false;
        }
        else if (isItemEnhance3Enter && itemEnhanceCount[2] < 20)
        {
            itemEnhanceCount[2]++;
            enhanceImageChange(2);
            isItemEnhance3Enter = false;
        }
        else if (isItemEnhance4Enter && itemEnhanceCount[3] < 20)
        {
            itemEnhanceCount[3]++;
            enhanceImageChange(3);
            isItemEnhance4Enter = false;
        }
        else if (isItemEnhance5Enter && itemEnhanceCount[4] < 20)
        {
            itemEnhanceCount[4]++;
            enhanceImageChange(4);
            isItemEnhance5Enter = false;
        }
        //どこか一つでも未強化なら表示
        else if (isItemEnhanceAllEnter && (itemEnhanceCount[0] < 20 || itemEnhanceCount[1] < 20 || itemEnhanceCount[2] < 20|| itemEnhanceCount[3] < 20|| itemEnhanceCount[4] < 20))
        {
            if (itemEnhanceCount[0] < 20)
            {
                itemEnhanceCount[0]++;
            }
            if (itemEnhanceCount[1] < 20)
            {
                itemEnhanceCount[1]++;
            }
            if (itemEnhanceCount[2] < 20)
            {
                itemEnhanceCount[2]++;
            }
            if (itemEnhanceCount[3] < 20)
            {
                itemEnhanceCount[3]++;
            }
            if (itemEnhanceCount[4] < 20)
            {
                itemEnhanceCount[4]++;
            }
            enhanceImageChange(5);
            isItemEnhanceAllEnter = false;
        }
        else if (textCounter > 0f)
        {
            textCounter -= Time.deltaTime;
        }
        if (textCounter <= 0f)
        {
            for(int i = 0; i < enhanceImage.Length; i++)
            {
                enhanceImage[i].enabled = false;
            }
            for (int i = 0; i < tempImage.Length; i++)
            {
                tempImage[i].enabled = false;
            }
            textCounter = 0f;
        }
    }
    void enhanceImageChange(int imageNum)
    {
        for (int i = 0; i < enhanceImage.Length; i++)
        {
            enhanceImage[i].enabled = false;
        }
        enhanceImage[imageNum].enabled = true;
        //能力上昇のSE
        if (imageNum <= 5)
        {
            if (!BotMode)
            {
                audioSource.PlayOneShot(a_enhanceGet);
            }
        }
        textCounter = 1;
    }
    void tempEnhanceStart(int num)
    {
        //プレイヤーの時のみ書き換え
        if (!BotMode)
        {
            enhanceTemps[num] = true;
            enhanceTempsTimer[num] = 0;
            enhanceTempImg[num].color = new Color32((byte)(enhanceTempImg[num].color.r * 255), (byte)(enhanceTempImg[num].color.g * 255), (byte)(enhanceTempImg[num].color.b * 255), 125);
        }
    }
    void tempImageChange(int imageNum)
    {
        for (int j = 0; j < tempImage.Length; j++)
        {
            tempImage[j].enabled = false;
        }
        tempImage[imageNum].enabled = true;
        //能力上昇のSE
        audioSource.PlayOneShot(a_enhanceGet,0.7f);
        audioSource.PlayOneShot(a_tempEnhance);
        textCounter = 1;
    }
    void gameEvent()
    {
        if (_lighting.isSize)
        {
            randSizeTime += Time.deltaTime;
            //徐々に玉の大きさを変化させる
            randSizeChange = Mathf.Lerp(nowSize, 0.2f * randSize, randSizeTime / sizeChangeTimeSet);
            if (randSizeTime > 5)
            {
                nowSize = this.gameObject.transform.localScale.x;
                randSize = Random.Range(5, 10) + 1;
                randSizeTime = 0;
            }
            this.gameObject.transform.localScale = new Vector3(randSizeChange, randSizeChange, randSizeChange);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    /// <summary>
    /// ice lance の攻撃で得た強化情報と値のリセット
    /// </summary>
    void ball_hit()
    {
        if (_mainAttack.tell_enhance1)
        {
            _mainAttack.tell_enhance1 = false;
            isItemEnhanceEnter = true;
        }
        if (_mainAttack.tell_enhance2)
        {
            _mainAttack.tell_enhance2 = false;
            isItemEnhance2Enter = true;
        }
        if (_mainAttack.tell_enhance3)
        {
            _mainAttack.tell_enhance3 = false;
            isItemEnhance3Enter = true;
        }
        if (_mainAttack.tell_enhance4)
        {
            _mainAttack.tell_enhance4 = false;
            isItemEnhance4Enter = true;
        }
        if (_mainAttack.tell_enhance5)
        {
            _mainAttack.tell_enhance5 = false;
            isItemEnhance5Enter = true;
        }
        if (_mainAttack.tell_enhanceAll)
        {
            _mainAttack.tell_enhanceAll = false;
            isItemEnhanceAllEnter = true;
        }
        if (_mainAttack.tell_temp[5])
        {
            for (int i = 0; i < enhanceTemps.Length; i++)
            {
                enhanceTemps[i] = true;
            }
            _mainAttack.tell_temp[5] = false;
            tempImageChange(5);
        }
        else
        {
            for (int i = 0; i < _mainAttack.tell_temp.Length-1; i++)
            {

                if (_mainAttack.tell_temp[i])
                {
                    _mainAttack.tell_temp[i] = false;
                    enhanceTemps[i] = true;
                    tempImageChange(i);
                }
            }
        }
        
        _mainAttack.kaeruman -= 1;
    }
    void moveBot()
    {
        _maruBot = _autoPlayer.maruBot;
        _batuBot = _autoPlayer.batuBot;
        _R2Bot = _autoPlayer.R2Bot;
        _axis_y = _autoPlayer.axis_y;
        _UDcounterBot = _autoPlayer.UDcounterBot;
        //Botの値を代入
        UDcounter = _UDcounterBot;
    }
    void spCharge()
    {
        if (spCount < 6 || lanceCount < 6)
        {
            //sp chrge speed,itemNum(n) * 0.2
            timeCharge += Time.deltaTime + Time.deltaTime*(0.2f * itemNumCount);
        }
        special_lerp = timeCharge / spChargeTime;
        if (timeCharge > spChargeTime)
        {
            spCount += 1;
            if (spCount > 6)
            {
                spCount = 6;
            }
            lanceCount += 2;
            if (lanceCount > 6)
            {
                lanceCount = 6;
            }
            timeCharge = 0f;
        }
        if (DebugMode)
        {
            lanceCount = 6;
        }
    }
    void SPconsum()
    {
        if (R1 || overR1 || R1_r)
        {
            //1つ目のアイテムでspが溜まっており、HPの条件を満たすとき
            if ((_selectAbiList[abiCount] == 0 || _autoPlayer.abiUse_bot[0]) && spCount >= useSpecial && nowHP > 0 && maxHP > nowHP && !sleep)
            {
                UseSpecial(0);
                sp1 = true;
            }
            //2つ目のアイテムでspが溜まっており、状態異常になっているとき
            else if ((_selectAbiList[abiCount] == 1|| _autoPlayer.abiUse_bot[1]) && spCount >= useSpecial && nowCondition)
            {
                UseSpecial(1);
                sp2 = true;
            }
            //ランダムステータスアップ
            else if ((_selectAbiList[abiCount] == 2 || _autoPlayer.abiUse_bot[2]) && spCount >= useSpecial && nowHP > 0 && !sleep && (itemEnhanceCount[0] < 20 || itemEnhanceCount[1] < 20 || itemEnhanceCount[2] < 20 || itemEnhanceCount[3] < 20 || itemEnhanceCount[4] < 20))
            {
                UseSpecial(2);
                sp3 = true;
            }
            //シールド起動
            else if ((_selectAbiList[abiCount] == 3 || _autoPlayer.abiUse_bot[3]) && spCount >= useSpecial && nowHP > 0 && !sleep && !shieldCheck)
            {
                UseSpecial(3);
                shieldCheck = true;
                sp4 = true;
            }
            //蘇生
            else if ((_selectAbiList[abiCount] == 4 || _autoPlayer.abiUse_bot[4]) && spCount >= useSpecial && nowHP <= 0)
            {
                UseSpecial(4);
                sp5 = true;
            }
            //加速
            else if ((_selectAbiList[abiCount] == 5 || _autoPlayer.abiUse_bot[5]) && spCount >= useSpecial && nowHP > 0 && !sleep)
            {
                UseSpecial(5);
                sp6 = true;
            }
            //ジャンプ
            else if ((_selectAbiList[abiCount] == 6 || _autoPlayer.abiUse_bot[6]) && spCount >= useSpecial && nowHP > 0 && !sleep && isGround)
            {
                UseSpecial(6);
                sp7 = true;
            }
            else if(!BotMode)
            {
                audioSource.PlayOneShot(a_specialSE[7]);
            }

            R1 = false;
            R1_r = false;
        }
    }
    void UseSpecial(int num)
    {
        _autoPlayer.abiUse_bot[num] = false;
        spCount -= useSpecial;
        audioSource.PlayOneShot(a_specialUsed);
        //蘇生は無し
        if (num != 5 || num != 4 ||num!=3|| num != 2 || num != 1 || num != 0)
        {
            audioSource.PlayOneShot(a_specialSE[num]);
        }
        
    }
    public bool sp4Check()
    {
        bool tasikame = false;
        if (sp4)
        {
            tasikame = true;
            sp4 = false;
        }
        return tasikame;
    }
    void enhanceCounter()
    {
        count1.fillAmount = Mathf.Lerp(0, 1, (float)itemEnhanceCount[0] / 20);
        count2.fillAmount = Mathf.Lerp(0, 1, (float)itemEnhanceCount[1] / 20);
        count3.fillAmount = Mathf.Lerp(0, 1, (float)itemEnhanceCount[2] / 20);
        count4.fillAmount = Mathf.Lerp(0, 1, (float)itemEnhanceCount[3] / 20);
        count5.fillAmount = Mathf.Lerp(0, 1, (float)itemEnhanceCount[4] / 20);
        //一定数バフの表示
        for(int i = 0; i < countConstants.Length; i++)
        {
            if (itemEnhanceCount[i] >= 20)
            {
                countConstants[i].fillAmount = bonusEnhanceLerp[4];
            }
            else if (itemEnhanceCount[i] >= 15)
            {
                countConstants[i].fillAmount = bonusEnhanceLerp[3];
            }
            else if (itemEnhanceCount[i] >= 10)
            {
                countConstants[i].fillAmount = bonusEnhanceLerp[2];
            }
            else if (itemEnhanceCount[i] >= 5)
            {
                countConstants[i].fillAmount = bonusEnhanceLerp[1];
            }
            else
            {
                countConstants[i].fillAmount = bonusEnhanceLerp[0];
            }
            
        }
        //一時強化の表示変更
        for(int i=0;i< enhanceTempImg.Length; i++)
        {
            if (enhanceTemps[i])
            {
                enhanceTempImg[i].enabled = true;
                if (enhanceTempsTimer[i] > enhanceTempsTime * 0.7f)
                {
                    float tempSin = Mathf.Sin(Time.time * 6) / 2+0.5f;
                    enhanceTempImg[i].color = new Color32((byte)(enhanceTempImg[i].color.r * 255), (byte)(enhanceTempImg[i].color.g * 255), (byte)(enhanceTempImg[i].color.b * 255), (byte)(tempSin*125));
                }
            }
            else
            {
                enhanceTempImg[i].enabled = false;
            }
        }
    }
    void enhanceTemp()
    {
        for(int i = 0; i < enhanceTemps.Length; i++)
        {
            //強化状態を10秒で終了
            if (enhanceTempsTimer[i] > enhanceTempsTime)
            {
                enhanceTempsTimer[i] = 0;
                enhanceTemps[i] = false;
            }
            if (enhanceTemps[i])
            {
                enhanceTempsTimer[i] += Time.deltaTime;
            }
        }
    }
    IEnumerator change_ball(int ballNum)
    {
        if (!selectBall[ballNum])
        {
            audioSource.PlayOneShot(a_changeBall);
            Instantiate(e_changeBall, this.transform.localPosition, Quaternion.Euler(this.transform.localEulerAngles),this.transform);
        }
        //エフェクトを待つ
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < selectBall.Length; i++)
        {
            selectBall[i] = false;
        }
        if (!BotMode)
        {
            //ゲーム全体の値に玉の種類を保存
            selectBallSet.selectHam = ballNum;
        }
        else
        {
            selectBallSet.selectBotHam[botNum] = ballNum;
        }
        selectBall[ballNum] = true;
        ballColor = ballColors[ballNum];
        render.material.color = ballColor;
        changeBall = true;
        
    }
    /// <summary>
    /// Botを自動強化
    /// </summary>
    /// <param name="waitTime">抽選をする間隔</param>
    /// <param name="hitRate">抽選に当たる確率(0~99)</param>
    /// <returns></returns>
    IEnumerator bot_autoEnhance(float waitTime,int hitRate)
    {
        while (true)
        {
            //0.5~1.5倍した時間で回してランダム性を持たせる
            float waitTimeRand = waitTime * Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(waitTimeRand);
            if (Random.Range(0, 100) < hitRate)
            {
                int enganceNum = Random.Range(0, 5);
                switch (enganceNum)
                {
                    case 0:
                        isItemEnhanceEnter = true;
                        break;
                    case 1:
                        isItemEnhance2Enter = true;
                        break;
                    case 2:
                        isItemEnhance3Enter = true;
                        break;
                    case 3:
                        isItemEnhance4Enter = true;
                        break;
                    case 4:
                        isItemEnhance5Enter = true;
                        break;
                }
                    
            }
        }
    }
    void playSE()
    {
        //両端をカット
        if (audioSource_move.time < 0.2f|| audioSource_move.time > 1.5f)
        {
            audioSource_move.time = 0.2f;
        }
        audioSource_move.volume = moveVolume*0.05f;
        if ((R2 || _R2Bot || R2_e) && chargeCoolTime <= 0 && chargeCount <= 0)
        {
            audioSource_charge.Play();
        }
        else if(!R2 && !_R2Bot && !R2_e)
        {
            audioSource_charge.Stop();
        }
        float chargePich = Mathf.Lerp(1f, 17.5f/Mathf.Sqrt(maxCharge), color_lerp_set);
        if (audioSource_charge.time < 0.2f)
        {
            audioSource_charge.time = 0.2f;
        }
        audioSource_charge.pitch = chargePich;

        if (isWaterEnter)
        {
            audioSource_water.Play();
            audioSource.PlayOneShot(a_inWater);
        }
        else if (isWaterExit)
        {
            audioSource_water.Stop();
            audioSource.PlayOneShot(a_inWater);
        }
        //一定の速さから、速さで音量可変
        if (!audioSource_dush.isPlaying && rb.velocity.magnitude > 25)
        {
            audioSource_dush.Play();
        }
        else if(rb.velocity.magnitude <= 25)
        {
            audioSource_dush.Stop();
        }
        else if (audioSource_dush.isPlaying)
        {
            float dushVolume = Mathf.Lerp(0, 0.5f, (rb.velocity.magnitude - 25) / 10);
            audioSource_dush.volume = dushVolume;
        }
        if (burnCount > 3 && burnRecover == 0)
        {
            audioSource.PlayOneShot(a_abnormality[0]);
        }
        else if (frostbiteCount > 3&&frostbiteRecover==0)
        {
            audioSource.PlayOneShot(a_abnormality[1]);
        }
        else if (paralysisCount > 3 && paralysisRecover==0)
        {
            audioSource.PlayOneShot(a_abnormality[2]);
        }
        else if (sleepCount > 3 && sleepRecover==0)
        {
            audioSource.PlayOneShot(a_abnormality[3]);
            if (nightmare)
            {

            }
            else
            {
                audioSource.PlayOneShot(a_abnormality[5]);
            }
        }
        else if (poisonCount > 3 &&poisonRecover==0)
        {
            audioSource.PlayOneShot(a_abnormality[4]);
        }
        else if (reverseCount > 3)
        {
            reverseCount = 0;
            audioSource.PlayOneShot(a_abnormality[6]);
        }
        if (chargeCount >= maxCharge * 0.9f && chargeCheck && (!R2 && !R2_e && !_R2Bot))
        {
            if(!selectBall[4])
            audioSource.PlayOneShot(a_chargeFire);
            chargeCheck = false;
        }
        if (!R2 && !R2_e && !_R2Bot)
        {
            chargeCheck = false;
        }

    }
    void avirityChange()
    {
        //十字横入力,マウスホイール
        mouseWheel += Input.GetAxis("Mouse ScrollWheel");
        juziH = Input.GetAxis("Horizontal D-Pad");
        if (mouseWheel - mouseWheel_old > 0 || Input.GetKeyDown(KeyCode.RightArrow))
        {
            juziRcheck = true;
        }
        if (mouseWheel - mouseWheel_old < 0 || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            juziLcheck = true;
        }
        mouseWheel_old = mouseWheel;
        if (juziH > 0.5f && !juziRcheckC)
        {
            juziRcheck = true;
            juziRcheckC = true;
        }
        //押しっぱなし連続入力を制限
        else if (juziH <= 0.5f)
        {
            juziRcheckC = false;
        }
        if (juziH < -0.5f && !juziLcheckC)
        {
            juziLcheck = true;
            juziLcheckC = true;
        }
        else if (juziH >= -0.5f)
        {
            juziLcheckC = false;
        }
        //次のアイテムへ変更
        if (Time.timeScale != 0)
        {
            if (juziRcheck)
            {
                abiCount += 1;
                //specialの数より多いとき
                if (abiCount >= 3)
                {
                    abiCount = 0;
                }
                //使うspの変更
                useSpecial = useSpecialList[_selectAbiList[abiCount]];

                juziRcheck = false;
            }
            //前のアイテムへ戻る
            if (juziLcheck)
            {
                abiCount -= 1;
                if (abiCount < 0)
                {
                    abiCount = 2;
                }
                useSpecial = useSpecialList[_selectAbiList[abiCount]];

                juziLcheck = false;
            }
        }
        
        if (!BotMode)
        {
            _special._abicount = abiCount;
        }
    }
    void warp()
    {
        if (!isWarp)
        {
            warpTime = 0;
        }
        if (isWarp&&!warpCheck)
        {
            warpTime += Time.deltaTime;
        }
        if (warpTime > 3)
        {
            Vector3 _warpPos = warpPos[Random.Range(0, warpPos.Length)];
            transform.position = _warpPos;
            _e_warp = Instantiate(e_warp, _warpPos, Quaternion.Euler(transform.eulerAngles));
            warpCount = 0.9f;
            warpCheck = true;
            audioSource.PlayOneShot(a_warp);
        }
        if (warpCheck)
        {
            warpCool += Time.deltaTime;
            
            if(_e_warp!=null && warpCount <= 0)
            {
                Destroy(_e_warp);
            }
            else if (_e_warp != null && warpCount <= 1)
            {
                //ワープ後のエフェクトを生成
                warpCount -= Time.deltaTime;
                _e_warp.GetComponent<Renderer>().material.SetFloat("_cutoff", warpCount);
            }
        }
        if (warpCool > 5)
        {
            warpCool = 0;
            warpCheck = false;
        }
        
    }
    void hitStopStart()
    {
        elapsedTime = 0;
        isHitStop = true;
        Time.timeScale = 0.1f;
    }
    void hitStopEnd()
    {
        isHitStop = false;
        Time.timeScale = 1;
    }
    void IsWarp()
    {
        if (isWarpEnter || isWarpStay)
        {
            isWarp = true;
        }
        if (isWarpExit)
        {
            isWarp = false;
        }
        isWarpEnter = false;
        isWarpStay = false;
        isWarpExit = false;
    }
    void IsWater()
    {
        if (isWaterEnter || isWaterStay)
        {
            isWater = true;
            //水中では火傷無効化
            burnRecover = 999;
        }
        if (isWaterExit)
        {
            isWater = false;
        }
        isWaterEnter = false;
        isWaterStay = false;
        isWaterExit = false;
    }
    void IsBubble()
    {
        if (isBubbleEnter || isBubbleStay)
        {
            isBubble = true;
        }
        if (isBubbleExit)
        {
            isBubble = false;
        }
        isBubbleEnter = false;
        isBubbleStay = false;
        isBubbleExit = false;
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "detectionRange")
        {
            detectionItemNum = true;
        }
        else
        {
            detectionItemNum = false;
        }
        if (collision.tag == "detectionRange2")
        {
            detectionItemNum2 = true;
        }
        else
        {
            detectionItemNum2 = false;
        }
        if (collision.tag == "detectionRange3")
        {
            detectionItemNum3 = true;
        }
        else
        {
            detectionItemNum3 = false;
        }

        if (collision.tag == "detectionRange" || collision.tag == "detectionRange2" || collision.tag == "detectionRange3")
        {
            targetCheck = true;
            targetPoint = collision.transform.position;
        }
        else
        {
            targetCheck = false;
        }
        if (collision.tag == "warp")
        {
            isWarpStay = true;
        }
        if (collision.tag == "water")
        {
            isWaterStay = true;
        }
        if (collision.tag == "bubble")
        {
            isBubbleStay = true;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (nowHP > 0)
        {
            GameObject eff;
            switch (collision.gameObject.tag)
            {
                case "itemEnhance":
                    isItemEnhanceEnter = true;
                    eff = Instantiate(enhanceParticle[0], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "itemEnhance2":
                    isItemEnhance2Enter = true;
                    eff = Instantiate(enhanceParticle[1], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "itemEnhance3":
                    isItemEnhance3Enter = true;
                    eff=Instantiate(enhanceParticle[2], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "itemEnhance4":
                    isItemEnhance4Enter = true;
                    eff=Instantiate(enhanceParticle[3], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "itemEnhance5":
                    isItemEnhance5Enter = true;
                    eff=Instantiate(enhanceParticle[4], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "tempEnhance1":
                    tempEnhanceStart(0);
                    tempImageChange(0);
                    eff=Instantiate(tempParticle[0], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "tempEnhance2":
                    tempEnhanceStart(1);
                    tempImageChange(1);
                    eff=Instantiate(tempParticle[1], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "tempEnhance3":
                    tempEnhanceStart(2);
                    tempImageChange(2);
                    eff=Instantiate(tempParticle[2], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "tempEnhance4":
                    tempEnhanceStart(3);
                    tempImageChange(3);
                    eff=Instantiate(tempParticle[3], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "tempEnhance5":
                    tempEnhanceStart(4);
                    tempImageChange(4);
                    eff=Instantiate(tempParticle[4], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "tempEnhanceAll":
                    for (int i = 0; i < enhanceTemps.Length; i++)
                    {
                        tempEnhanceStart(i);
                    }
                    tempImageChange(5);
                    eff=Instantiate(tempParticle[5], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "itemDamage":
                    itemDamage++;
                    Destroy(collision.gameObject);
                    break;
                case "itemAll":
                    isItemEnhanceAllEnter = true;
                    eff=Instantiate(enhanceParticle[5], transform.position, Quaternion.Euler(enhanceParticleAngle));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "itemNum":
                    itemNumCount++;
                    audioSource.PlayOneShot(a_itemNum);
                    eff = Instantiate(sunflowerParticle, transform.position, Quaternion.Euler(sunflowerParticle.transform.localEulerAngles));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    itemNumDes = true;
                    break;
                case "despone":
                    isDespone = true;
                    break;
                case "itemCollect":
                    itemCollect++;
                    Destroy(collision.gameObject);
                    break;
                case "Ball_light":
                    StartCoroutine(change_ball(0));
                    Destroy(collision.gameObject);
                    break;
                case "Ball_nomal":
                    StartCoroutine(change_ball(1));
                    Destroy(collision.gameObject);
                    break;
                case "Ball_heavey":
                    StartCoroutine(change_ball(2));
                    Destroy(collision.gameObject);
                    break;
                case "Ball_charge":
                    StartCoroutine(change_ball(3));
                    Destroy(collision.gameObject);
                    break;
                case "Ball_nocharge":
                    StartCoroutine(change_ball(4));
                    Destroy(collision.gameObject);
                    break;
                case "Ball_slick":
                    StartCoroutine(change_ball(5));
                    Destroy(collision.gameObject);
                    break;
                case "Ball_noturning":
                    StartCoroutine(change_ball(6));
                    Destroy(collision.gameObject);
                    break;
                case "Ball_superlight":
                    StartCoroutine(change_ball(7));
                    Destroy(collision.gameObject);
                    break;
                case "warp":
                    isWarpEnter = true;
                    break;
                case "water":
                    isWaterEnter = true;
                    break;
                case "bubble":
                    isBubbleEnter = true;
                    break;
                case "hitStop":
                    hitStopStart();
                    break;
                case "carrot":
                    audioSource.PlayOneShot(a_carrot);
                    carrotEnter = true;
                    eff=Instantiate(carrotParticle[0], collision.transform.position + transform.up * 1, Quaternion.Euler(transform.eulerAngles));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "carrot_fake":
                    audioSource.PlayOneShot(a_carrot);
                    carrot_fakeEnter = true;
                    eff=Instantiate(carrotParticle[1], collision.transform.position + transform.up * 1, Quaternion.Euler(transform.eulerAngles));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "carrot_max":
                    audioSource.PlayOneShot(a_carrot);
                    carrot_maxEnter = true;
                    eff=Instantiate(carrotParticle[2], collision.transform.position + transform.up * 1, Quaternion.Euler(transform.eulerAngles));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "carrot_rand":
                    audioSource.PlayOneShot(a_carrot);
                    carrot_randEnter = true;
                    eff=Instantiate(carrotParticle[3], collision.transform.position + transform.up * 1, Quaternion.Euler(transform.eulerAngles));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
                case "carrot_condition":
                    audioSource.PlayOneShot(a_carrot);
                    carrot_conditionEnter = true;
                    eff = Instantiate(carrotParticle[4], collision.transform.position + transform.up * 1, Quaternion.Euler(transform.eulerAngles));
                    eff.transform.parent = transform;
                    Destroy(collision.gameObject);
                    break;
            }

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "move_floor":
                transform.parent = null;
                break;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "warp":
                isWarpExit = true;
                break;
            case "water":
                isWaterExit = true;
                break;
            case "bubble":
                isBubbleExit = true;
                break;
        }
    }
    //player衝突ダメージ
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //突進の威力強化,符号が同じとき速度の速いほうはダメージを受けない(同じ向き),符号が違うとき(正面、左右)のときダメージ
            float damageX = collision.relativeVelocity.x - rb.velocity.x;
            float damageY = collision.relativeVelocity.y - rb.velocity.y;
            if (damageX > 0)
            {
                damageX = 0;
            }
            if (damageY > 0)
            {
                damageY = 0;
            }
            //HPがあるときダメージ
            if (nowHP > 0)
            {
                audioSource.PlayOneShot(a_clash);
                //ヒット時ダメージエフェクトを生成
                foreach (ContactPoint point in collision.contacts)
                {
                    GameObject hitDamageEff = Instantiate(e_hitDamage, point.point, Quaternion.Euler(collision.gameObject.transform.localEulerAngles));
                    hitDamageEff.transform.parent = this.transform;
                }
                //0以下になったときは0に戻す
                nowHP -= Mathf.Floor(Mathf.Abs(damageX) + Mathf.Abs(damageY) * 0.8f);
                if (nowHP < 0)
                {
                    nowHP = 0;
                }
            }
        }
        else if(collision.gameObject.tag== "mainAttack")
        {
            lanceAttackCheck = true;
            foreach (ContactPoint point in collision.contacts)
            {
                GameObject hitDamageEff = Instantiate(e_hitDamage, point.point, Quaternion.Euler(collision.gameObject.transform.localEulerAngles));
                hitDamageEff.transform.parent = this.transform;
            }
                
        }
        else if (collision.gameObject.tag == "move_floor")
        {
            transform.SetParent(collision.transform);
        }
    }
}