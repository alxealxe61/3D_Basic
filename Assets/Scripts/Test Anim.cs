using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TestAnim : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Animator animator;

    private RaycastHit wallHit;
    // 속도
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float dJumpPower = 6f;
    [SerializeField] private float wallRunSpeed = 6f;
    [SerializeField] private float wallCheckDistance = 1f;
    
    [SerializeField] float wallRunDuration = 1.2f;   // n초
    [SerializeField] float wallSlideStartSpeed = 0.5f;
    [SerializeField] float wallSlideAcceleration = 1.0f;

    [SerializeField] private float currentSlideSpeed = 1;
    
    // 상태 체크 
    private bool JDown;
    private bool isJump;
    private bool isDodge;
    private bool JJDown;
    private bool isJJDown;
    private bool isWallRunning;
    private float wallRunTimer;

    private bool isWallRunningleft = true;
    private bool isWallRunningRight = true;

    private bool WJDown;
    private bool isWJDown;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        HandleMove();
        GetInput();
        Jump();
        CheckWall();
        WallRun();
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        WallJump();
    }

    void HandleMove()
    {
        rigidbody.MovePosition(rigidbody.position + Vector3.forward * speed * Time.deltaTime);
        // 좌 우 점프만 입력 받아야함 

        if (Input.GetKey(KeyCode.A) && isWallRunningleft == true)
        {
            rigidbody.MovePosition(rigidbody.position + Vector3.left * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D) &&  isWallRunningRight == true)
        { 
            rigidbody.MovePosition(rigidbody.position + Vector3.right * speed * Time.deltaTime);
        }
    }
        
    void GetInput()
    {
        JDown = Input.GetButtonDown("Jump");
        JJDown = Input.GetButtonDown("Jump");
        WJDown =  Input.GetButtonDown("Jump");
    }

    void Jump()
    {
        if (JDown && !isJump)
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }
        else if (JJDown && !isJJDown)
        {
            rigidbody.AddForce(Vector3.up * dJumpPower, ForceMode.Impulse);
            isJJDown = true;
        }
    }

    void CheckWall()
    {
        bool wallRight = Physics.Raycast
            (transform.position, transform.right, out RaycastHit rightHit, 1f);
        bool wallLeft  = Physics.Raycast
            (transform.position, -transform.right, out RaycastHit leftHit, 1f);

        if ((wallRight || wallLeft) && isJump && isWallRunning == false)
        {
            wallHit = wallRight ? rightHit  : leftHit;
            if (wallRight == true)
            {
                isWallRunningRight = false;
            }
            else
            {
                isWallRunningleft = false;
            }
            StartWallRun();
        }
    }

    // 벽 점프 구현하고 

    void WallJump()
    {
        if (WJDown && !isWJDown)
        {
            rigidbody.AddForce(Vector3.up * dJumpPower, ForceMode.Impulse);
            isWJDown = true;
        }
         
    }
    
    void StartWallRun()
    {
        isWallRunning = true;
        wallRunTimer = 0f;
        currentSlideSpeed = wallSlideStartSpeed;
        rigidbody.useGravity = false;
    }

    void WallRun()
    {
        if (!isWallRunning) return;
        wallRunTimer += Time.fixedDeltaTime;

        // 1️⃣ 벽을 따라 앞으로 가는 방향
        Vector3 wallForward = Vector3.Cross(wallHit.normal, Vector3.up);

        if (Vector3.Dot(wallForward, transform.forward) < 0)
            wallForward = -wallForward;

        // 2️⃣ n초 동안은 완전한 벽 타기
        if (wallRunTimer < wallRunDuration)
        {
            rigidbody.linearVelocity = wallForward * wallRunSpeed;
        }
        // 3️⃣ 이후에는 점점 아래로 미끄러짐
        else
        {
            // 여기에서 벽 점프 활성화 하기
            isWJDown = false;
            currentSlideSpeed += wallSlideAcceleration * Time.fixedDeltaTime;

            Vector3 slideVelocity =
                wallForward * wallRunSpeed
                + Vector3.down * currentSlideSpeed;

            rigidbody.linearVelocity = slideVelocity;
        }

        // 4️⃣ 벽이 사라지면 즉시 종료
        if (!Physics.Raycast(transform.position, -wallHit.normal, wallCheckDistance))
        {
            StopWallRun();
        }
    }
    
    void StopWallRun()
    {
        isWallRunning = false;
        rigidbody.useGravity = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged") )
        {
            animator.SetTrigger("Hit");    
        }
        
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJump = false;
            isJJDown = false;
            isWallRunningleft = true;
            isWallRunningRight = true;
            StopWallRun();
        }
    }
}
