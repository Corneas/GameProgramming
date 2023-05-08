using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int healthAmountMax;
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    public event EventHandler OnHealed;
    public event EventHandler OnHealthAmountMaxChanged;

    private float healthAmount;
    private float healSpeed = 2f;

    private WaitForEndOfFrame waitForEndOfFrame;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    private void Start()
    {

    }

    public void Damage(int damageAmount)
    {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead()
    {
        return healthAmount == 0;
    }

    public float GetHealthAmount()
    {
        return healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public bool IsFullHealth()
    {
        return healthAmount == healthAmountMax;
    }

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;

        if (updateHealthAmount)
        {
            healthAmount = healthAmountMax;
        }

        OnHealthAmountMaxChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        healthAmount = healthAmountMax;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }

    public void RegenerateHealth()
    {
        StartCoroutine(IERegenerateHp());
    }

    private IEnumerator IERegenerateHp()
    {
        while (true)
        {
            if (healthAmount < healthAmountMax)
            {
                healthAmount += Time.deltaTime * healSpeed;
                healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
                OnHealed?.Invoke(this, EventArgs.Empty);
            }

            yield return waitForEndOfFrame;
        }
    }
}
