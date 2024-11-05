using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    [SerializeField]
    private HealthController healthController;
    [SerializeField]
    private Slider healthSlider;


    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        if(healthController != null)
            healthController.HealthChanged += HealthController_OnHealthChanged;
    }
    private void OnDisable()
    {
        if (healthController != null)
            healthController.HealthChanged -= HealthController_OnHealthChanged;
    }

    public void HealthController_OnHealthChanged()
    {
        UpdateView();
    }
    public void UpdateView()
    {
        if (healthController == null)
            return;
        if (healthSlider != null && healthController.MaxHealth != 0)
        {
            healthSlider.value = (float)healthController.CurrentHealth / (float)
            healthController.MaxHealth;
        }
    }
}
