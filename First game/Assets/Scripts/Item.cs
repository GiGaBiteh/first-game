using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public GameObject newItem, inventory;
    public int stack;
    void Start()
    {
        //Move the object randomly
        inventory = GameObject.Find("Canvas");
        float RNGx = Random.Range(-1f, 1f);
        float RNGy = Random.Range(1f, 2.3f);
        Rigidbody.velocity = new Vector2(RNGx, Rigidbody.velocity.y);
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, RNGy);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inventory.GetComponent<Inventory>().PickupItem(newItem, stack);
            Destroy(this.gameObject);
        }
    }
}
