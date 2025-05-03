using Input;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerDrone : MonoBehaviour
{

    private InputSystem_Actions inputActions;

    private Vector3 droneAxis;

    [SerializeField] private float thrust;
    [SerializeField] private float rollRate;
    [SerializeField] private float pitchRate;
    
    void Start()
    {
        CreateInputSystem();
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        inputActions.Drone.Flight.performed -= FlightPhys;
        inputActions.Drone.Flight.canceled -= ctx => droneAxis = Vector3.zero;
    }


    void Update()
    {
        var pitch = droneAxis.y;
        var roll = droneAxis.x;

    }

    void FixedUpdate()
    {
       
        
    }

    void CreateInputSystem()
    {
        inputActions = InputManager.InputSystem;
        
        inputActions.Drone.Enable();

        inputActions.Drone.Flight.performed += FlightPhys;
        inputActions.Drone.Flight.canceled += ctx => droneAxis = Vector3.zero;


    }

    void FlightPhys(InputAction.CallbackContext context)
    {
        droneAxis = context.ReadValue<Vector3>();

    }

    
}
 