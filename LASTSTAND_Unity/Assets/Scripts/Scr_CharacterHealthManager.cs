using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CharacterHealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealthPoint = 3;

    public int currentLife;
    public static Scr_CharacterHealthManager instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        currentLife = maxHealthPoint;
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            Debug.Log("DEATH");
        }
    }

}
