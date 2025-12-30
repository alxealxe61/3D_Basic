using UnityEngine;

namespace Study_Camera.Study_StatePattern.UsingMono
{
    public class ScratchState : BaseState
    {
        private static readonly int ATTACK = Animator.StringToHash("Attack");
        
        [SerializeField] private Collider ScratchCollider;
        [SerializeField] private AnimEventReceiver receiver;
        
        public override void EnterState()
        {
            gameObject.SetActive(true);
            ScratchCollider.enabled = true;
            BossAnimator.SetTrigger(ATTACK);

            receiver.OnAnimationTriggerReceived += OnTriggeredEvent;
        }

        public override void ExitState()
        {
            gameObject.SetActive(false);
            ScratchCollider.enabled = false;
            receiver.OnAnimationTriggerReceived -= OnTriggeredEvent;
        }
        
        private const string ATP_COLLIDER_ON = "Attack_Collider_On";
        private const string ATP_COLLIDER_OFF = "Attack_Collider_Off";
        private const string ATP_ANIM_END = "Attack_End";
        
        public void OnTriggeredEvent(string str)
        {
            switch (str)
            {
                case ATP_COLLIDER_ON:
                    ScratchCollider.enabled = true;
                    break;
                case ATP_COLLIDER_OFF:
                    ScratchCollider.enabled = false;
                    break;
                case ATP_ANIM_END:
                    BossAlfa.ChangeState<IdleState>();
                    break;
                default:
                    break;
            }
        }
    }
}