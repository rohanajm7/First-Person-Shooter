using UnityEngine;

public class Movement : MonoBehaviour
{

    CharacterController control;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3f;
    public bool isWalking;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.5f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
            isWalking = false;
        }
        else
        {
            moveSpeed = walkSpeed;
            isWalking = true;
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded == true && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 Move = transform.right * xMov + transform.forward * zMov;

        control.Move(Move * moveSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        control.Move(velocity * Time.deltaTime);
    }
}
