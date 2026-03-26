using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetMovement : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float radX;
    [SerializeField] private float radZ;
    [SerializeField] private float inclination;

    private float angle;
    private Quaternion orbitinclination;

    void Start()
    {
        orbitinclination = Quaternion.Euler(0, 0, inclination);
    }

    void Update()
    {
        angle += speed * Time.deltaTime;

        float x = radX * MathF.Cos(angle);
        float z = radZ * MathF.Sin(angle);

        float c = MathF.Sqrt(radX* radX - radZ * radZ);
        Vector3 focusOffset = new Vector3(c, 0, 0);

        //transform.RotateAround(pivot.position, Vector3.up, speed * Time.deltaTime);
        transform.position = pivot.position + focusOffset + new Vector3(x, 0, z);

        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}
