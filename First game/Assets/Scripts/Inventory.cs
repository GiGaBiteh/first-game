using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//This script needs to be walked through, put everything wrong with it here at the top and finish it some other time, most basic things seem to work.
//When selecting an item in the inventory, it permanently loops, this can be avoided by checking if input is made, then only loop
//Things to be done:
//When throwing away an item, it shouldn't be pickupable for some seconds and should be thrown away from the player instead of instantiating at the mouse position
//Selection visual for inventory
//Letting a selection stay even after there are no more items (currently deselects automatically)

public class Inventory : MonoBehaviour
{
    //Variables
    public GameObject[] slots;
    public GameObject player, mouseSlot, itemsParent;
    GameObject selectedObject;
    int maxStack = 10;

    //Raycast function handled by UIRaycast script
    public void inventoryRaycast(List<RaycastResult> results)
    {
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "Inventory")
            {
                //The left mouse button is pressed
                //This section handles moving around items in inventories using only the left mouse button
                if (Input.GetMouseButtonDown(0))
                {
                    if (mouseSlot.transform.childCount > 0 && result.gameObject.transform.childCount > 0)
                    {
                        if (mouseSlot.transform.GetChild(0).gameObject.name == result.gameObject.transform.GetChild(0).gameObject.name)
                        {
                            GetItem item1 = result.gameObject.transform.GetChild(0).gameObject.GetComponent<GetItem>();
                            GetItem item2 = mouseSlot.transform.GetChild(0).gameObject.GetComponent<GetItem>();
                            if (item1.stack + item2.stack < maxStack)
                            {
                                item1.stack += item2.stack;
                                Destroy(item2.gameObject);
                            }
                            else
                            {
                                item2.stack -= maxStack - item1.stack;
                                item1.stack = maxStack;
                            }
                            //Set the text of the inventory UI objects
                            item1.blackText.text = item1.stack.ToString();
                            item1.whiteText.text = item1.stack.ToString();
                            item2.blackText.text = item2.stack.ToString();
                            item2.whiteText.text = item2.stack.ToString();
                        }
                        else
                        {
                            mouseSlot.transform.GetChild(0).transform.SetParent(result.gameObject.transform, false);
                            result.gameObject.transform.GetChild(0).transform.SetParent(mouseSlot.transform, false);
                        }
                    }
                    else if (mouseSlot.transform.childCount > 0)
                    {
                        mouseSlot.transform.GetChild(0).transform.SetParent(result.gameObject.transform, false);
                    }
                    else if (result.gameObject.transform.childCount > 0)
                    {
                        result.gameObject.transform.GetChild(0).transform.SetParent(mouseSlot.transform, false);
                    }
                }

                //The left mouse button is pressed
                //This section handles moving around items using only the right mouse button
                if (Input.GetMouseButtonDown(1))
                {
                    if (mouseSlot.transform.childCount > 0 && result.gameObject.transform.childCount > 0)
                    {
                        //If there is an item in the mouseSlot but also in the resulting slot
                        //There is an item in both, you'll need to check the item in the current mouseslot, if it's the same as the item in the
                        //resulting slot, you can add one to the stack of the mouseslot and remove one from the inventory, check if the one in the inventory
                        //needs to be removed
                    }
                    else if (mouseSlot.transform.childCount > 0)
                    {
                        //If there is an item in the mouseSlot but none in the resulting slot
                        //Just place one item in there, remove one from the mouseslot, check if needs to be deleted.
                    }
                    else if (result.gameObject.transform.childCount > 0)
                    {
                        //If there is no item in the mouseSlot but there is one in the resulting slot
                        //grab one from the stack, check if needs to be removed, maybe this function is not necessary at all because it's exactly what the one above does
                        //The difference is that there is no item in the mouseslot meaning an item has to be added, just make it like this check later if it's possible
                        //to remove this one and change the other ones.
                    }
                }
            }
        }
    }

    void Update()
    {
        //Selects a slot
        for (int i = 1; i < 10; ++i)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                SelectSlot(i - 1);
            }
        }

        //Puts the mouseslot in the correct position
        if (mouseSlot.transform.childCount > 0) mouseSlot.transform.position = Input.mousePosition;
        else mouseSlot.transform.position = new Vector2(2000, 2000);

        //Handles the placing and dropping an item from the inventory
        if (Input.GetMouseButtonDown(1))
        {
            //TODO:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://
            //Instead of handling all the functions here, make seperate functions, placetile should be in a tile handler//
            //using tools should be in a tool handler etc.                                                              //
            //Even weapons with their own ability should have some kind of reference here.                              //
            //TODO:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::://

            //Check if there is an object selected, then uses it
            if (selectedObject)
            {
                GetItem item = selectedObject.GetComponent<GetItem>();
                bool usedItem = player.GetComponent<TileManager>().PlaceTile(selectedObject);
                if (usedItem == true)
                {
                    item.stack--;
                    if (item.stack <= 0)
                    {
                        Destroy(selectedObject);
                    }
                }
                string setStack = item.stack.ToString();
                item.blackText.text = setStack;
                item.whiteText.text = setStack;
            }

            //Throw away an item from the mouse slot if it exists
            if (mouseSlot.transform.childCount > 0)
            {
                GameObject instantiatedObject = Instantiate(mouseSlot.transform.GetChild(0).GetComponent<GetItem>().groundItem, itemsParent.transform);
                instantiatedObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                instantiatedObject.GetComponent<Item>().stack = mouseSlot.transform.GetChild(0).GetComponent<GetItem>().stack;
                Destroy(mouseSlot.transform.GetChild(0).gameObject);
            }
        }
    }

    //Handles the picking up of items, and stacking them properly into the inventory
    public void PickupItem(GameObject newItem, int stack)
    {
        //Loop through all the slots and divide the stack over all the existing items of the same type until satiated
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                if (slots[i].transform.GetChild(0).name == newItem.name + "(Clone)")
                {
                    GetItem item = slots[i].transform.GetChild(0).transform.gameObject.GetComponent<GetItem>();
                    if (item.stack + stack < maxStack)
                    {
                        item.stack += stack;
                        stack = 0;
                    }
                    else
                    {
                        stack -= maxStack - item.stack;
                        item.stack = maxStack;
                    }
                    string setStack = item.stack.ToString();
                    item.blackText.text = setStack;
                    item.whiteText.text = setStack;
                }
            }
        }

        //Whenever the stack is bigger than 0 and all existing stacks have been satiated, place new gameObjects in the slot until the stack is 0
        if (stack > 0)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].transform.childCount <= 0)
                {
                    GameObject instantiatedObject;
                    if (stack <= maxStack)
                    {
                        instantiatedObject = Instantiate(newItem, slots[i].transform);
                        instantiatedObject.GetComponent<GetItem>().stack = stack;
                        break;
                    }
                    else
                    {
                        instantiatedObject = Instantiate(newItem, slots[i].transform);
                        instantiatedObject.GetComponent<GetItem>().stack = maxStack;
                        stack -= maxStack;
                    }
                    instantiatedObject.GetComponent<GetItem>().blackText.text = instantiatedObject.GetComponent<GetItem>().stack.ToString();
                    instantiatedObject.GetComponent<GetItem>().whiteText.text = instantiatedObject.GetComponent<GetItem>().stack.ToString();
                }
            }
        }
        //If the stack is still bigger than 0, there is no place to put the item, there are no slots and all stacks of the same type are full.
        //Do a dropping function with this item and it's stack
    }

    //This function is used to select a slot
    public void SelectSlot(int slotNumber)
    {
        if (slots[slotNumber].transform.childCount > 0)
        {
            selectedObject = slots[slotNumber].transform.GetChild(0).gameObject;
        }
        else
        {
            selectedObject = null;
        }
    }
}
