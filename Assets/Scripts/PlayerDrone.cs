using Input;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerDrone : MonoBehaviour
{

    private InputSystem_Actions inputActions;

    private Vector3 droneAxis;

    private Vector2 droneThrottle;

    private Rigidbody rb;

    [SerializeField] private float thrust;
    [SerializeField] private float rollRate;
    [SerializeField] private float pitchRate;
    
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
        

    }

    void FixedUpdate()
    {
       var pitch = droneAxis.y;
       var roll = droneAxis.x;

       rb.AddTorque(transform.right*pitch*pitchRate);
       rb.AddTorque(-transform.forward*roll*rollRate);
       rb.AddForce(transform.up*(thrust + 1.1f*(float)droneThrottle.y));

        
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
 