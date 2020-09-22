using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private Transform myTransform;
    public float Velocity;
    public Animator animator;
    public GameManager gameManager;

    private bool isWorking = false;
    private DateTime startWorkingTime;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            SetAnimation();
            //myTransform.localPosition = myTransform.localPosition + Vector3.left * Velocity * Time.deltaTime;
            myTransform.Translate(Vector2.left * Velocity * Time.deltaTime);
            animator.SetFloat("XMotion", -1);
            animator.SetFloat("XPre", -1);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            SetAnimation();
            //myTransform.localPosition = myTransform.localPosition + Vector3.up * Velocity * Time.deltaTime;
            myTransform.Translate(Vector2.up * Velocity * Time.deltaTime);
            animator.SetFloat("YMotion", 1);
            animator.SetFloat("YPre", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetAnimation();
            //myTransform.localPosition = myTransform.localPosition + Vector3.down * Velocity * Time.deltaTime;
            myTransform.Translate(Vector2.down * Velocity * Time.deltaTime);
            animator.SetFloat("YMotion", -1);
            animator.SetFloat("YPre", -1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SetAnimation();
            //myTransform.localPosition = myTransform.localPosition + Vector3.right * Velocity * Time.deltaTime;
            myTransform.Translate(Vector2.right * Velocity * Time.deltaTime);
            animator.SetFloat("XMotion", 1);
            animator.SetFloat("XPre", 1);
        }
        else
        {
            animator.SetFloat("XMotion", 0);
            animator.SetFloat("YMotion", 0);
            //SetAnimation();
        }

        TimeSpan workDuration = DateTime.Now - startWorkingTime;

        if (isWorking && workDuration.Seconds < 5)
        {
            GameManager.instance.WorkLoad -= 0.1f;
            
        }
        else
        {
            isWorking = false;

        }
    }

    private void SetAnimation()
    {
        animator.SetFloat("XMotion", 0);
        animator.SetFloat("YMotion", 0);
        animator.SetFloat("XPre", 0);
        animator.SetFloat("YPre", 0);
    }

    // triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "collect")
        {
            GameManager.instance.WorkLoad -= 5;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "workzone")
        {
            startWorkingTime = DateTime.Now;
            Debug.Log("go inside");
            isWorking = true;

        }


        if (GameManager.instance.WorkLoad <= 0)
        {
            GameManager.instance.WorkLoad = 0;
            //WIN
            Debug.Log("You win");
            GameManager.instance.GameWin();
        }
    }

    // actions
    public void TakeTask(float task)
    {
        GameManager.instance.WorkLoad += task;
    }

}
