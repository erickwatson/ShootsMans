using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SimpleNetwork
{
    public class MPFirstPersonController : MonoBehaviour
    {

        private CharacterController controller;
        public float speed = 5.0f;
        public float turnSpeed;
        public float walkSpeed;
        public Vector3 jumpSpeed;
        public float jumpStrength; // could Vec3 this to get directional leaps
        public float mass = 3f;

        public float bindMin = 3f;
        public float bindMax = 10f;
        public float bindScale = 0.5f;

        private Vector3 isGrounded;


        
        private Camera playerCam;

        // FirstPerson variables
        private float rotX;
        private float rotY;
        public float MouseSensitivity;
        private float verticalUpMax = -90;
        private float verticalDownMax = 90;
        public Transform camLook;


        public float MoveSpeed;

        MPPlayerSyncPos syncPos;


        void Awake()
        {

        }

        // Use this for initialization
        void Start()
        {
            playerCam = gameObject.GetComponentInChildren<Camera>();
            controller = gameObject.GetComponent<CharacterController>();
            syncPos = GetComponent<MPPlayerSyncPos>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            if(syncPos && syncPos.isLocalPlayer)
                KeyboardInput();
        }


        bool PlatformPlayerShouldFire()
        {
            return Input.GetMouseButtonDown(0);

        }

        public void KeyboardInput()
        {

            CursorLogic();

            if (controller.isGrounded)
                jumpSpeed.Set(0, 0, 0);

            Vector3 direction = new Vector3(0, 0, 0);

            rotX += (Input.GetAxis("Mouse X")) * MouseSensitivity;
            rotY -= (Input.GetAxis("Mouse Y")) * MouseSensitivity;

            rotY = Mathf.Clamp(rotY, verticalUpMax, verticalDownMax);
            transform.localRotation = Quaternion.Euler(0, rotX, 0);
            camLook.localRotation = Quaternion.Euler(rotY, 0, 0);


            float h_input = Input.GetAxis("Horizontal");
            float v_input = Input.GetAxis("Vertical");
            direction = new Vector3(h_input, 0, v_input);

            direction = transform.TransformDirection(direction);


            direction *= speed;

            // Phsyics for jumping
            // change this
            jumpSpeed += Physics.gravity * mass * Time.deltaTime;


            // Jump
            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(ControllerMapping.PS4_X_BUTTON)) && controller.isGrounded)
            {
                //Jump
                jumpSpeed += new Vector3(0, jumpStrength, 0);
            }

            // fall speed calculation
            direction += jumpSpeed;


            direction *= Time.deltaTime;
            controller.Move(direction);
        }



        public void CursorLogic()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }



    }

}


