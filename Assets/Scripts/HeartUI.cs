using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField]
    Sprite fullHeart;

    [SerializeField]
    Sprite emptyHeart;
    
    Image heartImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        heartImage = GetComponent<Image>();
    }

    void SetHeartStatus(bool onOff)
    {
        if (onOff)
        {
            heartImage.sprite = fullHeart;
        }
        else
        {
            heartImage.sprite = emptyHeart;
        }
    }
}
