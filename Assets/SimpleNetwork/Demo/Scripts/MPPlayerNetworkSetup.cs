using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace SimpleNetwork
{ 
    public class MPPlayerNetworkSetup : NetworkBehaviour
    {

        [SerializeField] Camera PlayerCam;
        [SerializeField] AudioListener PlayerAudio;

        // Use this for initialization
        void Start()
        {
            if (isLocalPlayer)
            {
                GameObject.Find("Scene Camera").SetActive(false);
                GetComponent<CharacterController>().enabled = true;
                //GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
                PlayerCam.enabled = true;
                PlayerAudio.enabled = true;

            }
        }


    }

    }

