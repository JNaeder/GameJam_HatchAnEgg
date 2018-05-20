using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject mainScreen, winScreen, loseScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
