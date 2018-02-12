using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Transform[] spawnPoints;

    private bool onX = true;
    private BallController ballController;
    private float speed;
    private bool isPlaying = false;

    // Use this for initialization
    void Start () {
        ballController = ball.GetComponent<BallController>();
        SpawnPlatforms();
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel") && isPlaying) {
            Debug.Log("pause");
            PauseGame();
        }
        HUD.GetComponentInChildren<Text>().text = ballController.GetScore().ToString();
	}

    public void SpawnPlatforms()
    {
        int[] x = {2, -2};
        float pos = 0;
        GameObject instance = Instantiate(platform, new Vector3(2, pos, 0), Quaternion.identity) as GameObject;
        instance.transform.parent = GameObject.Find("Platforms").transform;
        pos += 2;
        onX = false;
        for(int i = 0; i < 100; i++)
        {            
            if(onX) 
            {
                instance = Instantiate(platform, new Vector3(x[Random.Range(0, x.Length)], pos, 0), Quaternion.identity) as GameObject;
                instance.transform.parent = GameObject.Find("Platforms").transform;
                pos += 2;
                onX = false;
            }
            else 
            {
                instance = Instantiate(platform, new Vector3(0, pos, x[Random.Range(0, x.Length)]), Quaternion.identity) as GameObject;
                instance.transform.parent = GameObject.Find("Platforms").transform;
                pos += 2;
                onX = true;
            }
        }
    }

    public void StartGame() {
        mainMenu.SetActive(false);
        Time.timeScale = 1;
        isPlaying = true;
        HUD.SetActive(true);
    }
    
    public void PauseGame() {
        if(!pauseMenu.activeSelf) {
            pauseMenu.SetActive(true);
            HUD.SetActive(false);
            speed = Time.timeScale;
            Time.timeScale = 0;
        } else {
            pauseMenu.SetActive(false);
            HUD.SetActive(true);
            Time.timeScale = speed;
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver() {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        HUD.SetActive(false);
        gameOver.GetComponentInChildren<Text>().text = "SCORE: " + ballController.GetScore();
    }

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
