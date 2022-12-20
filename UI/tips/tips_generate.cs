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
    //�^�C�g���̔w�i�F
    Color32[] titleColor = new Color32[] { new Color32(90, 235, 255, 255), new Color32(150, 255, 90, 255), new Color32(255, 60, 190, 255) };
    //���ꂼ��̃I�u�W�F�N�g�̈ʒu
    List<Vector3> rectLocalPosList = new List<Vector3>() { new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 0, 0), 
                                                           new Vector3(-424.2f, -38, 0), new Vector3(185, -34, 0), new Vector3(-189, 123, 0) };
    //��g�̈ʒu
    List<Vector3> rectFramePosList = new List<Vector3>() { new Vector3(910, 430, 0), new Vector3(2610, 430, 0), new Vector3(4310, 430, 0) };
    [SerializeField] Sprite[] playerImg;
    List<string> playerTitleTxtList = new List<string>() { "�{�[���F�y���{�[��","�{�[���F���ʂ̃{�[��","�{�[���F�d���{�[��","�{�[���F�`���[�W�{�[��",
                                                           "�{�[���F��b���{�[��","�{�[���F����{�[��","�{�[���F�����{�[��","�{�[���F���y���{�[��",
                                                           "�X�L���F�ɂ񂶂�","�X�L���F�Ԃɂ񂶂�","�X�L���F�Ђ܂��̎�","�X�L���F�V�[���h","�X�L���F����",
                                                           "�X�L���F����","�X�L���F�W�����v","�X�L���F�X�U��"};

    List<string> playerDescriptionTxtList = new List<string>() { "�󒆐��\�������A�X�L�������܂�₷���B\n����ɒn��ł̑��x���x���A�ϋv�͂��Ⴂ�B\n�W�����v�X�L���Ƒ������Q�B",
                                                           "��{�I�Ȑ��\�����B\n���S�҂�m�[�}���Ȑ��\���D�ސl�ɂ������߁B\n�l�X�ȃX�L����\n�g�ݍ��킹�čD�݂̃Z�b�g�������悤�B",
                                                           "�ō����x��ϋv�͂������B\n�`���[�W���x�������͂��Ⴂ�B\n�~�܂炸�ɂǂ�ǂ���������悤�B",
                                                           "�ő�`���[�W�������Ƃ��̑��x���i�i�ɑ����B\n����ŕ��i�̐��\�͒Ⴍ�A�X�L�������܂�ɂ����B\n�����X�L���Ƒ������Q�B",
                                                           "�S�̓I�ɔ\�͂������B\n�������A�`���[�W���邱�Ƃ��ł��������͂Ɍ�����B\n���肵�����肪�������Ƃ��ɑI�ڂ��B",
                                                           "�\�͂����߂ŃX�L�������܂�₷���B\n����A�u���[�L���قƂ�ǌ������i�ݑ����Ă��܂��B\n���鐫�\���������đ����Ƌ��������H",
                                                           "���x�������A�u���[�L�������B\n�`���[�W���ȊO�͋Ȃ��邱�Ƃ��ł���\n�O�ɂ����i�߂Ȃ��B\n�O���b�v�͂�����������������Ă݂悤�B",
                                                           "�S�Ă̔\�͂��������B\n�ϋv�͂����ɒႭ�A�X�L�������܂�Â炢�B\n��Ԉُ��U���ɋC��t���đ��낤�B",
                                                           "�K�v�|�C���g�F1\n�ő�HP��2�����񕜂��邱�Ƃ��ł���B\n�K�v�|�C���g�����Ȃ�����\n�ϋɓI�Ɏg���ėl�X�ȏꏊ�ɒ��킵�悤�B",
                                                           "�K�v�|�C���g�F2\n���ׂĂ̏�Ԉُ�𒼂����Ƃ��ł���B\n�󋵂ɍ��킹�Ďg�����B",
                                                           "�K�v�|�C���g�F3\n�����_���ŃX�e�[�^�X��\n��i�K�㏸������B\n�X�L���|�C���g���]�����Ƃ��ɂ҂�����B",
                                                           "�K�v�|�C���g�F1\n15�b�̊�\n��ѓ����g����邱�Ƃ��ł���B\n�U����h���ň��S�ɑ��낤",
                                                           "�K�v�|�C���g�F6\nHP��0�ɂȂ����Ƃ����Ԍo�߂�҂����ɕ����ł���B\n����Ă��܂����Ƃ��̕ی��Ƃ��Ă��������낤���B",
                                                           "�K�v�|�C���g�F2\n�ő�`���[�W�Ɠ��������͂𓾂邱�Ƃ��ł���B\n�`���[�W�ɂ����������{�[�����g���Ƃ��͐ϋɓI�ɑI�ڂ��B",
                                                           "�K�v�|�C���g�F2\n�n��ɂ���Ƃ��W�����v�����邱�Ƃ��ł���B\n�󒆋@���͂������{�[�����g���Ƃ���\n���̃X�L���ŃX�e�[�W�𑖂��낤�B",
                                                           "�X�|�C���g��1����邱�Ƃ�\n1���X�𔭎˂��邱�Ƃ��ł���B\n���˂����U���̓A�C�e���̉����\n�G�ւ̍U���A��̔j��Ȃǂ��s�����Ƃ��ł���B"};

    [SerializeField] Sprite[] systemImg;
    List<string> systemTitleTxtList = new List<string>() { "��{�F�ċz","��{�F�����o�[","��Ԉُ�F�Ώ�", "��Ԉُ�F����", "��Ԉُ�F�����₯","���", "��Ԉُ�F��", "��Ԉُ�F����",
                                                           "��Ԉُ�F����", "�T���F����", "�T���F�ΎR", "�T���F�ጴ", "�T���F�C","�T���F����", "�T���F�n��", "���[�X�F���H","���[�X�F���̎�",
                                                           "�M�~�b�N�F����","�M�~�b�N�F����","�M�~�b�N�F���[�v�|�C���g","�M�~�b�N�F�X������"};
    List<string> systemDescriptionTxtList = new List<string>() { "�����ł̊������Ԃ�\���B\n�Q�[�W���Ȃ��Ȃ�ƃ_���[�W���󂯂�B\n������o����A�����̖A�ɐG�ꂽ�肵�ăQ�[�W��\n�񕜂��Ȃ���C��t���ĒT�����悤�B",
                                                                 "���݂̋�����Ԃ�\���B\n�������5�W�߂邲�Ƃɂ������Ƃ�...?",
                                                                 "�Ώ���ԂɂȂ�A�_���[�W���󂯂�B\n�`���[�W���\�����ċȂ���Â炭�Ȃ�B\n�g�����Ȃ��΋��������ɂȂ邩���H",
                                                                 "������ԂɂȂ�A���񑀍삪���]����B\n������Ԃɖ|�M����Ȃ��悤�C��t���đ��낤�B",
                                                                 "�����₯��ԂɂȂ�A�`���[�W���x���Ȃ�B\n�ō����x���㏸����B\n�`���[�W���g��Ȃ���΋��������ɁB",
                                                                 "��჏�ԂɂȂ�A\n�`���[�W���ő�̔��������ł��Ȃ��Ȃ�B\n��჏�ԂɂȂ�Ȃ��悤�ɋC��t���悤�B",
                                                                 "�ŏ�ԂɂȂ�A�_���[�W���󂯂�B\n�Ȃ���͂��㏸����B\n�オ�����@���͂ŉ񕜃A�C�e����\n�����Ď��ɍs�����B",
                                                                 "�����ԂɂȂ�A�����Ȃ��Ȃ�B\n�����Ă���Ԃ�HP���񕜂���B\nHP�����Ȃ��Ƃ��͂ЂƖ��肵�Ă݂悤�B",
                                                                 "������ԂɂȂ�A�����Ȃ��Ȃ�B\n�����Ă���ԃ_���[�W���󂯂�B\n����Ƃ������_���ň��������邽�ߋC��t���悤�B",
                                                                 "����؂��������΂��ӂ��G���A�B\n�ł▰��ɂȂ�₷���B\n���̏ꏊ�ɃA�C�e�����������₷���B",
                                                                 "�R�Ɗ�΂łł����G���A�B\n�₯�ǂɂȂ�₷���B\n����󂵂ăn���X�^�[����Ă悤�B",
                                                                 "��̍~��␢�E�̃G���A�B\n�����₯�ɂȂ�₷���B\n��̉���󂵂ăn���X�^�|����Ă悤�B",
                                                                 "���Ŗ������ꂽ�C�̃G���A�B\n�ċz�̎c��ɒ��ӁB\n�y�ǂ̒������Ă݂悤�B",
                                                                 "�X�e�[�W�ɕ����ԂȂ��̓��B\n���̏�ɂ͗ǂ��A�C�e�������邱�Ƃ������B",
                                                                 "�X�e�[�W�̒n���ɂ���R���N���[�g�̃G���A�B\n�{�[����ς������Ƃ��ɍs���Ƃ��������H",
                                                                 "���Ɠ��H�łł������[�X��B\n�X�e�[�W�̂قƂ�ǂ������łł��Ă���B\n��̃G���A�قǒʂ�₷���B",
                                                                 "��ɕ����Ԕ��̎��^�̃��[�X��B\n�X�e�[�W�̂قƂ�ǂ��J�[�u�łł��Ă���B\n�Ȑ��Ɏ��M������{�[���ɂ������߁B",
                                                                 "�C���畬�o����鐅�B\n���ɏ���Ă���ԏ㏸�������邱�Ƃ��ł���B",
                                                                 "��ɉ�]���Ă��������̕��B\n���ɏ���Ă���ԏ㏸�������邱�Ƃ��ł���B",
                                                                 "�e�n�ɓ_�݂��Ă���Ȃ��̏ꏊ�B\n���΂炭��ɏ���Ă���Ƃǂ����֔�΂����B\n�������������Ă݂悤�B",
                                                                 "�X�̑��𔭐���������������B\n���̒����甭������X�ɓ�����ƃ_���[�W���󂯂�B\n���S�ȂƂ���ɓ����悤�B"};

    [SerializeField] Sprite[] itemImg;
    List<string> itemTitleTxtList = new List<string>() { "��F�X�s�[�h��", "��F�u���[�L��", "��F���񂩂���", "��F�`���[�W��", "��F��������", "��F�I�[����", "��F�ɂ����̎�",
                                                         "�ɂ񂶂�F�ӂ��̂ɂ񂶂�","�ɂ񂶂�F�Ԃɂ񂶂�","�ɂ񂶂�F���������ɂ񂶂�","�ɂ񂶂�F�Ȃ��̂ɂ񂶂�","�ɂ񂶂�F���ɂ񂶂�" ,
                                                         "���v�F�X�s�[�h���v","���v�F�u���[�L���v","���v�F���񂩂����v","���v�F�`���[�W���v","���v�F���������v","���v�F�I�[�����v","�Ђ܂��"};
    List<string> itemDescriptionTxtList = new List<string>() { "�ő呬�x���㏸����Ђ܂��̎�B",
                                                               "�u���[�L�͂��㏸����Ђ܂��̎�B",
                                                               "�Ȃ���͂��㏸����Ђ܂��̎�B",
                                                               "�`���[�W���x���㏸����Ђ܂��̎�B",
                                                               "�����͂��㏸����Ђ܂��̎�B",
                                                               "�S�Ă̔\�͂��㏸����Ђ܂��̎�B",
                                                               "�_���[�W���󂯂�Ђ܂��̎�̋U���B",
                                                               "HP���񕜂��镁�ʂ̂ɂ񂶂�B",
                                                               "��Ԉُ킪�񕜂���Ԃ��ɂ񂶂�B\n��Ԉُ�ł͂Ȃ��Ƃ��͎���Ă������N����Ȃ��B",
                                                               "�ŏ�ԂɂȂ镅�����ɂ񂶂�B\n�ł����܂ɂ������Ƃ����邩��...�H",
                                                               "���ƍ�������������ɂ񂶂�B\n��Ԉُ�̎��Ɏ��Ə�Ԉُ킪�񕜂���B",
                                                               "�l�X�Ȍ��ʂ������F�̂ɂ񂶂�B\n�ǂ����ʂ��l�܂��Ă���B\n������������ɍs�����B",
                                                               "�ő呬�x���ꎞ�I�ɍő�܂ŏ㏸���鎞�v�B\n���΂炭�o�ƌ��ʂ��Ȃ��Ȃ邽�ߒ��ӁB",
                                                               "�u���[�L�͂��ꎞ�I�ɍő�܂ŏ㏸���鎞�v�B\n���΂炭�o�ƌ��ʂ��Ȃ��Ȃ邽�ߒ��ӁB",
                                                               "�Ȃ���͂��ꎞ�I�ɍő�܂ŏ㏸���鎞�v�B\n���΂炭�o�ƌ��ʂ��Ȃ��Ȃ邽�ߒ��ӁB",
                                                               "�`���[�W���x���ꎞ�I�ɍő�܂ŏ㏸���鎞�v�B\n���΂炭�o�ƌ��ʂ��Ȃ��Ȃ邽�ߒ��ӁB",
                                                               "�����͂��ꎞ�I�ɍő�܂ŏ㏸���鎞�v�B\n���΂炭�o�ƌ��ʂ��Ȃ��Ȃ邽�ߒ��ӁB",
                                                               "�S�Ă̔\�͂��ꎞ�I�ɍő�܂ŏ㏸���鎞�v�B\n���΂炭�o�ƌ��ʂ��Ȃ��Ȃ邽�ߒ��ӁB",
                                                               "�X�L���|�C���g�ƕX�|�C���g��\n�������܂�悤�ɂȂ�Ђ܂��B\n���̒T����5�܂ł����o�����Ȃ�����\n�ϋɓI�Ɏ��ɍs�����B"};

    int selectNum = 1;
    int selectNumNow = 1;
    int selectNumR;
    int selectNumL;
    //���ړ��̊������m�F
    bool changeCheck = true;

    List<string> mametishiki = new List<string>() { "�V�X�e���܂߂�����", "�v���C���[�܂߂�����", "�A�C�e���܂߂�����" };
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
        //�Ԃ̍L�����擾
        marginX = rectFramePosList[1].x - rectFramePosList[0].x;
        //�v�f�����e�v�f�ɍ��킹��
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
    /// Tips��������������
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
                //���̏ꏊ�ŏ����ł���悤�ɉ����X�g�Ɋi�[
                childs.Add(childObj);
            }
            //�F�A�摜�A���̍����ւ�
            Image titleImg = childs[2].GetComponent<Image>();
            Image thumbImg = childs[3].GetComponent<Image>();
            TextMeshProUGUI descTxt = childs[4].GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI titleTxt = childs[5].GetComponent<TextMeshProUGUI>();
            titleImg.color = titlecolor;
            thumbImg.sprite = thumbnails[i];
            descTxt.text = descriptionText[i];
            titleTxt.text = titleText[i];

            //�ЂƂ܂Ƃ߂ɂ���
            GameObject semiParent = new GameObject();
            for (int j = 0; j < instantSet.Length; j++)
            {
                RectTransform _rect = childs[j].GetComponent<RectTransform>();
                //���ꂼ��̈ʒu�𒲐�
                Vector3 rectNowPos = new Vector3(/*45*/-1655 + rectLocalPosList[j].x, 350-350*i + rectLocalPosList[j].y, rectLocalPosList[j].z);
                _rect.anchoredPosition3D = rectNowPos;
                childs[j].transform.parent = semiParent.transform;
            }
            //�ЂƂ܂Ƃ߂��i�[
            allList[ListNum].Add(semiParent);
            //���ꂼ��̍��ڂ̑�g�Ɋi�[
            allList[ListNum][i].transform.parent = parents[ListNum].transform;
        }
        //�X�N���[���p�ɃO���[�o���ŕۑ�
        _rectTransforms.Add( parents[ListNum].GetComponent<RectTransform>());
        _rectTransforms[ListNum].anchoredPosition3D = rectFramePosList[ListNum];
    }
    // Update is called once per frame
    void Update()
    {
        //�J�ڂ��ĂȂ��Ƃ��̂ݓ��͗L����
        if (changeCheck)
        {
            inputGet();
            //���E�ǂ��炩�����͂��ꂽ�Ƃ�
            if ((rightArrow_down || leftArrow_down)&&!returnCheck)
            {
                StartCoroutine(moveLR());
            }
        }
        changeButton();
        //return�{�^���I�𒆂�tips�̓������~
        if (!returnCheck && coolTimer <= 0)
        {
            moveUD();
        }
        //return����߂����Ƃ����̂܂ܓ����n�߂Ȃ��悤�ɏ����x��������
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
            //�L�[�������ꂽ�瑦���ɓ�������悤�ɂ���
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
        //�^�C�}�[���Z�b�g
        moveLRTimer = 0;
        changeCheck = false;

        _AudioSource.PlayOneShot(a_select);

        
        float moveDistance = 0;
        if (rightArrow_down)
        {
            //�Ĕz�u����I�u�W�F�N�g�̒l��ۑ�
            selectNum -= 1;
            if (selectNum < 0)
            {
                selectNum = rectFramePosList.Count - 1;
            }
            //�J�ڌ�̒��S�̒l��ۑ�
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
            //�����ʒu�ƖړI�ʒu��ۑ�
            nowPos.Add(new Vector3(_rectTransforms[i].anchoredPosition3D.x, _rectTransforms[i].anchoredPosition3D.y, 0));
            targetPos.Add(new Vector3(_rectTransforms[i].anchoredPosition3D.x + moveDistance, _rectTransforms[i].anchoredPosition3D.y, 0));
        }
        while (moveLRTimer <= moveLRTime)
        {
            moveLRTimer += Time.deltaTime;
            //�w�莞�Ԃŏ��X�ɖړI�̈ʒu�܂ł��炷
            for (int i=0;i< _rectTransforms.Count; i++)
            {
                _rectTransforms[i].anchoredPosition3D = Vector3.Lerp(nowPos[i], targetPos[i], moveLRTimer / moveLRTime);
            }
            
            //���[�v�𔲂���^�C�~���O�ňړ�������,�z�u���C��
            if (moveLRTimer >= moveLRTime)
            {
                changeCheck = true;
                mameTxt.text = mametishiki[selectNumNow];
                if (leftArrow_down)
                {
                    //�����o���ꂽ�[�̑�g���t���ɍĔz�u����
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
                //y����������
                for(int i=0;i< _rectTransforms.Count; i++)
                {
                    _rectTransforms[i].anchoredPosition3D = new Vector3(_rectTransforms[i].anchoredPosition3D.x, rectFramePosList[i].y, _rectTransforms[i].anchoredPosition3D.z);
                }
                //�X�N���[���ʒu��������
                scrollNow = 0;
            }
            yield return null;
        }
        
    }
}
