using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject FruitPrefab;

    public bool GameRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameRunning = true;
        StartCoroutine(SpawnItem());
    }

    private IEnumerator SpawnItem()
    {
        while (GameRunning)
        {
            GameObject item = Instantiate(FruitPrefab);
            item.transform.position = new Vector3(Random.Range(-2, 2), 4, 0);
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(1);
    }
}
