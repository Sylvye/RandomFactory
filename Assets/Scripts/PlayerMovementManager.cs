using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PlayerMovementManager : MonoBehaviour
{
    public static PlayerMovementManager Main;
    [SerializeField] private float speed;
    // [SerializeField] private float turnSpeed;
    [SerializeField] private float sprintMult;
    private PlayerController _pc;
    private PlayerAbilityManager _pam;
    
    private void Awake()
    {
        Main = this;
    }
    
    public void Start()
    {
        _pc = PlayerController.Main;
        _pam = PlayerAbilityManager.Main;
    }

    public void FixedUpdate()
    {
        var movementInput = KeyboardInputManager.Main.GetMovementVector();
        var shiftHeld = KeyboardInputManager.Main.GetKeyState("leftShift");
        var sprinting = shiftHeld is KeyState.Pressed or KeyState.Held;
        // movement
        // Turn(force.x * turnSpeed);
        // Accelerate(force.y * speed);
        var force = speed * (sprinting ? sprintMult : 1) * movementInput.normalized;
        _pc.ApplyForce(force);
    }

    // public void Accelerate(float value)
    // {
    //     _rb.AddRelativeForce(Vector2.up * value);
    // }
    //
    // private void Turn(float torque)
    // {
    //     _rb.AddTorque(-torque);
    // }
}
