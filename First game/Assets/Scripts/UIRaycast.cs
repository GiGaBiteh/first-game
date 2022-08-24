using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRaycast : MonoBehaviour
{
    //This script handles UI raycasting, this is done here because of the large amount of lines of code necessary to do a UI raycast.

    //Initalize variables and assign default ones
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    //References to other scripts, like the inventory, which uses UI raycasting
    public Inventory inventoryReference;
    public Crafting craftingReference;

    //Raycast
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //Default stuff (getting the results)
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);

            //Hovering
            craftingReference.hoverRecipe(results);

            //Mouse clicks
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                //Which mouse button is clicked is checked in the other scripts (if done here more code is needed in those other scripts)
                inventoryReference.inventoryRaycast(results);
            }
        }
    }
}
