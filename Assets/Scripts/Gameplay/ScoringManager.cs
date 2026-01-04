using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoringManager : MonoBehaviour
{
    private HeartManager heartManager;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public float score = 0;
    private float scoreMultiplier = 1f;

    private void Start()
    {
        heartManager = GetComponent<HeartManager>();
    }

    public void ItemCaught(float currentBasketScale)
    {
        score += 1 * (2 - currentBasketScale) * scoreMultiplier;
        UpdateScore();
    }

    public void SetScoreMultiplier(float level)
    {
        scoreMultiplier = 1 + level / 10f;
    }

    public void TrashCaught()
    {
        heartManager.LoseLife();
    }

    public void UpdateScore()
    {
        scoreText.text = Math.Round(score, 2).ToString();
    }

    public void RewardForAdd()
    {
        score = score * 2;
        UpdateScore();
    }
}
