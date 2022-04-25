using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class EnemyStats : CharacterStats
    {



        Animator anim;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            anim.Play("Damage_1");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                anim.Play("Death_1");
            }
        }
    }
}
