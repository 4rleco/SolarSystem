using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject particlesPrefab;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Logic(float speed)
    {
        rigidbody.AddForce(transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(particlesPrefab, transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
