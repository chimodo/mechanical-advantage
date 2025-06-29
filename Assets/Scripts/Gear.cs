using UnityEngine;

public class Gear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float teethNumber;
    // note to self: remember these scripts are also objects so you can use another gear object for stuff
    public Gear prevGear;
    public Gear nextGear;
    public bool isDriver;// the speed of this is set in 
    public float speed; // make sure to only set this if it is actually a driver
    public float direction;
    private float gearRatio; // driver/driven

    public Rigidbody2D rb;

    // remember to handle none previous gears
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // so that physics applies to rotation

        // Rigidbody2D setup for torque-driven rotation:
        // Make sure Rigidbody2D is Dynamic, no gravity, and freeze X and Y position so it only rotates
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    void Start()
    {
        if (!isDriver)
        {
            // set gear ratio
            gearRatio = teethNumber / prevGear.teethNumber;

            // rotate this driver by driver speed
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Do the stuff
    }

    void FixedUpdate()
    {
        // So now fixed update must be used so it works with the physics
        if (!isDriver)
        {
            direction = -1 * prevGear.direction;
            speed = prevGear.speed / gearRatio;
        }

        // Instead of using MoveRotation, set angular velocity so physics applies torque-based rotation
        rb.angularVelocity = speed * direction;
    }
}
