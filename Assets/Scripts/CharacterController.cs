using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float maxHoldTime = 3f;           // how long you can charge
    public float maxVerticalJumpForce = 5f;  // max vertical force
    public float maxHorizontalJumpForce = 18f; // small forward nudge

    private float jumpTimer = 0f;
    private bool isGrounded = false;
    private bool isChargingJump = false;

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Start holding jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isChargingJump = true;
            jumpTimer = 0f;
        }

        // While holding jump
        if (Input.GetKey(KeyCode.Space) && isChargingJump)
        {
            jumpTimer += Time.deltaTime;
            jumpTimer = Mathf.Min(jumpTimer, maxHoldTime); // cap charge time
        }

        // On release, perform jump
        if (Input.GetKeyUp(KeyCode.Space) && isChargingJump)
        {
            PerformJump();
        }
    }

    void PerformJump()
    {
        isChargingJump = false;
        isGrounded = false;

        float chargeRatio = jumpTimer / maxHoldTime;

        float verticalForce = maxVerticalJumpForce * chargeRatio;
        float horizontalForce = maxHorizontalJumpForce * chargeRatio;

        Vector2 jumpForce = new Vector2(horizontalForce, verticalForce);

        rb.linearVelocity = Vector2.zero; // cancel previous momentum
        rb.AddForce(jumpForce, ForceMode2D.Impulse);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Gear"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Gear"))
        {
            isGrounded = false;
        }
    }
}
