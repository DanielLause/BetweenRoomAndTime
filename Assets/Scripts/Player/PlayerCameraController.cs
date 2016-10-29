﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCameraController : MonoBehaviour
{
    public const float SHAKE_DECAY = 0.00008f;
    public const float SHAKE_INTENSITY = 0.05f;

    public Transform ToFollow;
    public float CamLerpSpeed = 25;

    public float sensitivityX = 3F;
    public float sensitivityY = 3F;
    public float minimumX = -340F;
    public float maximumX = 340F;
    public float minimumY = -60F;
    public float maximumY = 60F;

    [Header("Crosshair")]
    public Texture CrosshairTex;
    public float CrosshairScale = 0.05f;

    [Header("View Bob")]
    public float BobbingSpeed = 0.18f;
    public float BobbingAmount = 0.2f;
    public float BobbingMidpoint = 2f;
    private float bobbingTimer = 0;

    private float rotationX = 0F;
    private float rotationY = 0F;

    [Header("EarthQuake")]
    private float shakeIntensity = 0f;
    private float shakeDekay = 0f;
    private bool Shaking = false;

    private Quaternion originalRotation;
    private PlayerGamemanger playerGamemaner;


    void Awake()
    {
        playerGamemaner = GetComponent<PlayerGamemanger>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        originalRotation = transform.localRotation;
    }

    void OnGUI()
    {
        if (!playerGamemaner.PlayerWakeUpController.IsAwake) return;
        Rect rect = new Rect((Screen.width / 2) - (CrosshairTex.width * CrosshairScale) / 2, (Screen.height / 2) - (CrosshairTex.height *CrosshairScale) / 2, CrosshairTex.width * CrosshairScale, CrosshairTex.height * CrosshairScale);
        GUI.DrawTexture(rect, CrosshairTex);
    }

    void Update()
    {
        if (!playerGamemaner.PlayerWakeUpController.IsAwake) return;

        if (Input.GetKeyDown(KeyCode.V))
        {
            EarthQuake(SHAKE_INTENSITY, SHAKE_DECAY);
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && playerGamemaner.PlayerMovementController.IsGrounded())
            ViewBob();
        else
            Camera.main.transform.position = ToFollow.position;

        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

        Quaternion wantedRot = originalRotation * xQuaternion * yQuaternion;
        Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, wantedRot, CamLerpSpeed * Time.deltaTime);

        Shake();
    }

    public void EarthQuake(float intensity, float dekay)
    {
        Shaking = true;
        shakeIntensity = intensity;
        shakeDekay = dekay;
    }

    private void Shake()
    {
        if (shakeIntensity > 0)
        {
            Vector3 tempPos = Camera.main.transform.position;

            tempPos  += Random.insideUnitSphere * shakeIntensity;
            Camera.main.transform.rotation = new Quaternion(Camera.main.transform.rotation.x + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                                        Camera.main.transform.rotation.y + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                                        Camera.main.transform.rotation.z + Random.Range(-shakeIntensity, shakeIntensity) * .2f,
                                        Camera.main.transform.rotation.w + Random.Range(-shakeIntensity, shakeIntensity) * .2f);

            shakeIntensity -= shakeDekay;
        }
        else if (Shaking)
        {
            Shaking = false;
        }
    }

    private void ViewBob()
    {
        float waveslice = 0;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 wantedPos = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            bobbingTimer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(bobbingTimer);
            bobbingTimer = bobbingTimer + BobbingSpeed;
            if (bobbingTimer > Mathf.PI * 2)
            {
                bobbingTimer = bobbingTimer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * BobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            wantedPos.y = BobbingMidpoint + translateChange;
        }
        else
        {
            wantedPos.y = BobbingMidpoint;
        }

        Camera.main.transform.position = wantedPos;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        return Mathf.Clamp(angle, min, max);
    }
}
