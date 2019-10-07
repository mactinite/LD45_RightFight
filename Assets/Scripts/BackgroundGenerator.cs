using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {

    public Transform[] backgrounds;
    public Camera mainCamera;

    public int startingAmount;
    public float loadBufferRange = 25.0f;

    public Vector3 backgroundOffset;
    private Vector3 currentCameraPos;
    private Vector3 lastSpawnPosition;
    private Vector3 nextSpawnPosition = Vector3.zero;


    private void Start() {
        for (int i = 0; i < startingAmount; i++) {
            Transform bg = backgrounds[Random.Range(0, backgrounds.Length)];
            Instantiate(bg, nextSpawnPosition, Quaternion.identity,transform);
            lastSpawnPosition = nextSpawnPosition;
            nextSpawnPosition += backgroundOffset;
        }
    }
    private void LateUpdate() {
        currentCameraPos = mainCamera.transform.position;
        if (Mathf.Abs(currentCameraPos.x - nextSpawnPosition.x) < loadBufferRange) {
            Transform bg = backgrounds[Random.Range(0, backgrounds.Length)];
            Instantiate(bg, nextSpawnPosition, Quaternion.identity, transform);
            lastSpawnPosition = nextSpawnPosition;
            nextSpawnPosition += backgroundOffset;
        }
    }

}
