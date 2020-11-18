using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject[] objectsToSpawn;

    private void Start()
    {
        GameObject instance;

        if (!gameObject.CompareTag("RoomSpawner"))
        {
            
            int rand = Random.Range(0, objectsToSpawn.Length);
            instance = Instantiate(objectsToSpawn[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
        else
        {
            if(PlayerStats.getCurrentDifficulty() == PlayerStats.Difficulty.EASY)
            {
              
                instance = Instantiate(objectsToSpawn[0], transform.position, Quaternion.identity);
                instance.transform.parent = transform;
            }else if (PlayerStats.getCurrentDifficulty() == PlayerStats.Difficulty.MEDIUM)
            {
                instance = Instantiate(objectsToSpawn[1], transform.position, Quaternion.identity);
                instance.transform.parent = transform;
            }
            else if (PlayerStats.getCurrentDifficulty() == PlayerStats.Difficulty.HARD)
            {
                instance = Instantiate(objectsToSpawn[2], transform.position, Quaternion.identity);
                instance.transform.parent = transform;
            }
        }
    }
}
