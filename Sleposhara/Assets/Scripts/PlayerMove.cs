using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour // - Вместо «PlayerMove» должно быть имя файла скрипта
{
    //------- Функция/метод, выполняемая при запуске игры ---------
    public Rigidbody2D rb;
    public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //-v- Для автоматического присваивания в переменную, радиуса коллайдера объекта «GroundCheck»
        GroundCheckRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }
    //------- Функция/метод, выполняемая каждый кадр в игре ---------
    void Update()
    {
        Walk();
        Reflect();
        CheckingGround();
        CheckingLadder();
        LaddersMechanics();
        LadderUpDown();
        LADDERS();

    }
    //------- Функция/метод для перемещения персонажа по горизонтали ---------
    public Vector2 moveVector;
    public int speed = 3;
    public Joystick joystick;
    void Walk()
    {
        moveVector.x = joystick.Horizontal;
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
    }
    //------- Функция/метод для отражения персонажа по горизонтали ---------
    public bool faceRight = true;
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }
    //------- Функция/метод для контролируемого прыжка ---------
    public float jumpForce = 40f;
    private int jumpCount = 0;
    public int maxJumpValue = 2;
    
    
    public void OnJumpButtonDown()
    {
        if (onGround && !onLadder)
        {
            anim.StopPlayback();
            anim.Play("jump");
            rb.AddForce(Vector2.up * jumpForce);
        }
        else if (++jumpCount < maxJumpValue)
        {
            anim.StopPlayback();
            anim.Play("doubleJump");
            rb.velocity = new Vector2(0, 5);
        }
        if (onGround)
        {
            jumpCount = 0;
        }


    }
   
    //------- Функция/метод для обнаружения земли ---------
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;
    private float GroundCheckRadius;
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        anim.SetBool("onGround", onGround);
    }

    public float check_RADIUS = 0.04f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(CHECK_Ladder.position, check_RADIUS);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(bottom_Ladder.position, check_RADIUS);
    }
    public float LadderSpeed = 1.5f;

    public Transform CHECK_Ladder;
    public bool checkedLadder;
    public LayerMask LadderMask;
    public Transform bottom_Ladder;
    public bool bottomCheckedLadder;

    void CheckingLadder()
    {
        checkedLadder = Physics2D.OverlapPoint(CHECK_Ladder.position, LadderMask);
        bottomCheckedLadder = Physics2D.OverlapPoint(bottom_Ladder.position, LadderMask);
    }

    void LaddersMechanics()
    {
        if (onLadder)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector2(rb.velocity.x, moveVector.y * LadderSpeed);
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    void LadderUpDown()
    {
        moveVector.y = joystick.Vertical;
        anim.SetFloat("moveY", moveVector.y);
    }
    float vertInput;
    public bool onLadder;
    void LADDERS()
    {
        vertInput = joystick.Vertical;
        if (checkedLadder || bottomCheckedLadder)
                    
        {
            if (!checkedLadder && bottomCheckedLadder) // ПЕРС СВЕРХУ
            {
                if (vertInput > 0) { onLadder = false; anim.Play("idle"); }
                else if (vertInput < 0) { onLadder = true; }
            }
            else if (checkedLadder && bottomCheckedLadder) // НА ЛЕСТНИЦЕ
            {
                if (vertInput > 0) { onLadder = true; }
                else if (vertInput < 0) { onLadder = true; }
            }
            else if (checkedLadder && !bottomCheckedLadder) // ПЕРС СНИЗУ
            {
                if (vertInput > 0) { onLadder = true; }
                else if (vertInput < 0) { onLadder = false; }
            }
        }


        else if (!checkedLadder)
        {
            onLadder = false;
        }
        LaddersMechanics();
        anim.SetBool("onLadder", onLadder);
    }
    //-----------------------------------------------------------------
}