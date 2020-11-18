using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int roomType;

    public float smoothSpeed;

    bool enterRoom;

    GameObject player;
    GameObject maincamera;

    void Awake(){
        enterRoom=false;
    }
    void Update(){
        if (enterRoom){
            Vector3 initPosition = Camera.main.transform.position;
            player = GameObject.FindWithTag("Player");
            smoothSpeed = 0.1f;
            Vector3 speed = Vector3.zero;
            Vector3 desiredPosition = this.transform.position + new Vector3(0,0,-10);;
            Vector3 smoothedPos = Vector3.Lerp(initPosition, desiredPosition, Time.deltaTime*2.5f);
            Camera.main.transform.position = smoothedPos;
        }
    }

    public void RoomDestruction() {

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D obj) {
        
        
        if(obj.gameObject.tag == "Player"){
            //Debug.Log("Enter Room");
            enterRoom = true;        
        }

        
    }

    void OnTriggerExit2D(Collider2D obj) {

         if(obj.gameObject.tag == "Player"){
            enterRoom = false;        
        }
    }
}
