using UnityEngine;
using System.Collections;

public class TimeShiftManager : MonoBehaviour
{

    [Range(-1, 1)]
    public int AnimationSlider = -1;
    public bool AnimateThisObject = true;
    public bool AnimateChilds = true;

    const float closed = -10;
    const float open = 10;

    private Animator thisAnimator;

    private Animator[] childAnimator;

    void Awake()
    {
        Init();
    }
    private void Init()
    {
        if (AnimateThisObject)
            thisAnimator = GetComponent<Animator>();

        if (AnimateChilds && transform.childCount > 0)
        {
            childAnimator = new Animator[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
                childAnimator[i] = transform.GetChild(i).GetComponent<Animator>();
        }
        else
            AnimateChilds = false;

    }
    void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        if (AnimateThisObject)
        {
            float temp = thisAnimator.GetFloat("AnimationValue");
            float newValue = 0;
            if (AnimationSlider == 0)
                newValue = Mathf.Lerp(temp, AnimationSlider == 1 ? open : closed, 2 * Time.deltaTime * AnimationSlider);
            else
                newValue = Mathf.Lerp(temp, AnimationSlider == 1 ? open : closed, 2 * Time.deltaTime);
            thisAnimator.SetFloat("AnimationValue", newValue);
        }

        if (AnimateChilds)
        {
            for (int i = 0; i < childAnimator.Length; i++)
            {
                float temp = childAnimator[i].GetFloat("AnimationValue");
                float newValue = 0;
                if (AnimationSlider == 0)
                    newValue = Mathf.Lerp(temp, AnimationSlider == 1 ? open : closed, 2 * Time.deltaTime * AnimationSlider);
                else
                    newValue = Mathf.Lerp(temp, AnimationSlider == 1 ? open : closed, 2 * Time.deltaTime);
                childAnimator[i].SetFloat("AnimationValue", newValue);
            }
        }


    }
}

