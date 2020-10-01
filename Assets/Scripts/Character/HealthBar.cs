using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // External Reference
    public CharacterStatus stats;
    public Image fill;
    public Slider slider;
    public Gradient gradient;

    void LateUpdate() {
        // Reflect the Health Status
        slider.maxValue = stats.maxHealth;
        slider.value = stats.health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
