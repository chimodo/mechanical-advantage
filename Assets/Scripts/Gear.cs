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

    // remember to handle none previous gears
    
    void Start()
    {
        if (!isDriver)
        {
            // set gear ratio
            gearRatio =  teethNumber / prevGear.teethNumber;

            // rotate this driver by driver speed
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Do the stuff
        
        if (!isDriver)
        {
            direction = -1 * prevGear.direction;
            speed = prevGear.speed / gearRatio;
        }
        
        transform.Rotate(0f, 0f, speed * direction * Time.deltaTime);

    }
}
