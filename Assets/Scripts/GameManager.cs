using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BasketController basketController;

    [SerializeField]
    FruitSpawner fruitSpawner;

    public void EndGame()
    {
        basketController.GameRunning = false;
        fruitSpawner.GameRunning = false;
        print("Game Ends");
    }

    public void ResumeGame()
    {
        basketController.GameRunning = true;
        fruitSpawner.GameRunning = true;
    }
}
