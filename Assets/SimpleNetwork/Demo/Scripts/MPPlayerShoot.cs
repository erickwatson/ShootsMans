using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace SimpleNetwork
{
    public class MPPlayerShoot : NetworkBehaviour
    {

        private int damage = 25;
        private float range = 200;
        [SerializeField] private Transform camTransform;
        private RaycastHit hit;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckIfShooting();

        }


        void CheckIfShooting()
        {
            if (!isLocalPlayer)
            {

                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
                
            }


        }

        void Shoot()
        {
            if (Physics.Raycast(camTransform.TransformPoint(0, 0, 0.5f), camTransform.forward, out hit, range))
            {
                Debug.DrawRay(camTransform.TransformPoint(0, 0, 0.5f), camTransform.forward, Color.red, 0.3f);
                Debug.Log(hit.transform.tag);

                if (hit.transform.tag == "Player")
                {
                    string uIdentity = hit.transform.name;
                    CmdTellServerWhoWasShot(uIdentity, damage);
                }

            }
        }

        [Command]
        void CmdTellServerWhoWasShot(string uniqueID, int dmg)
        {
            GameObject go = GameObject.Find(uniqueID);

            go.GetComponent<MPPlayerHealth>().DeductHealth(dmg);
            // Apply damage to player that's hit
        }


    }
}
