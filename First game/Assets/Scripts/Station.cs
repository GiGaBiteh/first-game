using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public bool inStation;
    public string stationType;
    public Crafting craftingReference;
    public string[] names;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Entered station
        if (other.name == "C_Player")
        {
            inStation = true;
            craftingReference.checkStations(stationType);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Left station
        if (other.name == "C_Player")
        {
            inStation = false;
            craftingReference.checkStations(stationType);
        }
    }
}
