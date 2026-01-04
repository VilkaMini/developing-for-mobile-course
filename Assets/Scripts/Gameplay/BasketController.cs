using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.InputSystem.Controls.AxisControl;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BasketController : MonoBehaviour
{
    public bool GameRunning = true;

    [SerializeField]
    private float baseSpeed = 5.0f;
    public float speed = 5f;

    private float adjustedScale = 0.0f;

    // Movement
    private InputAction m_moveAction;
    private Vector2 m_moveAmt;
    private float m_accelerationX;

    private InputAction primaryTouch;
    private InputAction secondaryTouch;
    private InputAction primaryTouchHold;

    // Managers
    [SerializeField]
    private ScoringManager scoringManager;

    float lastPinchDist;
    bool hadLastPinch = false;

    private void Awake()
    {
        m_moveAction = InputSystem.actions.FindAction("Move");
        primaryTouch = InputSystem.actions.FindAction("PrimaryTouchPos");
        secondaryTouch = InputSystem.actions.FindAction("SecondaryTouchPos");
        primaryTouchHold = InputSystem.actions.FindAction("TouchHold");
        gameObject.SetActive(false);
    }

    public void RunLogic(bool run)
    {
        GameRunning = run;
        gameObject.SetActive(run);
    }

    public void SetBasketLevel(float level)
    {
        adjustedScale = level / 10f;
        transform.localScale = Vector3.one + Vector3.one * level/10f;
    }

    public void SetBasketSpeed(float level)
    {
        speed = baseSpeed + level;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameRunning)
        {
            return;
        }

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

        // Commented out the touch input for moving basket (defeats purpose)
        /*
        if (primaryTouchHold.ReadValue<float>() > 0)
        {
            Vector2 touchPos = primaryTouch.ReadValue<Vector2>();
            float direction = (touchPos.x < Screen.width * 0.5f) ? -1f : 1f;
            newPos = transform.position + Vector3.right * direction * speed * Time.deltaTime;
        }
        */

        newPos.x = Mathf.Clamp(newPos.x, -2.3f, 2.3f);
        transform.position = newPos;

        var ts = Touchscreen.current;
        if (ts == null) return;

        var touches = ts.touches;
        int active = 0;
        foreach (var t in touches)
            if (t.press.isPressed) active++;

        if (active >= 2)
        {
            // Get first two touches
            Vector2 p1 = touches[0].position.ReadValue();
            Vector2 p2 = touches[1].position.ReadValue();

            float dist = Vector2.Distance(p1, p2);

            if (hadLastPinch)
            {
                float delta = dist - lastPinchDist;
                float scale = delta * 0.005f;

                float tempScale = Mathf.Clamp(transform.localScale.x + scale, 0.2f, 1.8f + adjustedScale);
                transform.localScale = new Vector3(tempScale, tempScale, transform.localScale.z);
            }

            lastPinchDist = dist;
            hadLastPinch = true;
        }
        else
        {
            hadLastPinch = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingItem"))
        {
            scoringManager.ItemCaught(transform.localScale.x - adjustedScale);
        }
        else if (collision.CompareTag("FallingTrash"))
        {
            scoringManager.TrashCaught();
        }
        Destroy(collision.gameObject);
    }
 

}
