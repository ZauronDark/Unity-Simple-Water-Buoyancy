using UnityEngine;

//  --- How to setup ---
//
//   -Add box Collider on water Object as Trigger.
//   -Give water object Tag as "Water" (can be variable with "waterVolumeTag" property)
//   -Add this script on Rigidbody that you want to float on water.
//   -To turn Buoyancy Off, just disable this component.
//
//
//
//  --- What this Dose ---
//
//   -rigidbody start floating on water after passed pivot center of this object. (can be variable by offsetY and buoyantForce property)
//   -buoyant force increases as rigibody dive deep underwater. (can be variable by depthPowerLimit property)
//
//
//
//  --- Limitations ---
//
//   -No additional forces or drag or waves, just simple buoyantForce (up force).
//   -Other collider types may not work properly, this Buoyancy component is designed for Box collider only.
//   -this component also limited to Y axis, so make sure your Y is up, however you can always modify.



[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour
{
    // --- ref var ---
    [Tooltip("increase value to make GameObject more buoyant")]
    public float buoyantForce = 8f;
    [Tooltip("value 0 mean no additional buoyant force underwater, 1 mean Double buoyantForce underwater")]
    [Range(0f, 1f)]
    public float depthPowerLimit = 1f;
    public float offsetY = 0f;
    public string waterVolumeTag = "Water";


    // --- private var ---

    private Rigidbody rb;
    private float newYValue, underWaterBuoyantForce;
    private bool inWater;



    //  --- Core func ---


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //with this you can set under water buoyancy force power from your other scripts, 0f to 1f for the best result.
    //assuming buoyantForce is low enough to sink object under water when depthPowerLimit is 0f, so you can control when to sink and float through other scripts.
    public void SetPower(float value)
    {
        depthPowerLimit = value;
    }

    //again Remember to set buoyantForce low enough to sink object under water when depthPowerLimit is 0f and float with when depthPowerLimit is 1f, otherwise this wont work.
    //this checks if underwater pressue is low that means sinking, returns true.
    //room for improvements, currently junky.
    public bool IsUnderWater()
    {
        return inWater ? underWaterBuoyantForce < 0.5f ? false : true : false;
    }



    //  --- Trigger func --- 


    private void OnTriggerStay(Collider water)
    {
        //Check if water volume touched or not,
        //if so, then check if object center is below the water level
        //if so, add up force, power based on clamped depth 
        if (water.gameObject.tag == waterVolumeTag)
        {
            newYValue = transform.position.y + offsetY;
            if (newYValue < water.bounds.max.y
            && transform.localPosition.x < water.bounds.max.x
            && transform.localPosition.z < water.bounds.max.z
            && transform.localPosition.x > water.bounds.min.x
            && transform.localPosition.z > water.bounds.min.z)
            {
                underWaterBuoyantForce = Mathf.Clamp01((water.bounds.max.y - newYValue) * depthPowerLimit);
                rb.AddForce(0f, buoyantForce + (buoyantForce * rb.mass * underWaterBuoyantForce), 0f);
                //values may prone to errors.

                if (!inWater) inWater = true;
            }
            else
            {
                if (inWater) inWater = false;
            }
        }
    }
}
