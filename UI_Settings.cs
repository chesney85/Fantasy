using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    #region Class Variables

    [Header("Main Camera")] [ReadOnly]
    public Camera cam;

    [Header("Settings Panels List")] 
    public List<GameObject> settingsPanels;

    [Header("Audio Settings")] public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioMixer audioMixer;
    public Toggle muteToggle;
    private float previousMasterVolume;

    [Header("Video Settings")] public Dropdown resolutionDropdown;
    public Dropdown refreshRateCapDropdown;
    public Dropdown vsyncDropDown;
    public Dropdown fieldOFViewDropDown;

    [Header("Quality Settings")] public Dropdown qualityPreset;
    public Dropdown textureResolutionDropdown;
    public Dropdown antiAliasingDropdown;
    public Dropdown shadowQualityDropdown;
    public Dropdown shadowResolutionDropdown;
    public Slider shadowDistanceSlider;
    public Slider drawDistanceSlider;

    [Header("Detects if quality Settings Changed")]
    private bool isFromQualityPreset;

    [Header("Save Variables")] [Header("Audio")] [ShowInInspector] [ReadOnly]
    private readonly string musicVolumeSave = "MusicVolume";

    [ShowInInspector] [ReadOnly]
    private readonly string masterVolumeSave = "MasterVolume";

    [ShowInInspector] [ReadOnly]
    private readonly string sfxVolumeSave = "SfxVolume";

    [ShowInInspector] [ReadOnly]
    private readonly string muteVolumeSave = "MuteVolume";

    [ShowInInspector] [ReadOnly]
    private readonly string previousMasterVolumeSave = "PreviousMasterVolume";

    [Header("Video")] [ShowInInspector] [ReadOnly]
    private readonly string fieldOfViewSave = "FieldOfView";

    [ShowInInspector] [ReadOnly]
    private readonly string ResolutionSave = "Resolution";

    [ShowInInspector] [ReadOnly]
    private readonly string vSyncSave = "Vsync";

    [ShowInInspector] [ReadOnly]
    private readonly string frameRateCapSave = "FrameRateCap";

    [Header("Quality")] [ShowInInspector] [ReadOnly]
    private readonly string QualityPresetSave = "QualityPreset";

    [ShowInInspector] [ReadOnly]
    private readonly string textureResolutionSave = "TextureResolution";

    [ShowInInspector] [ReadOnly]
    private readonly string shadowQualitySave = "ShadowQuality";

    [ShowInInspector] [ReadOnly]
    private readonly string shadowResolutionSave = "ShadowResolution";

    [ShowInInspector] [ReadOnly]
    private readonly string antiAliasingSave = "AntiAliasing";

    [ShowInInspector] [ReadOnly]
    private readonly string shadowDistanceSave = "ShadowDistance";

    [ShowInInspector] [ReadOnly]
    private readonly string drawDistanceSave = "DrawDistance";

    #endregion
    public void Startup()
    {
        cam = Camera.main;
        if (!ES3.KeyExists(masterVolumeSave))
        {
            SaveAllSettings();
        }

        GetAllSettings();
    }
    void SaveAllSettings()
    {
        SaveAudioSettings();
        SaveVideoSettings();
        SaveQualitySettings();
    }
    void GetAllSettings()
    {
        GetAllAudioSettings();
        GetAllVideoSettings();
        LoadQualitySettings();
    }

    #region Audio

    public void SetMasterVolume(float _volume)
    {
        audioMixer.SetFloat(masterVolumeSave, Mathf.Log10(_volume) * 20);
    }
    void SetMasterVolumeUI()
    {
        audioMixer.GetFloat(masterVolumeSave, out float vol);
        masterSlider.SetValueWithoutNotify(vol);
    }

    public void SetMusicVolume(float _volume)
    {
        audioMixer.SetFloat(musicVolumeSave, Mathf.Log10(_volume) * 20);
        
    }
    void SetMusicVolumeUI()
    {
        audioMixer.GetFloat(musicVolumeSave, out float vol);
        musicSlider.SetValueWithoutNotify(vol);
    }

    public void SetSfxVolume(float _volume)
    {
        audioMixer.SetFloat(sfxVolumeSave, Mathf.Log10(_volume) * 20);
    }
    void SetSfxVolumeUI()
    {
        audioMixer.GetFloat(sfxVolumeSave, out float vol);
        sfxSlider.SetValueWithoutNotify(vol);
    }

    public void MuteAudio(bool value)
    {
        if (value)
        {
            SetMasterVolume(-80f);
        }
        else
        {
            SetMasterVolume(masterSlider.value);
        }
    }

    void SetMuteUI()
    {
        bool m = ES3.Load<bool>(muteVolumeSave);
        muteToggle.isOn = m;
    }

    #endregion

    #region Quality Settings

    public void SetQualityLevel(int value)
    {
        if (value < 6)
        {
            isFromQualityPreset = true;
            QualitySettings.SetQualityLevel(value,true);
            SetAntiAliasingUI();
            SetTextureResolutionUI();
            SetShadowDistanceUI();
            SetShadowQualityUI();
            SetShadowResolutionUI();
            SetDrawDistanceUI();
        }
        else
        {
            isFromQualityPreset = false;
            SetQualityCustom();
        }
    }

    public void SetAntiAliasing(int value)
    {
        QualitySettings.antiAliasing = value;
    }

    void SetAntiAliasingUI()
    {
        if (!isFromQualityPreset)
            SetQualityCustom();
        antiAliasingDropdown.value = QualitySettings.antiAliasing;
    }

    public void SetTextureResolution(int value)
    {
        QualitySettings.masterTextureLimit = value;
    }

    void SetTextureResolutionUI()
    {
        if (!isFromQualityPreset)
         SetQualityCustom();
        textureResolutionDropdown.value = QualitySettings.masterTextureLimit;

    }

    public void SetShadowQuality(int value)
    {
        switch (value)
        {
            case 0:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case 2:
                QualitySettings.shadowResolution = ShadowResolution.High;
                break;
            case 3:
                QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                break;
            default:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
        }
    }

    void SetShadowQualityUI()
    {
        if (!isFromQualityPreset)
            SetQualityCustom();
        switch (QualitySettings.shadowResolution)
        {
            case ShadowResolution.Low:
                shadowQualityDropdown.SetValueWithoutNotify(0);
                break;
            case ShadowResolution.Medium:
                shadowQualityDropdown.SetValueWithoutNotify(1);
                break;
            case ShadowResolution.High:
                shadowQualityDropdown.SetValueWithoutNotify(2);
                break;
            case ShadowResolution.VeryHigh:
                shadowQualityDropdown.SetValueWithoutNotify(3);
                break;
            default:
                shadowQualityDropdown.SetValueWithoutNotify(0);
                break;
        }
    }

    public void SetShadowResolution(int value)
    {
        switch (value)
        {
            case 0:
                QualitySettings.shadows = ShadowQuality.Disable;
                break;
            case 1:
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
            case 2:
                QualitySettings.shadows = ShadowQuality.All;
                break;
            default:
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
        }
    }

    void SetShadowResolutionUI()
    {
        if(!isFromQualityPreset)
            SetQualityCustom();
        switch (QualitySettings.shadowResolution)
        {
            case ShadowResolution.Low:
                shadowResolutionDropdown.SetValueWithoutNotify(0);
                break;
            case ShadowResolution.Medium:
                shadowResolutionDropdown.SetValueWithoutNotify(1);
                break;
            case ShadowResolution.High:
                shadowResolutionDropdown.SetValueWithoutNotify(2);
                break;
            case ShadowResolution.VeryHigh:
                shadowResolutionDropdown.SetValueWithoutNotify(3);
                break;
            default:
                shadowResolutionDropdown.SetValueWithoutNotify(1);
                break;
        }
    }

    public void SetShadowDistance(float value)
    {
        QualitySettings.shadowDistance = value;
    }

    void SetShadowDistanceUI()
    {
        if(!isFromQualityPreset)
            SetQualityCustom();
        shadowDistanceSlider.value = QualitySettings.shadowDistance;
    }

    public void SetDrawDistance(float value)
    {
        if (cam)
            cam.farClipPlane = value;
    }

    void SetDrawDistanceUI()
    {
        if(!isFromQualityPreset)
            SetQualityCustom();
        drawDistanceSlider.value = cam.farClipPlane;
    }
    
    public void SetQualityCustom()
    {
        qualityPreset.SetValueWithoutNotify(6);
    }
    
    public void SaveQualitySettings()
    {
        //--------------Save Quality Settings----------
        ES3.Save(QualityPresetSave, qualityPreset.value);
        ES3.Save(textureResolutionSave, textureResolutionDropdown.value);
        ES3.Save(shadowQualitySave, shadowQualityDropdown.value);
        ES3.Save(shadowDistanceSave, shadowDistanceSlider.value);
        ES3.Save(shadowResolutionSave,shadowResolutionDropdown.value);
        ES3.Save(antiAliasingSave, antiAliasingDropdown.value);
        ES3.Save(drawDistanceSave, drawDistanceSlider.value);
       
    }

    public void LoadQualitySettings()
    {
        //--------------Load Quality Settings----------
        int qualityPre = ES3.Load<int>(QualityPresetSave);
        int textRes = ES3.Load<int>(textureResolutionSave);
        int shadowQ = ES3.Load<int>(shadowQualitySave);
        int shadowRes = ES3.Load<int>(shadowResolutionSave);
        float shadowDist = ES3.Load<float>(shadowDistanceSave);
        float drawDist = ES3.Load<float>(drawDistanceSave);
        int alias = ES3.Load<int>(antiAliasingSave);

        if (qualityPre < 6 && qualityPre > -1)
        {
            SetQualityLevel(qualityPre);
        }
        else if (qualityPre == 6)
        {
            SetAntiAliasing(alias);
            SetAntiAliasingUI();
            SetDrawDistance(drawDist);
            SetDrawDistanceUI();
            SetShadowDistance(shadowDist);
            SetShadowDistanceUI();
            SetShadowQuality(shadowQ);
            SetShadowQualityUI();
            SetShadowResolution(shadowRes);
            SetShadowResolutionUI();
            SetTextureResolution(textRes);
            SetTextureResolutionUI();
        }
        else
        {
            Debug.Log("Quality LevelDoesn't exist! - revert to Low");
            SetQualityLevel(0);
        }
    }

    #endregion

    #region Audio Settings

    void GetAllAudioSettings()
    {
        //--------------Load Audio Settings----------
        float master = ES3.Load<float>(masterVolumeSave);
        SetMasterVolume(master);
        SetMasterVolumeUI();
        float music = ES3.Load<float>(musicVolumeSave);
        SetMusicVolume(music);
        SetMusicVolumeUI();
        float sfx = ES3.Load<float>(sfxVolumeSave);
        SetSfxVolume(sfx);
        SetSfxVolumeUI();
        bool mute = ES3.Load<bool>(muteVolumeSave);
        MuteAudio(mute);
        SetMuteUI();
        previousMasterVolume = master;
    }

    public void SaveAudioSettings()
    {
        //--------------Save Audio Settings----------
        // audioMixer.GetFloat(masterVolumeSave, out float masterVolumeMixer);
        ES3.Save(masterVolumeSave, masterSlider.value);
        // audioMixer.GetFloat(musicVolumeSave, out float musicVolumeMixer);
        ES3.Save(musicVolumeSave, musicSlider.value);
        // audioMixer.GetFloat(sfxVolumeSave, out float sfxVolumeMixer);
        ES3.Save(sfxVolumeSave, sfxSlider.value);
        ES3.Save(previousMasterVolumeSave, previousMasterVolume);
        ES3.Save(muteVolumeSave, muteToggle.isOn);
    }

    #endregion

    #region Video Settings

    public void SetResolution(int value)
    {
        string resolutionText = resolutionDropdown.options[value].text;
        int width = int.Parse(resolutionText.Split('x')[0]);
        int height = int.Parse(resolutionText.Split('x')[1]);
        FullScreenMode screenMode = Screen.fullScreenMode;
        Screen.SetResolution(width, height, screenMode);
    }
    void SetResolutionUI()
    {
        int res = ES3.Load<int>(ResolutionSave);
        resolutionDropdown.SetValueWithoutNotify(res);
    }

    public void SetFieldOfView(int value)
    {
        switch (value)
        {
            case 0:
                cam.fieldOfView = 60f;
                break;
            case 1:
                cam.fieldOfView = 90f;
                break;
            case 2:
                cam.fieldOfView = 120f;
                break;
            default:
                cam.fieldOfView = 60f;
                Debug.Log("Error: FOV Type Mismatch - revert to 60 degrees");
                break;
        }
    }
    void SetFieldOfViewUI()
    {
        switch (cam.fieldOfView)
        {
            case 60f:
                fieldOFViewDropDown.SetValueWithoutNotify(0);
                break;
            case 90f:
                fieldOFViewDropDown.SetValueWithoutNotify(1);
                break;
            case 120f:
                fieldOFViewDropDown.SetValueWithoutNotify(2);
                break;
            default:
                fieldOFViewDropDown.SetValueWithoutNotify(0);
                Debug.Log("Error: FOV Type Mismatch - revert to 60 degrees");
                break;
        }
    }

    public void SetFramerateLimit(int value)
    {
        switch (value)
        {
            case 0:
                Application.targetFrameRate = -1;
                break;
            case 1:
                Application.targetFrameRate = 30;
                break;
            case 2:
                Application.targetFrameRate = 60;
                break;
            case 3:
                Application.targetFrameRate = 72;
                break;
            case 4:
                Application.targetFrameRate = 90;
                break;
            case 5:
                Application.targetFrameRate = 120;
                break;
            case 6:
                Application.targetFrameRate = 144;
                break;
            case 7:
                Application.targetFrameRate = 160;
                break;
            case 8:
                Application.targetFrameRate = 240;
                break;
            default:
                Application.targetFrameRate = -1;
                Debug.Log("Error: framerate type mismatch - revert to off");
                break;
        }
    }
    void SetFramerateLimitUI()
    {
        switch (Application.targetFrameRate)
        {
            case -1:
                refreshRateCapDropdown.SetValueWithoutNotify(0);
                break;
            case 30:
                refreshRateCapDropdown.SetValueWithoutNotify(1);
                break;
            case 60:
                refreshRateCapDropdown.SetValueWithoutNotify(2);
                break;
            case 72:
                refreshRateCapDropdown.SetValueWithoutNotify(3);
                break;
            case 90:
                refreshRateCapDropdown.SetValueWithoutNotify(4);
                break;
            case 120:
                refreshRateCapDropdown.SetValueWithoutNotify(5);
                break;
            case 144:
                refreshRateCapDropdown.SetValueWithoutNotify(6);
                break;
            case 160:
                refreshRateCapDropdown.SetValueWithoutNotify(7);
                break;
            case 240:
                refreshRateCapDropdown.SetValueWithoutNotify(8);
                break;
            default:
                refreshRateCapDropdown.SetValueWithoutNotify(0);
                Debug.Log("Error: framerate type mismatch - revert to off");
                break;
        }
    }

    public void SetVsync(int value)
    {
        switch (value)
        {
            case 0:
                QualitySettings.vSyncCount = 0;
                break;
            case 1:
                QualitySettings.vSyncCount = 1;
                break;
            case 2:
                QualitySettings.vSyncCount = 2;
                break;
            default:
                Debug.Log("Reverting to no Vsync");
                QualitySettings.vSyncCount = 0;
                break;
        }
    }
    void SetVsyncUI()
    {
        switch (QualitySettings.vSyncCount)
        {
            case 0:
                vsyncDropDown.SetValueWithoutNotify(0);
                break;
            case 1:
                vsyncDropDown.SetValueWithoutNotify(1);
                break;
            case 2:
                vsyncDropDown.SetValueWithoutNotify(2);
                break;
            default:
                Debug.Log("Reverting to no Vsync UI");
                vsyncDropDown.SetValueWithoutNotify(0);
                break;
        }
    }

    void GetAllVideoSettings()
    {
        int res = ES3.Load<int>(ResolutionSave);
        int fov = ES3.Load<int>(fieldOfViewSave);
        int fps = ES3.Load<int>(frameRateCapSave);
        int v = ES3.Load<int>(vSyncSave);
        
        SetResolution(res);
        SetResolutionUI();
        SetFieldOfView(fov);
        SetFieldOfViewUI();
        SetFramerateLimit(fps);
        SetFramerateLimitUI();
        SetVsync(v);
        SetVsyncUI();
    }

    public void SaveVideoSettings()
    {
        //--------------Save video Settings----------
        ES3.Save(fieldOfViewSave, fieldOFViewDropDown.value);
        ES3.Save(ResolutionSave, resolutionDropdown.value);
        ES3.Save(vSyncSave, vsyncDropDown.value);
        ES3.Save(frameRateCapSave, refreshRateCapDropdown.value);
    }

    #endregion
    
    #region Buttons

    public void SettingsButtonPressed(int panelIndex)
    {
        for (int i = 0; i < settingsPanels.Count; i++)
        {
            settingsPanels[i].SetActive(false);
        }

        settingsPanels[panelIndex].SetActive(true);
        Cursor.visible = true;
    }

    #endregion

    #region Misc

    public void QuitGame()
    {
        Application.Quit(0);
    }

    #endregion
}