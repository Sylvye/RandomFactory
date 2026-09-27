using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float sprintMult;
    private Rigidbody2D _rb;
    private Collider2D _col;
    
    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    public void FixedUpdate()
    {
        var movementInput = KeyboardInputManager.Main.GetMovementVector();
        var shiftHeld = KeyboardInputManager.Main.GetKeyState("leftShift");
        var sprinting = shiftHeld is KeyState.Pressed or KeyState.Held;
        var force = movementInput;
        if (sprinting)
        {
            force *= sprintMult;
        }
        // movement
        Turn(force.x * turnSpeed);
        Accelerate(force.y * speed);
    }

    public void Accelerate(float value)
    {
        _rb.AddRelativeForce(Vector2.up * value);
    }

    private void Turn(float torque)
    {
        _rb.AddTorque(-torque);
    }
}
