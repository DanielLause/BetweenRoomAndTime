using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TestTubeBehaviour : MonoBehaviour
{
    public int MaxShootAmount = 6;
    public int ShootsLeft = 0;
    public float LerpSpeep = 0;

    private float fillValue = 0;
    private Image fillImage = null;

    void Awake()
    {
        fillImage = GetComponent<Image>();
    }
    void Start()
    {
        ShootsLeft = MaxShootAmount;
        fillValue = 1 / MaxShootAmount;
    }
    void Update()
    {
        ShootsLeft = ShootsLeft > MaxShootAmount ? MaxShootAmount : ShootsLeft < 0 ? 0 : ShootsLeft;

        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, ((float)ShootsLeft/(float)MaxShootAmount), LerpSpeep * Time.deltaTime);
    }

    

}
