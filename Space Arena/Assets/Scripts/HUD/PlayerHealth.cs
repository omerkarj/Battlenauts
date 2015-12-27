using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public RectTransform healthBar;
    private float xPos;
    private float maxY;
    private float minY;
    public int currentHealth;
    public int maxHealth;
    public Text healthText;
    public Image visualHealth;
    public Transform visualDamage;
    public AudioClip healthAudio;
    public AudioClip[] hitsAudio;
    private AudioSource audioSource;
    private PlayerMovement playerMovement;


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();

        xPos = healthBar.position.x;
        maxY = healthBar.position.y;
        minY = healthBar.position.y - healthBar.rect.height;
        currentHealth = maxHealth;
        HandleHealth();

	}
	
	// Update is called once per frame
	void Update () {
       
	}


    public void HandleHealth() {
        if (!playerMovement.isDead)
        {
            if (currentHealth < 0)
            {
                healthText.text = "" + 0;
                playerMovement.KillPlayer();
                return;
            }

            healthText.text = "" + currentHealth;
            float currentYvalue = MapValues(currentHealth, 0, maxHealth, minY, maxY);
            //for (int i = 0; i < healthBar.position.y - currentYvalue; i++)
            //{
            healthBar.position = new Vector3(xPos, currentYvalue);
            //}
            Color healthColor;
            if (currentHealth > maxHealth / 2) //More then 50% health
            {
                healthColor = new Color(0.33f, 0.8f, 0.4f, 0.2f);
                visualHealth.GetComponent<Image>().color = healthColor;
                healthBar.GetComponent<Image>().color = healthColor;
            }
            else //less then 50% health
            {
                healthColor = new Color(0.8f, 0f, 0f, 0.2f);
                visualHealth.GetComponent<Image>().color = healthColor;
                healthBar.GetComponent<Image>().color = healthColor;
            }
        }

    }

    private float MapValues(float y, float inMin, float inMax, float outMin, float outMax)
    {
        return (y - inMin) * ((outMax - outMin) / (inMax - inMin)) + outMin;
    }


    void OnTriggerEnter(Collider other) {
        if (!playerMovement.isDead)
        {
            switch (other.gameObject.tag)
            {
                case "HealthDrop":
                    if (currentHealth <= 90)
                        currentHealth += 8;
                    audioSource.clip = healthAudio;
                    audioSource.Play();
                    break;
                // Hit by enemy shot
                case "EnemyShot":
                    Instantiate(visualDamage, transform.position, Quaternion.identity);
                    currentHealth -= Random.Range(5, 10);
                    audioSource.clip = hitsAudio[Mathf.RoundToInt(Random.Range(0, hitsAudio.Length - 1))];
                    audioSource.Play();
                    break;
                // Collision with enemy / asteroid
                case "target":
                    currentHealth -= Random.Range(10, 15);
                    audioSource.clip = hitsAudio[Mathf.RoundToInt(Random.Range(0, hitsAudio.Length - 1))];
                    audioSource.Play();
                    break;
                case "Asteroid":
                    currentHealth -= Random.Range(10, 20);
                    audioSource.clip = hitsAudio[Mathf.RoundToInt(Random.Range(0, hitsAudio.Length - 1))];
                    audioSource.Play();
                    break;
            }

            HandleHealth();
        }
    }





}
