using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    private const int NEUTRAL = 0;

    [Header("Forward")]
    public float WalkSpeed = 5;
    public float MaxWalkSpeed = 4;
    public float MinWalkSpeed = -4;
    private float forwardSpeed;


    [Header("Right")]
    public float StrafeSpeed = 5;
    public float MaxStrafeSpeed = 4;
    public float MinStrafeSpeed = -4;
    private float strafeSpeed;

    [Header("Jump")]
    public float Gravity = 2;
    public float JumpForce = 60;
    public float JumpTime = 0.3f;
    private float jumpVel = 0;
    private float maxJumpVel = 2;
    private float minJumpVel = -2;

    private Rigidbody rigidBody;
    private bool disableGravity = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
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

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, 0.5f))
            return true;

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
