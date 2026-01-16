using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    HeartManager m_HeartManager;

    [SerializeField]
    GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingItem")){
            Destroy(collision.gameObject);
            m_HeartManager.LoseLife();
            gameManager.PlaySFX(SoundType.Bad);
        }
    }
}
