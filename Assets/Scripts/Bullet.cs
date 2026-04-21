using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject particlesPrefab;
    [SerializeField] private AudioSource audioSource;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource.Stop();
    }

    public void Logic(float speed)
    {
        rigidbody.AddForce(transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Sun"))
        {
            AudioSource.PlayClipAtPoint(audioSource.clip, Camera.main.transform.position);
            Instantiate(particlesPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
