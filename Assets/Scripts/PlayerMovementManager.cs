using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] private float speed;
    // [SerializeField] private float turnSpeed;
    [SerializeField] private float sprintMult;
    private PlayerController _pc;
    private Rigidbody2D _rb;
    private Collider2D _col;
    
    public void Start()
    {
        _pc = PlayerController.Main;
        _rb = _pc.GetRB();
        _col = _pc.GetCol();
    }

    public void FixedUpdate()
    {
        var movementInput = KeyboardInputManager.Main.GetMovementVector();
        var shiftHeld = KeyboardInputManager.Main.GetKeyState("leftShift");
        var sprinting = shiftHeld is KeyState.Pressed or KeyState.Held;
        var force = movementInput;
        // movement
        // Turn(force.x * turnSpeed);
        // Accelerate(force.y * speed);
        
        _rb.AddForce(speed * (sprinting ? sprintMult : 1) * force.normalized);
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
