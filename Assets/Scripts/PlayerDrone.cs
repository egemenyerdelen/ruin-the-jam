using System.Collections;
using Input;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerDrone : MonoBehaviour
{
    private Rigidbody rb;
    private InputSystem_Actions inputActions;

    private Vector3 droneAxis;

    private Vector2 droneThrottle;

    

    [Header("Flight Characteristics")]
    [SerializeField] private float idleThrust;

    [SerializeField] private float maxThrust;
    [SerializeField] private float rollRate;
    [SerializeField] private float pitchRate;
    [SerializeField] private float yawRate;

    private float torqueX, torqueY, torqueZ;
    private float forceX, forceY, forceZ;
    

    [Header("Flight Assist")]

    [SerializeField] private float flightAssist;
    [SerializeField] private float rollDeadZone;
    [SerializeField] private float pitchDeadZone;
    [SerializeField] private float dragCoefficient;

   

    
    void Start()
    {
        CreateInputSystem();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        inputActions.Drone.Flight.performed -= FlightPhys;
        inputActions.Drone.Flight.canceled -= ctx => droneAxis = Vector3.zero;

        inputActions.Drone.Thrust.performed -= Thrust;
        inputActions.Drone.Thrust.canceled -= ctx => droneThrottle = Vector2.zero;
        
    }


    void Update()
    {
       var pitch = droneAxis.y;
       var roll = droneAxis.x;
       var yaw = droneThrottle.x;
       var throttle = droneThrottle.y;
       

       DronePitchRoll(pitch,roll);
       DroneEngineYaw(idleThrust,throttle,yaw);

       AirDrag(dragCoefficient);
       
       DroneFlightAssist(pitch,roll,flightAssist,pitchDeadZone,rollDeadZone);
        

    }

    void FixedUpdate()
    {
     
        rb.AddForce(new Vector3(forceX,forceY,forceZ));
        rb.AddTorque(new Vector3(torqueX,torqueY,torqueZ));

        
        
    }

    void DronePitchRoll(float pitch, float roll)
    {
       torqueZ = -roll*rollRate;
       torqueX = pitch*pitchRate;
    }

    void DroneEngineYaw(float idleThrust, float throttle, float yaw)
    {
        forceY = transform.up.y*(idleThrust + throttle*maxThrust);
        forceX = transform.up.x*(idleThrust + throttle*maxThrust);
        forceZ = transform.up.z*(idleThrust + throttle*maxThrust);
        torqueY = yaw*yawRate;
    }

    void AirDrag(float dragCoefficient)
    {
        forceX -= rb.linearVelocity.x*dragCoefficient;
        forceY -= rb.linearVelocity.y*dragCoefficient;
        forceZ -= rb.linearVelocity.z*dragCoefficient;
    }

 

   
   

    void DroneFlightAssist(float pitch, float roll, float flightAssist, float pitchDeadZone, float rollDeadZone)
    {
        
        Debug.Log(flightAssist);
        
        
        
        if(transform.rotation.eulerAngles.x <= 180f && transform.rotation.eulerAngles.x > pitchDeadZone && pitch == 0)
        {
            torqueX = -(rb.angularVelocity.x + transform.rotation.x*0.5f)*flightAssist;
            
          
            
            
            
           
           
        }
        else if(transform.rotation.eulerAngles.x > 180f && transform.rotation.eulerAngles.x < 360f - pitchDeadZone && pitch == 0)
        {
            torqueX = -(rb.angularVelocity.x + transform.rotation.x*0.5f)*flightAssist;
            
            
            
        }
        
        //if(Mathf.Approximately(transform.rotation.eulerAngles.x,0)  && pitch == 0){rb.AddTorque(new Vector3(0,0,0));}
        
       
       
       
       
        if(transform.rotation.eulerAngles.z <= 180f && transform.rotation.eulerAngles.z > rollDeadZone && roll == 0)
        {
            torqueZ = -(rb.angularVelocity.z + transform.rotation.z*0.7f)*flightAssist;
           
        }
        else if(transform.rotation.eulerAngles.z > 180f && transform.rotation.eulerAngles.z < 360f - rollDeadZone && roll == 0)
        {
            torqueZ = -(rb.angularVelocity.z + transform.rotation.z*0.7f)*flightAssist;
            
        }
        
      
        
        

        
        
        
        

        

        



    }

    IEnumerator Wait()
    {yield return new WaitForSeconds(0.1f);}
    void DroneFlightLimit()
    {




    }

    void CreateInputSystem()
    {
        inputActions = InputManager.InputSystem;
        
        inputActions.Drone.Enable();

        inputActions.Drone.Flight.performed += FlightPhys;
        inputActions.Drone.Flight.canceled += ctx => droneAxis = Vector3.zero;

        inputActions.Drone.Thrust.performed += Thrust;
        inputActions.Drone.Thrust.canceled += ctx => droneThrottle = Vector2.zero;
        


        


    }

    void FlightPhys(InputAction.CallbackContext context)
    {
        droneAxis = context.ReadValue<Vector3>();

    }

    void Thrust(InputAction.CallbackContext context)
    {
        droneThrottle = context.ReadValue<Vector2>();

    }



    
}
 