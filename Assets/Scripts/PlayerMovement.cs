using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float sprintMult;
    private Rigidbody2D _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var movementInput = KeyboardInputManager.Main.GetMovementVector();
        var shiftHeld = KeyboardInputManager.Main.GetKeyState("leftShift");
        var sprinting = shiftHeld is KeyState.Pressed or KeyState.Down;
        var force = movementInput;
        if (sprinting)
        {
            force *= sprintMult;
        }
        // movement
        Turn(force.x * turnSpeed);
        Accelerate(force.y * speed);
    }

    void Accelerate(float value)
    {
        _rb.AddRelativeForce(Vector2.up * value);
    }

    void Turn(float torque)
    {
        _rb.AddTorque(-torque);
    }
}
