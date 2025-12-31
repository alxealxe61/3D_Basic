using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Study_Camera.CombatSystem
{
    // 전투시스템을 구성하는 단위를 생각을 해봅시다
    // 스탯이라는 필드가 필요함 (전투 시스템을 거쳐서 변화할 데이터들)
    // HitBox라고 불리는 공격의 판정을 할 수 있는 개체가 필요합니다.
    // HurtBox라고 불리는 충돌 검사 개체가 필요합니다.
    
    // HitBox => CombatSystem => Stat
    // HitBox : 감지/판정 하는 역할
    // HurtBox : 감지/판정을 할 수 있게 해주는 역할
    // CombatSystem : 증재자, 매개자
    
    public class CombatSystem : SingletonBase<CombatSystem>
    {

        public class Events
        {
            // 데미지를 입었을때
            public Action<CombatEvent> OnSomeoneTakeDamage;
            
            // 누군가가 회복했을때
            public Action<CombatEvent> OnSomeoneHeal;
        }
        public Events Subscribe { get; private set; } = new Events();
        
        private const int EVENT_PROCESS_PER_FRAME = 10;
        private Dictionary<Collider, IHitTargetPart> HitTargetDic  { get; set; }
        private Queue<CombatEvent> CombatEventQueue { get; set; }
        protected override void Awake()
        {
            base.Awake();
            HitTargetDic  = new Dictionary<Collider, IHitTargetPart>();
            CombatEventQueue = new Queue<CombatEvent>();
        }
        
        private void Update()
        {
            for (int i = 0; i < EVENT_PROCESS_PER_FRAME; i++)
            {
                if (CombatEventQueue.Count == 0) break;
                var combatEvent = CombatEventQueue.Dequeue();
                HandleCombatEvent(combatEvent);
                
            }
        }

        public void AddCombatEvent(CombatEvent combatEvent)
        {
            CombatEventQueue.Enqueue(combatEvent);
        }

        private void HandleCombatEvent(CombatEvent combatEvent)
        {
            combatEvent.Receiver.TakeDamage(combatEvent.Damage);
            Subscribe.OnSomeoneTakeDamage?.Invoke(combatEvent);
        }

        #region HurtBox Management Methods

        public void AddHitBox(Collider col, HurtBox hurtBox)
        {
            HitTargetDic.Add(col, hurtBox);
        }

        public void RemoverHitBox(Collider col, HurtBox hurtBox)
        {
            if (HitTargetDic.ContainsKey(col) == false) return;
            HitTargetDic.Remove(col);
        }

        // 먼저 HasHurtBox로 조회해 본 후 호출하세요. Null 처리 해놓지 않았습니다
        public bool HasHitTarget(Collider collider)
        {
            return HitTargetDic.ContainsKey(collider);
        }

        public IHitTargetPart GetHitTarget(Collider collider)
        {
            return HitTargetDic[collider];
        }
        
        // 아래처럼 해도 무방합니다. 다만 Null을 반환할 수 있을 경우에는 함수명에 표현해주세요
        //public HurtBox GetHurtBoxOrNull(Collider collider)
        //{
        //    if (HurtBoxDic.ContainsKey(collider)) return  HurtBoxDic[collider];
        //    return null;
        //}

        #endregion
    }
}