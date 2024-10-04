using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class chasePlayer : MonoBehaviour
{

    /*
        position marker;
        float distanceToMarker = Distance(marker)

        if(canSeePlayer){
            markerer = player.position
            moveTowards(marker);

        }else{
            if(distanceToMarker > 5){
                moveTowardsMarker
            }else{
                roam()
            }
        }

        

    */
    int maxDistance = 100;
    public float speed;
    public GameObject player;
    public float distanceToPlayer;
    private Vector3 marker;
    private Rigidbody2D rb;
    void Start()
    {
        
    }

    void Awake(){
        //player = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
        marker = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        trackPlayer();
        // float distance = Vector3.Distance(transform.position, player.transform.position);

        // if(distance > distanceToPlayer){
        //     transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
        // }
    }

    void trackPlayer(){
        float distanceToMarker = Vector3.Distance(this.transform.position, marker);
        if(canSeePlayer()){
            marker = player.transform.position;
            moveTowardsMarker();
        }else{
            if(distanceToMarker > 1 || rb.velocity.magnitude > 0.1f){
                moveTowardsMarker();
            }else{
                roam();
            }
        }

        
    }
    void moveTowardsMarker(){
        this.transform.position = Vector3.MoveTowards(this.transform.position, marker, speed*Time.deltaTime);
    }

    void roam(){

        /*
            shoot a ray in a random direction
            set marker to that point 

        */
    }



    bool canSeePlayer(){

        if(player == null) {
            return false;
        }

        Vector3 directionToPlayer = player.transform.position - this.transform.position;

        distanceToPlayer = directionToPlayer.magnitude;    

        Debug.DrawRay(this.transform.position, directionToPlayer*10f, Color.white, 0);

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, directionToPlayer, maxDistance, LayerMask.GetMask("Default"));
        //Debug.Log(hit.transform.tag);
        if(hit.collider != null){
            if(hit.transform.tag == "Player"){
                //Debug.DrawRay(transform.position, directionToPlayer*10f, Color.red, 0);
                return true;
            }
            
        }
        return false;
    }
}
