using System;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedHPBar : MonoBehaviour
{
    public Image fillImage;
    private TextMeshProUGUI text;
    public Gradient healthGradient;
    
    [HideInInspector] public BankaObject bankaObject;
    private float currentHealth;
    private float hp;
    public event Action OpenBank;

    public void UpdateBanka()
    {
        hp = int.Parse(LocalizationManager.Localize(bankaObject.key, bankaObject.baseInfo.hpBanka));
        currentHealth = hp;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, hp);
        if (currentHealth == 0)
            OpenBank?.Invoke();
            
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if(text == null) text = fillImage.GetComponentInChildren<TextMeshProUGUI>();

        // Обновление заполнения и цвета
        text.text = $"{currentHealth}/{hp}";
        float healthPercentage = currentHealth / hp;
        fillImage.fillAmount = healthPercentage;
        fillImage.color = healthGradient.Evaluate(healthPercentage);
    }
}