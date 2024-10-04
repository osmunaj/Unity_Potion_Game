using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private GameObject player;
    private GameObject lt;
    private float distanceToPlayer;
    private string lootTableString = "coin,0.8\ncoin,0.4\ncoin,0.2\nsilverOre,0.5\nkey,0.15\nworm,-1";
    private float x;
    private float y;
    private bool inReach;

    string type;
    // Start is called before the first frame update

    void Awake(){
        x = transform.position.x;
        y = transform.position.y;
        player = GameObject.Find("Player");
        lt = GameObject.Find("LootTable");
    }

    // Update is called once per frame
    void Update()
    {        
        if(player != null){
            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            inReach = distanceToPlayer < 2;
        }
    }

    void OnMouseOver(){
        if(inReach && Input.GetMouseButtonDown(1)) openChest();
    }

    private void openChest(){
        lt.GetComponent<LootTable>().drop(lootTableString, x, y);
        gameObject.SetActive(false);
    }
}
