using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


namespace SimpleNetwork
{
    public class MPPlayerDeath : NetworkBehaviour
    {

        private MPPlayerHealth healthScript;
        private Image crossHairImage;


        // Use this for initialization
        void Start()
        {
            crossHairImage = GameObject.Find("Crosshair Image").GetComponent<Image>();
            healthScript = GetComponent<MPPlayerHealth>();
            healthScript.EventDie += DisablePlayer;

        }

        private void OnDisable()
        {
            healthScript.EventDie -= DisablePlayer;

        }


        void DisablePlayer()
        {
            GetComponent<CharacterController>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<MPPlayerShoot>().enabled = false;

            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer ren in renderers)
            {
                ren.enabled = false;
            }

            healthScript.isDead = true;

            if (isLocalPlayer)
            {
                GetComponent<MPFirstPersonController>().enabled = false;
                crossHairImage.enabled = false;
                GameObject.Find("GameManager").GetComponent<MPGameManagerRefs>().respawnButton.SetActive(true);
                if (crossHairImage.enabled == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }

        }

    }
}
