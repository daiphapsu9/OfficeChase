using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorkZone : MonoBehaviour
{
    bool isActive;
    bool isOnCooldown;
    //float workingSpeed;
    private DateTime startCooldownTime;
    private DateTime startWorkingTime;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan workingDuration = DateTime.Now - startWorkingTime;

        if (isActive && workingDuration.Seconds < 5)
        {
            GameManager.instance.workLoad -= 0.1f;
        }

        if (workingDuration.Seconds >= 5)
        {
            isActive = false;
        }
        

        TimeSpan cooldownDuration = DateTime.Now - startCooldownTime;
        if ( cooldownDuration.Seconds > 4)
        {
            isOnCooldown = false;
            
        }

        if (isOnCooldown)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        } else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("OnCollisionEnter player");
            if (isOnCooldown == false)
            {
                
                startWorkingTime = DateTime.Now;
                
                //isActive = true;  
                //change color to green   
                Debug.Log("isOnCooldown == false  change color");
                isActive = true;
                SoundManager.PlaySound("typing");
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player leave");
            //isActive = false; 
            //change color to red
            isOnCooldown = true;
            startCooldownTime = DateTime.Now;
            isActive = false;
        }
    }
}
