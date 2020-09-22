using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes:int
{
    Start = 0,
    Tutorial = 1,
    Level1 = 2,
    Level2 = 3,
    Level3 = 4,
    Level4 = 5,
    GameWin = 6,
    GameOver = 7
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public float workLoad = 50f;
    public float gameDuration = 60f;
    public float currentTime = 0f;
    public float maxTask = 100f;
    public BoardManager boardManager;
    public Scenes currentScene = Scenes.Start;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
        
        InitGame();
    }

    void InitGame()
    {
        
        ResetLevel();
    }

    public void LoadLevel(int x)
    {
        SceneManager.LoadScene(x);
    }

    public void LoadNextLevel()
    {
        currentScene++;
        ResetLevel();
        LoadLevel((int)(GameManager.instance.currentScene));
    }

    public void Replay()
    {
        currentScene = Scenes.Start;
        ResetLevel();
        LoadLevel((int)GameManager.instance.currentScene);
    }

    public void GoToLevel(Scenes scene)
    {
        currentScene = scene;
        ResetLevel();
        LoadLevel((int)GameManager.instance.currentScene);
    }

    void ResetLevel()
    {
        if (currentScene == Scenes.Level1)
        {
            
            maxTask = 100;
            workLoad = maxTask/2;
            gameDuration = 60;
        }
        else if (currentScene == Scenes.Level2)
        {
            maxTask = 400;
            workLoad = maxTask / 2;
            gameDuration = 80;
        }
        else if (currentScene == Scenes.Level3)
        {
            maxTask = 500;
            workLoad = 300;
            gameDuration = 100;
        }
        else if (currentScene == Scenes.Level4)
        {
            maxTask = 300;
            workLoad = maxTask / 2;
            gameDuration = 100;
        }
        else
        {
            maxTask = 400;
            workLoad = maxTask / 2;
            gameDuration = 500;
        }

        currentTime = gameDuration;
    }


    public void GameOver()
    {
        SceneManager.LoadScene("loseScene");
        //enabled = false;
    }

    public void GameWin()
    {
        GameManager.instance.GoToLevel(Scenes.GameOver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // HELPERS

    /* This method is used to change boss speed, bullet speed, fire speed depending on boss level*/
    public float ChangeValueBasedOnLevel(float baseValue)
    {
        //Debug.Log(baseValue);
        //Debug.Log(boardManager);
        //Debug.Log(boardManager.boss);
        //Debug.Log(boardManager.boss.level);

        return baseValue + (baseValue * boardManager.boss.level * 25 / 100);
    }
}
