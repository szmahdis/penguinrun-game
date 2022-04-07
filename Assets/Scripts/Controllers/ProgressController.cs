using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ProgressController : MonoBehaviour
{
    public int score;
    public int scoreToFinish;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip pickupAudio;

    //score UI text
    public Text scoreText;
    private string scoreString = "Score: ";
  
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "collectable")
        {
            audioSource.PlayOneShot(pickupAudio);
            score += 50;
            scoreText.text = scoreString + score.ToString();
            Destroy(other.gameObject);
            //update score visuals
            if(score>=scoreToFinish)
            {
                // You win
                var collectables = GameObject.FindGameObjectsWithTag("collectable");
                SceneManager.LoadScene("EndGame");
                Debug.Log("you win!!!");
            }
        }
    }
}
