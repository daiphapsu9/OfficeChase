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
    public GameObject Axe;
    public GameObject Book;

    public GameObject[] spawningZones; // where to spawn items
    public GameObject bookSpawningZone;
    public GameObject kitchenZone;
    public Boss createboss;
    public Boss boss;
    public Player player;
    CountDownTimer countDownTimer;

    public Text trapText;
    public Text AxeNoText;
    public Text bookNoText;
    //public GameObject kitchenZone;
    public string level;
    private float SpwanItemFrequency;
    // Start is called before the first frame update
    void Start()
    {
        InitialObject();
        StartCoroutine(SpawnTimer());
        StartCoroutine(BnnSpawnTimer());
        StartCoroutine(BookSpawnTimer());
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
            SpwanItemFrequency = 1f;
            boss.mute = true;
            //Debug.Log("LEVEL EXIST 3333");
        } else if (level == "Level3") {
            boss.mute = false;
            boss.ActivateSkills();
            SpwanItemFrequency = 2f;
        } else if (level == "Level2")
        {
            boss.mute = false;
            SpwanItemFrequency = 3f;
        }
        else
        {
            boss.mute = false;
            SpwanItemFrequency = 1.5f;
        }
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

    public IEnumerator BookSpawnTimer()
    {

        while (true)
        {
            SpawnBook();
            yield return new WaitForSeconds(5f);
        }
    }

    public void SpawnBook()
    {
        if (bookSpawningZone != null)
        {
            GameObject zone = bookSpawningZone;
            float zoneWidth = zone.GetComponent<BoxCollider2D>().bounds.size.x;
            float zoneHeight = zone.GetComponent<BoxCollider2D>().bounds.size.y;
            float randomX = UnityEngine.Random.Range(zone.transform.position.x, zone.transform.position.x + zoneWidth);
            float randomY = UnityEngine.Random.Range(zone.transform.position.y, zone.transform.position.y + zoneHeight);
            Instantiate(Book, new Vector3(randomX, randomY, 1), Quaternion.identity);
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
        if (kitchenZone != null)
        {
            GameObject zone = kitchenZone;
            float zoneWidth = zone.GetComponent<BoxCollider2D>().bounds.size.x;
            float zoneHeight = zone.GetComponent<BoxCollider2D>().bounds.size.y;
            float randomX = UnityEngine.Random.Range(zone.transform.position.x, zone.transform.position.x + zoneWidth);
            float randomY = UnityEngine.Random.Range(zone.transform.position.y, zone.transform.position.y + zoneHeight);
            Instantiate(banana, new Vector3(randomX, randomY, 1), Quaternion.identity);
        }
    }

    public void SpawnItems()
    {
        level = GameObject.Find("Level").tag.ToString();
        if (level == "Level4")
        {
            if (UnityEngine.Random.Range(0, 100) < 40)
            {
                GameObject myobj = ObjectToSpawn().gameObject;
                Instantiate(myobj, new Vector3(UnityEngine.Random.Range(-14f, 14f), UnityEngine.Random.Range(-9.3f, 9.3f), 0f), Quaternion.identity);
            }
            else if(UnityEngine.Random.Range(0, 100) < 55)
            {
                Instantiate(Axe, new Vector3(UnityEngine.Random.Range(-14f, 14f), UnityEngine.Random.Range(-9.3f, 9.3f), 0f), Quaternion.identity);
            }
            else if (UnityEngine.Random.Range(0, 100) < 85 && GameManager.instance.currentTime >= 10)
            {
                Instantiate(createboss, new Vector3(UnityEngine.Random.Range(-14f, 14f), UnityEngine.Random.Range(-9.3f, 9.3f), 0f), Quaternion.identity);
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
        trapText.text ="x "+Mathf.Floor(player.bananaCount).ToString();
        if (AxeNoText != null)
        {
            AxeNoText.text = "x " + Mathf.Floor(player.AxeNum).ToString();
        }
        if (bookNoText != null)
        {
            bookNoText.text = "x " + Mathf.Floor(player.bookCount).ToString();
        }


        IncreaseBossLevel();
        CheckGameOver();
        GoToNextScene();
    }

    private void IncreaseBossLevel()
    {

        
        if (GameManager.instance.currentTime <= GameManager.instance.gameDuration / 4)
        {
            if(boss.level != 3)
            {
                boss.level = 3;
                boss.Talking("OMAE WA MOU SHINDEIRU!!!", 3);
            }
            
        } else if (GameManager.instance.currentTime <= GameManager.instance.gameDuration / 4 * 2)
        {
            if (boss.level != 2)
            {
                boss.level = 2;
                boss.Talking("I am FASTER now!", 3);
            }

                
        } else if (GameManager.instance.currentTime <= GameManager.instance.gameDuration / 4 * 3)
        {
            if (boss.level != 1)
            {
                boss.level = 1;
                boss.Talking("I am POWERED up!!!", 3);
            }
                
        } else {
            
        }
    }

    float CalculateTask()
    {
        return GameManager.instance.workLoad / GameManager.instance.maxTask;
    }

    void GoToNextScene()
    {
        if (boss.level == 4)
        {
            if (GameObject.Find("Boss") == null && GameManager.instance.currentTime <= 0)
            {
                Debug.Log("boss gone");
                GameManager.instance.GameWin();
            }
            if (GameManager.instance.workLoad <= 0)
            {
                Debug.Log("win");
                GameManager.instance.GameWin();
            }
        }
        else if (GameManager.instance.workLoad <= 0)
        {
            GameManager.instance.LoadNextLevel();
        }
    }

    void CheckGameOver()
    { 
        if (GameManager.instance.workLoad >= GameManager.instance.maxTask)
        {
            GameManager.instance.GoToLevel(Scenes.GameOver);
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
