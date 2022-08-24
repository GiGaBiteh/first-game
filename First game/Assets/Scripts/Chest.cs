using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Chest : MonoBehaviour
{
    public GameObject chestUI;
    public GameObject[] slots;
    public GameObject[] chestItems;
    public GameObject itemParent;

    void Start()
    {
        chestItems = new GameObject[slots.Length];
    }

    void Update()
    {
        //Right mouse button interaction
        if (Input.GetMouseButtonDown(1))
        {
            //Check if the mouse is already over UI object, don't fire if so
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //Raycast to object
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
                //If object exists
                if (hit.collider != null)
                {
                    //If the object is chest
                    if (hit.collider.tag == "Chest")
                    {
                        //Place objects in the corresponding slots from GameObject slotsChildren array
                        for (int i = 0; i < slots.Length; i++)
                        {
                            if (chestItems[i] != null)
                            {
                                Instantiate(chestItems[i], slots[i].transform);
                                chestItems[i] = null;

                            }
                        }
                        //First handle showing the UI, to do this, have an object of ui withy chest interface, doesnt need to be a prefab
                        chestUI.SetActive(true);
                        foreach (Transform child in itemParent.transform)
                        {
                            Destroy(child.gameObject);
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisableChest();
        }

        //Also remove chest UI when far away from the chest, this can be tracked by taking the coordinates of the chest and then in the update function, checking the distance between
        //The place and the chest, allows the player to walk even when interacting with chests. 
    }
    void DisableChest()
    {
        //Disable the UI of the chest before removing the objects
        chestUI.SetActive(false);
        //Save the objects in chestItems array as gameObjects and destroy the 
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                chestItems[i] = Instantiate(slots[i].transform.GetChild(0).gameObject, itemParent.transform);
                Destroy(slots[i].transform.GetChild(0).gameObject);
            }
        }
    }
}