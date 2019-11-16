using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeBtn;
    public GameObject restartBtn;
    public GameObject levelClearTxt;

    private Scene currActiveScene;
    private Text titleInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        currActiveScene = SceneManager.GetActiveScene();
        pausePanel.SetActive(false);
        resumeBtn.SetActive(false);
        restartBtn.SetActive(false);
        levelClearTxt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        titleInfo = levelClearTxt.GetComponent<Text>();
        titleInfo.text = "Pause";
        levelClearTxt.SetActive(true);
        resumeBtn.SetActive(true);
    }
    
    public void resumeGame()
    {
        Time.timeScale = 1;
        levelClearTxt.SetActive(false);
        pausePanel.SetActive(false);
        resumeBtn.SetActive(false);
    }
    
    public void restartGame()
    {  
        Time.timeScale = 1;
        SceneManager.LoadScene(currActiveScene.name);
    }
    
    public void endGame()
    {
        resumeBtn.SetActive(false);

        pausePanel.SetActive(true);
        titleInfo = levelClearTxt.GetComponent<Text>();
        titleInfo.text = "Level Clear!";
        levelClearTxt.SetActive(true);
        restartBtn.SetActive(true);
    }
}
