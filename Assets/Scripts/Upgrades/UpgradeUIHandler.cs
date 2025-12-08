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

    int upgradeLevel = 1;

    public void UpgradeClicked()
    {
        if (upgradeLevel == 5)
        {
            return;
        }
        if (gameManager.TryToUpgrade(upgradeType, upgradeLevel, 1))
        {
            upgradeLevel++;
            RegisterUpgrade();
        }
    }

    void RegisterUpgrade()
    {
        if (upgradeLevel == 5)
        {
            upgradeButtonText.text = "Max";
        }
        else
        {
            upgradeButtonText.text = $"Level {upgradeLevel}";
        }
    }
}
