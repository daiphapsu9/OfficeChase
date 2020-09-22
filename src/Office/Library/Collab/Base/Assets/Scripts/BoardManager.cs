using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BoardManager : MonoBehaviour
{
    public Text myText;
    public Slider taskbar;
    public GameObject normalPen;
    public GameObject lightningPen;
    public GameObject bananaPeel;
    public GameObject banana;
    public GameObject ResignLetter;

    public GameObject[] spawningZones; // where to spawn items
    public GameObject createboss;
    public Boss boss;
    public Player player;
    CountDownTimer countDownTimer;

    public Text trapText;
    public Text AxeNoText;
   
    //public GameObject kitchenZone;
    private string level;
    private float SpwanItemFrequency;

    public int nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        InitialObject();
        StartCoroutine(SpawnTimer());
        StartCoroutine(BnnSpawnTimer());

    }

    public void InitialObject()
    {
        //level = "default";
        if (GameObject.Find("Level") != null)
        {
            //Debug.Log("LEVEL EXIST 1111");
            level = GameObject.Find("Level").tag.ToString();
            //Debug.Log("LEVEL EXIST 2222");
        }

        if (level == "Level4")
        {
            SpwanItemFrequency = 10f;
            //Debug.Log("LEVEL EXIST 3333");
        } else if (level == "Level3") {
            boss.ActivateSkills();
            SpwanItemFrequency = 2f;
        } else if (level == "Level2")
        {
            SpwanItemFrequency = 3f;
        }
        else
        {
            SpwanItemFrequency = 4f;
        }

        //if (boss == null && GameObject.Find("Boss") != null)
        //{
        //    //Debug.Log("LEVEL EXIST 1111");
        //    boss = GameObject.Find("Boss").GetComponent<Boss>();
        //    //Debug.Log("LEVEL EXIST 2222");
        //}

        //if (player == null &&  GameObject.Find("Player") != null)
        //{
        //    //Debug.Log("LEVEL EXIST 1111");
        //    player = GameObject.Find("Player").GetComponent<Player>();
        //    //Debug.Log("LEVEL EXIST 2222");
        //}
    }

    public IEnumerator SpawnTimer()
    {
        //Debug.Log("start SpawnTimer SpawnTimer !!!!!");
        while (true)
        {
            //Debug.Log("SpawnTimer SpawnTimer !!!!!");
            SpawnItems();
            yield return new WaitForSeconds(SpwanItemFrequency);
        }
    }

    public IEnumerator BnnSpawnTimer()
    {

        while (true)
        {
            //Debug.Log("SpawnTimer SpawnTimer !!!!!");
            SpawnBanana();
            yield return new WaitForSeconds(20f);
        }
    }

    public void SpawnBanana()
    {
        //if(kitchenZone != null)
        //{
            //GameObject zone = kitchenZone;
            //float zoneWidth = zone.GetComponent<BoxCollider2D>().bounds.size.x;
            //float zoneHeight = zone.GetComponent<BoxCollider2D>().bounds.size.y;
            //float randomX = UnityEngine.Random.Range(zone.transform.position.x, zoneWidth);
            //float randomY = UnityEngine.Random.Range(zone.transform.position.y, zoneHeight);
            float randomX = UnityEngine.Random.Range(10, 14);
            float randomY = UnityEngine.Random.Range(6, 9);
            Instantiate(banana, new Vector3(randomX, randomY, 1), Quaternion.identity);
        //}
    }

    public void SpawnItems()
    {
        level = GameObject.Find("Level").tag.ToString();
        if (level == "Level4")
        {
            if (UnityEngine.Random.Range(0, 100) < 70)
            {
                GameObject myobj = ObjectToSpawn().gameObject;
                Instantiate(myobj, new Vector3(UnityEngine.Random.Range(-14f, 14f), UnityEngine.Random.Range(-9.3f, 9.3f), 0f), Quaternion.identity);
            }
            else
            {
                Instantiate(ResignLetter, new Vector3(UnityEngine.Random.Range(-14f, 14f), UnityEngine.Random.Range(-9.3f, 9.3f), 0f), Quaternion.identity);
            }
        }
        if (spawningZones == null || spawningZones.Length <= 0) { return; }

        int randomZoneIndex = UnityEngine.Random.Range(0, spawningZones.Length-1);
        GameObject zone = spawningZones[randomZoneIndex];
        float zoneWidth = zone.GetComponent<BoxCollider2D>().bounds.size.x;
        float zoneHeight = zone.GetComponent<BoxCollider2D>().bounds.size.y;
        float randomX = UnityEngine.Random.Range(zone.transform.position.x, zone.transform.position.x + zoneWidth);
        float randomY = UnityEngine.Random.Range(zone.transform.position.y, zone.transform.position.y + zoneHeight);

        GameObject toInstantiate = ObjectToSpawn().gameObject;
        Instantiate(toInstantiate, new Vector3(randomX, randomY, 1), Quaternion.identity);
    }

    

    // Update is called once per frame
    void Update()
    {
        taskbar.value = CalculateTask();
        myText.text = "Task:" + Mathf.Floor(GameManager.instance.workLoad).ToString(); //Returns the largest integer smaller than or equal to TaskNum
        trapText.text ="x "+Mathf.Floor(GameManager.instance.bananaNum).ToString();
        AxeNoText.text = "x " + Mathf.Floor(GameManager.instance.AxeNum).ToString();
        IncreaseBossLevel();
        CheckGameOver();
        GoToNextScene();
    }

    private void IncreaseBossLevel()
    {
        //Debug.Log("ameManager.instance.currentTime ameManager.instance.currentTime == " + GameManager.instance.currentTime);
        if (GameManager.instance.currentTime <= GameManager.instance.gameDuration / 4)
        {
            boss.level = 3;
        } else if (GameManager.instance.currentTime <= GameManager.instance.gameDuration / 4 * 2)
        {
            boss.level = 2;
        } else if (GameManager.instance.currentTime <= GameManager.instance.gameDuration / 4 * 3)
        {
            boss.level = 1;
        } else {
            boss.level = 0;
        }
    }

    float CalculateTask()
    {
        return GameManager.instance.workLoad / GameManager.instance.maxTask;
    }

    void GoToNextScene()
    {
        if (GameManager.instance.workLoad <= 0)
        {
            GameManager.instance.LoadNextLevel();
        }
    }

    void CheckGameOver()
    {
        if (GameManager.instance.workLoad >= 100)
        {
            GameManager.instance.Replay();
        }
    }

    // random item to spawn

    private GameObject ObjectToSpawn()
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        // 10% will spawn banana peel
        if (randomNumber < 10) 
        {
            return bananaPeel;
        } else if (randomNumber < 40) // 30% spawn lightning pen
        {
            return lightningPen;
        }
        else //60% spawn normal pen
        {
            return normalPen;
        }

    }


}
