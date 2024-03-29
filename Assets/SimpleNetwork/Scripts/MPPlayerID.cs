﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace SimpleNetwork
{
    public class MPPlayerID : NetworkBehaviour
    {

        [SyncVar] private string playerUniqueIdentity;
        private NetworkInstanceId playerNetID;
        private Transform myTransform;

        public override void OnStartLocalPlayer()
        {
            GetNetIdentity();
            SetIdentity();
        }

        private void Awake()
        {
            myTransform = transform;
        }

        private void Update()
        {
            if (myTransform.name == "" || myTransform.name == "Player(Clone)")
            {
                SetIdentity();
            }
        }

        [Client]
        void GetNetIdentity()
        {
            playerNetID = GetComponent<NetworkIdentity>().netId;
            CmdTellServerMyIdentity(MakeUniqueIdentity());
        }


        void SetIdentity()
        {
            if (!isLocalPlayer)
            {
                myTransform.name = playerUniqueIdentity;

            }
            else
            {
                myTransform.name = MakeUniqueIdentity();
            }
        }

        string MakeUniqueIdentity()
        {
            string uniqueName = "Player" + playerNetID.ToString();
            return uniqueName;
        }

        [Command]
        void CmdTellServerMyIdentity(string name)
        {
            playerUniqueIdentity = name;
        }



    }
}
