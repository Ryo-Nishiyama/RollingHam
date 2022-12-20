using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum StateType
{
    Idle,
    ItemGet,
    Attack,
    AvoidWall,
}
public class autoPlayer : MonoBehaviour
{
    [SerializeField] StateType _stateType = StateType.Idle;
    StateType _nextStateType = StateType.Idle;
    public moveTPS _moveTPS;
    [SerializeField] GroundCheck ground;
    //�A�C�e���ƃv���C���[�ŒǐՔ͈͂�ς���
    [SerializeField] GameObject[] botObjectCheck;
    objectCheck _objectCheck;
    objectCheck _playerCheck;
    [SerializeField] float shotCoolTime = 1f;
    [SerializeField] float switchStateTime = 10f;
    [SerializeField, Range(0.0f, 1.0f)] float attackRate;
    [SerializeField, Range(0.0f, 1.0f)] float itemRate;
    [SerializeField] bool randBehaviorRateFlag = true;
    public itemCheck _itemCheck;
    public bool maruBot = true;
    public bool batuBot = false;
    public bool R2Bot = false;
    public bool attackBot = false;
    public float UDcounterBot = 0;
    public float axis_y = 0.0f;
    public bool race = false;
    RaycastHit hit;
    Rigidbody rb;

    public bool[] abiUse_bot = new bool[] { false, false, false, false, false, false, false };
    public float updataTimerSet = 1.0f;
    float notMoveTimer = 0;
    float updataTimer = 0.0f;
    float skillTimer = 0;
    float skillTimerSet = 2.0f;
    float skillRandom = 0;
    bool stopResolve = false;
    int randNum;
    bool changeDir = false;
    bool changeDir2 = false;
    bool itemTurn = false;
    bool targetCheck = false;
    bool isTargetCheckStay, isTargetCheckEnter, isTargetCheckExit = false;
    bool targetCharge = false;
    float targetTime = 0.0f;
    //�ǉ���̂Ƃ��ǂ��܂ŉ�]�����邩
    float turnWallAngle = 30;
    float turnAddStartAngle = 0;
    bool turnAddStartCheck = false;

    string targetObjectName, targetObjectNameNow;
    Vector3 targetObjectPos, targetObjectPosNow;

    bool nearDist = false;
    bool obstacleCheckR, obstacleCheckL = false;
    float alwaysRotate = 0;
    float axisTime = 0;
    float axisTimeMax = 0;
    float roadWidth;
    float roadWidthBefore = 0;
    bool roadWidthCheckR, roadWidthCheckL = false;
    int correctionCountR, correctionCountL = 0;
    int reversePreventionR, reversePreventionL = 0;
    int roadCounter = 0;
    float raceChargeCT = 0;
    //�`���[�W��1�b�̃N�[���^�C��
    float raceChargeCTL = 1;
    bool raceChargeCheck = false;

    float wallHitDistance = 15f;
    private bool isWall = false;
    private bool isWallEnter, isWallStay, isWallExit;

    int[] selectAbiList = new int[] { 7, 7, 7 };

    Vector3 rightWall, leftWall = new Vector3(999, 999, 999);
    Vector3 alwaysRotateR, alwaysRotateL = new Vector3(999, 999, 999);

    Quaternion targetQrot;
    Vector3 targetEuler;
    Vector3 targetPoint;
    LayerMask layerMaskWall;

    //state flag
    bool isIdle, isItemGet, isAttack, isAvoidWall;

    //�����A�C�e����_������
    float targetContinueTime = 10;
    float targetContinueTimer = 0;
    bool isItemCharge;
    bool targetItemCheck = false;
    string targetItemTag;
    float targetAngle;
    Vector3 targetItemPos;
    List<Vector3> collectItemList = new List<Vector3>();
    string[] enhanceItemTags = new string[] { "itemEnhance", "itemEnhance2", "itemEnhance3", "itemEnhance4", "itemEnhance5", "itemAll" };
    string[] tempItemTags = new string[] { "tempEnhance1", "tempEnhance2", "tempEnhance3", "tempEnhance4", "tempEnhance5", "tempEnhanceAll" };
    string[] crrotItemTags = new string[] { "carrot", "carrot_condition", "carrot_fake", "carrot_rand", "carrot_max" };
    string[] enhanceRockTags = new string[] { "enhanceRock" };

    float rayAngleDistance = Mathf.Sqrt(2) / 2;
    List<List<int>> behaviorTable = new List<List<int>>() { new List<int>() { } };

    // Start is called before the first frame update
    void Start()
    {
        //���������O�Ɍ��܂��Ă��Ȃ��Ƃ�
        if (randBehaviorRateFlag)
        {
            //�s�������������_���Ɍ���
            attackRate = Random.Range(0.0f, 1.0f);
            itemRate = Random.Range(0.0f, 1.0f);
        }
        behaviorTable = GenerateTable(attackRate, itemRate);
        //abi���Z�b�g
        selectAbiList = selectBallSet.selectAbiBotList[_moveTPS.botNum];
        rb = GetComponent<Rigidbody>();
        layerMaskWall = 1 << LayerMask.NameToLayer("wall");

        if (Physics.Raycast(gameObject.transform.position, transform.right, out hit, Mathf.Infinity, layerMaskWall))
        {
            alwaysRotateR = hit.point - this.gameObject.transform.position;
        }
        if (Physics.Raycast(gameObject.transform.position, -transform.right, out hit, Mathf.Infinity, layerMaskWall))
        {
            alwaysRotateL = hit.point - this.gameObject.transform.position;
        }
        //���������擾
        roadWidth = Mathf.Abs(alwaysRotateR.x) + Mathf.Abs(alwaysRotateR.z) + Mathf.Abs(alwaysRotateL.x) + Mathf.Abs(alwaysRotateL.z);
        roadWidthBefore = roadWidth;
        StartCoroutine(UDset());

        ///<summary>
        ///�X�e�[�g�̑J��
        ///</summary>
        if (!race)
        {
            {
                _objectCheck = botObjectCheck[0].GetComponent<objectCheck>();
                _playerCheck = botObjectCheck[1].GetComponent<objectCheck>();
            }
            float changeRate = attackRate + itemRate;
            //���v�̍ő傪1�ɂȂ�悤�ɕ␳
            if (changeRate > 1)
            {
                StartCoroutine(selectState(switchStateTime, attackRate / changeRate, itemRate / changeRate));
                StartCoroutine(iceAttack(attackRate / changeRate, itemRate / changeRate));
            }
            else
            {
                StartCoroutine(selectState(switchStateTime, attackRate, itemRate));
                StartCoroutine(iceAttack(attackRate,itemRate));
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updataTimer += Time.deltaTime;

        if (!race)
        {
            //�A�C�e�������蔻��Əꏊ�𓯊�
            botObjectCheck[0].transform.position = this.transform.position + this.transform.forward * 30;
            botObjectCheck[0].transform.eulerAngles = this.transform.eulerAngles;
            botObjectCheck[1].transform.position = this.transform.position + this.transform.forward * 60;
            botObjectCheck[1].transform.eulerAngles = this.transform.eulerAngles;
            basicActionBot();
            //�X�L���̓X�e�[�g�Ɉˑ����Ȃ�
            useSkill();

            ///<summary>
            ///update state
            ///</summary>
            {

                switch (_stateType)
                {
                    //�X�L���A�`���[�W����������_���ɍs��
                    case StateType.Idle:
                        IdleUpdate();
                        break;
                    //�A�C�e���̎擾��ڎw���ē���
                    case StateType.ItemGet:
                        ItemGetUpdate();
                        break;
                    //�v���C���[�߂����čU��
                    case StateType.Attack:
                        AttackUpdate();
                        break;
                    //�ǂȂǂɋl�܂����Ƃ�����s��
                    case StateType.AvoidWall:
                        AvoidWallUpdate();
                        break;
                }
            }
            ///<summary>
            ///End and Start state
            ///</summary>
            if (_stateType != _nextStateType)
            {
                switch (_stateType)
                {
                    case StateType.Idle:
                        IdleEnd();
                        break;
                    case StateType.ItemGet:
                        ItemGetEnd();
                        break;
                    case StateType.Attack:
                        AttackEnd();
                        break;
                    case StateType.AvoidWall:
                        AvoidWallEnd();
                        break;
                }
                //�X�e�[�g���X�V
                _stateType = _nextStateType;
                switch (_stateType)
                {
                    case StateType.Idle:
                        IdleStart();
                        break;
                    case StateType.ItemGet:
                        ItemGetStart();
                        break;
                    case StateType.Attack:
                        AttackStart();
                        break;
                    case StateType.AvoidWall:
                        AvoidWallStart();
                        break;
                }

            }
        }
        
        else
        {
            //raceBot();
            raceBot_v();
            raceBot_Charge();
            useSkill();
        }
    }
    /// <summary>
    /// �s������p�̃e�[�u���𐶐�
    /// </summary>
    /// <param name="_attackRate">�U���̍s������</param>
    /// <param name="_itemRate">�A�C�e������̍s������</param>
    /// <param name="tableVertical">�e�[�u���̏c��</param>
    /// <param name="tableWidth">�e�[�u���̉���</param>
    /// <param name="continueCutNum">Idle�����ފԊu</param>
    /// <returns></returns>
    List<List<int>> GenerateTable(float _attackRate,float _itemRate,int tableVertical = 10,int tableWidth=10,int continueCutNum=3)
    {
        List<List<int>> table = new List<List<int>>();
        for(int i = 0; i < tableVertical; i++)
        {
            
            if (_attackRate + _itemRate > 1)
            {
                //������1�ɂȂ�l�ɕ␳
                _attackRate /= _attackRate + _itemRate;
                _itemRate /= _attackRate + _itemRate;
            }
            float randNum = Random.Range(0.0f, 1.0f);
            int mainStateNum = 0;
            int continueCount = 0;
            //�A�C�e���d���z��𐶐�
            if (_itemRate >randNum)
            {
                mainStateNum = 1;
            }
            //�U���d���z��𐶐�
            else if (_attackRate+_itemRate > randNum)
            {
                mainStateNum = 2;
            }
            //�z���������
            List<int> tableSingle = new List<int>();

            for (int j = 0; j < tableWidth; j++)
            {
                tableSingle.Add(mainStateNum);
                continueCount += 1;
                //�l���A�������Ƃ����܂�Idle������
                if (continueCount > continueCutNum)
                {
                    if (Random.Range(0, 4) == 0)
                    {
                        tableSingle.Add(0);
                        continueCount = 0;
                    }
                }
            }
            table.Add(tableSingle);
        }
        return table;
    }
    void changeState(StateType nextState)
    {
        _nextStateType = nextState;
    }
    void IdleStart()
    {
        isIdle = true;
        IdleAction(3, 0.2f, 0.5f);
    }
    void IdleUpdate()
    {
        chargeReleaseRand();
    }
    void IdleEnd()
    {
        isIdle = false;
    }
    void ItemGetStart()
    {
        isItemGet = true;
    }
    void ItemGetUpdate()
    {
        //�p�x���߂��Ƃ��D�悳����悤�ɂ���
        chargeReleaseRand();
        string nowItem = _objectCheck.enterScopeObjectTag;
        Vector3 nowPos = _objectCheck.enterScopeObjectPos;
        //�ڕW�A�C�e�����Ȃ��A������������A�ڕW��ݒ肷��
        if (!targetItemCheck)
        {
            if (enhanceItemTags.Contains(nowItem))
            {
                ItemGetSetItem();
            }
            else if (tempItemTags.Contains(nowItem))
            {
                ItemGetSetItem();
            }
            else if (enhanceRockTags.Contains(nowItem))
            {
                ItemGetSetItem();
            }
        }
        //target��all�n�A�C�e���łȂ��Aall�A�C�e������������target�����ύX
        if (targetItemTag != "itemAll" && targetItemTag != "tempEnhanceAll"&&(nowItem== "itemAll"||nowItem== "tempEnhanceAll"))
        {
            targetItemTag = _objectCheck.enterScopeObjectTag;
            targetItemPos = _objectCheck.enterScopeObjectPos;
            targetContinueTimer = 0;
        }
        //���A�A�C�e����_���Ă���Ƃ��t���O
        if (targetItemTag == "itemAll" || targetItemTag == "tempEnhanceAll")
        {
            //���A�A�C�e����_���Ă���Ƃ��`���[�W�Ŋl�������グ��
            isItemCharge = true;
        }
        //�_���Ă���A�C�e��������Ƃ�
        if (targetItemCheck)
        {
            //�p�x��s�x����
            targetAngle = targetAngleSet(targetItemPos);
            targetContinueTimer += Time.deltaTime;
            //�K��̎��Ԃ܂łɎ��Ȃ�������Ⴄ�A�C�e���Ɉڂ�
            if (targetContinueTimer > targetContinueTime)
            {
                targetItemCheck = false;
                axis_y = 0;
                isItemCharge = false;
                targetItemTag = null;
            }
            //�A�C�e������������ɐ���
            if (targetAngle > 0.5f)
            {
                axis_y = 90;
            }
            else if (targetAngle < -0.5f)
            {
                axis_y = -90;
            }
            //target�Ƃ̊p�x���}0.5�ɂȂ�����ˌ����Ď��̃A�C�e����T��
            else
            {
                attackBot = true;
                targetItemCheck = false;
                axis_y = 0;
                isItemCharge = false;
                targetItemTag = null;
            }
            if (isItemCharge)
            {
                R2Bot = true;
            }
        }
    }
    void ItemGetEnd()
    {
        //�l��������
        axis_y = 0;
        isItemGet = false;
        targetItemCheck = false;
        isItemCharge = false;
    }
    void ItemGetSetItem()
    {
        //�ڕW�̈ʒu�Ɩ��O��ۑ�
        targetItemTag = _objectCheck.enterScopeObjectTag;
        targetItemPos = _objectCheck.enterScopeObjectPos;
        targetItemCheck = true;
        //�V�����A�C�e���ɂȂ邽�߃^�C�}�[�����Z�b�g
        targetContinueTimer = 0;
        //���܂Ƀ`���[�W���Ċl�����ɍs��
        if (Random.Range(0, 5) == 0)
        {
            isItemCharge = true;
        }
    }
    float targetAngleSet(Vector3 targetPos)
    {
        float angle= getAngle(this.transform.position, targetPos) - this.transform.eulerAngles.y;
        //angle���}180�ȓ��ɗ}����
        while (angle < -180)
        {
            angle += 180;
        }
        while (angle > 180)
        {
            angle -= 180;
        }
        return angle;
    }
    void AttackStart()
    {
        isAttack = true;
    }
    void AttackUpdate()
    {
        chargeReleaseRand();
        //�G�̊m�F
        if (_playerCheck.enterScopeObjectTag == "player")
        {
            //�G����������ʒu��ۑ�
            Vector3 nowPos = _playerCheck.enterScopeObjectPos;
            targetAngle = targetAngleSet(nowPos);
            //target�܂ł̋�����axis��ς���
            if (targetAngle > 0.5f)
            {
                axis_y = 45;
                if (targetAngle > 5f)
                {
                    axis_y = 90;
                }
            }
            else if (targetAngle < -0.5f)
            {
                axis_y = -45;
                if (targetAngle < -5f)
                {
                    axis_y = -90;
                }
            }
            //target�܂ł̊p�x���J���Ă���Ƃ���charge�ŋl�߂�
            if (Mathf.Abs(targetAngle) > 45)
            {
                R2Bot = true;
            }
        }
    }
    void AttackEnd()
    {
        isAttack = false;
    }
    void AvoidWallStart()
    {
        //30~90���܂ŕǂ��痣�ꂳ����
        turnWallAngle = Random.Range(30, 90);
        turnAddStartAngle = 0;
        turnAddStartCheck = false;

        //�ǂ���ړ�����Ԃ͏�Ƀ`���[�W���Đ���͂����コ����,�`���[�W�I���^�C�~���O�͕ʃX�e�[�g�ŊǗ�
        R2Bot = true;
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, wallHitDistance, layerMaskWall))
        {
            if (hit.collider.CompareTag("tree"))
            {
                turnWallAngle = Random.Range(5, 10);
            }
            float hitDistance = 0;
            bool turningR = true;
            if (Physics.Raycast(gameObject.transform.position, transform.forward + transform.right * 2 / 3, out hit, wallHitDistance * Mathf.Sqrt(2)*2, layerMaskWall))
            {
                hitDistance = Vector3.Distance(hit.point, this.transform.position);
            }
            //������Ȃ������Ƃ���������������
            else
            {
                hitDistance = 999;
            }
            if (Physics.Raycast(gameObject.transform.position, transform.forward - transform.right * 2 / 3, out hit, wallHitDistance * Mathf.Sqrt(2)*2, layerMaskWall))
            {
                //���̕����ǂɋ߂��Ƃ�����������E�ɐ؂�ւ�
                if (hitDistance < Vector3.Distance(hit.point, this.transform.position))
                {
                    turningR = false;
                }
            }
            if (turningR)
            {
                //axis_y��+90���E�����ő�
                axis_y = 90;
            }
            else
            {
                axis_y = -90;
            }
        }
    }
    void AvoidWallUpdate()
    {
        //�ǂ�hit���Ȃ������Ƃ�
        if (!Physics.Raycast(gameObject.transform.position, transform.forward, out hit, 30, layerMaskWall) && !turnAddStartCheck)
        {
            turnAddStartCheck = true;
            turnAddStartAngle = this.transform.eulerAngles.y;
        }
        float turnDifference = Mathf.Abs(turnAddStartAngle - this.transform.eulerAngles.y);
        //���Z�b�g�����܂����Ƃ��l��␳
        if (turnDifference > 180)
        {
            turnDifference = Mathf.Abs(turnDifference - 360);
        }
        //���ʂ̕ǂ����m���Ȃ��Ȃ��Ă���̊p�x�����𒴂�����I��
        if (turnDifference > turnWallAngle)
        {
            isAvoidWall = false;
            changeState(StateType.Idle);
        }
    }
    void AvoidWallEnd()
    {
        //������~�߂�
        axis_y = 0;
    }
    void chargeReleaseRand()
    {
        //8~10���`���[�W���I����Ă���Ƃ������_���Ń`���[�W����
        if (_moveTPS.maxCharge * Random.Range(0.8f, 1.0f) <= _moveTPS.chargeCount)
        {
            R2Bot = false;
        }
    }
    void moveBot_v()
    {
        ///<summary>
        ///debugRay
        ///</summary>
        {
            Debug.DrawRay(gameObject.transform.position, transform.forward * 10.0f, Color.blue, 0.1f, false);
            Debug.DrawRay(gameObject.transform.position, (transform.right / 3.0f + transform.forward) * 15.0f, Color.red, 0.1f, false);
            Debug.DrawRay(gameObject.transform.position, (-transform.right / 3.0f + transform.forward) * 15.0f, Color.red, 0.1f, false);
            Debug.DrawRay(gameObject.transform.position, (transform.right + transform.forward) * 15.0f, Color.green, 0.1f, false);
            Debug.DrawRay(gameObject.transform.position, (-transform.right + transform.forward) * 15.0f, Color.green, 0.1f, false);
        }
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, 20.0f))
        {
            //���ʂɕǂ�����Ƃ������_���ȃ^�C�~���O�Ő���J�n
            int distance = Random.Range(0, 15);
            if (distance <= 1)
            {
                if (hit.collider.CompareTag("wall") && !hit.collider.CompareTag("itemEnhance") && !changeDir)
                {
                    changeDir = true;
                    axis_y = (float)Random.Range(45.0f, 90.0f);
                    //���������50������
                    if (distance == 1)
                    {
                        axis_y *= -1;
                    }
                }
            }
            //�ǈȊO�̎������~
            if (!hit.collider.CompareTag("wall") && randNum != 0 && !IsTargetCheck())
            {
                changeDir = false;
                if (!itemTurn)
                {
                    axis_y = 0;
                }
            }
        }
        else
        {
            changeDir = false;
            if (!itemTurn && !IsTargetCheck() && Random.Range(0,100)==0)
            {
                axis_y = 0;
            }
        }
        iceShot();
        if (Physics.Raycast(gameObject.transform.position, transform.right / 3.0f + transform.forward, out hit, 30.0f))
        {
            if (hit.collider.CompareTag("itemEnhance") || hit.collider.CompareTag("itemEnhance2") || hit.collider.CompareTag("itemEnhance3") || hit.collider.CompareTag("itemEnhance4") || hit.collider.CompareTag("itemEnhance5") || hit.collider.CompareTag("itemAll"))
            {
                axis_y = 90f;
                itemTurn = true;
            }
        }
        else if (Physics.Raycast(gameObject.transform.position, transform.right / -3.0f + transform.forward, out hit, 30.0f))
        {
            if (hit.collider.CompareTag("itemEnhance") || hit.collider.CompareTag("itemEnhance2") || hit.collider.CompareTag("itemEnhance3") || hit.collider.CompareTag("itemEnhance4") || hit.collider.CompareTag("itemEnhance5") || hit.collider.CompareTag("itemAll"))
            {
                axis_y = -90f;
                itemTurn = true;
            }
        }
        
        if (IsTargetCheck() && targetTime < 15)
        {
            //���I�u�W�F�N�g�ƃ^�[�Q�b�g�Ƃ̊p�x���v�Z
            targetQrot = Quaternion.LookRotation(targetPoint - this.transform.position);
            targetEuler = targetQrot.eulerAngles;
            Vector3 itemNumEuler = this.gameObject.transform.localEulerAngles - targetEuler;
            if (itemNumEuler.y > 1.0f && itemNumEuler.y <= 180.0f)
            {
                axis_y = -90;
                targetTime += Time.deltaTime;

            }
            else if (itemNumEuler.y < -1.0f || itemNumEuler.y > 180.0f)
            {
                axis_y = 90;
                targetTime += Time.deltaTime;
            }
            else
            {
                axis_y = 0;
            }
            if (Random.Range(0, 5) == 0)
            {
                targetCharge = false;
            }
            if (targetCharge && (itemNumEuler.y > 1.0f || itemNumEuler.y < -1.0f))
            {
                R2Bot = true;
            }
            else if (itemNumEuler.y >= -1.0f && itemNumEuler.y <= 1.0f)
            {
                targetCharge = false;
            }
        }
        else if (targetTime > 0)
        {
            targetTime -= Time.deltaTime * 2.25f;
            targetCharge = false;
        }
        else
        {
            targetTime = 0;
            targetCharge = false;
        }
    }
    void basicActionBot()
    {
        //���x0.5�����̂Ƃ��A�����Ă��Ȃ��Ƃ݂Ȃ�
        if (rb.velocity.magnitude < 0.5f)
        {
            notMoveTimer += Time.deltaTime;
        }
        else
        {
            notMoveTimer = 0;
        }
        //2s�ȏ㓮�����A�`���[�W,���񂢂�������Ă��Ȃ��Ƃ�
        if (notMoveTimer > 2 && !R2Bot && axis_y == 0)
        {
            R2Bot = true;
            axis_y = Random.Range(60, 90);
            if (Random.Range(0, 2) == 0)
            {
                axis_y *= -1;
            }
        }
        ///<summary>
        ///debugRay
        ///</summary>
        {
            Debug.DrawRay(gameObject.transform.position, transform.forward * 10.0f, Color.blue, 0.1f, false);
            Debug.DrawRay(gameObject.transform.position, (transform.right / 3.0f + transform.forward) * 15.0f, Color.red, 0.1f, false);
            Debug.DrawRay(gameObject.transform.position, (-transform.right / 3.0f + transform.forward) * 15.0f, Color.red, 0.1f, false)
        }
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, wallHitDistance, layerMaskWall))
        {
            //���ʂɕǂ�����Ƃ������_���ȃ^�C�~���O�Ő���J�n
            int distance = Random.Range(0, 10);
            if (distance == 0)
            {
                //����X�e�[�g���N��
                isAvoidWall = true;
                //�X�e�[�g�̊��荞��
                changeState(StateType.AvoidWall);
            }
        }
    }
    //state�ɈڐA
    void moveBot_turning()
    {
        if (updataTimer > updataTimerSet)
        {
            randNum = Random.Range(1, 2);
        }
        //�n�`�ɋl�܂����Ƃ��`���[�W�Ɛ���
        if (rb.velocity.magnitude < 0.1f && !stopResolve)
        {
            if (Random.Range(0, 10) > 5)
            {
                stopResolve = true;
            }
        }

        if (itemTurn)
        {
            chargeBot();
        }

        if (!itemTurn && Random.Range(0, 300) == 0 && !IsTargetCheck())
        {
            turnRandom();
        }
        if (!itemTurn && Random.Range(0, 1000) == 0 && !IsTargetCheck())
        {
            axis_y = 0;
        }
        if (rb.velocity.magnitude >= 3.0f)
        {
            stopResolve = false;
            R2Bot = false;
        }
    }
    //�㉺�̓����𐧌�
    IEnumerator UDset()
    {
        float correctValue = 1;
        while (true)
        {

            switch (selectBallSet.selectBotHam[_moveTPS.botNum])
            {
                case 0:
                    correctValue = 1;
                    break;
                case 1:
                    correctValue = 1;
                    break;
                case 2:
                    correctValue = 0.1f;
                    break;
                case 3:
                    correctValue = 0.8f;
                    break;
                case 4:
                    correctValue = 1;
                    break;

            }
            if (_moveTPS.isWater)
            {
                UDcounterBot = Random.Range(0.4f, 1.0f) * correctValue;
            }
            else if (!ground.IsGround())
            {
                UDcounterBot = Random.Range(-0.6f, 0.8f);
            }
            else
            {
                UDcounterBot = 0;
            }
            yield return new WaitForSeconds(1);
        }
    }
    void raceBot()
    {
        Debug.DrawRay(gameObject.transform.position, transform.forward * 15.0f, Color.blue, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, (transform.right / 2.0f + transform.forward) * 10.0f, Color.red, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, (-transform.right / 2.0f + transform.forward) * 10.0f, Color.red, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, (transform.right + transform.forward) * 50.0f, Color.green, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, (-transform.right + transform.forward) * 50.0f, Color.green, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, transform.right * 20.0f, Color.yellow, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, -transform.right * 20.0f, Color.yellow, 0.1f, false);
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, 10.0f))
        {
            axisTime += Time.deltaTime;
            axisTimeMax = axisTime;
            //���ʂ�hit�����Ƃ����E�m�F
            if (Physics.Raycast(gameObject.transform.position, transform.right / 2.0f + transform.forward, out hit, 10.0f))
            {
                if (hit.collider.CompareTag("wall") || hit.collider.CompareTag("obstacle"))
                {
                    obstacleCheckR = true;
                }
            }
            else
            {
                obstacleCheckR = false;
            }
            if (Physics.Raycast(gameObject.transform.position, transform.right / -2.0f + transform.forward, out hit, 10.0f))
            {
                if (hit.collider.CompareTag("wall") || hit.collider.CompareTag("obstacle"))
                {
                    obstacleCheckL = true;
                }
            }
            else
            {
                obstacleCheckL = false;
            }
            if(obstacleCheckL && !obstacleCheckR)
            {
                axis_y = 90;
            }
            else if(!obstacleCheckL && obstacleCheckR)
            {
                axis_y = -90;
            }
            //���E�������ӂ������Ă���Ƃ�
            if (obstacleCheckL && obstacleCheckR)
            {
                //�ǂ�����ΐ�Βl�ŋ�����ۑ��A�Ȃ���Α傫�Ȑ����ŏ㏑��
                if (Physics.Raycast(gameObject.transform.position, transform.right + transform.forward, out hit, 100, layerMaskWall))
                {
                    rightWall.x = Mathf.Abs(hit.point.x - this.transform.position.x);
                    rightWall.z = Mathf.Abs(hit.point.z - this.transform.position.z);
                }
                else
                {
                    rightWall.x = 999;
                    rightWall.z = 999;
                }
                if (Physics.Raycast(gameObject.transform.position, -transform.right + transform.forward, out hit, 100, layerMaskWall))
                {
                    leftWall.x = Mathf.Abs(hit.point.x - this.transform.position.x);
                    leftWall.z = Mathf.Abs(hit.point.z - this.transform.position.z);
                }
                else
                {
                    leftWall.x = 999;
                    leftWall.z = 999;
                }
                //�E�̕ǂ̕��������������Ƃ��E�Ɍ���
                if (rightWall.x + rightWall.z > leftWall.x + leftWall.z)
                {
                    axis_y = 90;
                }
                else if (rightWall.x + rightWall.z < leftWall.x + leftWall.z)
                {
                    axis_y = -90;
                }
                //�l���߂��Ƃ��ǂ��炩�ɌX����
                if(Mathf.Abs((rightWall.x + rightWall.z) - (leftWall.x + leftWall.z)) < 30)
                {
                    nearDist = true;
                }
                else
                {
                    nearDist = false;
                }
                
            }
        }
        else
        {
            if (axisTime > 0)
            {
                //�p�x�ύX���Ԃ̔����̎��Ԕ��]
                if (axisTime == axisTimeMax)
                {
                    Debug.Log("reverse");
                    axis_y *= -0.5f;
                    axisTimeMax = 0;
                }
                axisTime -= Time.deltaTime * 3;
            }
            else
            {
                axis_y = 0;
            }
        }
        if (Physics.Raycast(gameObject.transform.position, transform.right, out hit, 50.0f))
        {
            if (hit.collider.CompareTag("wall") || hit.collider.CompareTag("obstacle"))
            {
                alwaysRotateR = hit.point - this.transform.position;

                //��Q�����班������Ă���or�ǂ̎�
                if((hit.collider.CompareTag("obstacle")&&Mathf.Abs(alwaysRotateR.x)+ Mathf.Abs(alwaysRotateR.z)>5)|| hit.collider.CompareTag("wall"))
                {
                    roadWidthCheckR = true;
                }
                else
                {
                    roadWidthCheckR = false;
                }
            }
        }
        if (Physics.Raycast(gameObject.transform.position, -transform.right, out hit, 50.0f))
        {
            if (hit.collider.CompareTag("wall") || hit.collider.CompareTag("obstacle"))
            {
                alwaysRotateL = hit.point - this.transform.position;
                if ((hit.collider.CompareTag("obstacle") && Mathf.Abs(alwaysRotateL.x) + Mathf.Abs(alwaysRotateL.z) > 5) || hit.collider.CompareTag("wall"))
                {
                    roadWidthCheckL = true;
                }
                else
                {
                    roadWidthCheckL = false;
                }
            }
        }
        float totalR = Mathf.Abs(alwaysRotateR.x) + Mathf.Abs(alwaysRotateR.z);
        float totalL = Mathf.Abs(alwaysRotateL.x) + Mathf.Abs(alwaysRotateL.z);
        
        if (totalR > totalL)
        {
            axis_y += 20;
            if (nearDist)
            {
                axis_y = 90;
            }
        }
        else if (totalR < totalL)
        {
            axis_y -= 20;
            if (nearDist)
            {
                axis_y = -90;
            }
        }

        if (roadWidthBefore > roadWidth * 1.25f && (roadWidthCheckR || roadWidthCheckL))
        {
            R2Bot = true;
            //�O�t���[����蓹�����L�������甽�]
            if (roadWidthBefore > totalR + totalL)
            {
                Debug.Log("reverse");
                axis_y *= -1;
            }
        }
        else if (roadWidthBefore < roadWidth + 5)
        {
            //�`���[�W��������x����Ă���false�ɂ���悤�ύX�\��
            R2Bot = false;
        }
        //�������I���Ă���X�V�A1f���炷
        roadWidthBefore = totalR + totalL;
    }
    
    //���Ȃ���
    void raceBot_v()
    {
        Debug.DrawRay(gameObject.transform.position, transform.forward * 15.0f, Color.blue, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position + transform.right * 0.5f, transform.forward * 15.0f, Color.blue, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position - transform.right * 0.5f, transform.forward * 15.0f, Color.blue, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, (transform.right / 1.5f + transform.forward) * 15.0f, Color.red, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, (-transform.right / 1.5f + transform.forward) * 15.0f, Color.red, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, (transform.right + transform.forward) * 50.0f, Color.green, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, (-transform.right + transform.forward) * 50.0f, Color.green, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, transform.right * 20.0f, Color.yellow, 0.1f, false);
        Debug.DrawRay(gameObject.transform.position, -transform.right * 20.0f, Color.yellow, 0.1f, false);
        
        Vector3 alwaysRotateR45 = new Vector3(999, 999, 999);
        Vector3 alwaysRotateL45 = new Vector3(999, 999, 999);
        bool rayCheckForward = false;
        bool rayCheckRight = false;
        bool rayCheckLeft = false;
        obstacleCheckR = false;
        obstacleCheckL = false;
        
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, 15.0f))
        {
            if (hit.collider.CompareTag("obstacle"))
            {
                rayCheckForward = true;
            }
        }
        //�ʂ̔��a�̕����ˈʒu�����炷
        if (Physics.Raycast(gameObject.transform.position + transform.right * 0.5f, transform.forward, out hit, 15.0f))
        {
            if (hit.collider.CompareTag("obstacle"))
            {
                rayCheckRight = true;
            }
        }
        if (Physics.Raycast(gameObject.transform.position - transform.right * 0.5f, transform.forward, out hit, 15.0f))
        {
            if (hit.collider.CompareTag("obstacle"))
            {
                rayCheckLeft = true;
            }
        }

        if (rayCheckForward || rayCheckRight || rayCheckLeft)
        {
            Vector3 wallR = new Vector3(999, 999, 999);
            Vector3 wallL = new Vector3(999, 999, 999);
            Vector3 wallR45 = new Vector3(999, 999, 999);
            Vector3 wallL45 = new Vector3(999, 999, 999);
            if (Physics.Raycast(gameObject.transform.position, transform.right, out hit, 300.0f, layerMaskWall))
            {
                wallR = hit.point - this.gameObject.transform.position;
            }
            if (Physics.Raycast(gameObject.transform.position, -transform.right, out hit, 300.0f, layerMaskWall))
            {
                wallL = hit.point - this.gameObject.transform.position;
            }
            if (Physics.Raycast(gameObject.transform.position, transform.forward + transform.right, out hit, 300.0f))
            {
                wallR45 = hit.point - this.gameObject.transform.position;
            }
            if (Physics.Raycast(gameObject.transform.position, transform.forward - transform.right, out hit, 300.0f))
            {
                wallL45 = hit.point - this.gameObject.transform.position;
            }
            float[] correctionWallR = forwardSin(wallR, wallR45);
            float[] correctionWallL = forwardSin(wallL, wallL45);

            if (Mathf.Abs(correctionWallR[0]) + correctionWallR[1] > Mathf.Abs(correctionWallL[0]) + correctionWallL[1])
            {
                reversePreventionR += 1;
                axis_y = 90;
            }
            else
            {
                reversePreventionL += 1;
                axis_y = -90;
            }
            
        }
        
        else
        {
            roadCounter += 1;
            //3f�̊Ԓ���
            if (roadCounter <= 4)
            {
                if (Random.Range(0, 2) == 0)
                {
                    axis_y = 0;
                }
            }
            //10f�Ɉ��A�K�^�K�^
            if (roadCounter == 5)
            {
                //�΂߂ɂȂ����Ƃ��n�ʂɐڐG����\�������邽��wall���C���[����
                if (Physics.Raycast(gameObject.transform.position, transform.right, out hit, 100.0f, layerMaskWall))
                {
                    alwaysRotateR = hit.point - this.gameObject.transform.position;
                }
                if (Physics.Raycast(gameObject.transform.position, -transform.right, out hit, 100.0f, layerMaskWall))
                {
                    alwaysRotateL = hit.point - this.gameObject.transform.position;
                }
                if (Physics.Raycast(gameObject.transform.position, transform.forward + transform.right, out hit, 100.0f, layerMaskWall))
                {
                    alwaysRotateR45 = hit.point - this.gameObject.transform.position;
                }
                if (Physics.Raycast(gameObject.transform.position, transform.forward - transform.right, out hit, 100.0f, layerMaskWall))
                {
                    alwaysRotateL45 = hit.point - this.gameObject.transform.position;
                }
                float[] correctionRotateR = forwardSin(alwaysRotateR, alwaysRotateR45);
                float[] correctionRotateL = forwardSin(alwaysRotateR, alwaysRotateL45);

                float randomWayR = 0;
                float randomWayL = 0;
                if (correctionCountR > correctionCountL)
                {
                    randomWayR = Random.Range(correctionCountR - 10, correctionCountR);
                }
                else
                {
                    randomWayL = Random.Range(correctionCountL - 10, correctionCountL);
                }


                if (correctionRotateR[1] + randomWayR > correctionRotateL[1])
                {
                    //����͂ɍ��킹��axis�ύX
                    axis_y = 60;
                    correctionCountR += 1;
                    correctionCountL -= 1;
                }
                else if (correctionRotateR[1] < correctionRotateL[1] + randomWayL)
                {
                    axis_y = -60;
                    correctionCountL += 1;
                    correctionCountR -= 1;
                }
                if (Mathf.Abs(correctionCountR) >= 20)
                {
                    correctionCountR /= 2;
                    correctionCountL /= 2;
                    if (Random.Range(0, 2) == 0)
                    {
                        correctionCountR = 0;
                        correctionCountL = 0;
                    }
                }
            }
            if (roadCounter >= 10)
            {
                roadCounter = 0;
            }
        }
        iceShot();
    }        
    void raceBot_Charge()
    {
        //�l�܂����Ƃ��`���[�W�œ˔j
        if ((rb.velocity.magnitude < Random.Range(0, 5) || Random.Range(0,1200) == 0) && raceChargeCT <= 0)
        {
            R2Bot = true;
        }
        //max��8�`10���̃`���[�W�Ŏˏo
        if (_moveTPS.maxCharge * Random.Range(8,11) * 0.1f <= _moveTPS.chargeCount)
        {
            raceChargeCheck = true;
            R2Bot = false;
        }
        if (raceChargeCheck)
        {
            raceChargeCT += Time.deltaTime;
            if (raceChargeCTL < raceChargeCT)
            {
                raceChargeCT = 0;
                raceChargeCheck = false;
            }
        }
    }
    float raceBot_roadWidth(float R, float L, float axis_y)
    {
        if (R > L)
        {
            axis_y += 20;
            if (nearDist)
            {
                axis_y = 90;
            }
        }
        else if (R < L)
        {
            axis_y -= 20;
            if (nearDist)
            {
                axis_y = -90;
            }
        }
        return axis_y;
    }
    float[] forwardSin(Vector3 alwaysRotate, Vector3 alwaysRotate2)
    {
        float[] array = new float[2];
        //�ʂ̌������猩�����W�ɉ�]
        Vector3 returnVec3 = new Vector3(0, 0, 0);
        Vector3 returnVec3_2 = new Vector3(0, 0, 0);
        float localRotateY = this.gameObject.transform.localEulerAngles.y * Mathf.Deg2Rad;

        returnVec3.x = Mathf.Cos(localRotateY) * alwaysRotate.x - Mathf.Sin(localRotateY) * alwaysRotate.z;
        returnVec3.z = Mathf.Sin(localRotateY) * alwaysRotate.x + Mathf.Cos(localRotateY) * alwaysRotate.z;

        returnVec3_2.x = Mathf.Cos(localRotateY) * alwaysRotate2.x - Mathf.Sin(localRotateY) * alwaysRotate2.z;
        returnVec3_2.z = Mathf.Sin(localRotateY) * alwaysRotate2.x + Mathf.Cos(localRotateY) * alwaysRotate2.z;
        array[0] = returnVec3.x;
        array[1] = Mathf.Abs(returnVec3_2.x) + Mathf.Abs(returnVec3_2.z);
        return array;
    }
    float forwardObject_position(Vector3 alwaysRotate)
    {
        //�ʂ̌������猩�����W�ɉ�]
        Vector3 returnVec3 = new Vector3(0, 0, 0);
        float localRotateY = this.gameObject.transform.localEulerAngles.y * Mathf.Deg2Rad;

        returnVec3.x = Mathf.Cos(localRotateY) * alwaysRotate.x - Mathf.Sin(localRotateY) * alwaysRotate.z;
        returnVec3.z = Mathf.Sin(localRotateY) * alwaysRotate.x + Mathf.Cos(localRotateY) * alwaysRotate.z;

        return returnVec3.x;
    }
    /// <summary>
    /// ��_��x,z���g���ē�_�Ԃ̊p�x��Ԃ�
    /// </summary>
    /// <param name="start">�J�n�n�_</param>
    /// <param name="target">�I���n�_</param>
    /// <returns></returns>
    float getAngle(Vector3 start,Vector3 target)
    {
        Vector3 distance = target - start;
        float radian = Mathf.Atan2(distance.x, distance.z);
        float degree = radian * Mathf.Rad2Deg;
        return degree;
    }
    void axisBot()
    {
        axis_y = (float)Random.Range(0.5f, 1.0f);
    }
    void itemCheckBot()
    {
        if (IsTargetCheck())
        {
            //���I�u�W�F�N�g�ƃ^�[�Q�b�g�Ƃ̊p�x���v�Z
            targetQrot = Quaternion.LookRotation(targetPoint - this.transform.position);
        }
    }
    void chargeBot()
    {
        int randCharge=10;
        if (_moveTPS.chargeCount / _moveTPS.maxCharge > 0.3f)
        {
            randCharge = Random.Range(1, 100);
        }
        
        if ((_moveTPS.maxCharge > _moveTPS.chargeCount && randCharge > 50) || targetCharge)
        {
            R2Bot = true;
        }
        else
        {
            R2Bot = false;
        }
    }
    void turnRandom()
    {
        axis_y = Random.Range(60, 91);
        if (Random.Range(0, 2) == 0)
        {
            axis_y *= -1;
        }
    }
    void turnAvoidBot()
    {
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, 10.0f))
        {
            int distance = Random.Range(0, 2);
            if (!changeDir2)
            {
                changeDir2 = true;
                axis_y = (float)Random.Range(45.0f, 90.0f);
                if (distance == 1)
                {
                    axis_y *= -1;
                }
            }
        }
        else if(!IsTargetCheck())
        {
            changeDir2 = false;
            axis_y = 0;
        }
    }
    void useSkill()
    {
        skillTimer += Time.deltaTime;
        if (skillTimer > skillTimerSet)
        {
            skillTimer = 0f;
            skillRandom = Random.Range(0, 4);
        }
        //HP0�̂Ƃ��X�L��4���g�p
        if (_moveTPS.nowHP == 0 && selectAbiList.Contains(4))
        {
            abiUse_bot[4] = true;
            _moveTPS.overR1 = true;
        }
        //��Ԉُ�ɂȂ����Ƃ��X�L��1�g�p
        else if (_moveTPS.nowCondition && selectAbiList.Contains(1))
        {
            abiUse_bot[1] = true;
            _moveTPS.overR1 = true;
        }
        //HP�c�ʂ�70����؂����Ƃ��X�L��0�g�p
        else if(_moveTPS.nowHP < _moveTPS.maxHP * 0.7f && skillRandom==0 && selectAbiList.Contains(0))
        {
            abiUse_bot[0] = true;
            _moveTPS.abiCount = 0;
            skillRandom = 10;
        }
        //sp��4�ȏォ��������v�����Ƃ��X�L��2�g�p
        else if(skillRandom == 1 && _moveTPS.spCount>4 && selectAbiList.Contains(2) && (_moveTPS.itemEnhanceCount[0] < 20 || _moveTPS.itemEnhanceCount[1] < 20 || _moveTPS.itemEnhanceCount[2] < 20 || _moveTPS.itemEnhanceCount[3] < 20 || _moveTPS.itemEnhanceCount[4] < 20))
        {
            abiUse_bot[2] = true;
            _moveTPS.overR1 = true;
            skillRandom = 10;
        }
        else if (skillRandom == 2 && selectAbiList.Contains(5))
        {
            abiUse_bot[5] = true;
        }
        else if (skillRandom == 3 && selectAbiList.Contains(6))
        {
            abiUse_bot[6] = true;
        }
        else
        {
            _moveTPS.overR1 = false;
        }
    }
    void iceShot()
    {
        //�ő�ɂȂ�����Ƃ肠��������
        if (_moveTPS.lanceCount == 6)
        {
            attackBot = true;
        }
        if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, 15.0f))
        {
            if ((hit.collider.CompareTag("player") || hit.collider.CompareTag("itemEnhance") || hit.collider.CompareTag("itemEnhance2") || hit.collider.CompareTag("itemEnhance3") || hit.collider.CompareTag("itemEnhance4") || hit.collider.CompareTag("itemEnhance5") || hit.collider.CompareTag("itemAll")))
            {
                axis_y = 0f;
                itemTurn = false;
                //�c�e������Ƃ�����
                if (_moveTPS.lanceCount > 0)
                {
                    attackBot = true;
                }
            }
        }
    }
    IEnumerator iceAttack(float AttackRate, float ItemRate)
    {
        bool attackFlag = false;
        bool itemFlag = false;
        while (true)
        {
            if (_moveTPS.lanceCount == 6 && !isAvoidWall)
            {
                attackBot = true;
            }
            //���ʋ���30�ȓ��Ƀ^�[�Q�b�g������Ƃ�attack
            if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, 30.0f))
            {
                //item�X�e�[�g�̂Ƃ��ł�attack�̊����������Ƃ��͂��łɍU�����s��
                if (Random.Range(0.0f, 1.0f) <= AttackRate)
                {
                    attackFlag = true;
                }
                else
                {
                    attackFlag = false;
                }
                if (Random.Range(0.0f, 1.0f) <= ItemRate)
                {
                    itemFlag = true;
                }
                else
                {
                    itemFlag = false;
                }
                //attack���v���C���[������Ƃ�oritemGet���A�C�e��������Ƃ�
                if ((hit.collider.CompareTag("player")&&(isAttack||attackFlag))||
                    ((hit.collider.CompareTag("itemEnhance") ||
                    hit.collider.CompareTag("itemEnhance2") || hit.collider.CompareTag("itemEnhance3") ||
                    hit.collider.CompareTag("itemEnhance4") || hit.collider.CompareTag("itemEnhance5") ||
                    hit.collider.CompareTag("itemAll")|| hit.collider.CompareTag("tempEnhance1")||
                    hit.collider.CompareTag("tempEnhance2") || hit.collider.CompareTag("tempEnhance3")) ||
                    hit.collider.CompareTag("tempEnhance4")|| hit.collider.CompareTag("tempEnhance5") ||
                    hit.collider.CompareTag("tempEnhanceAll") && (isItemGet||itemFlag)))
                {
                    //�c�e������Ƃ�����
                    if (_moveTPS.lanceCount > 0)
                    {
                        //���˂�����shotCoolTime������attack��҂�
                        attackBot = true;
                        yield return new WaitForSeconds(shotCoolTime);
                    }
                }
            }
            yield return null;
        }
    }
    /// <summary>
    /// change state
    /// </summary>
    /// <param name="switchTime"></param>
    /// <param name="AttackRate"></param>
    /// <param name="ItemRate"></param>
    /// <returns></returns>
    IEnumerator selectState(float switchTime,float AttackRate,float ItemRate)
    {
        int tableVerticalNum = 0;
        int tableWidthNum = 0;
        while (true)
        {
            //�ǉ���X�e�[�g�łȂ��Ƃ�
            if (!isAvoidWall)
            {
                float randNum = Random.Range(0.0f, 1.0f);
                //1/10�ŉ��ɑJ��
                if (randNum < 0.1f)
                {
                    tableVerticalNum += 1;
                    //�e�[�u���̒[���z�����烊�Z�b�g
                    if (tableVerticalNum >= behaviorTable.Count)
                    {
                        tableVerticalNum = 0;
                    }
                }
                else
                {
                    tableWidthNum += 1;
                    if (tableWidthNum >= behaviorTable[0].Count)
                    {
                        tableWidthNum = 0;
                    }
                }
                //�e�[�u���ɍ��킹���X�e�[�g�ɑJ��
                switch (behaviorTable[tableVerticalNum][tableWidthNum])
                {
                    case 0:
                        changeState(StateType.Idle);
                        break;
                    case 1:
                        changeState(StateType.ItemGet);
                        break;
                    case 2:
                        changeState(StateType.Attack);
                        break;
                }
            }
            //�ǉ�����߂��ɂ���Ƃ�����X�e�[�g�ɐ؂�ւ�
            if (isAvoidWall)
            {
                changeState(StateType.AvoidWall);
            }
            yield return new WaitForSeconds(switchTime);
        }
    }
    /// <summary>
    /// Idle state �̍s�����Ǘ�,chrgeRate,turnRate�͍��v1.0�܂�
    /// </summary>
    /// <param name="interval"></param>
    /// <param name="chargeRate"></param>
    /// <param name="turnRate"></param>
    /// <returns></returns>
    IEnumerator IdleAction(float interval,float chargeRate,float turnRate)
    {
        while (isIdle)
        {
            float actionNum = Random.Range(0.0f, 1.0f);
            if (actionNum < chargeRate)
            {
                R2Bot = true;
                if (Random.Range(0.0f, 1.0f) < turnRate)
                {
                    //�X�ɗ������͈͓��̎��`���[�W���ɐ��������
                    axis_y = Random.Range(45, 90);
                    if (Random.Range(0, 2) == 0)
                    {
                        axis_y *= -1;
                    }
                }
            }
            else if (actionNum < chargeRate + turnRate)
            {
                //45~90�̃����_���Ȑ���͂�^����
                axis_y = Random.Range(45, 90);
                //1/2�̊m���Ő�������𔽓]
                if (Random.Range(0, 2) == 0)
                {
                    axis_y *= -1;
                }
            }
            else
            {
                axis_y = 0;
            }
            Debug.Log(axis_y);
            yield return new WaitForSeconds(interval);
        }
    }

    bool IsTargetCheck()
    {
        if (_moveTPS.itemNumDes)
        {
            isTargetCheckStay = false;
            isTargetCheckEnter = false;
            isTargetCheckExit = true;
            _moveTPS.itemNumDes = false;
        }
        if (isTargetCheckStay || isTargetCheckEnter)
        {
            targetCheck = true;

        }
        else if (isTargetCheckExit)
        {
            targetCheck = false;
            
        }
        isTargetCheckStay = false;
        isTargetCheckEnter = false;
        isTargetCheckExit = false;
        return targetCheck;
    }

    public bool IsWall()
    {
        if (isWallEnter || isWallStay)
        {
            isWall = true;
        }
        else if (isWallExit)
        {
            isWall = false;
        }

        isWallEnter = false;
        isWallStay = false;
        isWallExit = false;
        return isWall;
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "wall")
        {
            isWallStay = true;
        }
        if (collision.tag == "detectionRange" || collision.tag == "detectionRange2" || collision.tag == "detectionRange3")
        {
            isTargetCheckStay = true;
            targetPoint = collision.transform.position;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "wall")
        {
            isWallEnter = true;
        }
        if (collision.tag == "detectionRange" || collision.tag == "detectionRange2" || collision.tag == "detectionRange3")
        {
            isTargetCheckEnter = true;
            targetPoint = collision.transform.position;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "wall")
        {
            isWallExit = true;
        }
        if (collision.tag == "detectionRange" || collision.tag == "detectionRange2" || collision.tag == "detectionRange3")
        {
            //�Ώ۔͈͂���o���t���[���ň�U���񃊃Z�b�g
            axis_y = 0;
            isTargetCheckExit = true;
            targetPoint = collision.transform.position;
        }
    }
}
