using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public float moveSpeed = 7f;

    Rigidbody2D rb;
    public GameObject player;
    Vector2 moveDirection;
    float playerHit = 3;
    float bossHit = 10;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //assign rig varible
        Vector3 starting = GameObject.FindObjectOfType<Player>().gameObject.transform.position;
        //Debug.Log(starting);
        // calculate direction to target by subtraction target position and bullet position
        // a resulting vector is normalized and multiplied by move speed
        float tempMoveSpeed = GameManager.instance.ChangeValueBasedOnLevel(moveSpeed);
        Vector3 wow = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        moveDirection = new Vector2(wow.x -starting.x, wow.y - starting.y).normalized * tempMoveSpeed;
        // set velocity to  to bullet rigidbody component according to calculated direction to target
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

        // if bullet doesn't meet any game object to collide with then it will be destroyed in 4 secs 
        // to keep the scene clear
        Destroy(gameObject, 4f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit" + collision);
        //if (LayerMask.LayerToName(collision.gameObject.layer) == "")
        //{
        //    Destroy(gameObject);
        //}

        //if (collision.gameObject.tag == "mapzone")
        //{
        //    Destroy(collision);
        //}
        if (collision.gameObject.tag == "boss")
        {
            Debug.Log("hit boss");
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }


    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
