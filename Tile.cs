using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tile : MonoBehaviour
{
    private GameObject chest;
    private GameObject portal;
    private int dungeonSize;
    public bool[] openings;
    public int rotation;
    public int x;
    public int y;
    public string tileType;
    public string biomeType;

    // Start is called before the first frame update
    void Start()
    {
        // if(tileType == "Block"){
        //     openings = new bool[4] {true, true, true, true};
        // }
        // calcOpenings();
    }

    void Awake(){
        x = (int) transform.position.x;
        y = (int) transform.position.y;
        rotation = (int) transform.rotation.eulerAngles.z;
        chest = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Chest.prefab");
        portal = (GameObject) AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Portal.prefab");

        // if(rotation == -90) rotation = 270;
        // if(rotation == -180) rotation = 180;

        //calcOpenings();

        float rand = Random.Range(0f, 1f);
        float portalChance = 0.1f;
        float chestChance = 0.5f;

        if(rand <= portalChance){
            generatePortal();
        }else{
            rand = Random.Range(0f, 1f);
            if(rand <= chestChance){
                generateChest();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool[] getOpenings(){
        calcOpenings(); // Calculate NESW Door Openings
        return this.openings;
    }

    private void generatePortal(){
        if(tileType == "Intersection1" || tileType == "Deadend1" || tileType == "Tsection1" || tileType == "Hallway1" || tileType == "Corner1"){
            int randX = Random.Range(2,4);
            int randY = Random.Range(2,4);
            if(randX == 2 && randY == 2){
                randX = 3;
            }
            int multX = Random.Range(0, 2) * 2 - 1;
            int multY = Random.Range(0, 2) * 2 - 1;
            Instantiate(portal, new Vector3(x + randX * multX, y + randY * multY, -1), Quaternion.identity);
        }
    }

    private void generateChest(){
        if(tileType == "Intersection1" || tileType == "Deadend1" || tileType == "Tsection1" || tileType == "Hallway1" || tileType == "Corner1"){
            int randX = Random.Range(2,4);
            int randY = Random.Range(2,4);
            if(randX == 2 && randY == 2){
                randX = 3;
            }
            int multX = Random.Range(0, 2) * 2 - 1;
            int multY = Random.Range(0, 2) * 2 - 1;
            Instantiate(chest, new Vector3(x + randX * multX, y + randY * multY, -1), Quaternion.identity);
        }
        
    }

    private void calcOpenings(){
        if(tileType == "Intersection1" || tileType == "Intersection2" || tileType == "Empty"){
            openings = new bool[4] {true, true, true, true};
        }
        
        if(tileType == "Block"){
            openings = new bool[4] {false, false, false, false};
        }

        if(tileType == "Corner1" || tileType == "Corner2"){
            if(rotation == 0){
                openings = new bool[4] {true, false, false, true};
            }else if(rotation == 90){
                openings = new bool[4] {false, false, true, true};
            }else if(rotation == 180){
                openings = new bool[4] {false, true, true, false};
            }else{
                openings = new bool[4] {true, true, false, false};
            }
        }

        if(tileType == "Hallway1" || tileType == "Hallway2"){
            if(rotation == 0 || rotation == 180){
                openings = new bool[4] {true, false, true, false};
            }else{
                openings = new bool[4] {false, true, false, true};
            }
        }

        if(tileType == "Tsection1" || tileType == "Tsection2"){
            if(rotation == 0){
                openings = new bool[4] {true, true, false, true};
            }else if(rotation == 90){
                openings = new bool[4] {true, false, true, true};
            }else if(rotation == 180){
                openings = new bool[4] {false, true, true, true};
            }else{
                openings = new bool[4] {true, true, true, false};
            }
        }

        if(tileType == "Deadend1" || tileType == "Deadend2"){
            if(rotation == 0){
                openings = new bool[4] {true, false, false, false};
            }else if(rotation == 90){
                openings = new bool[4] {false, false, false, true};
            }else if(rotation == 180){
                openings = new bool[4] {false, false, true, false};
            }else{
                openings = new bool[4] {false, true, false, false};
            }
        }
    }
}
