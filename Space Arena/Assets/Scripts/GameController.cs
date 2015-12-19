using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public List<GameObject> weaponPrefabs;
    public GameObject playerPrefab;
    private bool pauseToggle;
    public Image darkness;
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    public Image pauseMenu;

    // Use this for initialization
    void Start () {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        
        InvokeRepeating("SpawnWeapon", 0, 5f);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseToggle) {
                Time.timeScale = 1;
                darkness.color = new Color32(0, 0, 0, 0);
            } else {
                Time.timeScale = 0;
                darkness.color = new Color32(0, 0, 0, 200);

            }
            pauseToggle = !pauseToggle;
        }
    }

    private void SpawnWeapon()
    {
        Vector3 position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        //This will spawn them at random position with (0,0,0) rotation
        Instantiate(weaponPrefabs[Random.Range(0, weaponPrefabs.Count)], position, Quaternion.identity);
    }

    public void newPlayer()
    {
        playerMovement.gameObject.tag = "Untagged";
        Destroy(playerMovement.gameObject, 10f);

        GameObject clone = Instantiate(playerPrefab, Vector3.zero, Quaternion.Euler(new Vector3(0, 180, Random.Range(-20, 20)))) as GameObject;
        playerController = clone.GetComponent<PlayerController>();
        playerMovement = clone.GetComponent<PlayerMovement>();
        playerController.startEnterAnimation = true;
    }
}
