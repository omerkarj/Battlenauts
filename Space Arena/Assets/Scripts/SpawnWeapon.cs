using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnWeapon : MonoBehaviour {
    public List<GameObject> weaponPrefabs;
    public float SpawnProbabilty;
    // Use this for initialization

    public void CreateWeapon()
    {
        if (Random.Range(0, 1) <= SpawnProbabilty)
        {
            Instantiate(weaponPrefabs[Random.Range(0, weaponPrefabs.Count)], transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
  
}
