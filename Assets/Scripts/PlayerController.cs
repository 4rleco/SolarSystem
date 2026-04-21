using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Engine leftEngine;
    [SerializeField] private Engine rightEngine;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private GameObject particlesPrefab;

    [Header("Components")]
    [SerializeField] private AudioSource deathSound;

    [Header("Bullet")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform tip;
    [SerializeField] private Transform bulletPool;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
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
        deathSound.Stop();
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

        //          Kinetic MOVEMENT
        gameObject.transform.Rotate(rotate, rotationSpeed * Time.deltaTime);
        gameObject.transform.Translate(transform.up * (movement.z * speed * Time.deltaTime), Space.World);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bullet bullet = Instantiate(bulletPrefab, tip.position, tip.rotation, bulletPool);
            bullet.Logic(bulletSpeed);
        }
    }

    private void FixedUpdate()
    {
        //          Force MOVEMENT
        if (!kinecticMovement)
        {
            rigidbody.AddTorque(rotate * torqueForce, torqueForceMode);
            rigidbody.AddForce(transform.up * (movement.z * force), forceMode);
        }

        //          SFX
        leftEngine.Set(rotate.z > 0);
        rightEngine.Set(rotate.z < 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision with {collision.gameObject.name}");
        Instantiate(particlesPrefab, transform.position, Quaternion.identity);
        deathSound.Play();
        Destroy(gameObject, 2.0f);
    }

    private void OnDestroy()
    {
        gameManager.ResetGame();
    }
}
