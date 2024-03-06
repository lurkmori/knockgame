using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player_control : MonoBehaviour
{

    public Rigidbody rb;
    public Animator animator;
    public Vector3 direction;
    private float defaultbobSpeed;
    public float bobSpeed;
    public float mouseSensitivity = 2.5f;
    public GameObject GBhead;
    public GameObject GBcum;
    public float defaultYcum;
    public bool isGrounded;
    float pitch = 0;
    float yaw = 0;

    bool isRotating;

    public bool isMooving;
    public float MovmentSpeed;

    private void Start()
    {
        defaultbobSpeed = bobSpeed;

        rb = GetComponent<Rigidbody>();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        moving();
        rotateMouse();
        SetAnimation();
        if (Input.anyKey)
            SetSpeedBob();
    }

    public void moving()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 p = GetBaseInput();
            p *= Time.deltaTime * bobSpeed;
            Vector3 newPosition = transform.position;
            rb.transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;

            transform.position = newPosition;

            isMooving = true;
        }
        else isMooving = false;
    }

    public void rotateMouse()
    {      
        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

        pitch += mouseSensitivity * -Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -45, 45);

        rb.transform.localEulerAngles = new Vector3(0, yaw, 0);
        GBhead.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        GBcum.transform.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }

        return p_Velocity;
    }

    public void SetAnimation()
    {
        if (isMooving)
            if (MovmentSpeed < 1)
                MovmentSpeed += 0.01f;
            else MovmentSpeed = 1;
        else
        if (MovmentSpeed > 0)
            MovmentSpeed -= 0.05f;
        else MovmentSpeed = 0;



        animator.SetFloat("MovmentSpeed", MovmentSpeed);
    }

    public void SetSpeedBob()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            bobSpeed = defaultbobSpeed / 2;
        if (Input.GetKeyUp(KeyCode.LeftControl))
            bobSpeed = defaultbobSpeed;
        if (Input.GetKeyDown(KeyCode.LeftShift))
            bobSpeed = defaultbobSpeed * 1.5f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            bobSpeed = defaultbobSpeed;        
    }
}
