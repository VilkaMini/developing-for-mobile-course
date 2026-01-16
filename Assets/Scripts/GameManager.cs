using System;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Basket,
    Speed,
    Hearts,
    Multiplier
}

public enum SettingType
{
    Music,
    Sound,
    Sensitivity
}

public enum SoundType
{
    Good,
    Bad
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BasketController basketController;

    [SerializeField]
    GameObject standardMenuUI;

    [SerializeField]
    GameObject watchAddMenuUI;

    [SerializeField]
    GameObject upgradeMenuUI;

    [SerializeField]
    GameObject settingsMenuUI;

    [SerializeField]
    GameObject tutorialMenuUI;

    [SerializeField]
    FruitSpawner fruitSpawner;

    [SerializeField]
    ScoringManager scoringManager;

    [SerializeField]
    HeartManager heartManager;

    [SerializeField] 
    UnityAdsManager adManager;

    [SerializeField]
    public AudioSource audioPlayer;

    [SerializeField]
    public AudioSource sfcPlayer;

    [SerializeField]
    AudioClip goodSound;
    [SerializeField]
    AudioClip badSound;

    [SerializeField]
    Dictionary<UpgradeType, int> upgradeCounts = new Dictionary<UpgradeType, int>();

    private bool runActive = false;

    private void Start()
    {
        StopGame();
        ToggleUpgrades(false);
        ToggleAddScreen(false);
        ToggleSettings(false);
        upgradeCounts.Add(UpgradeType.Basket, 0);
        upgradeCounts.Add(UpgradeType.Speed, 0);
        upgradeCounts.Add(UpgradeType.Hearts, 0);
        upgradeCounts.Add(UpgradeType.Multiplier, 0);
    }

    public void PlaySFX(SoundType sound)
    {
        switch (sound)
        {
            case SoundType.Good:

                sfcPlayer.PlayOneShot(goodSound, 1);
                break;
            case SoundType.Bad:
                sfcPlayer.PlayOneShot(badSound, 1);
                break;
        }
    }

    public void StopGame()
    {
        basketController.RunLogic(false);
        fruitSpawner.RunLogic(false);
        if (fruitSpawner.currentItem)
        {
            Destroy(fruitSpawner.currentItem);
        }
        ToggleMainMenu(true);
    }

    public void ResumeGame()
    {
        basketController.RunLogic(true);
        fruitSpawner.RunLogic(true);
        ToggleMainMenu(false);

        if (!runActive)
        {
            runActive = true;
        }
    }

    public void EndGame()
    {
        adManager.LoadAd();
        basketController.RunLogic(false);
        fruitSpawner.RunLogic(false);
        fruitSpawner.UpdateSpawningInterval(true);
        if (fruitSpawner.currentItem)
        {
            Destroy(fruitSpawner.currentItem);
        }
        runActive = false;
        heartManager.ResetLifes();
        ToggleAddScreen(true);
    }

    // UI functions
    public void ToggleUpgrades(bool open)
    {
        if (!runActive) 
        {
            upgradeMenuUI.SetActive(open);
        }
    }

    public void ToggleSettings(bool open)
    {
        settingsMenuUI.SetActive(open);
    }

    public void ToggleMainMenu(bool open)
    {
        standardMenuUI.SetActive(open);
    }

    public void ToggleTutorial(bool open)
    {
        tutorialMenuUI.SetActive(open);
    }

    public void ToggleAddScreen(bool open)
    {
        watchAddMenuUI.SetActive(open);
        standardMenuUI.SetActive(!open);
    }

    public void GiveRewardForAdd()
    {
        scoringManager.RewardForAdd();
    }

    public bool TryToUpgrade(UpgradeType upgradeType, int currentLevel, float moneyRequired)
    {
        if (scoringManager.score >= moneyRequired)
        {
            scoringManager.score -= moneyRequired;
            scoringManager.UpdateScore();

            upgradeCounts[upgradeType] = currentLevel + 1;
            RunUpgradeUpdate(upgradeType);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void RunUpgradeUpdate(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.Basket:
                basketController.SetBasketLevel(upgradeCounts[upgradeType]);
                break;
            case UpgradeType.Speed:
                basketController.SetBasketSpeed(upgradeCounts[upgradeType]);
                break;
            case UpgradeType.Hearts:
                print("Increase hearts not implemented");
                break;
            case UpgradeType.Multiplier:
                scoringManager.SetScoreMultiplier(upgradeCounts[upgradeType]);
                break;
        }

    }
}
