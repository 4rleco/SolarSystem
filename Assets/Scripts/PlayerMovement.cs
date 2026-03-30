using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Engine leftEngine;
    [SerializeField] private Engine rightEngine;

    private void Update()
    {
        //          INPUT
        Vector3 movement = Vector3.zero;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.z = 0;
        float roatateZ = 0;

        if (Input.GetKey(KeyCode.Space))
            movement.z = -1;

        if (Input.GetKey(KeyCode.LeftShift))
            movement.z = 1;

        if (Input.GetKey(KeyCode.Q))
            roatateZ = 1;

        if (Input.GetKey(KeyCode.E))
            roatateZ = -1;

        //          MOVEMENT
        transform.Rotate(0, 0, roatateZ);
        transform.Translate(movement * (speed * Time.deltaTime));

        //          SFX
        leftEngine.Set(movement.x > 0);
        rightEngine.Set(movement.x < 0);
    }
}