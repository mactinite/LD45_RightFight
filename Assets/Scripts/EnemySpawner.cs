using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomInput;
public class EnemySpawner : MonoBehaviour {
    public float startDelay = 5;
    private float startTimer;
    public WeaponAsset[] weapons;
    public Transform enemyPrefab;
    public Transform target;

    public Transform secondSpawnPos;

    public float randomRange = 3;

    private int enemiesAlive;
    private int enemiesKilled;
    private int enemiesSpawned;
    public int spawnRate = 2;

    public float spawnInterval = 5;

    public int minEnemies = 3;

    public int enemiesInRound = 10;

    private float spawnTimer;

    public bool roundStarted;

    public UnityEvent onRoundEnd;

    private HUDController hudController;

    private void Awake() {
        hudController = HUDController._instance;
    }
    private void OnEnable() {
        hudController = HUDController._instance;
        startTimer = 0;
        spawnTimer = 0;
        enemiesSpawned = 0;
        enemiesAlive = 0;
        enemiesKilled = 0;
        roundStarted = false;
        hudController.RoundStart = false;
        hudController.IndicatorTextString = "FIGHT";
    }
    // Update is called once per frame
    void Update() {
        spawnTimer += Time.deltaTime;
        startTimer += Time.deltaTime;
        if (startTimer > startDelay) {
            roundStarted = true;
            hudController.RoundStart = true;
        } else {
            hudController.WaveCountdown = Mathf.RoundToInt(startDelay - startTimer);
            hudController.RoundStart = false;
        }

        if (roundStarted && spawnTimer > spawnInterval && enemiesAlive < minEnemies && enemiesSpawned < enemiesInRound) {
            SpawnEnemies(Mathf.Min(enemiesInRound - enemiesSpawned, spawnRate));
            spawnTimer = 0;
        }
        if (enemiesInRound - enemiesKilled <= 0) {
            hudController.IndicatorTextString = "GO";
            hudController.RoundStart = false;
            onRoundEnd.Invoke();
        } else if (enemiesInRound - enemiesKilled <= 3) {
            hudController.IndicatorTextString = enemiesInRound - enemiesKilled + " LEFT";
        } else {
            hudController.IndicatorTextString = "FIGHT";
        }
    }


    public void SpawnEnemies(int count) {
        for (int i = 0; i < count; i++) {
            Transform spawnPos = Random.value >= 0.5f ? secondSpawnPos : transform;
            Vector3 randomRangeOffset = new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));
            Transform Enemy = Instantiate(enemyPrefab, spawnPos.position + randomRangeOffset, enemyPrefab.rotation);
            WeaponManager weaponManager = Enemy.gameObject.GetComponent<WeaponManager>();
            CPUInput cpu = Enemy.gameObject.GetComponent<CPUInput>();
            SendMessageOnDeath smod = Enemy.gameObject.GetComponent<SendMessageOnDeath>();
            int randomIndex = Random.Range(-1, weapons.Length);

            if (randomIndex < 0) {
                weaponManager.equippedWeapon = null;

            } else {
                weaponManager.equippedWeapon = weapons[randomIndex];
            }

            smod.onDestroy.AddListener(EnemyDead);
            cpu.target = target;
            enemiesAlive++;
            enemiesSpawned++;
        }
    }

    public void EnemyDead() {
        enemiesKilled++;
        enemiesAlive--;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, randomRange);
        Gizmos.DrawWireSphere(secondSpawnPos.position, randomRange);
    }
}
