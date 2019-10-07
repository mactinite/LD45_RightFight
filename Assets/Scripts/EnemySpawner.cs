using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;
public class EnemySpawner : MonoBehaviour {
    public float startDelay = 5;
    private float startTimer;
    public WeaponAsset[] weapons;
    public Transform enemyPrefab;
    public Transform target;

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

    private void OnEnable() {
        startTimer = 0;
        spawnTimer = 0;
        enemiesSpawned=0;
        enemiesAlive=0;
        enemiesKilled=0;
        roundStarted = false;
        HUDController._instance.RoundStart = false;
        HUDController._instance.IndicatorTextString = "FIGHT";
    }
    // Update is called once per frame
    void Update() {
        spawnTimer += Time.deltaTime;
        startTimer += Time.deltaTime;
        if(startTimer > startDelay){
            roundStarted = true;
            HUDController._instance.RoundStart = true;
        } else {
            HUDController._instance.WaveCountdown = Mathf.RoundToInt(startDelay - startTimer);
            HUDController._instance.RoundStart = false;
        }

        if (roundStarted && spawnTimer > spawnInterval && enemiesAlive < minEnemies && enemiesSpawned < enemiesInRound) {
            for (int i = 0; i < spawnRate; i++) {
                Vector3 randomRangeOffset = new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));
                Transform Enemy = Instantiate(enemyPrefab, transform.position + randomRangeOffset, enemyPrefab.rotation);
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
            
            spawnTimer = 0;
        }
        if(enemiesInRound - enemiesKilled  <= 0){
            HUDController._instance.IndicatorTextString = "GO";
            HUDController._instance.RoundStart = false;
        } else if(enemiesInRound - enemiesKilled <= 3){
            HUDController._instance.IndicatorTextString = enemiesInRound - enemiesKilled + " LEFT";
        } else {
            HUDController._instance.IndicatorTextString = "FIGHT";
        }
    }


    public void EnemyDead(){
        enemiesKilled++;
        enemiesAlive--;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, randomRange);
    }
}
