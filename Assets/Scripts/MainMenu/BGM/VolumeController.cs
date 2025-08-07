using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        // 슬라이더가 null이 아닐 경우, 현재 볼륨값으로 초기화
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
