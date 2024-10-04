using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    
    public GameObject intersection1;
    public GameObject intersection2;
    public GameObject corner1;
    public GameObject corner2;
    public GameObject deadEnd1;
    public GameObject deadEnd2;
    public GameObject hallway1;
    public GameObject hallway2;
    public GameObject longHallway;
    public GameObject largeRoom;
    public GameObject tSection1;
    public GameObject tSection2;
    public GameObject empty;
    public GameObject block;
    public float openChance;
    

    public int dungeonSize = 31;
    private GameObject[,] dungeon;
    
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
        // When the dungeon is first set up

        dungeon = new GameObject[dungeonSize, dungeonSize];
        // Set all Dungeon Values to Empty

        for(int i = 0; i < dungeonSize; i++){
            for(int j = 0; j < dungeonSize; j++){
                dungeon[i, j] = empty;
            }
        }

        instantiateTile(intersection1, ((dungeonSize-1)/2), ((dungeonSize-1)/2));


        instantiateTile(largeRoom, 3, 3);

        for(int i = 0; i < dungeonSize; i++){
            for(int j = 0; j < dungeonSize; j++){
                instantiateTile(i, j);
            }
        }

        

        // instantiateTile(intersection1, 5, 5);    // Center

        // instantiateTile(5, 6);

        // instantiateTile(6, 5);
        // instantiateTile(5, 4);
        // instantiateTile(4, 5);
        
        // instantiateTile(5, 7);
        // instantiateTile(7,5);
        // instantiateTile(5, 3);
        // instantiateTile(3, 5);
    }

    private void instantiateTile(int x, int y){
        instantiateTile(empty, x, y);
    }

    public int getDungeonSize(){
        return dungeonSize;
    }

    private void instantiateTile(GameObject g, int x, int y){
        if(dungeon[x, y] != empty) return;


        if(g == largeRoom){
            dungeon[x+1,y] = intersection1;
            dungeon[x-1,y] = intersection1;
            dungeon[x,y+1] = intersection1;
            dungeon[x,y-1] = intersection1;
            dungeon[x+1,y+1] = block;
            dungeon[x+1,y-1] = block;
            dungeon[x-1,y+1] = block;
            dungeon[x-1,y-1] = block;
            dungeon[x,y] = Instantiate(largeRoom, new Vector2(x*9,y*9), Quaternion.Euler(new Vector3(0, 0, 0)));
            return;
        }

        int rotation = 0;

        GameObject northTile = empty;
        GameObject eastTile = empty;
        GameObject southTile = empty;
        GameObject westTile = empty;

        // +x = east
        // -x = west
        // +y = north
        // -y = south


        // Checking for Border Tiles
        if(x == 0){
            eastTile = dungeon[x+1, y];
            westTile = block;
        }else if(x == dungeonSize-1){
            westTile = dungeon[x-1, y];
            eastTile = block;
        }else{            
            eastTile = dungeon[x+1, y];
            westTile = dungeon[x-1, y];
        }
        
        if(y == 0){
            northTile = dungeon[x, y+1];
            southTile = block;
        }else if(y == dungeonSize-1){
            southTile = dungeon[x, y-1];
            northTile = block;
        }else{
            northTile = dungeon[x, y+1];
            southTile = dungeon[x, y-1];
        }
        
        // Debug.Log(northTile);
        // Debug.Log(eastTile);
        // Debug.Log(southTile);
        // Debug.Log(westTile);
        if(g != empty){

        }else{

            // 0 -> Closed
            // 1 -> Open
            // 2 -> Either

            // False = closed
            // True = open

            bool northState = true;
            bool eastState = true;
            bool southState = true;
            bool westState = true;
            
            float rand;
            
            
            rand = Random.Range(0f, 1f);
            if(northTile == empty){
                northState = rand < openChance;
            }else if (northTile == block){
                northState = false;
            }else{
                northState = northTile.GetComponent<Tile>().getOpenings()[2];
            }

            rand = Random.Range(0f, 1f);
            if(eastTile == empty){
                eastState = rand < openChance;
            }else if (eastTile == block){
                eastState = false;
            }else{
                eastState = eastTile.GetComponent<Tile>().getOpenings()[3];
            }

            rand = Random.Range(0f, 1f);
            if(southTile == empty){
                southState = rand < openChance;
            }else if (southTile == block){
                southState = false;
            }else{
                southState = southTile.GetComponent<Tile>().getOpenings()[0];
            }

            rand = Random.Range(0f, 1f);
            if(westTile == empty){
                westState = rand < openChance;
            }else if (westTile == block){
                westState = false;
            }else{
                westState = westTile.GetComponent<Tile>().getOpenings()[1];
            }
            
            // T T T T      Intersection

            // F T T T      tSection 180
            // T F T T      tSection 90
            // T T F T      tSection 0
            // T T T F      tSection 270
            
            // F F T T      corner 90
            // T F F T      corner 0
            // T T F F      corner 270
            // F T T F      corner 180
            // T F T F      hallway 0
            // F T F T      hallway 90

            // T F F F      deadEnd 0 
            // F T F F      deadEnd 270
            // F F T F      deadEnd 180
            // F F F T      deadEnd 90

            // F F F F      block



            string switchString = "";

            if(northState == true) switchString += "T"; else switchString += "F";
            if(eastState == true) switchString += "T"; else switchString += "F";
            if(southState == true) switchString += "T"; else switchString += "F";
            if(westState == true) switchString += "T"; else switchString += "F";

            switch(switchString){
                case "TTTT":
                    g = getTileByType("intersection");
                    break;
                case "FTTT":
                    g = getTileByType("tSection");
                    rotation = 180;
                    break;
                case "TFTT":
                    g = getTileByType("tSection");
                    rotation = 90;
                    break;
                case "TTFT":
                    g = getTileByType("tSection");
                    rotation = 0;
                    break;
                case "TTTF":
                    g = getTileByType("tSection");
                    rotation = 270;
                    break;
                case "FFTT":
                    g = getTileByType("corner");
                    rotation = 90;
                    break;
                case "TFFT":
                    g = getTileByType("corner");
                    rotation = 0;
                    break;
                case "TTFF":
                    g = getTileByType("corner");
                    rotation = 270;
                    break;
                case "FTTF":
                    g = getTileByType("corner");
                    rotation = 180;
                    break;
                case "TFTF":
                    g = getTileByType("hallway");
                    rotation = 0;
                    break;
                case "FTFT":
                    g = getTileByType("hallway");
                    rotation = 90;
                    break;
                case "TFFF":
                    g = getTileByType("deadEnd");
                    rotation = 0;
                    break;
                case "FTFF":
                    g = getTileByType("deadEnd");
                    rotation = 270;
                    break;
                case "FFTF":
                    g = getTileByType("deadEnd");
                    rotation = 180;
                    break;
                case "FFFT": 
                    g = getTileByType("deadEnd");
                    rotation = 90;
                    break;
                case "FFFF":
                    g = getTileByType("block");
                    break;

            }
        }


        dungeon[x,y] = Instantiate(g, new Vector2(x*9,y*9), Quaternion.Euler(new Vector3(0, 0, rotation)));

        //dungeon[x, y] = g;

        //instantiateObject(g, x*9, y*9, rotation);

        // 0 = north, 1 = east, 2 = south, 3 = west
       

    }

    private GameObject getTileByType(string type){
        float rand = Random.value;

        if(type == "intersection"){
            if(rand < 0.5){
                return intersection1;
            }else{
                return intersection2;
            }
        }
        if(type == "corner"){
            if(rand < 0.5){
                return corner1;
            }else{
                return corner2;
            }
        }
        if(type == "deadEnd"){
            if(rand < 0.5){
                return deadEnd1;
            }else{
                return deadEnd2;
            }
        }
        if(type == "hallway"){
            if(rand < 0.5){
                return hallway1;
            }else{
                return hallway2;
            }
        }

        if(type == "longHallway") return longHallway;
        if(type == "largeRoom") return largeRoom;

        if(type == "tSection"){
            if(rand < 0.5){
                return tSection1;
            }else{
                return tSection2;
            }
        }
        
        return block;
    }

    private void instantiateObject(GameObject g, int x, int y, int rot){
        Instantiate(g, new Vector2(x,y), Quaternion.Euler(new Vector3(0, 0, rot)));
    }
}
