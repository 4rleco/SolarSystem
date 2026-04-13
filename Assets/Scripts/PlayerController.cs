using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Engine leftEngine;
    [SerializeField] private Engine rightEngine;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform tip;
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private float torqueForce;
    [SerializeField] private ForceMode forceMode;
    [SerializeField] private ForceMode torqueForceMode;

    private GameManager gameManager;

    private Rigidbody rigidbody;
    private Vector3 movement;
    private Vector3 rotate;
    private bool kinecticMovement = true;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
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
        rotate = Vector3.zero;

        if (Input.GetKey(KeyCode.Space))
            movement.z = 1;

        rotate.x = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Q))
        {
            rotate.y = -1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotate.y = 1;
        }


        rotate.z = Input.GetAxisRaw("Horizontal");


        if (Input.GetKey(KeyCode.R))
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            kinecticMovement = true;
        }

        if (kinecticMovement)
        {
            Debug.Log("using kinectic movement");
        }

        if (kinecticMovement && Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("using physics");
            kinecticMovement = false;
        }

        gameObject.transform.Rotate(rotate, speed * Time.deltaTime);
        gameObject.transform.Translate(transform.up * (movement.z * speed * Time.deltaTime), Space.World);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bullet bullet = Instantiate(bulletPrefab, tip.position, tip.rotation);
            bullet.Logic(bulletSpeed);
        }
    }

    private void FixedUpdate()
    {
        //          MOVEMENT
        if (!kinecticMovement)
        {
            rigidbody.AddTorque(rotate * torqueForce, torqueForceMode);
            rigidbody.AddForce(transform.up * (movement.z * force), forceMode);
        }

        //          SFX
        leftEngine.Set(movement.x > 0);
        rightEngine.Set(movement.x < 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            Debug.Log($"Collision with {collision.gameObject.name}");
            gameManager.ResetGame();
        }
    }
}
