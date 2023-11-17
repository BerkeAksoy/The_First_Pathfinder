using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private Image shieldCooldown;

    
    // Update is called once per frame
    void Update()
    {
        healthSlider.value = healthManager.GetHealth();
        shieldSlider.value = healthManager.GetShield();

        if (healthManager.GetShieldIsCooldown())
            shieldCooldown.fillAmount = healthManager.GetShieldCooldownTimer() / 5f;
    }
}
