using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 10;
        CurrentHealth = 10;
        slider.maxValue = MaxHealth;
        slider.value = CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CurrentHealth;
    }

}