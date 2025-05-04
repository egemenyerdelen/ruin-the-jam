using System;
using System.Collections;
using Input;
using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerDrone : MonoBehaviour
{
    private Rigidbody rb;
    private InputSystem_Actions inputActions;

    private Vector3 droneAxis;

    private Vector2 droneThrottle;

    [SerializeField] private float batteryCap;

    [SerializeField] private Canvas HUD;
    [SerializeField] private RawImage[] BatteryHUD;
    private float battery;
    

    [Header("Flight Characteristics")]
    [SerializeField] private float idleThrust;

    [SerializeField] private float maxThrust;
    [SerializeField] private float rollRate;
    [SerializeField] private float pitchRate;
    [SerializeField] private float yawRate;

   

    [Header("Flight Assist")]

    [SerializeField] private float flightAssist;
    [SerializeField] private float rollDeadZone;
    [SerializeField] private float pitchDeadZone;
    [SerializeField] private float rollLimit;
    [SerializeField] private float pitchLimit;
    [SerializeField] private float dragCoefficient;
    [SerializeField] private float limitCoefficient;

     private float torqueX, torqueY, torqueZ;

    private Vector3 rollVec, pitchVec, yawVec;
    private float forceX, forceY, forceZ;

   

    
    void Start()
    {
        CreateInputSystem();
        rb = GetComponent<Rigidbody>();
        battery = batteryCap;
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
       
       battery -= Time.deltaTime;

       if(battery < 75f && BatteryHUD[3].gameObject.activeSelf){BatteryHUD[3].gameObject.SetActive(false);}
       if(battery < 50f && BatteryHUD[2].gameObject.activeSelf){BatteryHUD[2].gameObject.SetActive(false);}
       if(battery < 25f && BatteryHUD[1].gameObject.activeSelf){BatteryHUD[1].gameObject.SetActive(false);}
       if(battery <= 0f && BatteryHUD[0].gameObject.activeSelf){BatteryHUD[1].gameObject.SetActive(false);}
       



       
       
       
       var pitch = droneAxis.y;
       var roll = droneAxis.x;
       var yaw = droneThrottle.x;
       var throttle = droneThrottle.y;
       
     

    if(battery > 0)
    {
       DronePitchRoll(pitch,roll);
       DroneEngineYaw(idleThrust,throttle,yaw);
    }
    else
    {
        forceX = 0;
        forceY = 0;
        forceZ = 0;
    }

      
       
       DroneFlightAssist(pitch,roll,flightAssist,pitchDeadZone,rollDeadZone);



        AirDrag(dragCoefficient);
       


        

    }

    void FixedUpdate()
    {
     
        rb.AddForce(new Vector3(forceX,forceY,forceZ));
        rb.AddTorque(new Vector3(torqueX,torqueY,torqueZ));
        rb.AddTorque(rollVec);
        rb.AddTorque(pitchVec);
        rb.AddTorque(yawVec);
       

        
        
    }

    void DronePitchRoll(float pitch, float roll)
    {
       rollVec = -roll*rollRate*transform.forward;//*(1/(1+Mathf.Exp(limitCoefficient*(transform.rotation.eulerAngles.z-rollLimit))));
       pitchVec = pitch*pitchRate*transform.right;

      
    }

    void DroneEngineYaw(float idleThrust, float throttle, float yaw)
    {
        forceY = transform.up.y*(idleThrust + throttle*maxThrust);
        forceX = transform.up.x*(idleThrust + throttle*maxThrust);
        forceZ = transform.up.z*(idleThrust + throttle*maxThrust);
        yawVec = yaw*yawRate*transform.up;
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
        
     
        
       
       
       
       
        if(transform.rotation.eulerAngles.z <= 180f && transform.rotation.eulerAngles.z > rollDeadZone && roll == 0)
        {
            torqueZ = -(rb.angularVelocity.z + transform.rotation.z)*flightAssist;
           
        }
        else if(transform.rotation.eulerAngles.z > 180f && transform.rotation.eulerAngles.z < 360f - rollDeadZone && roll == 0)
        {
            torqueZ = -(rb.angularVelocity.z + transform.rotation.z)*flightAssist;
            
        }
        

        torqueY = -rb.angularVelocity.y*flightAssist;
      
        
        

        
        
        
        

        

        



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
 