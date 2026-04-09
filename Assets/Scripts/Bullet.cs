using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

class Bullet : MonoBehaviour
{
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Logic(float speed)
    {
        rigidbody.AddForce(transform.forward * speed);
    }
}