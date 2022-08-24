using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//For now this script appends crafting recipes to the crafting menu, you can also place them in beforehand and just activate them instead of appending
//Depending on the name, this is not so hard to change but might make the game look better and faster.

public class Crafting : MonoBehaviour
{
    public GameObject stationsParent;
    public GameObject recipesParent;
    public void checkStations(string stationType)
    {
        //Deactivate all the recipes, the right ones are added below
        foreach (Transform child in recipesParent.transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }

        //Set active the correct recipes
        foreach (Transform child in stationsParent.transform)
        {
            if (child.gameObject.GetComponent<Station>().inStation == true)
            {
                string[] names = child.gameObject.GetComponent<Station>().names;
                //Loop through the recipe names
                foreach (Transform recipe in recipesParent.transform)
                {
                    //Compare the names, if they're the same set them active, recipe is added to crafting UI
                    foreach (string name in names)
                    {
                        if (recipe.name == name)
                        {
                            recipe.gameObject.SetActive(true);
                        }
                    }
                }
                break;
            }
        }
    }

    //Raycast function handled by UIRaycast script
    public void hoverRecipe(List<RaycastResult> results)
    {
        //Loop through the results and get the right crafting recipe with the recipe tag
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == "Recipe")
            {
                GameObject[] ingredients = result.gameObject.GetComponent<Ingredients>().ingredients;
                foreach(GameObject ingredient in ingredients){
                    //This happens every frame, to check wether you hover over a new object, save the object, if the next object is the same as the current object, stop the function
                    //Otherwise continue with the function with the new object. DO THIS BEFORE DOING THE NEXT ONE

                    //For every ingredient instantiate some kind of slider (without the slider function) next to the crafting or onto some object assigned to your mouse
                }
            }
        }
    }
}

