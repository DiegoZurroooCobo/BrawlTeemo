using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    public float walkingSpeed, runningSpeed, acceleration, rotationSpeed, gravityScale, jumpForce;
    public KeyCode throwRandomStuff;
    public GameObject randomPrefab;


    private Vector3 dir = Vector3.forward;
    private float yVelocity = 0, currentspeed, x, z;
    private CharacterController characterController;
    private Vector3 auxMovementVector;
    private bool shiftPressed;
    // Start is called before the first frame update
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
        shiftPressed = Input.GetKey(KeyCode.LeftShift);
        float mouseX = Input.GetAxis("Mouse X");
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        if (Input.GetKey(throwRandomStuff))
        {
           Instantiate(randomPrefab, dir, Quaternion.identity);
        }

        Jump(jumpPressed);
        InterpolateSpeed();
        Movement(x, z, shiftPressed);

        Rotation(mouseX);

    }

    void Jump(bool jumpPressed)
    {
        if (jumpPressed && characterController.isGrounded)
        {
            yVelocity += Mathf.Sqrt(jumpForce * 3 * gravityScale); // raiz cuadrada que suaviza el salto
        }
    }

    void Movement(float x, float z, bool shiftPressed)
    {
        Vector3 movementVector = transform.forward * currentspeed * z + transform.right * currentspeed * x;
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
}
