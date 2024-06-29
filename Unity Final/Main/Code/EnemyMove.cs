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
        //���� �̵�
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //�÷���(��������) üũ
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }
    //����Լ�
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        //������ȯ �ִϸ��̼�
        anim.SetInteger("WalkSpeed", nextMove);

        //������ȯ
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
        // ����ǥ��
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //������Ʈ y�� ��ȯ
        spriteRenderer.flipY = true;
        //�浹 ��Ȱ��ȭ
        circlecollider.enabled = false;
        //�״� ���
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //������
        Invoke("DeActive", 5);
    }
    void DeActive()
    {
        gameObject.SetActive(false);
    }
}
