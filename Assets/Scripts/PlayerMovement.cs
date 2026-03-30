using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Engine leftEngine;
    [SerializeField] private Engine rightEngine;
    [SerializeField] private float force;
    [SerializeField] private float toqrueForce;
    [SerializeField] private ForceMode forceMode;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //          INPUT
        Vector3 movement = Vector3.zero;

        float rotateZ = 0;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.z = 0;

        if (Input.GetKey(KeyCode.Space))
            movement.z = -1;

        if (Input.GetKey(KeyCode.LeftShift))
            movement.z = 1;

        if (Input.GetKey(KeyCode.Q))
            rotateZ = 1;

        if (Input.GetKey(KeyCode.E))
            rotateZ = -1;

        //          MOVEMENT
        rigidbody.AddTorque(Vector3.up * (rotateZ * force), forceMode);
        rigidbody.AddForce(movement * force, forceMode);

        //          SFX
        leftEngine.Set(movement.x > 0);
        rightEngine.Set(movement.x < 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Planet"))
        {
            Debug.Log($"Collision with {collision.gameObject.name}");
        }
    }
}