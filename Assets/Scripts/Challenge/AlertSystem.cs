using System.Collections.Generic;
using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    // fov가 45라면 45도 각도안에 있는 aesteriod를 인식할 수 있음.
    [SerializeField] private float fov = 45f;
    // radius가 10이라면 반지름 10 범위에서 aesteriod들을 인식할 수 있음.
    [SerializeField] private float radius = 10f;
    private float alertThreshold;

    private Animator animator;
    private static readonly int blinking = Animator.StringToHash("isBlinking");

    //=================================================[이민석]
    Queue<GameObject> targets = new Queue<GameObject>();
    
    public GameObject currentPlayer;
    public CircleCollider2D playerCollider;
    
    private RocketMovementC playerInfo;
    private Vector2 FrontVector;
    private float degree;
    private float Timer;
    
    [SerializeField] float Cross01 = 0f;
    [SerializeField] float Cross02 = 0f;
    //=================================================

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerInfo = currentPlayer.GetComponent<RocketMovementC>();
        playerCollider.radius = radius;
        // FOV를 라디안으로 변환하고 코사인 값을 계산     
    }

    private void Update()
    {
        FrontVector = playerInfo.GetFrontVector();
        degree = playerInfo.GetDegree();
        CheckAlert();
        AnimUpdate();
    }

    private void CheckAlert()
    {

        float outRad = (degree - fov / 2) * Mathf.Deg2Rad;
        float inRad = (degree + fov / 2) * Mathf.Deg2Rad;
        Transform playerPos = playerInfo.gameObject.transform;

        Vector2 dest = Vector2.zero;
        if (targets.Count != 0)
        {
            dest = (targets.Dequeue().transform.position - playerPos.position).normalized;
            Debug.DrawLine(playerPos.position, playerPos.position + (Vector3)dest * 5, Color.black);
        }
        
        Vector2 outLine = new Vector2(Mathf.Cos(outRad), Mathf.Sin(outRad)).normalized;
        Debug.DrawLine(playerPos.position, playerPos.position + (Vector3)outLine * 5, Color.green);

        Vector2 inLine = new Vector2(Mathf.Cos(inRad), Mathf.Sin(inRad)).normalized;
        Debug.DrawLine(playerPos.position, playerPos.position + (Vector3)inLine * 5, Color.blue);

        if (dest == Vector2.zero) //큐에 입력된 객체가 없음 // 즉 주변에 행성이 없음
        {
            Timer += Time.deltaTime;
            if(Timer > 1f) //행성이 없는 시간 체크 // 1초를 경과하면 연산값을 초기화함.
            {
                Cross01 = 0;
                Cross02 = 0;
            }
            return;
        }

        Timer = 0;
        Cross01 = Cross(outLine, dest);
        Cross02 = Cross(dest, inLine);
    }

    private void AnimUpdate()
    {
        animator.SetBool(blinking, (Cross01 > 0 && Cross02 > 0));
    }
    
    public float Cross(Vector2 a, Vector2 b)
    {
        return a.x * b.y - a.y * b.x;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("DestThing"))
            targets.Enqueue(collision.gameObject);
    }
   
}