using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace SimpleNetwork
{
    public class MPPlayerHealth : NetworkBehaviour
    {

        [SyncVar(hook = "OnHealthChanged")] private int health = 200;
        [SerializeField] private int healthStart = 200;
        private Text healthText;
        private bool shouldDie = false;
        public bool isDead = false;

        public delegate void DieDelegate();
        public event DieDelegate EventDie;

        public delegate void RespawnDelegate();
        public event RespawnDelegate EventRespawn;


        // Use this for initialization
        void Start()
        {
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            SetHealthText();
        }

        // Update is called once per frame
        void Update()
        {
            CheckCondition();
        }

        void CheckCondition()
        {
            if (health <= 0 && !shouldDie && !isDead)
            {
                shouldDie = true;
            }

            if (health <= 0 && shouldDie)
            {
                if (EventDie != null)
                {
                    EventDie();
                }

                shouldDie = false;
            }

            if (health > 0 && isDead)
            {
                if (EventRespawn != null)
                {
                    EventRespawn();
                }
                isDead = false;
            }

        }

        void SetHealthText()
        {
            if (isLocalPlayer)
            {
                healthText.text = health.ToString();
            }
        }

        public void DeductHealth(int dmg)
        {
            health -= dmg;
        }

        void OnHealthChanged(int hlth)
        {
            health = hlth;
            SetHealthText();
        }

        public void ResetHealth()
        {
            health = healthStart;
        }

    }
}
