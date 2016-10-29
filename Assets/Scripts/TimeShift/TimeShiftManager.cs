using UnityEngine;
using System.Collections;

public class TimeShiftManager : MonoBehaviour
{

    [Range(-1, 1)]
    public int AnimationSlider;
    public bool AnimateThisObject = true;
    public bool AnimateChilds = true;

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
            thisAnimator.SetFloat("AnimationValue", AnimationSlider);

        if (AnimateChilds)
            for (int i = 0; i < childAnimator.Length; i++)
                childAnimator[i].SetFloat("AnimationValue", AnimationSlider);
    }
}

