using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    
    public GameObject coin;
    public GameObject silverOre;
    public GameObject key;
    public GameObject worm;
    private float x;
    private float y;
    private float rand;

    public void drop(string lootTableString, float posX, float posY){
        x = posX;
        y = posY;
        string[] items = lootTableString.Split('\n');
        bool dropped = false;

        foreach(string item in items){
            string[] temp = item.Split(",");
            // Item Name
            string itemName = temp[0];
            // Odds of being dropped
            float val = float.Parse(temp[1]);

            rand = Random.Range(0f,1f); 
            if(val >= 0){
                if(val > rand){
                    dropItem(itemName);
                    dropped = true;
                }
            }else{
                // No item was dropped
                if(dropped == false){
                    dropItem(itemName);
                }
            }

        }
    }

    private void dropItem(string name){
        if(name.Equals("coin")){
            Instantiate(coin, new Vector3(x, y, -1), Quaternion.Euler(new Vector3(0, 0, 360*Random.value)));
            return;
        }
        if(name.Equals("silverOre")){
            Instantiate(silverOre, new Vector3(x, y, -1), Quaternion.Euler(new Vector3(0, 0, 360*Random.value)));
            return;
        }
        if(name.Equals("key")){
            Instantiate(key, new Vector3(x, y, -1), Quaternion.Euler(new Vector3(0, 0, 360*Random.value)));
            return;
        }
        if(name.Equals("worm")){
            Instantiate(worm, new Vector3(x, y, -1), Quaternion.Euler(new Vector3(0, 0, 360*Random.value)));
            return;
        }
    }
}
