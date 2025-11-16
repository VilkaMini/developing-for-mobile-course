using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasketController : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;

    // Movement
    private InputAction m_moveAction;
    private Vector2 m_moveAmt;
    private float m_accelerationX;

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
        // Set initial values
        Vector3 newPos = transform.position;
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_accelerationX = Input.acceleration.x;


        // Do the magic (movement)
        if (Mathf.Abs(m_accelerationX) > 0.1) // 0.1 here is a cutoff so that the basket would not drift
        {
            newPos = transform.position + new Vector3(m_accelerationX * speed * Time.deltaTime, 0, 0);
        }

        if (m_moveAction.IsPressed())
        {
            newPos = transform.position +  new Vector3(m_moveAmt[0] * speed * Time.deltaTime, 0, 0);
        }

        newPos.x = Mathf.Clamp(newPos.x, -2.3f, 2.3f);

        transform.position = newPos;
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
