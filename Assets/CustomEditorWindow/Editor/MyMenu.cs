using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterRespawn : MonoBehaviour
{
    // Add a menu item named "Do Something" to MyMenu in the menu bar.
    Transform startSpawnTransform;
    Transform player;
    [MenuItem("MyMenu/CharacterRespawn/RespawnInSpring")]
    
    static void RespawnInSpring()
    {
        var spawner= GameObject.FindObjectOfType<SpawnController>();
        spawner.ResetInSpring();

    }

    [MenuItem("MyMenu/CharacterRespawn/RespawnInWinter")]
    static void RespawnInWinter()
    {
        var spawner = GameObject.FindObjectOfType<SpawnController>();
        spawner.ResetInWinter();

    }

    [MenuItem("MyMenu/CharacterRespawn/RespawnInSummer")]
    static void RespawnInSummer()
    {
        var spawner = GameObject.FindObjectOfType<SpawnController>();
        spawner.ResetInSummer();

    }

    [MenuItem("MyMenu/Collectables/DeactivateCollectables")]
    static void DeactivateCollectables()
    {
        
        var objectToDestroy = GameObject.FindGameObjectsWithTag("collectable");
        Debug.Log("Deactivating Collectables");
        foreach(var temp in objectToDestroy)
        {
            temp.GetComponent<Renderer>().enabled = false;
        }
    }

    [MenuItem("MyMenu/Collectables/ActivateCollectables")]
    static void ActivateCollectables()
    {
        var objectToDestroy = GameObject.FindGameObjectsWithTag("collectable");
        Debug.Log("Activating Collectables");
        foreach (var temp in objectToDestroy)
        {
            temp.GetComponent<Renderer>().enabled = true;
        }
    }
}
