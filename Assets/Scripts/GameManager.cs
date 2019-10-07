using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public AnimationCurve rate;
    public int minRate = 1;
    public int maxRate = 5;
    public AnimationCurve interval;
    public int maxInterval = 5;
    public AnimationCurve total;
    public int maxTotal = 50;
    public int minTotal = 5;
    public EnemySpawner spawner;

    private CameraController cameraController;

    [Range(0, 1)]
    public float currentDifficulty = 0;
    public float difficultyGrowth = 0.05f;

    private bool roundEnded;
    private void Start() {
        cameraController = GetComponent<CameraController>();
        spawner.spawnRate = Mathf.RoundToInt(minRate + (maxRate * rate.Evaluate(currentDifficulty / 1)));
        spawner.spawnInterval = Mathf.RoundToInt(maxInterval * interval.Evaluate(currentDifficulty / 1));
        spawner.enemiesInRound = Mathf.RoundToInt(minTotal + maxTotal * total.Evaluate(currentDifficulty / 1));
        spawner.enabled = true;
        spawner.onRoundEnd.AddListener(SetUpNextRound);
    }

    private void Update() {
        if (roundEnded) {
            LoadNextRound();
            roundEnded = false;
        }
    }

    public void SetUpNextRound() {
        spawner.enabled = false;
        roundEnded = true;
        currentDifficulty += difficultyGrowth;
        currentDifficulty = Mathf.Clamp(currentDifficulty, 0, 1);
        spawner.spawnRate = Mathf.RoundToInt(minRate + (maxRate * rate.Evaluate(currentDifficulty / 1)));
        spawner.spawnInterval = Mathf.RoundToInt(maxInterval * interval.Evaluate(currentDifficulty / 1));
        spawner.enemiesInRound = Mathf.RoundToInt(minTotal + maxTotal * total.Evaluate(currentDifficulty / 1));
    }

    public void LoadNextRound() {
        if (Mathf.Abs(transform.position.x - cameraController.maxX) <= 1) {
            spawner.enabled = true;
            cameraController.maxX += 20;
        }
    }

}
