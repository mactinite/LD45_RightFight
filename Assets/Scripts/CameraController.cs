using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxX = 10;
    public float minX = -10;

    public float speed = 20;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate() {
        Vector3 lastPos = transform.position;
        Vector3 nextPos = transform.position;
        nextPos.x = Mathf.Clamp(target.position.x, minX, maxX);

        transform.position = Vector3.Lerp(lastPos, nextPos, Time.deltaTime * speed);
    }
}
