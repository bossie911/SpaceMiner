using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : MonoBehaviour
{
    public GameObject rotatePoint;
    public ParticleSystem particle;

    float rotationSpeed = 10f;
    float miningLaserRange = 10f;

    public float laserDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            rotatePoint.transform.Rotate(0, 0, rotationSpeed);
            particle.Emit(1);

            //Shoots a raycast and damages the mineral hit
            //TODO: make it use a layermask for better performance
            RaycastHit hit;
            if (Physics.Raycast(particle.transform.position, transform.TransformDirection(Vector3.forward), out hit, miningLaserRange))
            {
                if (hit.collider.gameObject.tag == "Ore")
                {                    
                    Ore ore = hit.collider.gameObject.GetComponent<Ore>();
                    ore.health -= laserDamage;
                    Debug.Log(ore.health);

                    //Destroys ore if healh is 0
                    if (ore.health <= 0)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
