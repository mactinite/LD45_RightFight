using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    GameOver _instance;
    ShowPanels showPanels;

    public Text scoreText;

    private int score = 0;
    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(this);
        }
        showPanels = GetComponent<ShowPanels>();
    }

    public void TriggerGameOver(){
        score = ScoreManager._instance.CurrentScore;
        scoreText.text = score.ToString();
        showPanels.ShowGameOverPanel();
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        showPanels.HideGameOverPanel();
        Destroy(this.gameObject);
    }
}
