using UnityEngine;

class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 target;
    public void SetTarget(Vector3 other)
    {
        target = other;
    }

    private void Update()
    {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Debug.DrawRay(transform.position, target - transform.position);
    }
}