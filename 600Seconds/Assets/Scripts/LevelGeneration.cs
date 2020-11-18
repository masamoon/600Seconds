using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelGeneration : MonoBehaviour {

    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0 --> closed, index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRBT
    public GameObject pickup;
    public GameObject chest;
    public GameObject[] portals;
   // GameObject[] allportals;
    public List<GameObject> criticalPath;
    public GameObject initialroom;
    public GameObject spawnroom; //room where we drop
    public GameObject chestroom; //last room with loot

    private int direction;
    public bool stopGeneration;
    private int downCounter;

    public float moveIncrement;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    public LayerMask whatIsRoom;

    private bool playerspawned;

    public GameObject Player;   

    public int Score;

    Vector3 chestLoc;

    GameObject[] critpath;

    private void Start()
    {

        LoadingScreen.isloading = true;
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        GameObject room = Instantiate(rooms[1], transform.position, Quaternion.identity);
        //add to critical path
        criticalPath.Add(room);
        direction = Random.Range(1, 6);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (timeBtwSpawn <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else {
            timeBtwSpawn -= Time.deltaTime;
        }

        if (stopGeneration == true && playerspawned == false)
        {

            StartCoroutine(BecomeTemporarilyInvincible());

            foreach (GameObject cpath in criticalPath) //check if critical path contains deleted Rooms 
            {
                if(cpath.transform.childCount == 0){
                    criticalPath.Remove(cpath);
                }
            }


            critpath = criticalPath.ToArray();
            GameObject maincamera = GameObject.FindWithTag("MainCamera");

            portals = GameObject.FindGameObjectsWithTag("Portal");
            //find where spawnpoint and chestspawn is
            Transform chestroom_tr = RecursiveFindChild(critpath[critpath.Length - 1].transform, "Portal"); 
            Transform spawnroom_tr = RecursiveFindChild(critpath[0].transform, "Portal");
           
            Instantiate(Player, spawnroom_tr.position, Quaternion.identity);
           
            Instantiate(chest, chestroom_tr.position, Quaternion.identity);

            spawnroom_tr.gameObject.tag = "PortalSpawn";


            

            playerspawned = true;
            LoadingScreen.isloading = false;

           
        }

        //Destroy remaining Portals
        GameObject[] portalstodestroy = GameObject.FindGameObjectsWithTag("Portal");
        if (portalstodestroy.Length > 0 && playerspawned)
        {
            for (int i = 0; i < portalstodestroy.Length; i++)
            {
                Destroy(portalstodestroy[i]);
            }
        }








    }

    private void Move()
    {

        if (direction == 1 || direction == 2)
        { // Move right !
          
            if (transform.position.x < 25)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                GameObject room = Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                criticalPath.Add(room);

                // Makes sure the level generator doesn't move left !
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4)
        { // Move left !
           
            if (transform.position.x > 0)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                GameObject room = Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                criticalPath.Add(room);

                direction = Random.Range(3, 6);
            }
            else {
                direction = 5;
            }
           
        }
        else if (direction == 5)
        { // MoveDown
            downCounter++;
            if (transform.position.y > -25)
            {
                // Now I must replace the room BEFORE going down with a room that has a DOWN opening, so type 3 or 5
                Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
                //Debug.Log(previousRoom);
                if (previousRoom.GetComponent<Room>().roomType != 4 && previousRoom.GetComponent<Room>().roomType != 2)
                {

                    // My problem : if the level generation goes down TWICE in a row, there's a chance that the previous room is just 
                    // a LRB, meaning there's no TOP opening for the other room ! 

                    if (downCounter >= 2)
                    {
                        criticalPath.Remove(previousRoom.gameObject);
                        previousRoom.GetComponent<Room>().RoomDestruction();
                       
                        GameObject room = Instantiate(rooms[4], transform.position, Quaternion.identity);
                        criticalPath.Add(room);
                    }
                    else
                    {
                        criticalPath.Remove(previousRoom.gameObject);
                        previousRoom.GetComponent<Room>().RoomDestruction();
                        
                        int randRoomDownOpening = Random.Range(2, 5);
                        if (randRoomDownOpening == 3)
                        {
                            randRoomDownOpening = 2;
                        }
                        GameObject room = Instantiate(rooms[randRoomDownOpening], transform.position, Quaternion.identity);
                        criticalPath.Add(room);
                    }

                }
                
               
  
                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = pos;

                // Makes sure the room we drop into has a TOP opening !
                int randRoom = Random.Range(3, 5);
                initialroom = Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
                criticalPath.Add(initialroom);
                direction = Random.Range(1, 6);
            }
            else {
                stopGeneration = true;
                
            }
            
        }

        
    }

    public static GameObject FindComponentInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.gameObject;
            }
        }
        return null;
    }

    Transform RecursiveFindChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(childName))
            {
                return child;
            }
            else
            {
                Transform found = RecursiveFindChild(child, childName);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }



    void MethodThatTriggersInvulnerability()
    {
        
         StartCoroutine(BecomeTemporarilyInvincible());
        
    }


    private IEnumerator BecomeTemporarilyInvincible()
    {
             

        yield return new WaitForSeconds(20);
        
    }
}
