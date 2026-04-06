using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Engine leftEngine;
    [SerializeField] private Engine rightEngine;
    [SerializeField] private float force;
    [SerializeField] private float torqueForce;
    [SerializeField] private ForceMode forceMode;
    [SerializeField] private ForceMode torqueForceMode;

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
        movement.y = 0;
        movement.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space))
            movement.y = 1;

        if (Input.GetKey(KeyCode.LeftShift))
            movement.y = -1;

        if (Input.GetKey(KeyCode.Q))
            rotateZ = 1;

        if (Input.GetKey(KeyCode.E))
            rotateZ = -1;

        if (Input.GetKey(KeyCode.R))
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        //          MOVEMENT

        rigidbody.AddTorque(Vector3.up * (rotateZ * torqueForce), torqueForceMode);
        rigidbody.AddForce(movement * force, forceMode);


        //          SFX
        leftEngine.Set(movement.x > 0);
        rightEngine.Set(movement.x < 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            Debug.Log($"Collision with {collision.gameObject.name}");
        }
    }
}