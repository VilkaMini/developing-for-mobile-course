using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    [SerializeField]
    List<HeartUI> heartList;

    [SerializeField]
    GameManager gameManager;

    public void LoseLife()
    {
        int counter = 0;

        for (int i = 0; i < heartList.Count; i++)
        {
            if (heartList[i].isFull)
            {
                heartList[i].SetHeartStatus(false);
                break;
            }
            counter++;
        }

        if (counter == heartList.Count - 1)
        {
            gameManager.EndGame();
        }
    }
}
