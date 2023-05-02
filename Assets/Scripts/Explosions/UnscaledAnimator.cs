using UnityEngine;

public class UnscaledAnimator : MonoBehaviour
{
    private Animator animator;
    private AnimatorOverrideController overrideController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
    }

    void Update()
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 1f);
            animator.Update(Time.unscaledDeltaTime);
        }
    }
}
