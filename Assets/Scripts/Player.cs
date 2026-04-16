using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("이동 설정")]
    public float movingSpeed = 6.5f; // 속도를 살짝 올렸습니다 (기존 5 -> 6.5). Inspector 덮어쓰기 방지를 위해 변수명 변경.

    [Header("점프 설정")]
    public float jumpStrength = 12.5f;

    [Header("더블 점프 설정")]
    public int maxJumps = 2; // 최대 점프 횟수
    private int jumpsRemaining; // 남아 있는 점프 횟수

    [Header("조작감 개선 (점프 선입력)")]
    public float jumpBufferTime = 0.15f;
    private float jumpBufferCounter;

    [Header("크기 변환(Q, E) 설정")]
    public float scaleUpMultiplier = 2.2f;   // Q로 커질 때의 비율 (더 크게)
    public float scaleDownMultiplier = 0.4f; // E로 작아질 때의 비율 (더 작게)
    private int sizeLevel = 0;          // -1: 작음, 0: 기본, 1: 큼
    private Vector3 baseAbsoluteScale;  // 시작할 때의 원본 절대 크기

    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.gravityScale = 3f;

        // 시작할 때 플레이어 오브젝트의 월래 크기 절대값을 저장해둡니다.
        baseAbsoluteScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
    }

    void Update()
    {
        // ---------------- 크기 변환 (Q, E) 로직 ----------------
        // Q: 커지기 (최대 1단계까지만)
        if (Input.GetKeyDown(KeyCode.Q) && sizeLevel < 1)
        {
            sizeLevel++;
            ApplySizeLevel();
        }
        // E: 작아지기 (최소 -1단계까지만)
        else if (Input.GetKeyDown(KeyCode.E) && sizeLevel > -1)
        {
            sizeLevel--;
            ApplySizeLevel();
        }

        // ---------------- 이동 로직 ----------------
        // 왼쪽(-1) 혹은 오른쪽(1) 방향키 입력 받기 (아무것도 안 누르면 0)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // 캐릭터 좌우 반전 로직 (스케일 반전)
        if (moveInput != 0)
        {
            // 원래 캐릭터 크기를 유지하면서 왼쪽일 때 -1, 오른쪽일 때 1을 곱해줌
            float direction = Mathf.Sign(moveInput) * Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
        }

        // y(점프) 속도는 그대로 유지하면서 x축(좌우) 속도만 변경하여 움직이면서 점프 가능하게 함
        rb.linearVelocity = new Vector2(moveInput * movingSpeed, rb.linearVelocity.y);

        // ---------------- 점프 로직 ----------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("state", 1);
            if (isGrounded)
            {
                // 바닥일 경우: 선입력 타이머 가동
                jumpBufferCounter = jumpBufferTime;
            }
            else if (jumpsRemaining > 0)
            {
                // 공중일 경우: 더블 점프 즉시 실행
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength);
                jumpsRemaining--;
                jumpBufferCounter = 0f; // 공중 점프를 했으므로 선입력 타이머는 취소
            }
            else
            {
                // 점프를 다 썼지만 바닥에 닿기 직전일 경우 (착지 직전 선입력용)
                jumpBufferCounter = jumpBufferTime;
            }
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // 바닥에 도달했는데 선입력 카운터가 남아있다면 첫 번째 점프 실행!
        if (jumpBufferCounter > 0f && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength);
            jumpBufferCounter = 0f;
            jumpsRemaining = maxJumps - 1; // 첫 번째 점프를 소모했으므로 1회 남음
        }
    }

    private void ApplySizeLevel()
    {
        // 상태(-1, 0, 1)에 따라 비율 결정
        float ratio = 1f;
        if (sizeLevel == 1) ratio = scaleUpMultiplier;
        else if (sizeLevel == -1) ratio = scaleDownMultiplier;

        // 현재 보고 있는 방향(+인지 -인지)을 추출
        float currentDirectionX = Mathf.Sign(transform.localScale.x);

        // 원본 크기에 비율을 곱한 뒤, x축은 현재 바라보는 방향을 유지하도록 부호를 맞춰줌
        transform.localScale = new Vector3(baseAbsoluteScale.x * ratio * currentDirectionX,
                                           baseAbsoluteScale.y * ratio,
                                           baseAbsoluteScale.z * ratio);
    }

    void LateUpdate()
    {
        // ---------------- 카메라 경계 이탈 방지 ----------------
        // 현재 위치를 메인 카메라 뷰포트(비율, 0~1) 좌표로 변환
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        // x 좌표가 너무 왼쪽(< 0.03)이나 너무 오른쪽(> 0.97)으로 나가지 못하게 고정
        pos.x = Mathf.Clamp(pos.x, 0.03f, 0.97f);

        // 고정된 뷰포트 좌표를 다시 월드 좌표로 변환하여 실제 위치로 적용
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGrounded(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        CheckGrounded(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        // 절벽에서 점프하지 않고 그냥 걸어서 떨어졌을 경우 첫 범프를 사용한 것으로 간주
        if (jumpsRemaining == maxJumps)
        {
            jumpsRemaining = maxJumps - 1;
        }
    }

    private void CheckGrounded(Collision2D collision)
    {
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            // 공중에 있다가 방금 닿았다면 점프 횟 충전
            if (!isGrounded)
            {
                jumpsRemaining = maxJumps;
                anim.SetInteger("state", 2);
            }
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player triggerEnter : " + other.gameObject.name);
    }
}
