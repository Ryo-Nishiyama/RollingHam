using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Master : MonoBehaviour
{
    [SerializeField] AudioClip[] BGMlist;
    AudioSource _AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _AudioSource = GetComponent<AudioSource>();
        //BGM���ݒ肳��Ă��Ȃ��Ƃ������_���ōĐ�
        if (_AudioSource.clip == null)
        {
            _AudioSource.clip = BGMlist[Random.Range(0, BGMlist.Length)];
        }
        _AudioSource.Play();
    }
}
