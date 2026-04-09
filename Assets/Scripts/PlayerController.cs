using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Engine leftEngine;
    [SerializeField] private Engine rightEngine;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform tip;
    [SerializeField] private float force;
    [SerializeField] private float torqueForce;
    [SerializeField] private ForceMode forceMode;
    [SerializeField] private ForceMode torqueForceMode;

    private Rigidbody rigidbody;
    private Vector3 movement;
    private float rotateZ;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        movement = Vector3.zero;
        rotateZ = 0;

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
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bullet bullet = Instantiate(bulletPrefab, tip.position, Quaternion.identity);
            bullet.Logic(bulletSpeed);
        }
    }

    private void FixedUpdate()
    {   
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
