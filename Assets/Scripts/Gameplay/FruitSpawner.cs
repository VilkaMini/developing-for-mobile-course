using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject FruitPrefab;

    [SerializeField]
    GameObject TrashPrefab;

    [SerializeField]
    public List<Sprite> itemSprites;

    public bool GameRunning;

    public GameObject currentItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameRunning = false;
        StartCoroutine(SpawnItem());
    }

    public void RunLogic(bool run)
    {
        GameRunning = run;
    }

    private Sprite DetermineSpawnItem()
    {
        return itemSprites[Random.Range(0, itemSprites.Count)];
    }

    private IEnumerator SpawnItem()
    {
        while (true)
        {
            if (GameRunning) 
            {
                GameObject item;
                if (Random.value > 0.8f)
                {
                    item = Instantiate(TrashPrefab);
                }
                else
                {
                    item = Instantiate(FruitPrefab);
                    item.GetComponent<SpriteRenderer>().sprite = DetermineSpawnItem();
                }

                item.transform.position = new Vector3(Random.Range(-2, 2), 4, 0);

                item.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50, 50), 0)); // ADDED FROM FEEDBACK
                item.transform.Rotate(0f, 0f, Random.Range(-180, 180));                           // ADDED FROM FEEDBACK

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
