using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{

    [SerializeField] private float healthValue;
    public AudioSource healingSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            healingSound.Play();
            other.GetComponent<Health>().AddHealth(healthValue);
            Destroy(gameObject);
        }
    }
}