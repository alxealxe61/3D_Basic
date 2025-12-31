using System;
using UnityEngine;
using Study_Camera.CombatSystem;
using UnityEngine.Serialization;

namespace Study_Camera.Study_StatePattern.UsingMono
{
    public class Attack_3_State : BaseState
    {
        private static readonly int Property = Animator.StringToHash("Attack(3)");
        [SerializeField] private Collider leftCollider;
        [SerializeField] private Collider rightCollider;
        [SerializeField] private AnimEventReceiver receiver;
        [SerializeField] private BossAttack3Detector  detector;

        protected override void Awake()
        {
            base.Awake();
            detector.Initialize(BossAlfa);
            
        }
        public override void EnterState()
        {
            detector.EnableDetection();
            gameObject.SetActive(true);
            leftCollider.enabled = true;
            rightCollider.enabled = true;
            BossAnimator.SetTrigger(Property);

            receiver.OnAnimationTriggerReceived += OnTriggeredEvent;
        }

        public override void ExitState()
        {
            detector.DisableDetection();
            gameObject.SetActive(false);
            leftCollider.enabled = false;
            rightCollider.enabled = false;
            receiver.OnAnimationTriggerReceived += OnTriggeredEvent;
        }
        
        private const string ATP_COLLIDER_ON = "Attack_Collider_On";
        private const string ATP_COLLIDER_OFF = "Attack_Collider_Off";
        private const string ATP_ANIM_END = "Attack_End";
        
        public void OnTriggeredEvent(string str)
        {
            switch (str)
            {
                case ATP_COLLIDER_ON:
                    leftCollider.enabled = true;
                    rightCollider.enabled = true;
                    break;
                case ATP_COLLIDER_OFF:
                    leftCollider.enabled = false;
                    rightCollider.enabled = false;
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