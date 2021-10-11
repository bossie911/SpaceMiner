using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShield : MonoBehaviour
{
    GameObject cam;

    public bool playerRotate = false;

    private Quaternion desiredRot;
    private float playerRotSpeed = 0.5f;

    private GameObject player;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {

        //Use this for later on making rotation smoother
        /*
        if(playerRotate == true)
        {
            desiredRot = Quaternion.Euler(new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y, 0));
            player.transform.rotation = Quaternion.Lerp(player.transform.localRotation, desiredRot, Time.deltaTime);               
        }
        */
    }

    void OnTriggerEnter(Collider other)
    {
        if  (other.tag == "Player")
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.isInSpace = !playerMovement.isInSpace;


            //Change de parent of the player
            if (playerMovement.isInSpace == false)
            {
                other.transform.SetParent(GameObject.FindGameObjectWithTag("Ship").transform, false);
                other.transform.localRotation = Quaternion.Euler(0, other.transform.rotation.y - 180, 0);

                //TODO make rotation smooth
                //Sets bool on true which starts the player rot lerp
                //playerRotate = true;
            }
            else
            {               
                other.transform.SetParent(null);

                playerRotate = false;

                //TODO make rotation smooth
                //Resets the camAngles
                Quaternion targetRot = Quaternion.identity;
                targetRot = Quaternion.Euler(0, 0, 0);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, targetRot, 1f * Time.deltaTime);
            }
        }
    }
}
