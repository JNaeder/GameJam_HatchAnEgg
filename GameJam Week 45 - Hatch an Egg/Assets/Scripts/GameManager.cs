using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject mainScreen, winScreen, loseScreen, pauseScreen;


    public static bool isPaused;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused) {
                pauseGame();
            }
            if (isPaused)
            {
                UnpauseGame();
            }
            isPaused = !isPaused;

        }

    }

    public void WinGame() {
        winScreen.SetActive(true);
    }


    public void LoseGame() {
        mainScreen.SetActive(false);
        loseScreen.SetActive(true);
    }


    public void StartGame() {
        SceneManager.LoadScene(1);

    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        print("Quit Game");
        Application.Quit();

    }


    void pauseGame() {
        print("Pause");
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void UnpauseGame() {
        print("UnPause");
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
