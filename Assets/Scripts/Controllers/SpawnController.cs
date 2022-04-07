using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Transform[] spawnpositions;
    private int spawnID;

    private void Start()
    {
        spawnID = 5;
        Respawn(spawnID);
    }

    private void Respawn(int id)
    {
        Debug.Log(this.transform.position);
        this.transform.position = spawnpositions[id].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Deathzone")
        {
            Debug.Log("Deathzone Hit!");
            Respawn(spawnID);
        }
        if (other.tag == "CheckPoint")
        {
            if (int.Parse(other.name) != spawnID)
            {
                Debug.Log("Checkpoint!" + int.Parse(other.name));
                spawnID = int.Parse(other.name);
            }
        }
    }
    public void ResetInSpring()
    {
        Debug.Log(this.transform.position);
        this.transform.position = spawnpositions[0].position;
    }

    public void ResetInWinter()
    {
        Debug.Log(this.transform.position);
        this.transform.position = spawnpositions[2].position;
    }

    public void ResetInSummer()
    {
        Debug.Log(this.transform.position);
        this.transform.position = spawnpositions[4].position;
    }
}
