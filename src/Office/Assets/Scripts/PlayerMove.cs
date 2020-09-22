using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Transform myTransform;
    public float speed;
    Vector2 vecLeft = new Vector2(0, -1);
    Vector2 vecRight = new Vector2(-1, 0);

    // Start is called before the first frame update
    void Start()
    {
        myTransform = gameObject.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) //Vector2.right = (0,1)
        {
            //Moves the transform in the dierction and distance of translation
            myTransform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            myTransform.Translate(vecRight * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            myTransform.Translate(vecLeft * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))  //Vector2.right = (1,0)
        {
            myTransform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}
