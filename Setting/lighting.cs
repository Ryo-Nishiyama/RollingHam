using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class lighting : MonoBehaviour
{
    public Material nightMoon;
    public Material epicSun;
    public Light DirectionalLight;
    public GameObject enhanceRock;
    GameObject _enhanceRock;
    GameObject[] _enhanceRocks = new GameObject[3];
    public bool isEvent, isCharge, isRock, isRecover, isMass, isBias, isSize, isNight = false;
    bool popSE_Check = false;
    public bool debug = true;
    List<Vector3> rockPosition = new List<Vector3> {new Vector3(-8, -12, 90) ,new Vector3(90, 5.5f, 100), new Vector3(3, 3, 200),
                                                    new Vector3(211,7.05f,277),new Vector3(200,-14,-25),new Vector3(265,32,27)};
    AudioSource _AudioSource;
    [SerializeField] AudioClip a_eventPop;

    float fogLerp = 5f;
    float fogTime,eventTime = 0f;
    float EndTime = 30f;
    float marginTime = 10f;
    float randTime = 0f;
    public int randEventCheck = 0;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        if (!debug)
        {
            if (eventSet.EventFrecency() == 0)
            {
                marginTime = 120f;
                randTime = -60;
            }
            else if(eventSet.EventFrecency() == 1)
            {
                marginTime = 60;
                randTime = -30;
            }
            else if (eventSet.EventFrecency() == 2)
            {
                marginTime = 30;
            }
            else if (eventSet.EventFrecency() == 3)
            {
                marginTime = 10;
                randTime = EndTime;
            }
        }
        else
        {
            randTime += EndTime+3000;
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(randEventCheck == 1)
        {
            fogGenerator();
        }
        else if (randEventCheck == 2)
        {
            chargeEvent();
        }
        else if (randEventCheck == 3)
        {
            recoverEvent();
        }
        else if(randEventCheck == 4)
        {
            rockEvent();
        }
        else if (randEventCheck == 5)
        {
            massGeneration();
        }
        else if (randEventCheck == 6)
        {
            biasEvent();
        }
        else if (randEventCheck == 7)
        {
            changeSizeEvent();
        }
        else if (randEventCheck == 8)
        {
            nightEvent();
        }
        randEvent();
    }
    void randEvent()
    {
        randTime += Time.deltaTime;
        //�C�x���g�I������marginTime�b��ɂ�����x�C�x���g�J�n
        if (randTime > EndTime + marginTime)
        {
            popSE_Check = false;
            randEventCheck = Random.Range(0, 9);
            randTime = 0f;
            
        }
        else if (randTime > EndTime && isEvent == false)
        {
            randEventCheck = 0;
            eventTime = 0;
        }
        if (isEvent && !popSE_Check) 
        {
            popSE_Check = true;
            //�C�x���g�J�n�̒ʒm����炷
            StartCoroutine(popSE());
        }
    }

    void fogGenerator()
    {
        if (fogTime == 0)
        {
            //�Z���𔭐�������
            isEvent = true;
            RenderSettings.fog = true;
            RenderSettings.fogDensity = 0f;
        }
        fogTime += Time.deltaTime;
        //fogLerp�b�Ŗ����Z���Ȃ�
        if (fogTime <= fogLerp)
        {
            RenderSettings.fogDensity = Mathf.Lerp(0f, 0.1f, fogTime / fogLerp);
        }
        
        //fogLerp�b�Ŗ��������Ȃ�
        else if (fogTime > EndTime - 3f)
        {
            RenderSettings.fogDensity = Mathf.Lerp(0f, 0.1f, (EndTime - fogTime) / fogLerp);
        }
        if (fogTime >= EndTime)
        {
            RenderSettings.fog = false;
            isEvent = false;
            fogTime = 0;
            Debug.Log(isEvent);
        }
    }
    void chargeEvent()
    {
        if (eventTime == 0f)
        {
            isEvent = true;
            isCharge = true;
        }
        else if (eventTime >= EndTime)
        {
            isEvent = false;
            isCharge = false;
        }
        eventTime += Time.deltaTime;
    }
    void recoverEvent()
    {
        if (eventTime == 0f)
        {
            isRecover = true;
            isEvent = true;
        }
        else if (eventTime >= EndTime)
        {
            isRecover = false;
            isEvent = false;
        }
        eventTime += Time.deltaTime;
    }
    void rockEvent()
    {
        if (eventTime == 0f)
        {
            isEvent = true;
            isRock = true;
            //���n����꒼��
            List<Vector3> _rockPosition = new List<Vector3>(rockPosition);
            //�����_����3��enhanceRock���o��
            for (int i = 0; i < 3; i++)
            {
                int selectNum = Random.Range(0, _rockPosition.Count);
                 Vector3 nowPosition = _rockPosition[selectNum];
                //�I�����ꂽ�ʒu���폜
                _rockPosition.RemoveAt(selectNum);
                _enhanceRocks[i] = Instantiate(enhanceRock, nowPosition, Quaternion.identity);
            }
        }
        else if (!_enhanceRock)
        {
            isEvent = false;
            isRock = false;
        }
        else if (eventTime >= EndTime)
        {
            isEvent = false;
            isRock = false;
            for (int i = 0; i < 3; i++)
            {
                //�c���Ă�������폜
                if (_enhanceRocks[i])
                {
                    Destroy(_enhanceRock);
                }
            }
        }
        eventTime += Time.deltaTime;
    }
    void massGeneration()
    {
        if (eventTime == 0f)
        {
            isMass = true;
            isEvent = true;
        }
        else if (eventTime >= EndTime)
        {
            isMass = false;
            isEvent = false;
        }
        eventTime += Time.deltaTime;
    }
    void biasEvent()
    {
        if (eventTime == 0f)
        {
            isEvent = true;
            isBias = true;
        }
        else if (eventTime >= EndTime)
        {
            isEvent = false;
            isBias = false;
        }
        eventTime += Time.deltaTime;
    }
    void changeSizeEvent()
    {
        if (eventTime == 0f)
        {
            isSize = true;
            isEvent = true;
        }
        else if (eventTime >= EndTime)
        {
            isSize = false;
            isEvent = false;
        }
        eventTime += Time.deltaTime;
    }
    void nightEvent()
    {
        if (eventTime == 0f)
        {
            isNight = true;
            isEvent = true;
            RenderSettings.skybox = nightMoon;

        }
        else if (eventTime >= EndTime)
        {
            isNight = false;
            isEvent = false;
            RenderSettings.skybox = epicSun;
            //�C�x���g���I������犮�S�Ɍ��̐F�ɖ߂�
            DirectionalLight.GetComponent<Light>().color = new Color(1, 0.9568627f, 0.8392157f);
        }
        if (isNight)
        {
            //���X�Ƀ��C�g�̐F��ύX����
            if (eventTime <= 4)
            {
                DirectionalLight.GetComponent<Light>().color = Color.Lerp(new Color(1, 0.9568627f, 0.8392157f), new Color(0,0,0), eventTime / 5);
            }
            else if(eventTime >= 26)
            {
                DirectionalLight.GetComponent<Light>().color = Color.Lerp(new Color(0, 0, 0), new Color(1, 0.9568627f, 0.8392157f), (eventTime - 25) / 5);
            }
            
        }
        eventTime += Time.deltaTime;
    }
    IEnumerator popSE()
    {
        for(int i=0; i < 2; i++)
        {
            _AudioSource.PlayOneShot(a_eventPop);
            yield return new WaitForSeconds(0.2f);
        }
    }
}