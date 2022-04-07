using UnityEditor;
using UnityEngine;

public class ObjectSpawner : EditorWindow
{

    Transform objectContainer;
    string objectName = "";
    int objectID = 1;
    GameObject objectToSpawn;
    float objectScale;
    float spawnRadius = 5f;

    float yoffset = 1f;

    [MenuItem("Tools/Object Spawner")] // shows up in menu bar in 

    public static void showWindow()
    {
        GetWindow(typeof(ObjectSpawner)); // GetWindow is inherited from EditorWindow class
    }

    // this is where different fields will be setted
    private void OnGUI()
    {
        GUILayout.Label("Spawn New Object", EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Object Name", objectName);
        objectID = EditorGUILayout.IntField("Object ID", objectID);
        objectContainer = EditorGUILayout.ObjectField("Object Container", objectContainer, typeof(Transform), true) as Transform;
        objectToSpawn = EditorGUILayout.ObjectField("Prefab to Spawn", objectToSpawn, typeof(GameObject), false) as GameObject;
        //objectScale = EditorGUILayout.Slider("Object Scale", objectScale, 0.5f, 3f);
        spawnRadius = EditorGUILayout.FloatField("Spawn Radius", spawnRadius);
        yoffset = EditorGUILayout.FloatField("Y offset", yoffset);
        if (GUILayout.Button("Spawn Object"))
        {
            SpawnObject();
        }
        if(GUILayout.Button("Reset ID"))
        {
            ResetID();
        }
    }

    private void ResetID()
    {
        objectID = 1;
    }

    //instantiates a object with the parameters that we have
    private void SpawnObject()
    {

        if (objectToSpawn == null)
        {
            Debug.LogError("Error: Please assign an object to be spawned.");
            return;
        }

        if (objectName == string.Empty)
        {
            Debug.LogError("Error: Please enter a name for the object");
            return;
        }
        var ContainerSize = objectContainer.GetComponent<Collider>().bounds.size;

        Vector3 ContainerCenter = objectContainer.position;

        //Calculating Spawn position
        var Yposition = objectToSpawn.GetComponent<Renderer>().bounds.size.y + ContainerSize.y + yoffset;
        var Xposition = Random.Range(ContainerCenter.x - ContainerSize.x / 2, ContainerCenter.x + ContainerSize.x / 2);
        var ZPosition = Random.Range(ContainerCenter.z - ContainerSize.z / 2, ContainerCenter.z + ContainerSize.z / 2);
        Vector3 spawnPosition = new Vector3(Xposition, ContainerCenter.y + Yposition, ZPosition);

        GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, objectContainer);
        newObject.name = objectName + objectID;
        //newObject.transform.localScale = Vector3.one * objectScale;

        objectID++;
    }
}
