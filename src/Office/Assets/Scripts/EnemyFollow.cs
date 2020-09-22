using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    //public float speed;
    //public Animator animator;
    //Vector3 preMoveDirection;
    public Transform target;

    public float attackRange;
    public int task; //similar to demage
    private float lastAttackTime;
    public float attackDelay;


    // Start is called before the first frame update
    void Start()
    {
        //target equals to Transform of Player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()

    {
        //Vector3 moveDirection = (target.transform.position - transform.position).normalized;
        ////Move to Player's position at a speed 
        //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);





        ////magitude is size 
        ////Calculating the squared magnitude instead of the magnitude is much faster.
        ////Vector3.sqrManitude  Returns the squared length of this vector
        //if (moveDirection.sqrMagnitude > 0)
        //{
        //    animator.SetBool("walk", true);
        //    preMoveDirection = moveDirection;
        //}
        //else
        //{
        //    animator.SetBool("walk", false);
        //    moveDirection = preMoveDirection;
        //}
        //animator.SetFloat("MoveX", moveDirection.x);
        //animator.SetFloat("MoveY", moveDirection.y);

        //check the distance between boss and player
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if(distanceToPlayer < attackRange)
        {
            //Check to see if enough time has passed since last attack
            if (Time.time > lastAttackTime + attackDelay)
            {
                target.SendMessage("TakeTask", task);
                //Record the time give task
                lastAttackTime = Time.time; //current time of game clock
            }
        }


    }


    void OnColliderEnter2D(Collider2D other)
    {
        Debug.Log("detected");
        //change direction when collide with collider
    }
}
