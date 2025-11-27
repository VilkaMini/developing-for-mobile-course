using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    BasketController basketController;

    [SerializeField]
    GameObject standardMenuUI;

    [SerializeField]
    GameObject watchAddMenuUI;

    [SerializeField]
    FruitSpawner fruitSpawner;

    private void Start()
    {
        StopGame();

    }

    public void EndGame()
    {
        StopGame();
        print("Game Ends");
    }

    public void StopGame()
    {
        basketController.GameRunning = false;
        fruitSpawner.GameRunning = false;
        standardMenuUI.SetActive(true);
        if (fruitSpawner.currentItem)
        {
            Destroy(fruitSpawner.currentItem);
        }
    }

    public void ResumeGame()
    {
        basketController.GameRunning = true;
        fruitSpawner.GameRunning = true;
        standardMenuUI.SetActive(false);
    }
}
