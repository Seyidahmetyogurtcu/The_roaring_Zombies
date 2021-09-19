using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //floats
    public float speed, maxZ, minZ;
    public float gravity = 9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight;
    public float sensitivity;
    public float sensMultiplier;
    public float xRotation;
    //integers
    public int gunNumber, minGunNumber, maxGunNumber, HP;
    //bools
    public bool isGrounded, isAiming;
    //components
    public CharacterController controller;
    //gameobjects
    public Transform groundCheck;
    public Transform playerCam;
    public Transform orientation;
    public GameObject deathScreen;
    public Text healthUI;
    //vectors
    Vector3 velocity;
    //other
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        playerMove();

        if (Input.GetButtonDown("Fire2"))
        {
            if (isAiming == false)
            {
                sensitivity = 5;
                isAiming = true;
            }
            else
            {
                sensitivity = 50;
                isAiming = false;
            }
        }

        //map limits
        if (transform.position.z >= maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ - 0.1f);
        }
        else if (transform.position.z <= minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ + 0.1f);
        }
    }

    void playerMove()
    {
        //checks if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //moves the player
        float sides = Input.GetAxis("Horizontal");
        float frontback = Input.GetAxis("Vertical");

        Vector3 move = transform.right * sides + transform.forward * frontback;

        controller.Move(move * speed * Time.deltaTime);

        //jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //fall
        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
        transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }
}
