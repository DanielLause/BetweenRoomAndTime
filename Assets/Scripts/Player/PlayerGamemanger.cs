using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGamemanger : MonoBehaviour
{
    private const string MESSAGE_TEXT_GO_NAME = "MessageBox";

    private const int MAX_SHAKE_DELAY = 20;
    private const int MIN_SHAKE_DELAY = 13;

    [HideInInspector]
    public PlayerMovementController PlayerMovementController;
    [HideInInspector]
    public PlayerCameraController PlayerCameraController;
    [HideInInspector]
    public PlayerWakeUpController PlayerWakeUpController;
    [HideInInspector]
    public TestTubeBehaviour TestTubeBehaviour;

    private Text messageText;
    private Coroutine messageCoroutine;

    void Awake()
    {
        PlayerMovementController = GetComponent<PlayerMovementController>();
        PlayerCameraController = GetComponent<PlayerCameraController>();
        PlayerWakeUpController = GetComponent<PlayerWakeUpController>();
        TestTubeBehaviour = FindObjectOfType<TestTubeBehaviour>();
        messageText = GameObject.Find(MESSAGE_TEXT_GO_NAME).GetComponent<Text>();
        messageText.gameObject.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(ShakeTimer());
    }

    public void ShowMessage(string text, float time)
    {
        messageText.text = text;

        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);
        messageCoroutine = StartCoroutine(MessageTimer(time));
    }

    private IEnumerator MessageTimer(float delay)
    {
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        messageText.gameObject.SetActive(false);
    }

    private IEnumerator ShakeTimer()
    {
        yield return new WaitForSeconds(Random.Range(MIN_SHAKE_DELAY, MAX_SHAKE_DELAY));

        //PlayerCameraController.EarthQuake(PlayerCameraController.SHAKE_INTENSITY, PlayerCameraController.SHAKE_DECAY);

        StartCoroutine(ShakeTimer());
    }
}
