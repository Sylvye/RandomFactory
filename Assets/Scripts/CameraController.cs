using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 0.05f;
    private Transform _target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _target.position.x, speed), Mathf.Lerp(transform.position.y, _target.position.y, speed), -10);
    }
}
