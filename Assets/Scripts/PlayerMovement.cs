using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool godmod;
    [SerializeField] bool canMove;
    [SerializeField] bool hasUnlockedDoubleJump;
    [SerializeField] float moveSpeedOriginal;
    [SerializeField] float jumpForceOriginal;
    [SerializeField] float shortJumpMultiplier;
    [SerializeField] float coyoteTime;
    [SerializeField] float jumpBuffer;
    [SerializeField] float fallingSpeedToDie;
    [SerializeField] float secondsToDieFromIdle;
    [SerializeField] float sinkingSandSpeed;
    [SerializeField] float sinkingSankJump;
    [SerializeField] float jumpSoundVolume;
    [SerializeField] float doubleJumpSoundVolume;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip doubleJumpSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GroundCheck groundCheck;
    [SerializeField] Animator runEffect;
    [SerializeField] Animator doubleJumpEffect;
    [SerializeField] GameObject landingEffect;
    [SerializeField] GameObject jumpEffect;


    Animator animator;
    Rigidbody2D rb;
    DeathManager deathManager;
    public bool facingRight { get; private set; }

    float direction;
    bool canDoubleJump;
    bool isMoving = false;
    bool slomoActivated = false;
    float counterForDoubleJumpAnimation = 1f;
    bool isGrounded = false;
    float moveSpeed;
    float jumpForce;

    float coyoteTimeCounter;
    float jumpBufferCounter;

    string currentAnimationState;

    float fallingSpeed;
    float timerForIdle;
    bool countForIdle = true;

    //animation states
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_DOUBLE_JUMP = "Double Jump";
    const string PLAYER_FALL = "Fall";
    const string PLAYER_RUN = "Run";

    void Start()
    {
        jumpForce = jumpForceOriginal;
        moveSpeed = moveSpeedOriginal;
        coyoteTimeCounter = coyoteTime;
        jumpBufferCounter = 1f;
        facingRight = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        deathManager = GetComponent<DeathManager>();
        if (godmod)
        {
            moveSpeed *= 2f;
            jumpForce *= 2f;
            hasUnlockedDoubleJump = true;
        }
    }

    void Update()
    {
        CheckForDirectionToFlip();
        if (countForIdle)
            timerForIdle += Time.deltaTime;
        if (timerForIdle >= secondsToDieFromIdle)
        {
            deathManager.SetDiedOfIdle();
            countForIdle = false;
        }
        jumpBufferCounter += Time.deltaTime;
        counterForDoubleJumpAnimation += Time.deltaTime;

        if (groundCheck.IsGrounded())
        {
            if (isMoving)
                runEffect.SetBool("Moving", true);
            if (!isGrounded) // wasn't grounded, and now is, so dust will activate
                CreateLandingEffect();
            if (jumpBufferCounter < jumpBuffer)
            {
                NormalJump();
                jumpBufferCounter = jumpBuffer + 1;
            }

            canDoubleJump = true;
            isGrounded = true;
            if (coyoteTimeCounter == -1f) //weird thing happens - the update will run one more time after getting the input for jumping, thus resetting all the values. check here if this is -1, means the player jumped last frame, BUT the physics didn't update it yet so he appears on the ground. this float is faster then the physics.
                return;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            runEffect.SetBool("Moving", false);
            coyoteTimeCounter = coyoteTimeCounter - Time.deltaTime;
            isGrounded = false;
        }

    }

    void FixedUpdate()
    {
        fallingSpeed = rb.velocity.y;

        if (isMoving)
        {
            Vector2 velocityVector = new Vector2(direction * moveSpeed, rb.velocity.y);
            rb.velocity = velocityVector;
        }

        ChangeAnimationStates();

    }

    public void SetCanMove()
    {
        canMove = !canMove;
    }

    public void EnteredSinkingSand()
    {
        moveSpeed = sinkingSandSpeed;
        jumpForce = sinkingSankJump;
    }
    public void ExitSinkingSand()
    {
        float multi = 1;
        if (godmod)
            multi = 2;
        jumpForce = jumpForceOriginal * multi;
        moveSpeed = moveSpeedOriginal * multi;
    }


    public float GetFallSpeed()
    {
        return fallingSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            if (Mathf.Abs(fallingSpeed) >= fallingSpeedToDie)
                deathManager.SetDiedOfFall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InfiniteFall"))
            if (Mathf.Abs(fallingSpeed) > fallingSpeedToDie)
                InfiniteFall();
    }

    public void InfiniteFall()
    {
        if (!deathManager.SetDiedOfInfiniteFall())
        {
            Item i = GetComponent<PlayerInteraction>().GetCurrentItemInInventory();
            if (i != null) i.PutDownItem();
            gameObject.transform.position = gameObject.transform.position - new Vector3(0, 3f, 0);
        }
    }

    private void HandleJump()
    {
        if (!groundCheck.IsGrounded())
            jumpBufferCounter = 0f;

        if (groundCheck.IsGrounded() || coyoteTimeCounter >= 0)
        {
            NormalJump();
        }
        else if (canDoubleJump && hasUnlockedDoubleJump) // double jump
        {
            DoubleJump();
        }
    }

    private void DoubleJump()
    {
        audioSource.PlayOneShot(doubleJumpSound, doubleJumpSoundVolume);
        doubleJumpEffect.SetTrigger("Double Jump");
        MakePlayerJump();
        canDoubleJump = false;
        ChangeAnimationState(PLAYER_DOUBLE_JUMP);
        counterForDoubleJumpAnimation = 0f;
    }

    private void NormalJump()
    {
        audioSource.PlayOneShot(jumpSound, jumpSoundVolume);
        coyoteTimeCounter = -1f; // resetting this so no double jump
        MakePlayerJump();
        CreateJumpEffect();
    }

    private void CreateJumpEffect()
    {
        var jumpEffectInstance = Instantiate(jumpEffect);
        jumpEffectInstance.transform.SetParent(gameObject.transform.parent);
        jumpEffectInstance.gameObject.transform.position =
            gameObject.transform.position - new Vector3(0, 0.2f, 0);
    }
    private void CreateLandingEffect()
    {
        var landEffectInstance = Instantiate(landingEffect);
        landEffectInstance.transform.SetParent(gameObject.transform.parent);
        landEffectInstance.gameObject.transform.position =
            gameObject.transform.position - new Vector3(0, 0.4f, 0);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        timerForIdle = 0;
        if (context.started && canMove)
        {
            HandleJump();
        }

        if (context.canceled && rb.velocity.y > 0f) //maybe add && canMove ?
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * shortJumpMultiplier);
        }
    }

    private void ChangeAnimationStates()
    {
        if (rb.velocity.y < -0.01f)
        {
            ChangeAnimationState(PLAYER_FALL);
        }
        else if (rb.velocity.y > 0.01f)
        {
            ChangeAnimationState(PLAYER_JUMP);
        }
        else if (isMoving)
        {
            ChangeAnimationState(PLAYER_RUN);
        }
        else if (groundCheck.IsGrounded())
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
    }

    public void ChangeAnimationState(string newState)
    {
        if (counterForDoubleJumpAnimation < 1f && !groundCheck.IsGrounded()) return;
        if (currentAnimationState == newState) return;

        animator.Play(newState);
        currentAnimationState = newState;
    }

    public void UnlockDoubleJump()
    {
        hasUnlockedDoubleJump = true;
    }

    public void SlowMotion(InputAction.CallbackContext context)
    {
        if (context.started)
            slomoActivated = !slomoActivated;

        if (slomoActivated)
            Time.timeScale = 0.1f;
        else
            Time.timeScale = 1;
    }

    private void MakePlayerJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);// to reset effects of gravity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }



    public void Move(InputAction.CallbackContext context)
    {
        timerForIdle = 0;
        direction = context.ReadValue<float>();
        if (context.started && canMove)
        {
            isMoving = true;
        }

        else if (context.canceled)
        {
            isMoving = false;
            if (direction == 0) // to stop from slipping
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            runEffect.SetBool("Moving", false);
        }

        else if (context.performed)
        {

        }
    }

    private void CheckForDirectionToFlip()
    {
        if (facingRight && direction < 0)
        {
            FlipCharacter();
        }
        else if (!facingRight && direction > 0)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
