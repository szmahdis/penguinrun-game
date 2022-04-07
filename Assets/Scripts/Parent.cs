using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    public void BecomeParent(string type)
    {
        GameObject[] children = GameObject.FindGameObjectsWithTag(type);
        
        if (children != null)
        {
            foreach (var child in children)
            {
                child.gameObject.transform.SetParent(this.gameObject.transform);
            }
        }
        
    }
    
}
