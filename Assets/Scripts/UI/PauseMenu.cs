using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject victoryMenu;

    [SerializeField]
    private GameObject interactable;

    [SerializeField]
    private TMP_Text obeliskCountUI;
    private float obeliskCount = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (obeliskCount >= 10)
            Victory();

    }

    public void GameOver()
    {
        Time.timeScale = 0f; // Stop time when the game is over
        gameOverMenu.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerShooting>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("Shoot Position").GetComponent<WeaponPivot>().enabled = false;
    }
    public void Victory()
    {
        Time.timeScale = 0f; // Stop time when the game is over
        victoryMenu.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerShooting>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("Shoot Position").GetComponent<WeaponPivot>().enabled = false;
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Reload the current scene
        Resume();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        Resume();
    }

    public void Pause()
    {
        Time.timeScale = 0f; // Stop time when the game is paused
        isPaused = true;
        pauseMenu.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerShooting>().enabled = false;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("Shoot Position").GetComponent<WeaponPivot>().enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Resume time when the game is unpaused
        isPaused = false;
        pauseMenu.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerShooting>().enabled = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.Find("Shoot Position").GetComponent<WeaponPivot>().enabled = true;
    }

    public void ShowInteractable(bool show)
    {
        interactable.SetActive(show);
    }

    public void SetObeliskCount()
    {
        obeliskCount++;
        obeliskCountUI.text = obeliskCount.ToString();
    }
}
