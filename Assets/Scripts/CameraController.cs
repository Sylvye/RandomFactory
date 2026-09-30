using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 0.05f;
    public float overShoot = 10;
    private Transform _target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target = PlayerController.Main.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 target = (Vector2)_target.position + PlayerController.Main.GetVelocity() * overShoot;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x, speed), Mathf.Lerp(transform.position.y, target.y, speed), -10);
    }
}
