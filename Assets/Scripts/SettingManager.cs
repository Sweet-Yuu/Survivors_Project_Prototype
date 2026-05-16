using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    [Header("--- Audio Mixer ---")]
    public AudioMixer mainMixer;

    [Header("--- UI Elements ---")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;

    void Start()
    {
        // gán sự kiện khi người chơi kéo thanh Slider

        masterSlider.onValueChanged.AddListener(SetMasterVolumn);
        musicSlider.onValueChanged.AddListener(SetMusicVolumn);
        sfxSlider.onValueChanged.AddListener(SetSFXVolumn);

        // Gán sự kiện cho nút tắt tiếng
        muteToggle.onValueChanged.AddListener(SetMute);

        masterSlider.value = 1f;
        musicSlider.value = 1f;
        sfxSlider.value = 1f;
    }

    public void SetMasterVolumn (float value)
    {
        float volumn = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20; 
        mainMixer.SetFloat("MasterVol", volumn);
    }

    public void SetMusicVolumn (float value)
    {
        float volumn =Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f) * 20);
        mainMixer.SetFloat("MusicVol", volumn);
    }

    public void SetSFXVolumn (float value)
    {
        float volumn = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f) * 20);
        mainMixer.SetFloat("SFXVol", volumn);
    }

    public void SetMute(bool isMuted)
    {
        if (isMuted)
        {
            mainMixer.SetFloat("MasterVol", -80f); // -80dB là im lặng hoàn toàn
            masterSlider.interactable = false;    // Khóa thanh gạt khi đang mute
        }
        else
        {
            masterSlider.interactable = true;
            SetMasterVolumn(masterSlider.value);  // Trả lại âm lượng cũ
        }
    }

    // Hàm để đóng/mở bảng cài đặt
    public void ToggleSettingPanel(bool isActive)
    {
        gameObject.SetActive(isActive);

        // Nếu đang trong trận (ở màn 1 đến màn 11) thì tự động dừng game khi mở bảng
        // Bạn có thể check điều kiện nếu đang ở Scene gameplay
        /*
        if (isActive) {
            Time.timeScale = 0f; // Tạm dừng game
        } else {
            Time.timeScale = 1f; // Chạy tiếp
        }
        */
    }
}
