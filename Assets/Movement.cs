using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float playerHeight;
    [SerializeField] float groundDrag;
    public LayerMask whatIsGround;
    public bool grounded;

    Rigidbody rb;
    [SerializeField] float moveSpeed;
    
    public Transform orientation;

    public KeyCode JumpKey;
    [SerializeField] float jumpForce;
    [SerializeField] float airMultiplier;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if(grounded){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }
    }
    void FixedUpdate(){
        MovePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKey(JumpKey) && grounded){
            Jump();
        }
    }
    void Jump()
    {
        //first makes y velocity 0 to make sure all jumps are given equal force
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    void MovePlayer()
    {
        //makes the movedirection the direction that the player is facing 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded){
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f , ForceMode.Force);
        }
        //limits movement control while in the air with the airMultiplier
        else if(!grounded){
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    } 
}
