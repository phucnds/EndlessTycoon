using Unity.Collections;
using UnityEngine;

namespace EndlessTycoon.Core
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        private float moveDir = 0;

        private void Start()
        {
            Setup();
        }

        private void Update()
        {
            float x = transform.position.x;
            anim.GetComponent<SpriteRenderer>().flipX = x < moveDir;
            moveDir = x;
        }

        private void Setup()
        {
            if (TryGetComponent<MoveAction>(out MoveAction moveAction))
            {
                moveAction.OnStartMoving += moveAction_OnStartMoving;
                moveAction.OnStopMoving += moveAction_OnStopMoving;
            }
        }

        private void moveAction_OnStopMoving()
        {
            anim.SetBool("isMoving", false);
        }

        private void moveAction_OnStartMoving()
        {
            anim.SetBool("isMoving", true);
        }
    }
}
