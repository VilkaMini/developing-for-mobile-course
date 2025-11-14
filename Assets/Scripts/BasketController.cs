using UnityEngine;
using UnityEngine.InputSystem;

public class BasketController : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;

    // Movement
    private InputAction m_moveAction;
    private Vector2 m_moveAmt;

    // Managers
    [SerializeField]
    private ScoringManager scoringManager;

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();

        if (m_moveAction.IsPressed())
        {
            transform.position += new Vector3(m_moveAmt[0] * speed * Time.deltaTime, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingItem"))
        {
            scoringManager.ItemCaught();
            Destroy(collision.gameObject);
        }
    }

}
