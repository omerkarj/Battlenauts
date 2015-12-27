using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//using System;

public class GameController : MonoBehaviour {

    public GameObject playerPrefab;
    public bool pauseToggle;
    public Image darkness;
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private PlayerHealth playerhealth;
    public Image pauseMenu;
    public int nextScoreLevel=500;
    private int scoreCounter;
    public Text difficultyText;
    private int level = 1;
    private float healthReducer=1;

    private AudioSource audioSource;
    public AudioClip difficultyUpSound;

    // Use this for initialization
    void Start () {
        GetComponent<Enemy2Spawner>().enabled = false;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerhealth= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        scoreCounter = 1;

        audioSource = gameObject.AddComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        increaseDiffuculty();
        if (level == 3)
            GetComponent<Enemy2Spawner>().enabled = true;

        if (Input.GetKeyDown(KeyCode.N))
            playerMovement.ResetPlayer();

        //Reduce oxygen level every second;
        healthReducer -= Time.deltaTime;
        if (healthReducer <= 0)
        {
            playerhealth.currentHealth--;
            healthReducer = 1;
            playerhealth.HandleHealth();
        }
    }



    public void newPlayer()
    {
        playerMovement.gameObject.tag = "Untagged";
        Destroy(playerMovement.gameObject, 10f);

        GameObject clone = Instantiate(playerPrefab, Vector3.zero, Quaternion.Euler(new Vector3(0, 180, Random.Range(-20, 20)))) as GameObject;
        playerController = clone.GetComponent<PlayerController>();
        playerMovement = clone.GetComponent<PlayerMovement>();
        playerMovement.startEnterAnimation = true;
    }

    public void increaseDiffuculty()
    {
        PlayerScore ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
        int diffculty = ps.score / scoreCounter;
        if (diffculty > nextScoreLevel)
        {
            //Increase here enemy spawn and difficulties
            scoreCounter++;
            nextScoreLevel += 50;
            StartCoroutine(diffcultyUpText());
            reduceSpawnTime();
            level++;
        }
    }

    private IEnumerator diffcultyUpText()
    {
        difficultyText.text = "Difficulty Up!";
        audioSource.clip = difficultyUpSound;
        audioSource.volume = 1f;
        audioSource.Play();
        yield return new WaitForSeconds(1.5f);
        difficultyText.text = "";
    }

    private void reduceSpawnTime()
    {
        EnemySpawner es = gameObject.GetComponent<EnemySpawner>();
        if (es.spawnInterval > 2F)
        {
            es.spawnInterval--;
        }
        else
        {
            es.spawnInterval = es.spawnInterval * 0.90F;
        }
    }
}
