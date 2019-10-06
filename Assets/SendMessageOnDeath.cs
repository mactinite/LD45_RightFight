using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SendMessageOnDeath : MonoBehaviour
{
    public UnityEvent onDestroy;
    private void OnDestroy() {
        onDestroy.Invoke();
    }
}
