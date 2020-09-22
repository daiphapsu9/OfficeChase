using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;// bullet game object variable that we can assign an insepector

    public float fireRate=0.5f;//ed:changed to public, so that it can be adjusted for different level
    float nextFire;
    

    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time;
        Boss BossSpeed = GetComponent<Boss>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
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
