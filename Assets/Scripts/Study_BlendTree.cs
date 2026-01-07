using UnityEngine;

public class Study_BlendTree : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float speed;
    private static readonly int InputX = Animator.StringToHash("InputX");
    private static readonly int InputY = Animator.StringToHash("InputY");
    
    private static readonly int HitX = Animator.StringToHash("HitX");
    private static readonly int HitY = Animator.StringToHash("HitY");
    private static readonly int Hit = Animator.StringToHash("Hit");

    [SerializeField] private Transform HitTransform;

    private Vector2 GoalInput;

    [Range(0, 1)] [SerializeField] private float daming = 0.0f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        Vector2 inputAxis = 
            new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputAxis *= 2.0f;
        }
        
        GoalInput = inputAxis;
        
        Vector2 currentAnimatorInput = new Vector2(animator.GetFloat(InputX), animator.GetFloat(InputY));
        Vector2 applyInput = Vector2.Lerp(currentAnimatorInput, GoalInput, daming);
        
        animator.SetFloat(InputX,  applyInput.x);
        animator.SetFloat(InputY,  applyInput.y);
        
        transform.Translate(new Vector3(inputAxis.x,0.0f,inputAxis.y) * speed * Time.deltaTime);

        
        // 공의 위치랑 내 위치를 그냥 그대로 가져오기?
        // 공의 위치에 따라 자연스럽게 대응되는 블렌드 트리 hit 애니메이션 만들어 보기
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // {transform}.TransformPoint(Vector3 localPosition)
            // : LocalPosition 좌표를 {transform}의 World포지션 좌표로 바꿉니다.
            
            // {transform}.InverseTransformPoint(Vector3 localPosition)
            // : worldPosition 좌표를 {transform}의 local 포지션 좌표로 바꿉니다.
            
            Vector3 localPosition = transform.InverseTransformPoint(HitTransform.position);
            // 아래와 동일함 
            // Vector3 localPosition = HitTransform.position - transform.position
            
            animator.SetTrigger(Hit);
            
            animator.SetFloat(HitX, localPosition.normalized.x);
            animator.SetFloat(HitY, localPosition.normalized.z);
        }
    }
}
