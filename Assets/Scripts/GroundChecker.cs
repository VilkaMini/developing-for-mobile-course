using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingItem")){
            Destroy(collision.gameObject);
        }
    }
}
