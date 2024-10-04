using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryDisplay;
    private int coins;
    private int size;
    private string[] list;
    private int[] quantity;
    private bool showInventory;
    
    // Start is called before the first frame update
    void Start()
    {
        size = 0;
        list = new string[10];
        quantity = new int[10];
    }

    void Awake(){
        showInventory = false;
    }

    // Update is called once per frame
    void Update()
    {
        setText();

        if(Input.GetKeyDown("e")){
            //Debug.Log("Inventory");
            if(!showInventory){
                showInventory = true;
            }else{
                showInventory = false;
            }
            inventoryDisplay.SetActive(showInventory);
        }
    }

    private void setText(){
        
    }

    public void addToInventory(string item){
        // Cut off (Clone) from string
        item = item.Substring(0, item.Length - 7);

                
        if(item.Equals("Coin")){
            coins++;
            return;
        }

        if(size == 10){
            // If inventory is full
            //Debug.Log("Inventory Full");
        }else{
            for(int i = 0; i < list.Length; i++){     
                if(list[i] == null){
                    // Empty Slot
                    size++;
                    list[i] = item;
                    quantity[i] = 1;
                    break;
                }else if(list[i].Equals(item)){
                    quantity[i]++;
                    break;
                }
            }
        }
        
        getInventoryString();
        
        //Debug.Log(item);
        //Debug.Log("Size: " + size);
    }

    private void getInventoryString(){
        string temp = "";
        for(int i = 0; i < size; i++){
            temp += list[i] + " x" + quantity[i] + "\n";
        }

        Debug.Log(temp);
    }
}
