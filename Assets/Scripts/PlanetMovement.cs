using UnityEngine;
using UnityEngine.UIElements;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] Transform pivot;

    void Update()
    { 
        transform.RotateAround(pivot.position, Vector3.up, speed * Time.deltaTime);

        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}
