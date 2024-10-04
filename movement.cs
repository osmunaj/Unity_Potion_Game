using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class movement : MonoBehaviour
{
    public float speed = 5f;

    private int score1;

    public GameObject dungeon;
    private Rigidbody2D rb;
    public SceneChanger sceneChanger;
    private float playerX;
    private float playerY;
    private Vector2 movementVector;
    private float horizontalMovement;
    private float verticalMovement;
    private Inventory inventory;
   
    private float dashPower = 30f;
    private bool canDash;
    private int dungeonSize;
    void Start()
    {
        if(sceneChanger.getCurrentScene() == "DungeonScene"){
            dungeonSize = dungeon.GetComponent<Dungeon>().getDungeonSize();
            tpToPosition(((dungeonSize-1)/2)*9,((dungeonSize-1)/2)*9);
        }
        canDash = true;
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
    }

    void tpToPosition(int x, int y){
        Vector3 pos = new Vector3(x, y, -1); 
        transform.position = pos;
    }


     
    void Update(){

        // Dash
        if(Input.GetKeyDown("space") && canDash){
            canDash = false;
            //rb.velocity = movementVector * speed;
            StartCoroutine(dashDelay());
            
        }   
    }

    IEnumerator dashDelay(){
        
        float formerSpeed = speed;
        speed = dashPower;

        yield return new WaitForSeconds(0.1f);
        
        speed = formerSpeed;
        yield return new WaitForSeconds(0.5f);
        canDash = true;
    }

    void FixedUpdate()
    {
        // Movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        movementVector = new Vector2(horizontalMovement, verticalMovement);
        rb.velocity = movementVector * speed;

        // Left
        if(Input.GetKeyDown("left")){
            
        }
        // Right
        if(Input.GetKeyDown("right")){

        }
        if(Input.GetKeyDown("up")){

        }
        if(Input.GetKeyDown("down")){

        }
        


        // Get Player Coords
        playerX = transform.position.x;
        playerY = transform.position.y;

    }

    public GameObject getPlayer(){
        return gameObject;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        // Manage Collisions
        if (other.gameObject.CompareTag("collectable")) {
            inventory.addToInventory(other.name);
            Destroy(other);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Portal")){
            sceneChanger.ChangeScene("Assets/Scenes/ShopScene.unity");
        }

        if (other.gameObject.CompareTag("DungeonEntrance")){
            sceneChanger.ChangeScene("Assets/Scenes/DungeonScene.unity");
        }

        //other.gameObject.SetActive(false);
    }
}
