using System;
using Study_Camera.CombatSystem;
using UnityEngine;

namespace Study_Camera.Study_StatePattern.UsingMono
{
    public class BreathState : BaseState
    {
        private static readonly int BREATH = Animator.StringToHash("Breath");
        
        private bool isBreathing = false;
        [SerializeField] private ParticleSystem breathEffect;
        [SerializeField] private Collider BreathCollider;
        [SerializeField] private Transform firePoint;
        [SerializeField] private AnimEventReceiver receiver;
        
        [SerializeField] private BossBreathDetector detector;
        
        protected override void Awake()
        {
            base.Awake();
            detector.Initialize(BossAlfa);
        }

        public override void EnterState()
        {
            gameObject.SetActive(true);
            receiver.OnAnimationTriggerReceived += OnTriggeredEvent;
            breathEffect.gameObject.SetActive(true);
            breathEffect.Play();
            
            
            BossAnimator.SetTrigger(BREATH);
        }

        private void Update()
        {
            if (isBreathing)
            {
                breathEffect.transform.position = firePoint.position;
                detector.EnableDetection();
            }
        }

        public override void ExitState()
        {
            isBreathing = false;
            detector.DisableDetection();
            gameObject.SetActive(false);
            receiver.OnAnimationTriggerReceived -= OnTriggeredEvent;
        }
        
        private const string ATP_COLLIDER_ON = "Breath_Collider_On";
        private const string ATP_COLLIDER_OFF = "Breath_Collider_Off";
        private const string ATP_ANIM_END = "Breath_End";
        
        public void OnTriggeredEvent(string str)
        {
            switch (str)
            {
                case ATP_COLLIDER_ON:
                    BreathCollider.enabled = true;
                    isBreathing = true;
                    break;
                case ATP_COLLIDER_OFF:
                    BreathCollider.enabled = false;
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