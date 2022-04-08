using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroler : MonoBehaviour
{

    public float speed;
    public float speedAgry;
    public int positionOfPatrol;
    public Transform point;
    bool moveingRight = true;

    public bool flipBot;
    public Transform target;
    private  Transform player;

    public float stoppingDistance;
    public float stoppingDistanceAttack;
    bool chill = false;
    bool angry = false;
    bool goBack = false;
    bool stopMan = false;
 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        if (target.transform.position.x < transform.position.x)
        {
            flipBot = false;
            Flip();
        }
        if (target.transform.position.x > transform.position.x)
        {
            flipBot = true;
            Flip();
        }

        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
            {

                chill = true;
            }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;
        }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistanceAttack)
        {
            stopMan = true;
            angry = false ;
            chill = false;
            goBack = false;
            
        }


        if (chill == true)
        {
            stopMan = false;
            goBack = false;
            Chill();
        }
        else if (angry == true)
        {
            stopMan = false;
            Angry();
        }
        else if (goBack == true)
        {
            stopMan = false;
            GoBack();
        }
        else if(stopMan == true)
        {
            angry = false;
            StopMan();
        }

    }
    void Chill()
    {
        if (transform.position.x > point.position.x + positionOfPatrol)
        {

            moveingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {

            moveingRight = true;
        }
        if (moveingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector2(-1, 1);
        }
    }
    void Angry()
    {

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        Flip();


    }
    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }
    void Flip()
    {
        if (flipBot == true)
        {
            transform.localScale = new Vector2(1, 1);
        }
        if (flipBot == false)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    void StopMan()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speedAgry * Time.deltaTime);
        Flip();
        
    }




}
