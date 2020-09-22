using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public Slider TimerBar;


    // Start is called before the first frame update
    void Start()
    {

        TimerBar.value = GameManager.instance.currentTime / GameManager.instance.gameDuration;

    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.currentTime -= 1 * Time.deltaTime;

        TimerBar.value = GameManager.instance.currentTime / GameManager.instance.gameDuration;

        if (GameManager.instance.currentTime <= 0)
        {
            GameManager.instance.currentTime = 0;
            Debug.Log("Time is over");
            GameManager.instance.GoToLevel(Scenes.GameOver);
        }
    }
}
