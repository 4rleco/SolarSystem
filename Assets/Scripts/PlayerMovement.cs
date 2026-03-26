using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        float movementY = 0;
        float roatateZ = 0;

        if (Input.GetKey(KeyCode.Space))
            movementY = 1;

        if (Input.GetKey(KeyCode.LeftShift))
            movementY = -1;

        if (Input.GetKey(KeyCode.Q))
            roatateZ = 1;

        if (Input.GetKey(KeyCode.E))
            roatateZ = -1;

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), movementY);
        transform.Rotate(0, 0, roatateZ);
        transform.Translate(movement * (speed * Time.deltaTime));

    }
}