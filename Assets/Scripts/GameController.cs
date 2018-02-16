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
    [SerializeField] private Text score;
    //[SerializeField] private GameObject cam;

    private bool onX = true;
    private BallController ballController;
    private float speed;
    private bool isPlaying = false;

    // Use this for initialization
    void Start () {
        ballController = ball.GetComponent<BallController>();
        SpawnInitialPlatforms();
        Time.timeScale = 0;
        // PlayerPrefs.SetInt("Score", 0);
        if (PlayerPrefs.GetInt("Score") > 0)
        {
            score.text = "HIGH SCORE: " + PlayerPrefs.GetInt("Score").ToString();
        }
        else {
            score.text = "HIGH SCORE: 0";
        }
        //StartCoroutine(ChangeBackground());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Cancel") && isPlaying) {
            Debug.Log("pause");
            PauseGame();
        }
        HUD.GetComponentInChildren<Text>().text = ballController.GetScore().ToString();
	}

    //IEnumerator ChangeBackground() {
    //    while(true) {
    //        yield return new WaitForSeconds(0.1f);
    //        if(cam.GetComponent<Camera>().backgroundColor.r == 1 && cam.GetComponent<Camera>().backgroundColor.b == 0 && cam.GetComponent<Camera>().backgroundColor.g < 1) {
    //            Color c = new Vector4(1, cam.GetComponent<Camera>().backgroundColor.g, 0, 0);
    //            c.g += 0.1f;
    //            cam.GetComponent<Camera>().backgroundColor = c;
    //        } else if(cam.GetComponent<Camera>().backgroundColor.b == 0 && cam.GetComponent<Camera>().backgroundColor.g == 1 && cam.GetComponent<Camera>().backgroundColor.r > 0) {
    //            Color c = new Vector4(cam.GetComponent<Camera>().backgroundColor.r, 1, 0, 0);
    //            c.r -= 0.1f;
    //            cam.GetComponent<Camera>().backgroundColor = c;
    //        } else if(cam.GetComponent<Camera>().backgroundColor.r == 0 && cam.GetComponent<Camera>().backgroundColor.g == 1 && cam.GetComponent<Camera>().backgroundColor.b < 1) {
    //            Color c = new Vector4(0, 1, cam.GetComponent<Camera>().backgroundColor.b, 0);
    //            c.b += 0.1f;
    //            cam.GetComponent<Camera>().backgroundColor = c;
    //        } else if(cam.GetComponent<Camera>().backgroundColor.r == 0 && cam.GetComponent<Camera>().backgroundColor.b == 1 && cam.GetComponent<Camera>().backgroundColor.g > 0) {
    //            Color c = new Vector4(0, cam.GetComponent<Camera>().backgroundColor.g, 1, 0);
    //            c.g -= 0.1f;
    //            cam.GetComponent<Camera>().backgroundColor = c;
    //        } else if(cam.GetComponent<Camera>().backgroundColor.g == 0 && cam.GetComponent<Camera>().backgroundColor.b == 1 && cam.GetComponent<Camera>().backgroundColor.r < 1) {
    //            Color c = new Vector4(cam.GetComponent<Camera>().backgroundColor.r, 0, 1, 0);
    //            c.r += 0.1f;
    //            cam.GetComponent<Camera>().backgroundColor = c;
    //        } else if(cam.GetComponent<Camera>().backgroundColor.r == 1 && cam.GetComponent<Camera>().backgroundColor.g == 0 && cam.GetComponent<Camera>().backgroundColor.b > 0) {
    //            Color c = new Vector4(1, 0, cam.GetComponent<Camera>().backgroundColor.b, 0);
    //            c.b -= 0.1f;
    //            cam.GetComponent<Camera>().backgroundColor = c;
    //        }
    //        Debug.Log(cam.GetComponent<Camera>().backgroundColor);
    //    }
    //}


    private void SpawnInitialPlatforms()
    {
        int[] x = {2, -2};
        float pos = 0;
        GameObject instance = Instantiate(platform, new Vector3(2, pos, 0), Quaternion.identity) as GameObject;
        instance.transform.parent = GameObject.Find("Platforms").transform;
        pos += 2;
        onX = false;
        for(int i = 0; i < 3; i++)
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

    public void SpawnPlatform(float speed) {
        GameObject instance = Instantiate(platform, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity) as GameObject;
        instance.transform.parent = GameObject.Find("Platforms").transform;
        instance.GetComponent<MoveDown>().speed = -speed;
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
        if (PlayerPrefs.GetInt("Score") < ballController.GetScore())
        {
            PlayerPrefs.SetInt("Score", ballController.GetScore());
        }
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
