using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject FruitPrefab;

    public bool GameRunning;

    public GameObject currentItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameRunning = false;
        StartCoroutine(SpawnItem());
    }

    private IEnumerator SpawnItem()
    {
        while (true)
        {
            if (GameRunning) 
            {
                GameObject item = Instantiate(FruitPrefab);
                item.transform.position = new Vector3(Random.Range(-2, 2), 4, 0);
                currentItem = item;
                yield return new WaitForSeconds(2);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }
}
