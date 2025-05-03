using System.Collections;
using Input;
using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
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

    private bool inLimitPitch, inLimitRoll;
    

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
       
       DroneFlightLimit(45,45);

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
       if(inLimitRoll){torqueZ = -roll*rollRate;}
       if(inLimitPitch){torqueX = pitch*pitchRate;}

       Debug.Log(inLimitPitch);
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
        
        
    
        

        
        if(transform.rotation.eulerAngles.x <= 180f && transform.rotation.eulerAngles.x > pitchDeadZone && pitch == 0)
        {
            torqueX = -(rb.angularVelocity.x + transform.rotation.x)*flightAssist;
            
          
            
            
            
           
           
        }
        else if(transform.rotation.eulerAngles.x > 180f && transform.rotation.eulerAngles.x < 360f - pitchDeadZone && pitch == 0)
        {
            torqueX = -(rb.angularVelocity.x + transform.rotation.x)*flightAssist;
            
            
            
        }
        
        //if(Mathf.Approximately(transform.rotation.eulerAngles.x,0)  && pitch == 0){rb.AddTorque(new Vector3(0,0,0));}
        
       
       
       
       
        if(transform.rotation.eulerAngles.z <= 180f && transform.rotation.eulerAngles.z > rollDeadZone && roll == 0)
        {
            torqueZ = -(rb.angularVelocity.z + transform.rotation.z)*flightAssist;
           
        }
        else if(transform.rotation.eulerAngles.z > 180f && transform.rotation.eulerAngles.z < 360f - rollDeadZone && roll == 0)
        {
            torqueZ = -(rb.angularVelocity.z + transform.rotation.z)*flightAssist;
            
        }
        
      
        
        

        
        
        
        

        

        



    }
    void DroneFlightLimit(float pitchLimit, float rollLimit)
    {
        if(transform.rotation.eulerAngles.x <= 180f && transform.rotation.eulerAngles.x > pitchLimit)
        {
            inLimitPitch = false;
            
            torqueX = 0;
        }
        else if(transform.rotation.eulerAngles.x > 180f && transform.rotation.eulerAngles.x < 360f - pitchLimit)
        {
            inLimitPitch = false;
            
            torqueX = 0;

        }
        else
        {
            inLimitPitch = true;
        }


        
        if(transform.rotation.eulerAngles.z <= 180f && transform.rotation.eulerAngles.z > rollLimit)
        {
            inLimitRoll = false;
        }
        else if(transform.rotation.eulerAngles.z > 180f && transform.rotation.eulerAngles.z < 360f - rollLimit)
        {
            inLimitRoll = false;

        }
        else
        {
            inLimitRoll = true;
        }


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
 