using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float moveSpeed = 4f;

    Rigidbody2D rb;

    Player target;

    Vector2 moveDirection;

    float task=3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //assign rig varible

        target = GameObject.FindObjectOfType<Player>(); //assign target varible

        // calculate direction to target by subtraction target position and bullet position
        // a resulting vector is normalized and multiplied by move speed
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed; 

        // set velocity to  to bullet rigidbody component according to calculated direction to target
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        // if bullet doesn't meet any game object to collide with then it will be destroyed in 4 secs 
        // to keep the scene clear
        //Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {

            target.SendMessage("TakeTask", task);

            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
