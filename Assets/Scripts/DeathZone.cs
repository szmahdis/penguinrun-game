using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = FindObjectOfType<Health>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Fall Damage");
            _health.TakeDamage(1);
        }
    }
}
