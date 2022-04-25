using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class NemesisStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10;
        public int maxStamina;
        public int currentStamina;

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
            anim.SetBool("checkInteracting", true);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                anim.Play("Death_1");
                anim.SetBool("checkInteracting", true);
            }
        }
    }
}
