using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class PlayerWakeUpController : MonoBehaviour
{
    private const float VIGNETTE_STANDARD_VAL = 0.15f;
    private const float VIGNETTE_START_VAL = 1f;

    private const float MIN_DIST_TO_TARGET = 0.1f;

    public Transform CamStartPos;
    public Transform CamLookAtRight;
    public Transform CamLookAtLeft;
    public float CamLerpSpeed;
    public float CamRotLerpSpeed;
    public float VignetteLerpSpeed;

    public float CamLookSpeed;
    public float LookaAroundTime = 2f;

    [HideInInspector]
    public bool IsAwake = false;

    private bool IsStanding = false;
    private float lookTimer;

    private PlayerGamemanger playerGamemanager;
    private VignetteAndChromaticAberration vignette;

    void Awake()
    {
        Camera.main.transform.position = CamStartPos.position;
        Camera.main.transform.rotation = CamStartPos.rotation;

        playerGamemanager = GetComponent<PlayerGamemanger>();
        vignette = Camera.main.GetComponent<VignetteAndChromaticAberration>();

        vignette.intensity = VIGNETTE_START_VAL;
    }

    void Update()
    {
        if (!IsStanding)
            WakeUp();
        else if (IsStanding && !IsAwake)
            LookAround();
    }

    private void WakeUp()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, playerGamemanager.PlayerCameraController.ToFollow.position, CamLerpSpeed * Time.deltaTime);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, playerGamemanager.PlayerCameraController.ToFollow.rotation, CamRotLerpSpeed * Time.deltaTime);
        vignette.intensity = Mathf.Lerp(vignette.intensity, VIGNETTE_STANDARD_VAL, VignetteLerpSpeed * Time.deltaTime);

        if (Vector3.Distance(Camera.main.transform.position, playerGamemanager.PlayerCameraController.ToFollow.position)  <= MIN_DIST_TO_TARGET)
        {
            vignette.intensity = VIGNETTE_STANDARD_VAL;
            IsStanding = true;
            Camera.main.transform.position = playerGamemanager.PlayerCameraController.ToFollow.position;
            StartCoroutine(LookAroundTimer());
        }
    }

    private void LookAround()
    {
        lookTimer += Time.deltaTime;

        if (lookTimer <= 0.5f)
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, CamLookAtLeft.rotation, CamLookSpeed * Time.deltaTime);
        else if (lookTimer <= 1.5f)
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, CamLookAtRight.rotation, CamLookSpeed * Time.deltaTime);
        else
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, playerGamemanager.PlayerCameraController.ToFollow.rotation, CamLookSpeed * 2 * Time.deltaTime);
    }

    private IEnumerator LookAroundTimer()
    {
        yield return new WaitForSeconds(LookaAroundTime);

        Camera.main.transform.rotation = playerGamemanager.PlayerCameraController.ToFollow.rotation;
        IsAwake = true;
    }
}
