using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiningLaser : MonoBehaviour
{
    public GameObject rotatePoint;
    public ParticleSystem particle;

    float rotationSpeed = 10f;
    float miningLaserRange = 10f;
    float laserDamage = 0.1f;

    public Slider slider;
    public TextMeshProUGUI sliderHP;
    public TextMeshProUGUI target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //Animation for the tool and particle emmiting
            rotatePoint.transform.Rotate(0, 0, rotationSpeed);
            particle.Emit(1);

            //Shoots a raycast and damages the mineral hit
            //TODO: make it use a layermask for better performance
            RaycastHit hitOre;
            if (Physics.Raycast(particle.transform.position, transform.TransformDirection(Vector3.forward), out hitOre, miningLaserRange) && hitOre.collider.gameObject.tag == "Ore")
            {                  
                Ore ore = hitOre.collider.gameObject.GetComponent<Ore>();
                ore.health -= laserDamage;

                //Destroys ore if healh is 0
                if (ore.health <= 0)
                {
                    ParticleSystem system = hitOre.collider.gameObject.transform.Find("OreDestroyedParticle").GetComponent<ParticleSystem>();
                    system.Play();
                    system.transform.SetParent(null);
                    Destroy(hitOre.collider.gameObject);
                }                
            }
        }



        HpUI();
    }

    //Displays the HP of an Ore in the UI with a slider and textfield
    void HpUI()
    {
        //Checks if Raycast hits an Ore
        RaycastHit hitOre;
        if (Physics.Raycast(particle.transform.position, transform.TransformDirection(Vector3.forward), out hitOre, miningLaserRange) && hitOre.collider.gameObject.tag == "Ore")
        {
            //Set UI elements on
            slider.gameObject.SetActive(true);
            target.gameObject.SetActive(true);
            
            //Changing the UI elements
            Ore ore = hitOre.collider.gameObject.GetComponent<Ore>();
            slider.value = ore.health;
            sliderHP.text = Mathf.Round(ore.health) + "%";

            target.text = "Green Mineral";
        }
        else
        {
            //Turning UI elements off
            slider.gameObject.SetActive(false);
            target.gameObject.SetActive(false);
        }
    }
}
