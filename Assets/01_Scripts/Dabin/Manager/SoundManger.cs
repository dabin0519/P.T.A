using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("��������(SoundManger)");
        }
    }

    public void Play(AudioClip playSound)
    {
        
    }
}
