using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{

    public GameObject scoreText;
    public static int theScore;
    public static int ScoreTofinish;

    
    // Update is called once per frame
    void Update()
    {
        scoreText.GetComponent<Text>().text = "SCORE: " + theScore;   
    }
}
