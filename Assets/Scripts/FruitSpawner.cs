using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject FruitPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnItem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnItem()
    {
        while (true)
        {
            GameObject item = Instantiate(FruitPrefab);
            item.transform.position = new Vector3(Random.Range(-2, 2), 4, 0);
            yield return new WaitForSeconds(2);
        }
    }
}
