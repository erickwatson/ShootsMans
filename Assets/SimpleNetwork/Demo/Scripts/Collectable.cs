using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{



    public Transform player;

    [Header("Collection")]
    public float collectDistance;
    public float collectSpeed;

    [Header("Animation")]
    public float rotateSpeedX;
    public float rotateSpeedY;
    public float rotateSpeedZ;

    public float bobSpeed;
    public float bobRadius;

    private Vector3 startPosition;
    private Vector3 startScale;
    private float bobAngle = 0;

    private bool moveToPlayer = false;

    // Use this for initialization
    void Start()
    {

        startPosition = transform.position;
        startScale = transform.localScale;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {



        AudioSource pickupSound = GetComponent<AudioSource>();

        if (!moveToPlayer)
        {
            bobAngle += bobSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(rotateSpeedX * Time.deltaTime, rotateSpeedY * Time.deltaTime, rotateSpeedZ * Time.deltaTime));
            transform.position = new Vector3(startPosition.x, Mathf.Sin(bobAngle) * bobRadius + startPosition.y, startPosition.z);

            if (Vector3.Distance(startPosition, player.position) <= collectDistance)
            {
                bobAngle = 0.0f;
                moveToPlayer = true;
                startPosition = transform.position;
                //pickupSound.Play(0);
            }
        }
        else
        {
            if (bobAngle < Mathf.PI / 2)
            {
                bobAngle += collectSpeed * Time.deltaTime;
                Vector3 distance = new Vector3(player.transform.position.x - startPosition.x, player.transform.position.y - startPosition.y, player.transform.position.z - startPosition.z);
                transform.position = new Vector3(Mathf.Sin(bobAngle) * distance.x + startPosition.x, Mathf.Sin(bobAngle) * distance.y + startPosition.y, Mathf.Sin(bobAngle) * distance.z + startPosition.z);
                transform.localScale = startScale * (1 - (bobAngle / (Mathf.PI / 2)));
            }
            else
            {
                if (!pickupSound.isPlaying)
                    Destroy(gameObject);
                else
                    GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
