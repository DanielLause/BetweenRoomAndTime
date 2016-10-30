using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
    private const string HANDSDOCKPOINT_NAME = "HandsPosition";
    private const string HANDS_NAME = "Hands";
    private const string ANIMATOR_SPEED_NAME = "speed";
    private const string ANIMATOR_ATTACK_NAME = "attack";

    public float HandsLerpSpeed = 10f;

    private Animator Animator;
    private GameObject Hands;
    private Transform HandsDockPos;
    private PlayerGamemanger playerGamemanager;

    void Awake()
    {
        playerGamemanager = GetComponent<PlayerGamemanger>();
        HandsDockPos = GameObject.Find(HANDSDOCKPOINT_NAME).transform;
        Hands = GameObject.Find(HANDS_NAME);
        Animator = Hands.GetComponent<Animator>();
    }

    void Update()
    {
        float speed = Mathf.Clamp(playerGamemanager.PlayerMovementController.forwardSpeed + playerGamemanager.PlayerMovementController.strafeSpeed, -1, 1);
        Animator.SetFloat(ANIMATOR_SPEED_NAME, speed);
        Hands.transform.position = HandsDockPos.position;
        Hands.transform.rotation = Quaternion.Lerp(Hands.transform.rotation, Camera.main.transform.rotation, HandsLerpSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
            Animator.SetTrigger(ANIMATOR_ATTACK_NAME);
    }
}
