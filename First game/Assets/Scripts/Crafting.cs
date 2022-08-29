using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//For now this script appends crafting recipes to the crafting menu, you can also place them in beforehand and just activate them instead of appending
//Depending on the name, this is not so hard to change but might make the game look better and faster.

public class Crafting : MonoBehaviour
{
    public GameObject stationsParent, recipesParent, ingredientsBox;
    GameObject currentObject;

    void Update()
    {
        if (ingredientsBox.transform.childCount > 0) ingredientsBox.transform.position = Input.mousePosition;
        else ingredientsBox.transform.position = new Vector2(2000, 2000);
    }
    public void checkStations(string stationType)
    {
        //Do something about the extra crafting recipe things, it keeps instantiating recipes into the ingredientbox, maybe if there is mouse input, reset the ingredientbox

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
            //Check if the object is of recipe tag and if the result is already saved
            if (result.gameObject.tag == "Recipe" && currentObject != result.gameObject)
            {
                //Save the result to prevent looping, needs to be set back in remove function
                currentObject = result.gameObject;
                GameObject[] ingredients = result.gameObject.GetComponent<Ingredients>().ingredients;
                foreach (GameObject ingredient in ingredients)
                {
                    Instantiate(ingredient, ingredientsBox.transform);
                }
            }
        }
        //If the result of the raycast to UI objects is less or is 0, remove all the children in the ingredientBox
        if (results.Count <= 0)
        {
            currentObject = null;
            foreach (Transform child in ingredientsBox.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

