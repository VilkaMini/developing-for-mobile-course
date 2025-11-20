using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEditor.PlayerSettings;

public class BasketController : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;

    // Movement
    private InputAction m_moveAction;
    private Vector2 m_moveAmt;
    private float m_accelerationX;

    private InputAction primaryTouch;
    private InputAction primaryTouchHold;

    // Managers
    [SerializeField]
    private ScoringManager scoringManager;

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        primaryTouch = InputSystem.actions.FindAction("TouchInput");
        primaryTouchHold = InputSystem.actions.FindAction("TouchHold");
    }

    // Update is called once per frame
    void Update()
    {
        // Set initial values
        Vector3 newPos = transform.position;
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_accelerationX = Input.acceleration.x;

        //print(primaryTouch.IsPressed());
        //print(primaryTouch.ReadValue<Vector2>());
        /*
        if (primaryTouch.WasPerformedThisFrame())
        {
            print("Perform");
        }
        if (primaryTouch.WasReleasedThisFrame()) {
            print("Complete");
        }
        */
        
        
        // Do the magic (movement)
        if (Mathf.Abs(m_accelerationX) > 0.1) // 0.1 here is a cutoff so that the basket would not drift
        {
            newPos = transform.position + new Vector3(m_accelerationX * speed * Time.deltaTime, 0, 0);
        }

        if (m_moveAction.IsPressed())
        {
            newPos = transform.position +  new Vector3(m_moveAmt[0] * speed * Time.deltaTime, 0, 0);
        }

        if (primaryTouchHold.ReadValue<float>() > 0)
        {
            Vector2 touchPos = primaryTouch.ReadValue<Vector2>();
            float direction = (touchPos.x < Screen.width * 0.5f) ? -1f : 1f;
            newPos = transform.position + Vector3.right * direction * speed * Time.deltaTime;
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
