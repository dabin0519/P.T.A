using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
public enum CameraShakeStrength { Weak, Medium, Strong };
public class CameraEffectManger : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    public PostProcessVolume redEffectPostProcessVolume;
    public PostProcessVolume blurEffectPostProcessVolume;
    void Start()
    {
        //virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        ShakeCamera();
        RedVisionEffectCamera();
        StartCoroutine(BlurVisionEffectCameraCorutine());
    }

    public void RedVisionEffectCamera()
    {
        StartCoroutine(RedVisionEffectCameraCorutine());
    }

    public void ShakeCamera(CameraShakeStrength shakePower = CameraShakeStrength.Weak, float delay = 0.6f)
    {
        StartCoroutine(ShakeCameraCorutine(shakePower, delay));
    }

    IEnumerator ShakeCameraCorutine(CameraShakeStrength shakePower = CameraShakeStrength.Weak, float delay = 0.6f)
    {
        float power = shakePower == CameraShakeStrength.Weak ? 18 : shakePower == CameraShakeStrength.Medium ? 25 : 40;
        float currentGain = virtualCameraNoise.m_AmplitudeGain;

        virtualCameraNoise.m_FrequencyGain = 1;
        DOTween.To(
            () => virtualCameraNoise.m_AmplitudeGain,
            x => virtualCameraNoise.m_AmplitudeGain = x,
            power, 1
         );
        yield return new WaitForSeconds(delay);
        DOTween.To(
            () => virtualCameraNoise.m_AmplitudeGain,
            x => virtualCameraNoise.m_AmplitudeGain = x,
            currentGain, 1
         );
        DOTween.To(
            () => virtualCameraNoise.m_FrequencyGain,
            x => virtualCameraNoise.m_FrequencyGain = x,
            0, 1
         );
    }

    IEnumerator RedVisionEffectCameraCorutine()
    {
        float originalOrthographicSizeValue = virtualCamera.m_Lens.OrthographicSize;
        float changedOrthographicSizeValue = originalOrthographicSizeValue - 2;
        DOTween.To(
            () => redEffectPostProcessVolume.weight,
            x => redEffectPostProcessVolume.weight = x,
            1.0f, 1
         );
        DOTween.To(
            () => virtualCamera.m_Lens.OrthographicSize,
            x => virtualCamera.m_Lens.OrthographicSize = x,
            changedOrthographicSizeValue, 1
         );

        yield return new WaitForSeconds(2f);

        DOTween.To(
            () => redEffectPostProcessVolume.weight,
            x => redEffectPostProcessVolume.weight = x,
            0, 1
         );
        DOTween.To(
            () => virtualCamera.m_Lens.OrthographicSize,
            x => virtualCamera.m_Lens.OrthographicSize = x,
            originalOrthographicSizeValue, 1
         );
    }

    IEnumerator BlurVisionEffectCameraCorutine()
    {
        // 5초동안 블러
        DOTween.To(
            () => blurEffectPostProcessVolume.weight,
            x => blurEffectPostProcessVolume.weight = x,
            1.0f, 5
         );

        yield return new WaitForSeconds(2f);
        DOTween.To(
            () => blurEffectPostProcessVolume.weight,
            x => blurEffectPostProcessVolume.weight = x,
            0f, 2
         );
    }
}
