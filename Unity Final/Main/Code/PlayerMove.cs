using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    public float maxSpeed;
    public float jumpPower;

    GameObject BackgroundMusic;
    AudioSource backmusic;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circlecollider;
    Animator anim;
    AudioSource audioSource;

    void Awake()
    {
        BackgroundMusic = GameObject.Find("BackgroundMusic");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //������� �����ص�
        //�ʱ�ȭ
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circlecollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
        audioSource.Play();
    }

    void Update()
    {
        //����
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            PlaySound("JUMP");
        }
        //���߱�
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //���� �ٲٱ�
        if(Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        //�ִϸ��̼� ��ȯ
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
        //���� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        //�÷��̾� �̵�
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) //Right
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1)) //Left
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //�÷����� ����
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    anim.SetBool("isJumping", false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
                OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            //Point
            gameManager.stagePoint += 100;
            //�����
            collision.gameObject.SetActive(false);
            PlaySound("ITEM");
        }
        else if (collision.gameObject.tag == "Finish")
        {
            //���� ����������
            gameManager.NextStage();
            PlaySound("FINISH");
        }
    }


    void OnAttack(Transform enemy)
    {
        //Point
        gameManager.stagePoint += 100;
        //�ݹ߷�
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //���� ���
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamage();
        PlaySound("ATTACK");
    }

    void OnDamaged(Vector2 targetPos)
    {
        //ü�°���
        gameManager.HealthDown();

        //���̾� ��ȯ
        gameObject.layer = 11;

        //�ǰݽ� ����
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //�ðܳ�����
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*10, ForceMode2D.Impulse);

        //�ִϸ��̼�
        anim.SetTrigger("Damaged");
        Invoke("OffDamaged", 2);
        PlaySound("DAMAGED");
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        // ����ǥ��
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //������Ʈ y�� ��ȯ
        spriteRenderer.flipY = true;
        //�浹 ��Ȱ��ȭ
        circlecollider.enabled = false;
        //�״� ���
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        if (backmusic.isPlaying) backmusic.Pause();
        PlaySound("DIE");
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector3.zero;
    }

}