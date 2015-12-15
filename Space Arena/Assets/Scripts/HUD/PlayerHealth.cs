using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public RectTransform healthBar;
    private float xPos;
    private float maxY;
    private float minY;
    private int currentHealth;
    public int maxHealth;
    public Text healthText;
    public Image visualHealth;



    // Use this for initialization
    void Start () {
        xPos = healthBar.position.x;
        maxY = healthBar.position.y;
        minY = healthBar.position.y - healthBar.rect.height;
        currentHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {
       
	}


    private void HandleHealth() {
        if(currentHealth < 0)
        {
            healthText.text = "" + 0;
            gameObject.GetComponent<PlayerMovement>().KillPlayer();
            return;
        }
            
        healthText.text = "" + currentHealth;
        float currentYvalue = MapValues(currentHealth, 0, maxHealth, minY, maxY);
        //for (int i = 0; i < healthBar.position.y - currentYvalue; i++)
        //{
            healthBar.position = new Vector3(xPos, currentYvalue);
        //}

        if (currentHealth > maxHealth / 2) //More then 50% health
        {
          visualHealth.color = new Color32((byte)MapValues(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 75);
        }
        else //less then 50% health
        {
           visualHealth.color = new Color32(255, (byte)MapValues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 75);
        }

    }

    private float MapValues(float y, float inMin, float inMax, float outMin, float outMax)
    {
        return (y - inMin) * ((outMax - outMin) / (inMax - inMin)) + outMin;
    }


    void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag)
        {
            case "HealthDrop":
                currentHealth += 10;
                break;
            // Hit by enemy shot
            case "EnemyShot":
                currentHealth -= Random.Range(5, 10);
                HandleHealth();
                break;
            // Collision with enemy / asteroid
            case "EnemyKamikaze":
            case "Asteroid":
                currentHealth -= 20;
                break;
        }
    }





}
