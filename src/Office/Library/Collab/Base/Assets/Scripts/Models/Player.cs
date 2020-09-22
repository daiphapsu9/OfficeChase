using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : ItemCollector
{
    [SerializeField]
    Bullet bullet;

    [SerializeField]
    GameObject banana;

    public float fireRate;
    float nextFire;
    public float bananaPutRate;
    float nextPut;

    private const float defaultSpeed = 5;
    private Transform myTransform;
    public float speed = defaultSpeed;
    public Animator animator;
    public GameManager gameManager;
    public int bananaCount = 0;
    public int bookCount = 0;
    public int AxeNum = 0;
    public bool isWorking = false;
    private DateTime startWorkingTime;

    //public GameObject ZoneColor;
    //private Renderer rend;
    public GameObject placeobject;
    public GameObject placeoboss;
    public GameObject AttackingAxe;
    private GameObject maparea;
    private string level;

    //private bool canHide = false;
    //private bool hiding = false;
    //private DateTime startHideTime;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Level") != null) 
        {
            level = GameObject.Find("Level").tag.ToString();
        }
        maparea = GameObject.Find("background");
        myTransform = gameObject.GetComponent<Transform>();
        //if(ZoneColor != null)
        //{
        //    rend = ZoneColor.GetComponent<Renderer>();
        //    rend.material.color = Color.red;
        //}

    }


    // Update is called once per frame
    override public void Update()
    {

        //float zoneWidth = zone.GetComponent<BoxCollider2D>().bounds.size.x;
        //Debug.Log("CalculateSpeed() == " + CalculateSpeed());

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        animator.SetFloat("XMotion", movement.x);
        animator.SetFloat("YMotion", movement.y);
        animator.SetFloat("XPre", movement.x);
        animator.SetFloat("YPre", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        if (Mathf.Abs(movement.x) + Mathf.Abs(movement.y) >= 2f)
        {
            movement.x *= (float)Math.Sqrt(0.5);
            movement.y *= (float)Math.Sqrt(0.5);
        }

        transform.position = transform.position + movement * CalculateSpeed() * Time.deltaTime;
        TimeSpan workDuration = DateTime.Now - startWorkingTime;

        CheckUserKeyInput();
        base.Update();

        //TimeSpan hideDuration = DateTime.Now - startHideTime;
        //Debug.Log(hideDuration.Seconds);
        //if (hiding ==true && hideDuration.Seconds >3 )
        //{
        //    Debug.Log("555555555");
        //    gameObject.SetActive(true);
        //    hiding = false;
        //}
    }

    private void SetAnimation()
    {
        animator.SetFloat("XMotion", 0);
        animator.SetFloat("YMotion", 0);
        animator.SetFloat("XPre", 0);
        animator.SetFloat("YPre", 0);
    }

    private float CalculateSpeed()
    {
        if (appliedEffect == null)
        {
            animator.SetBool("Stun", false);//need some condition to distinguish effect
            return speed;
        }
        else
        {
            switch (appliedEffect.type)
            {
                case EffectType.IncreaseSpeed:
                    return speed + appliedEffect.value;
                case EffectType.ReduceSpeed:
                    return speed - appliedEffect.value;
                case EffectType.Stun:
                    animator.SetBool("Stun", true);
                    SoundManager.PlaySound("stun");
                    return 0;
                default:
                    return speed;
            }
        }


    }

    // triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.tag == "hide" && Input.GetKey(KeyCode.H))
        //{
        //    Debug.Log("hhhh");         

        //        startHideTime = DateTime.Now;
        //        gameObject.SetActive(false);
        //        hiding = true;
            
        //}
    }

    // actions
    public void GetHit(float task)
    {
        SoundManager.PlaySound("hurt");
        GameManager.instance.workLoad += task;
    }

    void CheckUserKeyInput()
    {

        if (appliedEffect != null && appliedEffect.type == EffectType.Stun) return;

        if (level == "Level4")
        {
            if (Input.GetKeyDown("space") && AxeNum >= 1)
            {
                Debug.Log(transform.position.x + " and " + transform.position.y);

                Vector3 wow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                Vector2 wow2 = new Vector2(wow.x - transform.position.x, wow.y - transform.position.y).normalized;
                Vector3 tar = new Vector2(transform.position.x + wow2.x * 2, transform.position.y + wow2.y);
                GameObject obj = Instantiate(AttackingAxe, tar, Quaternion.identity);
                AxeNum -= 1;
            }
        } else
        {
            if (Input.GetKey(KeyCode.B) && bananaCount > 0)
            {
                if (Time.time > nextPut) //if current time greater than next fire value
                {
                    SoundManager.PlaySound("put");

                    Vector3 bossPos = GameObject.FindObjectOfType<Boss>().gameObject.transform.position;

                    //Vector3 banaPos = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                    Vector3 banaPos = Vector3.Lerp(transform.position, bossPos, 0.25f);
                    Instantiate(banana, banaPos, transform.rotation);

                    nextPut = Time.time + bananaPutRate;
                    //Debug.Log(banaPos);
                    bananaCount -= 1;
                }

            }

            if (Input.GetKey(KeyCode.Space) && bookCount > 0)
            {
                SoundManager.PlaySound("put");
                if (Time.time > nextFire) //if current time greater than next fire value
                {
                    SoundManager.PlaySound("shoot");
                    //instantiate a bullet at BOSS position and reset next fire varible 
                    // so next instantiation will be load after one second
                    bullet.target = GameObject.FindObjectOfType<Boss>().gameObject; //assign target varible
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    nextFire = Time.time + fireRate;

                    bookCount -= 1;
                }
            }
        }


    }

    public override void OnPickupItem(CollectableItem item)
    {
        if(item.effect != null) {
            appliedEffect = item.effect;
            appliedEffect.StartDurationCountDown();
        }
        
        

        if (item is InventoryItem)
        {
            if (item.tag == "banana")
            {
                bananaCount += 1;
                return;
            }

            if (item.tag == "book")
            {
                bookCount += 5;
                return;
            }

            if (item.tag == "Axe")
            {
                AxeNum += 1;
                return;
            }
        }

        if (!(item is Traps))
        {
            GameManager.instance.workLoad -= 5;
        }

    }

}
