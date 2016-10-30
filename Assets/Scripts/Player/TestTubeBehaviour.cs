using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TestTubeBehaviour : MonoBehaviour
{
    public int MaxShootAmount = 6;
    public int ShootsLeft = 0;
    public float LerpSpeep = 0;

    private Image fillImage = null;

    void Awake()
    {
        fillImage = GetComponent<Image>();
    }

    void Start()
    {
        ShootsLeft = MaxShootAmount;
    }

    void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, ((float)ShootsLeft/(float)MaxShootAmount), LerpSpeep * Time.deltaTime);
    }

    public void IncreaseAmmo()
    {
        ShootsLeft++;
        ShootsLeft = ShootsLeft > MaxShootAmount ? MaxShootAmount : ShootsLeft < 0 ? 0 : ShootsLeft;
    }

    public bool DeCreaseAmmo()
    {
        if (ShootsLeft > 0)
        {
            ShootsLeft--;
            return true;
        }
        return false;
    }
}
