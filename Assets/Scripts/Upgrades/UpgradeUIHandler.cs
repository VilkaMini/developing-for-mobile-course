using TMPro;
using UnityEngine;

public class UpgradeUIHandler : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    UpgradeType upgradeType;

    [SerializeField]
    TextMeshProUGUI upgradeText;

    [SerializeField]
    TextMeshProUGUI upgradeButtonText;

    [SerializeField]
    TextMeshProUGUI upgradeCostText;

    int upgradeLevel = 1;
    int upgradeCost = 1;

    public void UpgradeClicked()
    {
        if (upgradeLevel == 5)
        {
            return;
        }
        if (gameManager.TryToUpgrade(upgradeType, upgradeLevel, upgradeCost))
        {
            upgradeLevel++;
            upgradeCost = upgradeLevel * 2;
            RegisterUpgrade();
        }
    }

    void RegisterUpgrade()
    {
        if (upgradeLevel == 5)
        {
            upgradeButtonText.text = "Max";
            upgradeCostText.text = "";
        }
        else
        {
            upgradeButtonText.text = $"Level {upgradeLevel}";
            upgradeCostText.text = $"{upgradeCost}";
        }
    }
}
