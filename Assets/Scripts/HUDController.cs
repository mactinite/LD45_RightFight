using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : SingletonBehavior {

    private float comboTimer;
    private float comboBufferTime;
    private int comboCount = 0;

    private int playerHealth;

    private int score = 0;

    private string currentWeaponUses;

    private Sprite currentWeaponSprite;

    public float ComboTimer {
        get => comboTimer;
        set {
            comboTimerImage.fillAmount = 1f - ComboTimer / ComboBufferTime;
            comboTimer = value;
        }
    }
    public int ComboCount {
        get => comboCount;
        set {
            comboCount = value;
            comboCountUIText.text = value.ToString() + "x";
        }
    }
    public int PlayerHealth {
        get => playerHealth;
        set {
            playerHealth = value;
            healthBar.SetLevel(playerHealth / 100.0f);
        }
    }
    public float ComboBufferTime { get => comboBufferTime; set => comboBufferTime = value; }
    public int Score {
        get => score;
        set {
            score = value;
            scoreText.text = value.ToString();
        }
    }

    public string CurrentWeaponUses {
        get => currentWeaponUses;
        set {
            currentWeaponUses = value;
            equipmentUsesText.text = value;
        }
    }
    public Sprite CurrentWeaponSprite {
        get => currentWeaponSprite;
        set {
            currentWeaponSprite = value;
            equipmentImage.sprite = value;
        }
    }

    public Text comboCountUIText;
    public Image comboTimerImage;
    public ProgressBar healthBar;
    public Text scoreText;
    public Image equipmentImage;
    public Text equipmentUsesText;
}
