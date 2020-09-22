using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public GameObject startButton;
    public GameObject tutorialButton;
    public GameObject level1Button;
    public GameObject level2Button;
    public GameObject level3Button;
    public GameObject level4Button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideLevelButtons()
    {
        level1Button.gameObject.SetActive(false);
        level2Button.gameObject.SetActive(false);
        level3Button.gameObject.SetActive(false);
        level4Button.gameObject.SetActive(false);
    }

    public void ShowLevelButtons()
    {
        level1Button.gameObject.SetActive(true);
        level2Button.gameObject.SetActive(true);
        level3Button.gameObject.SetActive(true);
        level4Button.gameObject.SetActive(true);
    }

    public void HideStartButtons()
    {
        startButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
    }

    public void ShowStartButtons()
    {
        startButton.gameObject.SetActive(true);
        tutorialButton.gameObject.SetActive(false);
    }
}
