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
        private const int EVENT_PROCESS_PER_FRAME = 10;
        private Dictionary<Collider, HurtBox> HurtBoxDic { get; set; }
        private Queue<CombatEvent> CombatEventQueue { get; set; }
        protected override void Awake()
        {
            base.Awake();
            HurtBoxDic = new Dictionary<Collider, HurtBox>();
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
        }

        #region HurtBox Management Methods

        public void AddHurtBox(Collider col, HurtBox hurtBox)
        {
            if(HurtBoxDic.ContainsKey(col) ==  false) return;
            
            HurtBoxDic.Add(col, hurtBox);
        }

        public void RemoverHurtBox(Collider col, HurtBox hurtBox)
        {
            if (HurtBoxDic.ContainsKey(col) == false) return;
            HurtBoxDic.Remove(col);
        }

        // 먼저 HasHurtBox로 조회해 본 후 호출하세요. Null 처리 해놓지 않았습니다
        public bool HasHurtBox(Collider col)
        {
            return HurtBoxDic[col];
        }

        public HurtBox GetHurtBox(Collider col)
        {
            return HurtBoxDic[col];
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