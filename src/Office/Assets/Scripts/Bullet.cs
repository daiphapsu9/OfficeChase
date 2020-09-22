using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 7f;

    Rigidbody2D rb;

    public GameObject target;

    Vector2 moveDirection;
    float playerHit = 3;
    float bossHit = 5;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //assign rig varible
        if (target == null) return;
        // calculate direction to target by subtraction target position and bullet position
        // a resulting vector is normalized and multiplied by move speed
        float tempMoveSpeed = GameManager.instance.ChangeValueBasedOnLevel(moveSpeed);
        moveDirection = (target.transform.position - transform.position).normalized * tempMoveSpeed; 

        // set velocity to  to bullet rigidbody component according to calculated direction to target
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        // if bullet doesn't meet any game object to collide with then it will be destroyed in 4 secs 
        // to keep the scene clear
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Blocking Layer")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == target.gameObject.tag )
        {
            if (target.gameObject.tag == "Player")
            {
                target.SendMessage("GetHit", playerHit);
            } else
            {
                target.SendMessage("GetHit", bossHit);
            }
            
            Destroy(gameObject);
        } 

        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
