using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInput;
public class EnemySpawner : MonoBehaviour {
    public float startDelay = 5;
    private float startTimer;
    public WeaponAsset[] weapons;
    public Transform enemyPrefab;
    public Transform spawnPosition;

    public Transform target;

    public float randomRange = 3;

    public int enemiesAlive;

    //Temporary
    public int spawnRate = 2;

    public float spawnInterval = 5;

    private float spawnTimer;
    /*-------*/

    private void OnEnable() {
        startTimer = 0;
        spawnTimer = 0;
        if(!spawnPosition){
            spawnPosition = this.transform;
        }
    }
    // Update is called once per frame
    void Update() {
        spawnTimer += Time.deltaTime;
        startTimer += Time.deltaTime;
        if (startTimer > startDelay && spawnTimer > spawnInterval && enemiesAlive <= 2) {
            for (int i = 0; i < spawnRate; i++) {
                Vector3 randomRangeOffset = new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));
                Transform Enemy = Instantiate(enemyPrefab, spawnPosition.position + randomRangeOffset, enemyPrefab.rotation);
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
            }
            
            spawnTimer = 0;
        }
    }


    public void EnemyDead(){
        enemiesAlive--;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(spawnPosition ? spawnPosition.transform.position : transform.position, randomRange);
    }
}
