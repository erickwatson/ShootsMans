using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


namespace SimpleNetwork
{
    public class MPPlayerSyncPos : NetworkBehaviour
    {
        
        // These variables are forced to be synced between the client and the server
        [SyncVar] private Vector3 syncPos;
        [SyncVar] private Quaternion syncRotate;

        // This is for the Unity Inspector, drop in your player and adjust the amount of lerping
        [SerializeField] Transform myTransform;
        [SerializeField] float lerpRate = 15;
        private float normalLerpRate = 16;

        // These variables help us keep track of the player clientside, it helps the server ensure
        // we are where we are and that we're sending only the information the server needs
        private Vector3 lastPos;
        private Quaternion lastRotate;
        private float posThreshold = 0.1f;
        private float rotateThreshold = 0.2f;

        private NetworkClient nClient;
        public int Latency {get; private set;}


        void Start()
        {            
            nClient = NetworkManager.singleton.client;
            lerpRate = normalLerpRate;
        }

        // Update is called once per frame
        private void Update()
        {
            
            ShowLatency();
        }

  
        void FixedUpdate()
        {
            LerpPosition();
            TransmitPosition();
        }

        // Here we interpolate the player's movement (position and roatation)
        void LerpPosition()
        {
            // We check if it's not the locaPlayer
            if (!isLocalPlayer)
            {
                // And apply tweening to their movements
                myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
                myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncRotate, Time.deltaTime * lerpRate);
                
            }
        }

        [Command]
        void CmdProvidePositionToServer(Vector3 pos, Quaternion rot)
        {
            syncPos = pos;
            syncRotate = rot;
        }


        [ClientCallback]
        void TransmitPosition()
        {
            if (isLocalPlayer && (Vector3.Distance(myTransform.position, lastPos) > posThreshold || Quaternion.Angle(myTransform.rotation, lastRotate) > rotateThreshold))
            {
                CmdProvidePositionToServer(myTransform.position, myTransform.rotation);
                lastPos = myTransform.position;
                lastRotate = myTransform.rotation;
                
            }


        }

        void ShowLatency()
        {
            if (isLocalPlayer)
            {
                Latency = nClient.GetRTT();
                
            }
        }

    }

}