using System;
using UnityEngine;

public enum UpgradeType
{
    Basket,
    Speed,
    Hearts,
    Multiplier
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
    FruitSpawner fruitSpawner;

    [SerializeField]
    ScoringManager scoringManager;

    private void Start()
    {
        StopGame();
        ToggleUpgrades(false);
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
    }

    public void EndGame()
    {
        StopGame();
        print("Game Ends");
    }

    // UI functions
    public void ToggleUpgrades(bool open)
    {
        upgradeMenuUI.SetActive(open);
    }

    public void ToggleMainMenu(bool open)
    {
        standardMenuUI.SetActive(open);
    }

    public bool TryToUpgrade(UpgradeType upgradeType, int currentLevel, float moneyRequired)
    {
        if (scoringManager.score >= moneyRequired)
        {
            scoringManager.score -= moneyRequired;
            scoringManager.UpdateScore();
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
