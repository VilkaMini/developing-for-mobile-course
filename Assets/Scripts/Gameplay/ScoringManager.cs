using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoringManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private float score = 0;

    public void ItemCaught(float currentBasketScale)
    {
        score += 1 * (2 - currentBasketScale);
        scoreText.text = score.ToString();
    }
}
