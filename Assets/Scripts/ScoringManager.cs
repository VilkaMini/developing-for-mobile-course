using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoringManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int score = 0;

    public void ItemCaught()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}
