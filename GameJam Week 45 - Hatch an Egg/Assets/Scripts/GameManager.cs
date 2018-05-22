using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject mainScreen, winScreen, loseScreen, pauseScreen;

	public GameObject loseMainMenu, winMainMenu, pauseResume;


    public static bool isPaused;
	public EventSystem eS;

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
		eS.SetSelectedGameObject(winMainMenu);
    }


    public void LoseGame() {
        mainScreen.SetActive(false);
		loseScreen.SetActive(true);
		eS.SetSelectedGameObject(loseMainMenu);
    }


    public void StartGame() {
        SceneManager.LoadScene(1);

    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
		Time.timeScale = 1;
    }

    public void QuitGame() {
        print("Quit Game");
        Application.Quit();

    }


    void pauseGame() {
        print("Pause");
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
		eS.SetSelectedGameObject(pauseResume);
		
        
    }

    public void UnpauseGame() {
		isPaused = false;
        print("UnPause");
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
		eS.SetSelectedGameObject(null);
    }
}
