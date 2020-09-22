using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ButtonType
{
    NewGame,
    Tutorial,
    Level1,
    Level2,
    Level3,
    Level4
}

public class PlayButton : MonoBehaviour
{
    public ButtonType type;
    public StartScene scene;

    private void OnMouseDown()
    {
        Debug.Log("CLICKED!!!");
        switch (type)
        {
            case ButtonType.NewGame:
                scene.ShowLevelButtons();
                scene.HideStartButtons();
                break;
            case ButtonType.Tutorial:
                SceneManager.LoadScene(1);
                break;
            case ButtonType.Level1:
                SceneManager.LoadScene(2);
                break;
            case ButtonType.Level2:
                SceneManager.LoadScene(3);
                break;
            case ButtonType.Level3:
                SceneManager.LoadScene(4);
                break;
            case ButtonType.Level4:
                SceneManager.LoadScene(5);
                break;

        }
        
    }
}
