using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circlecollider;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circlecollider = GetComponent<CircleCollider2D>();
        Invoke("Think", 5);
    }
    void FixedUpdate()
    {
        //몬스터 이동
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //플랫폼(낭떠러지) 체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }
    //재귀함수
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        //방향전환 애니메이션
        anim.SetInteger("WalkSpeed", nextMove);

        //방향전환
        if(nextMove !=0)
            spriteRenderer.flipX = nextMove == 1;

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 5);
    }

    public void OnDamage()
    {
        // 투명표현
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //오브젝트 y축 전환
        spriteRenderer.flipY = true;
        //충돌 비활성화
        circlecollider.enabled = false;
        //죽는 모션
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //없어짐
        Invoke("DeActive", 5);
    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
