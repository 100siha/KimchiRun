using UnityEngine;

public class Player : MonoBehaviour
{
    // 물리를 담당하는 컴포넌트를 저장할 변수
    private Rigidbody2D rb;

    [Header("점프 설정")]
    public float jumpStrength = 12.5f;

    [Header("더블 점프 설정")]
    public int maxJumps = 2; // 최대 점프 가능 횟수 (2면 더블 점프!)
    private int jumpsRemaining; // 현재 공중에서 추가로 남은 점프 횟수

    // 현재 플레이어가 바닥에 닿아 있는지 확인하는 변수
    private bool isGrounded = false;

    void Start()
    {
        // 게임이 시작될 때 Rigidbody2D 컴포넌트를 찾아서 rb 변수에 저장합니다.
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 스페이스바를 누른 순간 점프 검사를 시작합니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 1. 바닥에 서 있거나 (isGrounded)
            // 2. 공중에 떠 있더라도 아직 점프 쿠폰(jumpsRemaining)이 남아있다면!
            if (isGrounded || jumpsRemaining > 0)
            {
                // 위쪽(y축)으로 jumpStrength 만큼 쏘아올립니다.
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength);

                // 점프 쿠폰을 한 장 사용했으니 개수를 줄입니다.

                jumpsRemaining--;

                // 바닥에서 발이 떨어졌으니 공중 상태로 바꿔줍니다.

                isGrounded = false;
            }
        }
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
        // 어떤 물체에서 떨어지면 무조건 공중 상태로 간주합니다.
        isGrounded = false;
    }

    // 부딪힌 물체가 바닥인지 확인하는 함수
    private void CheckGrounded(Collision2D collision)
    {
        // 충돌한 면의 방향(법선 벡터)이 위쪽을 향하고 있다면 바닥으로 판정합니다.
        if (collision.contacts.Length > 0 && collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;

            // 바닥에 안전하게 닿았으므로 점프 횟수를 다시 최대로 충전(리필)해 줍니다!

            jumpsRemaining = maxJumps;
        }
    }
}
