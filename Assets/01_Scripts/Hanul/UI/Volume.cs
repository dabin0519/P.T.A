using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] Slider _volume;
    [SerializeField] Slider _effectVolume;
    public float _volumeValue;
    public float _effectvolumeValue;

    private void Awake()
    {
        _volumeValue = 100;
        _effectvolumeValue = 100;
        _volume.onValueChanged.AddListener(ChangeVolum);
        _effectVolume.onValueChanged.AddListener(ChangeEffectVolum);
    }
    public void ChangeVolum(float value)
    {
        _volumeValue = value;
    }
    public void ChangeEffectVolum(float value)
    {
        _effectvolumeValue = value;
    }
}