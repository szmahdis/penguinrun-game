using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleManager : MonoBehaviour
{
    int nrPlatforms;
    int toggleTime;

    [SerializeField]
    float cycleTime = 2f;

    [SerializeField]
    GameObject[] platforms;

    // Start is called before the first frame update
    void Start()
    {
        nrPlatforms = platforms.Length;

        if (nrPlatforms - 1 == 0)
            toggleTime = 1;
        else
            toggleTime = nrPlatforms - 1;

        StartCoroutine(StartManagingPlatforms());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartManagingPlatforms()
    {
        for (int i = 0; i < nrPlatforms; i++)
        {
            StartCoroutine(ManagePlatform(platforms[i]));
            yield return new WaitForSeconds(cycleTime);
        }
    }

    IEnumerator ManagePlatform(GameObject platform)
    {
        while (true)
        {
            platform.SetActive(true);
            yield return new WaitForSeconds(cycleTime);
            platform.SetActive(false);
            yield return new WaitForSeconds(toggleTime * cycleTime);
        }
    }
}
