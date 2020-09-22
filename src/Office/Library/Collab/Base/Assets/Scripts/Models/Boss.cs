using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : ItemCollector, IThrowable
{

    [SerializeField]
    Bullet bullet;// bullet game object variable that we can assign an insepector

    public float fireRate;
    float nextFire;
    private Transform bossTransform;
    private GameObject target;
    public float speed;
    private Vector2 headingDir;
    private Vector2 scan;
    private float changeDirTime = 0;
    private bool detection = false;
    private bool Hit = false;
    private bool bullettrigger= false;
    public Animator animator;
    private float bossUnlockTime;
    private Vector3 bossUnlockPos;
    public int level = 0;
    public Manager[] managers;
    private float startSpeed;
    private float startFireRate;
    //private float lastdetectionTime;


    //public Collider2D bossCollider;
    // Start is called before the first frame update
    void Start()
    {
        bossTransform = gameObject.GetComponent<Transform>();
        target = GameObject.Find("Player");
        headingDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        nextFire = Time.time;
        bossUnlockPos = bossTransform.position;
        level = 0;
        startSpeed = speed; // save starting speed
        startFireRate = fireRate;
    }

    

    // Update is called once per frame
    override public void Update()
    {
        detection = false;
        float i = 0;
        for (float n = 1; i*n <= 90; n=-n)
        {
            scan = Quaternion.AngleAxis(n*i, Vector3.forward) * headingDir;
            Vector2 bossVec2 = new Vector2(bossTransform.position.x, bossTransform.position.y);
            //Debug.Log(i+ "heading direction:" + headingDir + "Scanning direction" + scan);
            //DrawLine(bossVec2, bossVec2 + scan*50, Color.red, 0.1f);
            i++;
            RaycastHit2D hit = Physics2D.Raycast(bossVec2, scan, Mathf.Infinity, 100000010);
            if (hit.collider != null)
            {
                if (hit.collider.tag.ToString() == "Player")
                {
                    headingDir = target.transform.position - bossTransform.position;
                    detection = true;
                    bullettrigger = true;//player detected and start to shoot
                    //set a timer for last detection, for later stablising 
                    //lastdetectionTime = Time.time;
                    break;
                }
            }
            
           // if (Time.time - lastdetectionTime > 2f)
            //{
                bullettrigger = false;
            //}
            
            base.Update();
        }
        //if no player is detected, and since last change direction is 5 seconds,
        //boss change direction and set the timer to the current time
        if (Hit == true || changeDirTime + 5f < Time.time && detection == false )
        {
            changeDirTime = Time.time;
            headingDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }

        //set boss animation
        animator.SetFloat("MoveX", headingDir.normalized.x);
        animator.SetFloat("MoveY", headingDir.normalized.y);

        //if boss has stayed in an area for too long
        if(bossUnlockTime + 1f < Time.time)
        {
            bossUnlockTime = Time.time;

            if (detection == false)
            {
                if(bossUnlockPos.x - bossTransform.position.x < 0.0001f || bossUnlockPos.y - bossTransform.position.y < 0.0001f)
                {
                    //Debug.Log("xxxxxxxxxxxxxxx  defrost position, detection:" + detection);
                    headingDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                }
            }
            bossUnlockPos = bossTransform.position;
        }

        //Actualisation of boss movement
        //if boss detect player, he will heads to the direction, otherwise random movement
        bossTransform.Translate(headingDir.normalized * CalculateSpeed() * Time.deltaTime);

        if (Hit == true)
        {
            //Debug.Log("hit object");
        }
        Hit = false;

        Throws();
        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Table"))
        {
            Debug.Log(other.ToString());
            Hit = true;
        }

        if (other.gameObject.CompareTag("mapzone"))
        {
            Debug.Log(other.ToString());
            Hit = true;
        }
    }

    public override void OnPickupItem(CollectableItem item)
    {
        appliedEffect = item.effect;
        appliedEffect.StartDurationCountDown();
    }

    private float CalculateSpeed()
    {
        float tempSpeed = GameManager.instance.ChangeValueBasedOnLevel(speed);
        if (appliedEffect == null)
        {
            return tempSpeed;
        }
        else
        {
            switch (appliedEffect.type)
            {
                case EffectType.IncreaseSpeed:
                    return tempSpeed + appliedEffect.value;
                case EffectType.ReduceSpeed:
                    return tempSpeed - appliedEffect.value;
                case EffectType.Stun:
                    return 0;
            }
        }
        
        return tempSpeed; // boss speed increases by leveling

    }

    // Throwing
    public void Throws()
    {
        if (appliedEffect != null && appliedEffect.type == EffectType.Stun) return;
        if (Time.time > nextFire && bullettrigger == true) //if current time greater than next fire value
        {
            //SoundManager.PlaySound("shoot");
            //instantiate a bullet at BOSS position and reset next fire varible 
            // so next instantiation will be load after one second
            bullet.target = GameObject.FindObjectOfType<Player>().gameObject; //assign target varible
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    // SKILLS

    public void ActivateSkills()
    {
        InvokeRepeating("UseSKill", 7, 7);
    }
    void UseSKill()
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        // 10% will use ultimate skill
        if (randomNumber < 10)
        {
            ManagersAssemble();
        }
        else if (randomNumber < 40) // 30 % use skill 2
        {
            AutoBuff();
        }
        else if (randomNumber < 80) // 40 % use skill 2
        {
            MoreTasks();
        } 
        else {
            // 20% do nothing
        }
    }

    void ManagersAssemble()
    {
        foreach (Manager manager in managers)
        {
            manager.gameObject.SetActive(true);
        }
        
    }

    void AutoBuff()
    {
        speed += speed * 60 / 100;
        Invoke("ClearBuff", 5);
    }

    void MoreTasks()
    {
        fireRate = 0;
        Invoke("ClearBuff", 5);
    }

    void ClearBuff()
    {
        speed = startSpeed;
        fireRate = startFireRate;
    }


    // HELPERS

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    // actions
    public void GetHit(float task)
    {
        SoundManager.PlaySound("hurt");
        GameManager.instance.workLoad -= task;
    }
}
