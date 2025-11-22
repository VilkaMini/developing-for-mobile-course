using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField]
    Sprite fullHeart;

    [SerializeField]
    Sprite emptyHeart;

    public bool isFull;
    
    Image heartImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFull = true;
        heartImage = GetComponent<Image>();
    }

    public void SetHeartStatus(bool onOff)
    {
        if (onOff)
        {
            heartImage.sprite = fullHeart;
            isFull = true;
        }
        else
        {
            heartImage.sprite = emptyHeart;
            isFull = false;
        }
    }
}
