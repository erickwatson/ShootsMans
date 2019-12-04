using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


namespace SimpleNetwork
{
    public class MPPlayerRespawn : NetworkBehaviour
    {

        private MPPlayerHealth healthScript;
        private Image crossHairImage;
        private GameObject respawnButton;

        public override void PreStartClient()
        {
            healthScript = GetComponent<MPPlayerHealth>();
            healthScript.EventRespawn += EnablePlayer;
        }

        public override void OnStartLocalPlayer()
        {
            crossHairImage = GameObject.Find("Crosshair Image").GetComponent<Image>();
            SetRespawnButton();
        }

        void SetRespawnButton()
        {
            if (isLocalPlayer)
            {
                respawnButton = GameObject.Find("GameManager").GetComponent<MPGameManagerRefs>().respawnButton;
                respawnButton.GetComponent<Button>().onClick.AddListener(CommenceRespawn);
                respawnButton.SetActive(false);
            }
        }

        public override void OnNetworkDestroy()
        {
            healthScript.EventRespawn -= EnablePlayer;
        }

        void EnablePlayer()
        {
            GetComponent<CharacterController>().enabled = true;
            GetComponent<MPPlayerShoot>().enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;


            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer ren in renderers)
            {
                ren.enabled = true;
            }

            if (isLocalPlayer)
            {
                GetComponent<MPFirstPersonController>().enabled = true;
                crossHairImage.enabled = true;
                respawnButton.SetActive(false);
            }
        }

        void CommenceRespawn()
        {
            CmdRespawnOnServer();
        }

        [Command]
        void CmdRespawnOnServer()
        {
            healthScript.ResetHealth();
        }

    }
}
