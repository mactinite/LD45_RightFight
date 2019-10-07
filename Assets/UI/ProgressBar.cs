using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    float currentLevel = 1;
    float velocity = 0.0f;
    [SerializeField] float dampingFactor = 0.25f;
    [SerializeField] float targetLevel = 1;
    [SerializeField] Image progressBar;

    private void Start()
    {
        if (!progressBar)
        {
            progressBar = GetComponentsInChildren<Image>()[1];
        }
    }

    private void Update()
    {
        this.currentLevel = Mathf.SmoothDamp(this.currentLevel, this.targetLevel, ref this.velocity,  Time.deltaTime * dampingFactor);
        progressBar.fillAmount = this.currentLevel;
        
    }

    public void SetLevel(float number)
    {
        this.targetLevel = number;
    }
}
