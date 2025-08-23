using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movimiento")]

    private CharacterController controller;
    private float movX;
    private float movZ;

    [SerializeField] private float movSpeed;

    [Header("Gravedad y salto")]

    [SerializeField] private float gravedad = -9.8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpForce;

    private Vector3 velY;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsGround;


    public int vida = 100;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (controller != null && controller.enabled) 
        {
            Movimiento();
            Gravedad();
        }

        if (Input.GetKeyDown(KeyCode.F9)) 
        {
            GameManager.Instance.CargarGuardado("Save1");
        }
    }

    private void Movimiento()
    {
        movX = Input.GetAxis("Horizontal") * movSpeed * Time.deltaTime;
        movZ = Input.GetAxis("Vertical") * movSpeed * Time.deltaTime;

        Vector3 movimiento = transform.right * movX + transform.forward * movZ;

        controller.Move(movimiento);
    }
    public void GolpearPlayer()
    {
        //Muerte
    }    

    private void Gravedad()
    {
        velY.y += gravedad * Time.deltaTime;

        if (IsGrounded() && velY.y > 0)
        {
            velY.y = 0;
        }
       
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            AudioManager.instance.Play("Jump");
            velY.y = Mathf.Sqrt(jumpForce * gravedad * -2);
        }

        controller.Move(velY * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, radius, whatIsGround);
    }
}
