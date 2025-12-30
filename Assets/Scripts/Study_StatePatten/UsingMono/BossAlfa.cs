using System;
using System.Collections.Generic;
using UnityEngine;

namespace Study_Camera.Study_StatePattern.UsingMono
{
    public class BossAlfa : MonoBehaviour
    {
        private Dictionary<Type, BaseState> States { get; set; }
        private BaseState CurrentState { get; set; }
        private BaseState DefaultState { get; set; }
        
        private void Awake()
        {
            States = new Dictionary<Type, BaseState>();
            
            var allStates = GetComponentsInChildren<BaseState>();
            foreach (var state in allStates)
            {
                States.Add(state.GetType(), state);
                state.gameObject.SetActive(false);
            }

            DefaultState = States[typeof(IdleState)];
            ChangeState<IdleState>();
        }

        public void ChangeState<T>() where T : BaseState
        {
            var prevState = CurrentState;
            prevState?.ExitState();

            CurrentState = DefaultState;
            if (States.ContainsKey(typeof(T))) CurrentState = States[typeof(T)];
            CurrentState.EnterState();
            Debug.Log($"{prevState?.GetType().Name} changed to {CurrentState.GetType().Name}");
        }
    }
}