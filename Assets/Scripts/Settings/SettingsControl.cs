using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsControl : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    BasketController basketManager;

    [SerializeField]
    SettingType settingType;

    [SerializeField]
    TextMeshProUGUI upgradeButtonText;

    private bool settingMusic = true;
    private bool settingSounds = true;
    private int settingSensitivity = 1;

    private List<string> sensitivityNames = new List<string>{"Low", "Normal", "High" };

    public void ButtonPressed()
    {
        switch (settingType)
        {
            case SettingType.Music:
                settingMusic = !settingMusic;
                upgradeButtonText.text = (settingMusic) ? "On" : "Off";
                if (settingMusic)
                {
                    gameManager.audioPlayer.Play();
                }
                else
                {
                    gameManager.audioPlayer.Pause();
                }
                break;
            case SettingType.Sound:
                settingSounds = !settingSounds;
                upgradeButtonText.text = (settingSounds) ? "On" : "Off";
                gameManager.sfcPlayer.mute = settingMusic;
                break;
            case SettingType.Sensitivity:
                if (settingSensitivity == 2)
                {
                    settingSensitivity = 0;
                }
                else
                {
                    settingSensitivity += 1;
                }
                upgradeButtonText.text = sensitivityNames[settingSensitivity];
                basketManager.SetBasketSensitiivty(settingSensitivity);
                break;
        }
    }
}
