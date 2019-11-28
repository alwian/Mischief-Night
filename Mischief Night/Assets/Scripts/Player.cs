/*
 * Author: Colton Campbell (B00693513)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : DimensionedObject
{
    [Header("Required References")]
    [SerializeField] HealthBar healthGui;

    [Header("Options")]
    [SerializeField] float maxHealth = 100f;

    [SerializeField] float healthLossRate = 1.5f;
    [SerializeField] float healthRecoveryRate = 12f;

    [SerializeField] float healthRecoveryDelay = 5f;
    [SerializeField] float healthLossDelay = 15f;
    [SerializeField] float respawnDelay;


    PlayerController controller;

    float _health;
    float health
    {
        get { return _health; }
        set
        {
            if (value != _health)
            {
                _health = value;
                healthGui.UpdateDisplay(value/maxHealth);

                if (value <= 0f)
                    Kill();
            }
        }
    }


    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
        health = maxHealth;
    }

    bool isDead = false;
    public void Kill()
    {
        if (isDead)
            return;

        isDead = true;
        controller.enablePlayerControl = false;
        controller.DropCamera();
        StartCoroutine(DelayedReload());
    }

    IEnumerator DelayedReload()
    {
        CameraManager.Instance.DeathFadeOut(respawnDelay);
        yield return new WaitForSeconds(respawnDelay);
        GameManager.Instance.Reload();
    }

    IEnumerator dimensionRoutine;
    protected override void SetOverworld()
    {
        if (dimensionRoutine != null)
            StopCoroutine(dimensionRoutine);

        dimensionRoutine = GainHealth();
        StartCoroutine(dimensionRoutine);
    }

    protected override void SetUpsideDown()
    {
        if (dimensionRoutine != null)
            StopCoroutine(dimensionRoutine);

        dimensionRoutine = LoseHealth();
        StartCoroutine(dimensionRoutine);
    }

    IEnumerator GainHealth()
    {
        yield return new WaitForSeconds(healthRecoveryDelay);

        while (true)
        {
            health = Mathf.Clamp(health + (healthRecoveryRate * Time.deltaTime), 0f, maxHealth);
            yield return null;
        }
    }
    IEnumerator LoseHealth()
    {
        yield return new WaitForSeconds(healthLossDelay);

        while (true)
        {
            health = Mathf.Clamp(health - (healthLossRate * Time.deltaTime), 0f, maxHealth);
            yield return null;
        }
    }
}
