using UnityEngine;

public class Piayermovement : MonoBehaviour
{
    [Header("기본 이동 설정")]
    public float moveSpeed = 5.0f;
    public float jumpSpeed = 5.0f;
    public float turnSpeed = 5.0f;

    [Header("점프 개선 설정")]
    public float fallmultiplier = 2.5f;
    public float lowJumpMultipiler = 2.0f;

    [Header("지면 감지 설정")]
    public float coyoteTime = 0.15f;
    public float coyoteTimeCounter;
    public bool realGround = true;

    [Header("글라이더 설정")]
    public GameObject gliderObject;
    public float gliderFalISpeed = 1.0f;
    public float gliderFMoveSpeed = 7.0f;
    public float gliderMaxTime = 5.0f;
    public float gliderTimerLeft;
    public bool isGliding = false;

    public Rigidbody rb;

    public bool isGrounded = true;

    public int coinCount = 0;

    void Start()
    {
        coyoteTimeCounter = 0;

        if (gliderObject != null)
        {
            gliderObject.SetActive(false);
        }
        gliderTimerLeft = gliderMaxTime;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateGrounededState();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.G) && !isGrounded && gliderTimerLeft > 0)
        {
            if (!isGliding)
            {
                EnableGlider();
            }
            gliderTimerLeft -= Time.deltaTime;

            if (gliderTimerLeft <= 0)
            {
                DisableGlider();
            }
        }
        else if (isGliding)
        {
            DisableGlider();
        }

        if (isGliding)
        {
            ApplyGliderMovement(moveHorizontal, moveVertical);
        }
        else
        {

            rb.linearVelocity = new Vector3(moveHorizontal * moveSpeed, rb.linearVelocity.y, moveVertical * moveSpeed);


            if (rb.linearVelocity.y < 0)
            {

                rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallmultiplier - 1) * Time.deltaTime;
            }
            else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallmultiplier - 1) * Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
            realGround = false;
            coyoteTimeCounter = 0f;
        }

        if (isGrounded)
        {
            if (isGliding)
            {
                DisableGlider();

                gliderTimerLeft = gliderMaxTime;
            }

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGround = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGround = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")

            realGround = false;
    }
    void UpdateGrounededState()
    {
        if (realGround)
        {
            coyoteTimeCounter = coyoteTime;
            isGrounded = true;
        }
        else
        {
            //실제로는 지면에 없지만 코요테 타임 내에 있으면 여전히 지면으로 판단
            if (coyoteTimeCounter > 0)
            {
                coyoteTimeCounter -= Time.deltaTime;
                isGrounded = true;
            }
            else
            {

                isGrounded = false;
            }
        }
    }
    void EnableGlider()
    {
        isGliding = true;


        if (gliderObject != null)
        {
            gliderObject.SetActive(true);
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, gliderFalISpeed, rb.linearVelocity.z);
    }

    void DisableGlider()
    {
        isGliding = false;


        if (gliderObject != null)
        {

            gliderObject.SetActive(false);
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
    }
    void ApplyGliderMovement(float horizontal, float vertical)
    {

        Vector3 gliderVelocity = new Vector3(horizontal * gliderFMoveSpeed, -gliderFalISpeed, vertical * gliderFMoveSpeed);

        rb.linearVelocity = gliderVelocity;

    }
}

