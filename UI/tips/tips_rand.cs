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
                                                           "�M�~�b�N�F����","�M�~�b�N�F����","�M�~�b�N�F���[�v�|�C���g"};
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
                                                                 "�e�n�ɓ_�݂��Ă���Ȃ��̏ꏊ�B\n���΂炭��ɏ���Ă���Ƃǂ����֔�΂����B\n�������������Ă݂悤�B"};

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
