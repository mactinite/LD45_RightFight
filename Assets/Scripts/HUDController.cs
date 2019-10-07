using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : SingletonBehavior {

    private float comboTimer;
    private float comboBufferTime;
    private int comboCount = 0;

    private int playerHealth;

    public float ComboTimer { get => comboTimer; set => comboTimer = value; }
    public int ComboCount { get => comboCount; set => comboCount = value; }
    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public float ComboBufferTime { get => comboBufferTime; set => comboBufferTime = value; }

    public Text comboCountUIText; 
    public Image comboTimerImage;
    void Update() {
        comboCountUIText.text = ComboCount.ToString() + "x";
        comboTimerImage.fillAmount = 1f - ComboTimer / ComboBufferTime;
    }
}
