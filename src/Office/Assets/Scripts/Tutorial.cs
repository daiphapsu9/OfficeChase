using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TutorialLevel : MonoBehaviour
{
    public int level;
    public bool isFinished;
    public string narratingText = "";
    public bool isConditionPassed;
    public DateTime conditionPassedTime;
    public float startWorkLoad;
    public bool hasStarted = false;
    public Boss boss = null;
    public Player player;

    public TutorialLevel(int level, bool isFinished = false)
    {
        this.level = level;
        this.isFinished = isFinished;
    }

    public void SetStartingValue(GameObject bossObject = null, GameObject playerObject = null)
    {
        if (!hasStarted)
        {
            startWorkLoad = GameManager.instance.workLoad;
            hasStarted = true;
            if (bossObject)
            {
                boss = bossObject.GetComponent<Boss>();
            }

            if (playerObject)
            {
                player = playerObject.GetComponent<Player>();
            }

           
        }
        
    }

    public void CheckCondition()
    {
        if (level == 1)
        {
            isConditionPassed = ConditionLevel1();
        }
        else if (level == 2)
        {
            isConditionPassed = ConditionLevel2();
        }
        else if (level == 3)
        {
            isConditionPassed = ConditionLevel3();
        }
        else if (level == 4)
        {
            isConditionPassed = ConditionLevel4();
        }

        else if (level == 5)
        {
            isConditionPassed = ConditionLevel5();
        }

        else if (level == 6)
        {
            isConditionPassed = ConditionLevel6();
        }

        else if (level == 7)
        {
            isConditionPassed = ConditionLevel7();
        }

        TimeSpan timePassed = DateTime.Now - conditionPassedTime;
        if (isConditionPassed && timePassed.Seconds >= 4f)
        {
            isFinished = true;
        }
    }

    bool ConditionLevel1()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (isConditionPassed == false) {
                conditionPassedTime = DateTime.Now;
                narratingText = "Welldone! You has mastered moving skill!";
            }
            return true;
        } else
        {
            return isConditionPassed;
        }
    }

    bool ConditionLevel2()
    {
        if (startWorkLoad > GameManager.instance.workLoad)
        {
            if (isConditionPassed == false)
            {
                conditionPassedTime = DateTime.Now;
                narratingText = "You reduced your work load by picking up pens.\nYou will finish you level when your tasks is reduced to 0.";
            }
            return true;
        }
        return isConditionPassed;
    }

    bool ConditionLevel3()
    {
        if (startWorkLoad > GameManager.instance.workLoad)
        {
            if (isConditionPassed == false)
            {
                conditionPassedTime = DateTime.Now;
                narratingText = "Great! You reduced your work load by going to the work zone";
            }
            return true;
        }
        return isConditionPassed;

    }

    bool ConditionLevel4()
    {

        if (boss == null) return false;
        if (boss.appliedEffect != null && boss.appliedEffect.type == EffectType.Stun)
        {
            if (isConditionPassed == false)
            {
                conditionPassedTime = DateTime.Now;
                narratingText = "Amazing work! The boss is stunned and stop attacking";
            }
            return true;
        }

  


        if (player == null) return false;
        if (player.appliedEffect != null && player.appliedEffect.type == EffectType.Stun)
        {
            if (isConditionPassed == false)
            {
                conditionPassedTime = DateTime.Now;
                narratingText = "Amazing work! Your stunned yourself";
            }
            return true;
        }
        return isConditionPassed;

    }

    bool ConditionLevel5()
    {
        if (startWorkLoad > GameManager.instance.workLoad)
        {
            if (isConditionPassed == false)
            {
                conditionPassedTime = DateTime.Now;
                narratingText = "You reduced your work load by throwing books at the boss\nHow amazing is that!!! LOL";
            }
            return true;
        }
        return isConditionPassed;

    }

    bool ConditionLevel6()
    {
    
        if (Input.GetKeyDown("f"))
        {

            if (isConditionPassed == false)
            {
     
                conditionPassedTime = DateTime.Now;
                narratingText = "Good job! You throwed the Axe!";
            }
            return true;
        }
        return isConditionPassed;

    }

    bool ConditionLevel7()
    {

        if (boss == null)
        {

            if (isConditionPassed == false)
            {
     
                conditionPassedTime = DateTime.Now;
                narratingText = "Awesome! Now you're fully prepared for the real challenge";
            }
            return true;
        }
        return isConditionPassed;

    }

}

public class Tutorial : MonoBehaviour
{

    public Text narratingText;
    int currentLevel = 1;
    public List<TutorialLevel> allLevels = new List<TutorialLevel>();

    //level 2 objects
    public CollectableItem pen;
    // level 3 objects
    public GameObject desk;
    public GameObject workZone;
    // level 4 objects
    public GameObject banana;
    public Boss boss;
    public Player player;
    // level 5 objects
    public GameObject book;
    // level 6 objects
    public GameObject Axe;
    // level 7 objects
    public GameObject ResignLetter;
    public GameObject ResignLetter2;
    public Boss boss2;


    // Start is called before the first frame update
    void Start()
    {
        CreateTutorialLevel(); 
    }

    // Update is called once per frame
    void Update()
    {
        foreach (TutorialLevel tutorial in allLevels)
        {
            if (currentLevel == tutorial.level)
            {
                if (tutorial.isFinished)
                {
                    if (currentLevel == 7)
                    {
                        GameManager.instance.GoToLevel(Scenes.Level1);
                    }
                    currentLevel++;
                    SetupLevelWhenStarting();
                }
                else
                {
                    GameObject bossObject = null;
                    GameObject playerObject = null;

                    if (boss != null)
                    {
                        bossObject = boss.gameObject;
                    }

                    if (currentLevel == 7)
                    {
                        if (boss2 != null)
                        {
                            bossObject = boss2.gameObject;
                        }
                    }
                    
                    if (player != null)
                    {
                        playerObject = player.gameObject;
                    }
                    tutorial.SetStartingValue(bossObject, playerObject);
                    narratingText.text = tutorial.narratingText;
                    tutorial.CheckCondition();
                    break;
                }
            }
            
        }
    }

    void CreateTutorialLevel()
    {
        TutorialLevel tutorial1 = CreateTutorialLevel1();
        TutorialLevel tutorial2 = CreateTutorialLevel2();
        TutorialLevel tutorial3 = CreateTutorialLevel3();
        TutorialLevel tutorial4 = CreateTutorialLevel4();
        TutorialLevel tutorial5 = CreateTutorialLevel5();
        TutorialLevel tutorial6 = CreateTutorialLevel6();
        TutorialLevel tutorial7 = CreateTutorialLevel7();
        
        allLevels.Add(tutorial1);
        allLevels.Add(tutorial2);
        allLevels.Add(tutorial3);
        allLevels.Add(tutorial4);
        allLevels.Add(tutorial5);
        allLevels.Add(tutorial6);
        allLevels.Add(tutorial7);
    }


    TutorialLevel CreateTutorialLevel1()
    {
        string narratingText = "Use A, W, S, D to move around the map";
        TutorialLevel tutorial = new TutorialLevel(1);
        tutorial.narratingText = narratingText;
        return tutorial;
    }

    TutorialLevel CreateTutorialLevel2()
    {
        string narratingText = "Now pick up the pen to reduce your work load!";
        TutorialLevel tutorial = new TutorialLevel(2);
        tutorial.narratingText = narratingText;
        return tutorial;
    }

    TutorialLevel CreateTutorialLevel3()
    {
        string narratingText = "Great! You can also move to a working zone to reduce your workload. Now \nmove to the green area. Remember the workzone has duration and cooldown";
        TutorialLevel tutorial = new TutorialLevel(3);
        tutorial.narratingText = narratingText;
        return tutorial;
    }

    TutorialLevel CreateTutorialLevel4()
    {
        string narratingText = "Now pick up the banana and press 'B' to put it on the floor\n Then lure your boss into the bana peel";
        TutorialLevel tutorial = new TutorialLevel(4);
        tutorial.narratingText = narratingText;
        return tutorial;
    }

    TutorialLevel CreateTutorialLevel5()
    {
        string narratingText = "Now pick up the book and press 'SPACE' to throw at the boss";
        TutorialLevel tutorial = new TutorialLevel(5);
        tutorial.narratingText = narratingText;
        return tutorial;
    }

    TutorialLevel CreateTutorialLevel6()
    {
        string narratingText = "Now pick up the Great Axe and press 'F' to attack the boss. \nUse your mouse to aim";
        TutorialLevel tutorial = new TutorialLevel(6);
        tutorial.narratingText = narratingText;
        return tutorial;
    }

    TutorialLevel CreateTutorialLevel7()
    {
        string narratingText = "It's time to kick him out of the company\nLure the BOSS to Resignation Letter";
        TutorialLevel tutorial = new TutorialLevel(7);
        tutorial.narratingText = narratingText;
        return tutorial;
    }

    public void SetupLevelWhenStarting()
    {
        if(currentLevel == 2)
        {
            pen.gameObject.SetActive(true);
        } else if (currentLevel == 3)
        {
            desk.gameObject.SetActive(true);
            workZone.gameObject.SetActive(true);
        }else if (currentLevel == 4)
        {
            desk.gameObject.SetActive(false);
            workZone.gameObject.SetActive(false);
            banana.gameObject.SetActive(true);
            boss.gameObject.SetActive(true);
        }else if (currentLevel == 5)
        {
            book.gameObject.SetActive(true);
        }else if (currentLevel == 6)
        {
            Axe.gameObject.SetActive(true);
        }
        else if (currentLevel == 7)
        {
            boss2.gameObject.SetActive(true);
            ResignLetter.gameObject.SetActive(true);
            ResignLetter2.gameObject.SetActive(true);
        }
    }
}
