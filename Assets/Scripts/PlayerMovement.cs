using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Terresquall;

public class PlayerMovement : MonoBehaviourPun
{
    public float walkingSpeed, runningSpeed, acceleration, rotationSpeed, gravityScale;

    private Vector3 dir = Vector3.forward;
    public float velocity = 20f;
    public float yVelocity = 0, currentspeed, x, z, xMobile, zMobile;
    private CharacterController characterController;
    private Vector3 auxMovementVector;
    private bool shiftPressed;
    public FixedJoystick joystick;
    // Start is called before the first frame update
    private void Awake()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        //joystick.gameObject.SetActive(false);
    }



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gravityScale = Mathf.Abs(gravityScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

        if (characterController.isGrounded)
        {
            yVelocity = 0;
        }
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        xMobile = VirtualJoystick.GetAxis("Horizontal", 1);
        zMobile = VirtualJoystick.GetAxis("Vertical", 1);

        float mouseX = Input.GetAxis("Mouse X");
        InterpolateSpeed();

#if UNITY_EDITOR || UNITY_STANDALONE 
        Movement(x, z);

#elif UNITY_ANDROID
       MovementMobile(xMobile, zMobile);

#endif
        Rotation(mouseX);
    }

    void Movement(float x, float z)
    {
        Vector3 movementVector = transform.forward * currentspeed * z + transform.right * currentspeed * x;
        auxMovementVector = movementVector;

        yVelocity -= gravityScale;

        movementVector.y = yVelocity;

        movementVector *= Time.deltaTime; // se mueve igual si importar el framerate
        characterController.Move(movementVector); // metodo de character controller para moverlo
    }

    void MovementMobile(float xMobile, float zMobile)
    {
        Vector3 movementVector = transform.forward * currentspeed * zMobile + transform.right * currentspeed * xMobile;
        auxMovementVector = movementVector;

        yVelocity -= gravityScale;

        movementVector.y = yVelocity;

        movementVector *= Time.deltaTime; // se mueve igual si importar el framerate
        characterController.Move(movementVector); // metodo de character controller para moverlo
    }

    void InterpolateSpeed()
    {
        if (shiftPressed && (x != 0 || z != 0)) // si se esta presionando shift y la z o la x no son 0
        {
            currentspeed = Mathf.Lerp(currentspeed, runningSpeed, acceleration * Time.deltaTime);   // Interpolacion lineal. Pasa de la velocidad actual a corriendo
        }
        else if (x != 0 || z != 0) // si la z o la x no son 0
        {
            currentspeed = Mathf.Lerp(currentspeed, walkingSpeed, acceleration * Time.deltaTime); // pasa a velocidad normal 
        }
        else // si no
        {
            currentspeed = Mathf.Lerp(currentspeed, 0, acceleration * Time.deltaTime); // no se mueve
        }
    }

    public float GetCurrentSpeed()
    {
        return currentspeed; // devuelve la velocidad actual
    }

    void Rotation(float mouseX)
    {
        Vector3 rotation = new Vector3(0, mouseX, 0) * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotation); // el transform rote a la rotacion a la que estamos moviendo
    }
    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            joystick.gameObject.SetActive(true);
            Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
            characterController.Move(Move * velocity * Time.deltaTime);

        }
    }
}
