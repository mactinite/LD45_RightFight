using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private int currentMultiplier;
    private int currentScore = 0;

    private HUDController hudController;

    public int CurrentMultiplier { get => currentMultiplier; set => currentMultiplier = value; }

    public static ScoreManager _instance = null;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(this);
        }
    }

    void Start() {
        hudController = HUDController._instance as HUDController;
        hudController.Score = currentScore;
    }
    public void AddScore(int basePoints) {
        currentScore += basePoints * CurrentMultiplier;
        hudController.Score = currentScore;
    }
}
