using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInfo : MonoBehaviour
{
    public bool hasPaused;
    public GameObject PauseMenuItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        print("quit the game");
        Application.Quit();
    }
    public void QuitToMenu()
    {
        print("quit to menu");
        SceneManager.LoadScene(0);
    }

    public void ResumeTime()
    {
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        hasPaused = !hasPaused;
        if(hasPaused == false)
        {
            Time.timeScale = 0.0f;
            PauseMenuItems.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            PauseMenuItems.SetActive(false);
        }

    }
}
