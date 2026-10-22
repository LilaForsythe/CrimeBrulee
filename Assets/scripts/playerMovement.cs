using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    private PlayerInput playerInput;
    private Rigidbody rb;
    float movementX;
    float movementY;
    bool isMovingRight;
    [SerializeField] int speed = 1;

    // Start is called once before the first execution of =Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float xMoveDistance = movementX * speed * Time.fixedDeltaTime;
        float yMoveDistance = movementY * speed * Time.fixedDeltaTime;
        transform.position = new Vector2(transform.position.x + xMoveDistance, transform.position.y + yMoveDistance);
    }

    void OnMove(InputValue value)
    { //get the input of the player
        Vector2 v = value.Get<Vector2>(); //assign it to the vector 2

        movementX = v.x; //x value of the vector
        movementY = v.y; // y value of the vector
    }
}
