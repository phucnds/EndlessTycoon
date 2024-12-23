using UnityEngine;


namespace EndlessTycoon.Core
{
    public class CharacterVisual : MonoBehaviour
    {
        [SerializeField] private AnimatorOverrideController[] animators;
        [SerializeField] private Animator animator;

        private AnimatorOverrideController animatorOverride = null;

        public void SetVisualCustomer()
        {
            animatorOverride = animators[Random.Range(2,6)];
            SetupAnimator();
        }

        public void SetVisualStaff(int i)
        {
            animatorOverride = animators[i];
            SetupAnimator();
        }

        private void SetupAnimator()
        {
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }
    }
}

