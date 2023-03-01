using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void Mute()
    {
        _audioSource.mute = !_audioSource.mute;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
