using UnityEngine;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    public AudioSource damageSound;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        damageSound.Play();
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player hurt
        }
        else
        {
            //player dead
            SceneManager.LoadScene("GameOver");
        }

    }


  /*  private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Deathzone")
        {
            Debug.Log("Fall Damage");
            TakeDamage(1);
        }

    }
  */


    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

}
