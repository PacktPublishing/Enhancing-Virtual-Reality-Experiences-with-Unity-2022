using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationPlayer : MonoBehaviour
{
    public Animator animator;
    private List<AnimationClip> animationClips;
    private int clipCount;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator != null)
        {
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
            animationClips = new List<AnimationClip>(clips);
            clipCount = animationClips.Count;

            if (clipCount > 0)
            {
                PlayRandomAnimation();
            }
        }
        else
        {
            Debug.LogError("Animator component is missing!");
        }
    }

    void PlayRandomAnimation()
    {
        int randomIndex = Random.Range(0, clipCount);
        AnimationClip randomClip = animationClips[randomIndex];
        string clipName = randomClip.name;

        animator.Play(clipName);
        Invoke("PlayRandomAnimation", randomClip.length);
    }
}
