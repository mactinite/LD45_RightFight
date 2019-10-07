using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

    private float comboTimer;
    private float comboBufferTime;
    private int comboCount = 0;

    private int playerHealth;

    private int score = 0;
    private WeaponAsset onGround;

    private string currentWeaponUses;

    private Sprite currentWeaponSprite;

    public static HUDController _instance = null;

    public Image swapImage;
    public Image swapPanel;
    void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(this);
        }
    }

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

    public WeaponAsset OnGround {
        get => onGround;
        set {
            onGround = value;
            if (value) {
                swapPanel.gameObject.SetActive(true);
                swapImage.sprite = value.sprite;
            } else {
                swapPanel.gameObject.SetActive(false);
            }
        }
    }

    public Text comboCountUIText;
    public Image comboTimerImage;
    public ProgressBar healthBar;
    public Text scoreText;
    public Image equipmentImage;
    public Text equipmentUsesText;
}
