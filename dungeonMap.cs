// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class dungeonMap : MonoBehaviour
// {

//     private const int size = 19;


//     public GameObject player;
//     private float playerX;
//     private float playerY;

//     private int playerTileX;
//     private int playerTileY;

//     private int tilePosX;
//     private int tilePosY;

//     private bool hasGenerated;

//     protected Tile[,] dungeon;

//     private int[,,] doors;

//     // 0 = null, 1 = open, 2 = closed
//     // 0 = north, 1 = east, 2 = south, 3 = west
    
//     public GameObject tile;
//     private int random;

//     // Start is called before the first frame update
//     void Start()
//     {
//         dungeon = new Tile[size,size];
//         doors = new int[size,size,4];
//         hasGenerated = false;
//         generateTiles();
//     }

//     void getPlayerPos(){
//         playerX = player.transform.position.x;
//         playerY = player.transform.position.y;

//         playerTileX = (int) (4.5+(playerX / 9));
//         playerTileY = (int) (4.5+(playerY/ 9));

//         //Debug.Log(playerTileX + "," + playerTileY);
//     }

//     void generateTiles(){

//         for(float i = -1 * size/2; i <= size/2; i++){
//             for(float j = -1 * size/2; j <= size/2; j++){
//                 // For each tile
//                 Tile t = new Tile(i*9, j*9, size);
//                 instantiateRoom(t);
//                 //addWalls(i*9, j*9);
//             } 
//         }
//     }

//     void instantiateRoom(Tile t){
//         float tileX = t.getTileX();
//         float tileY = t.getTileY();
//         Instantiate(tile, new Vector3(tileX, tileY, 1), Quaternion.identity);
//         int[] wallValues = t.getWallValues();

//         // North Wall
//         if(wallValues[0] == 1){
//             // Open North
//             Instantiate(northWallOpen, new Vector2(tileX-3, tileY+4), Quaternion.identity);
//         }else if(wallValues[0] == 2){
//             // Closed North
//             Instantiate(northWallClosed, new Vector2(tileX, tileY+4), Quaternion.identity);
//         }

//         // East Wall
//         if(wallValues[1] == 1){
//             // Open East
//             Instantiate(eastWallOpen, new Vector2(tileX+4, tileY+2.5f), Quaternion.identity);
//         }else if(wallValues[1] == 2){
//             // Closed East
//             Instantiate(eastWallClosed, new Vector2(tileX+4, tileY-0.5f), Quaternion.identity);
//         }

//         // South Wall
//         if(wallValues[2] == 1){
//             // Open South
//             Instantiate(southWallOpen, new Vector2(tileX-3, tileY-4), Quaternion.identity);
//         }else if(wallValues[2] == 2){
//             // Closed South
//             Instantiate(southWallClosed, new Vector2(tileX, tileY-4), Quaternion.identity);
//         }

//         // West Wall
//         if(wallValues[3] == 1){
//             // Open West
//             Instantiate(westWallOpen, new Vector2(tileX-4, tileY+2.5f), Quaternion.identity);
//         }else if(wallValues[3] == 2){
//             // Closed West
//             Instantiate(westWallClosed, new Vector2(tileX-4, tileY), Quaternion.identity);
//         }
//     }  

//     // Update is called once per frame
//     void FixedUpdate()
//     {
//         getPlayerPos();
//         if(!hasGenerated){
//             hasGenerated = true;
//         }
        
//     }
// }
