namespace Study_Camera.Study_ObjectPool
{
    public class Study_ObjectPool
    {
        // Study_ObjectPool은 08. Generic Animation씬에서
        // 인게임 시스템이나 메인시스템을 대체하는 역활의 컴포넌트 입니다
        private CombatEventBinder combatEventBinder =  new CombatEventBinder();

        private void OnEnable()
        {
            combatEventBinder.();
        }

        private void OnDisable()
        {
            
        }
    }
}