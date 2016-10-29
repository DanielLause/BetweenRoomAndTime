using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    private const string DOOR_TAG = "Door";

    private const int NEUTRAL = 0;
    private const float DIST_TO_JUMP_PEAK = 0.05f;

    [Header("Forward")]
    public float WalkSpeed = 5;
    public float MaxWalkSpeed = 4;
    public float MinWalkSpeed = -4;
    public float forwardSpeed;


    [Header("Right")]
    public float StrafeSpeed = 5;
    public float MaxStrafeSpeed = 4;
    public float MinStrafeSpeed = -4;
    public float strafeSpeed;

    [Header("Jump")]
    public float Gravity = 2;
    public float JumpForce = 60;
    public float JumpTime = 0.3f;
    private float jumpVel = 0;
    private float maxJumpVel = 8;

    private Rigidbody rigidBody;
    private PlayerGamemanger playerGamemanager;
    private bool disableGravity = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerGamemanager = GetComponent<PlayerGamemanger>();
    }

    void Update()
    {
        if (!playerGamemanager.PlayerWakeUpController.IsAwake) return;

        GetMoveForwardInput();
        GetStrafeInput();
        GetJumpInput();
        ApplyVelocity();
    }

    private void GetMoveForwardInput()
    {
        if (Input.GetKey(KeyCode.W))
            forwardSpeed = Mathf.Lerp(forwardSpeed, MaxWalkSpeed, WalkSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.S))
            forwardSpeed = Mathf.Lerp(forwardSpeed, MinWalkSpeed, WalkSpeed * Time.deltaTime);
        else
            forwardSpeed = Mathf.Lerp(forwardSpeed, NEUTRAL, WalkSpeed * 2 * Time.deltaTime);
    }

    private void GetStrafeInput()
    {
        if (Input.GetKey(KeyCode.D))
            strafeSpeed = Mathf.Lerp(strafeSpeed, MaxStrafeSpeed, StrafeSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.A))
            strafeSpeed = Mathf.Lerp(strafeSpeed, MinStrafeSpeed, StrafeSpeed * Time.deltaTime);
        else
            strafeSpeed = Mathf.Lerp(strafeSpeed, NEUTRAL, StrafeSpeed * Time.deltaTime);
    }

    private void GetJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
                Jump();
        }
    }

    private void ApplyVelocity()
    {
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;

        Vector3 toMoveAt = new Vector3();

        if (!IsGrounded() && !disableGravity)
            toMoveAt += new Vector3(0, -Gravity, 0);
        else
            toMoveAt += new Vector3(0, jumpVel, 0);

        toMoveAt += forward * forwardSpeed;
        toMoveAt += Camera.main.transform.right * strafeSpeed;

        rigidBody.velocity = toMoveAt;
    }

    public bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            if (hit.transform.tag != DOOR_TAG)
                return true;
            else
                return false;
        }

        return false;
    }


    private void Jump()
    {
        StartCoroutine(GravityDisabler());
        StartCoroutine(JumpUpdate());
    }

    private IEnumerator JumpUpdate()
    {
        yield return new WaitForFixedUpdate();

        jumpVel = Mathf.Lerp(jumpVel, maxJumpVel, JumpForce * Time.deltaTime);

        if (disableGravity)
            StartCoroutine(JumpUpdate());
        else
            jumpVel = 0;
    }

    private IEnumerator GravityDisabler()
    {
        disableGravity = true;
        yield return new WaitForSeconds(JumpTime);
        disableGravity = false;
    }
}
