using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoringManager : MonoBehaviour
{
    private HeartManager heartManager;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private float score = 0;

    private void Start()
    {
        heartManager = GetComponent<HeartManager>();
    }

    public void ItemCaught(float currentBasketScale)
    {
        score += 1 * (2 - currentBasketScale);
        scoreText.text = score.ToString();
    }

    public void TrashCaught()
    {
        heartManager.LoseLife();
    }
}
