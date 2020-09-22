using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour, IThrowable
{
    [SerializeField]
    GameObject bullet;// bullet game object variable that we can assign an insepector

    public float fireRate;
    float nextFire;
    float existDuration = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Throws();
    }



    public void Throws()
    {
        if (Time.time > nextFire) //if current time greater than next fire value
        {
            //instantiate a bullet at BOSS position and reset next fire varible 
            // so next instantiation will be load after one second
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
   
}
