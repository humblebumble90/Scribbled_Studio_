﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HpBarController : MonoBehaviour
{
    /// <summary>
    /// File Name: HpBarController.cs
    /// Author: Philip Lee
    /// Last Modified by: Philip Lee
    /// Date Last Modified: Nov. 29, 2019
    /// Reference: Tom Tsiliopoulos
    /// Description: Hp Bar controller
    /// Revision History:
    /// </summary>
    /// 
    public float health;
    public float damage = 0.0f;
    public float damageStep = 1.0f;

    public Transform hpBarFront;
    public Transform hpBarDmg;

    public float hpBarLerp;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= Mathf.Epsilon)
        {
            StopCoroutine(TakeDamage());
            if (Mathf.Approximately(health, 0.0f))
            {
                health = 0.0f;
            }
        }

        hpBarLerp = 
            Mathf.Lerp(hpBarDmg.transform.localScale.x, hpBarFront.localScale.x, Time.deltaTime * 2);

        hpBarDmg.localScale = new Vector3(hpBarLerp, 100.0f, 100.0f);
    }

    public void SetDamage(float dmg)
    {
        if (health > 0.0f)
        {
            damage = dmg;
            StartCoroutine(TakeDamage());
        }

    }
    public void addHP(float hp)
    {
        health += hp;
        if (health > 100.0f)
        {
            health = 100.0f;
        }
        hpBarFront.localScale = new Vector3(health, 100.0f, 100.0f);
    }

    //Coroutine
    private IEnumerator TakeDamage()
    {
        for (; damage > 0.01f; damage -= damageStep)
        {
            health -= damageStep;
            if (health < 0.0f)
            {
                health = 0;
            }
            hpBarFront.localScale = new Vector3(health, 100.0f, 100.0f);
            yield return null;
        }

    }
}
