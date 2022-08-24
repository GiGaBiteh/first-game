using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class TileManager : MonoBehaviour
{
    public Tilemap world;
    public GameObject itemsParent;
    public GameObject[] tileDrops;

    void Update()
    {
        //Break
        if (Input.GetMouseButtonDown(0))
        {
            BreakTile();
        }
    }

    public void BreakTile()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //Mouse Position
            Vector3Int worldPosition = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //World to cell (To have it on the same Z position)
            Vector3Int tilePos = world.WorldToCell(worldPosition);
            // Tile you are currently hovering over
            TileBase currentTile = world.GetTile(tilePos);
            if (currentTile != null)
            {
                for (int i = 0; i < tileDrops.Length; i++)
                {
                    if (currentTile.name.Split('_')[1] == tileDrops[i].name.Split('_')[1])
                    {
                        GameObject instantiatedObject = Instantiate(tileDrops[i], new Vector2(tilePos.x + 0.5f, tilePos.y + 0.5f), Quaternion.identity, itemsParent.transform);
                        instantiatedObject.GetComponent<Item>().stack = 1;
                        world.SetTile(tilePos, null);
                    }
                }
            }
        }
    }

    public bool PlaceTile(GameObject selectedObject)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //Mouse Position
            Vector3Int worldPosition = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //World to cell (To have it on the same Z position)
            Vector3Int tilePos = world.WorldToCell(worldPosition);
            // Tile you are currently hovering over
            TileBase currentTile = world.GetTile(tilePos);
            if (currentTile == null)
            {
                world.SetTile(tilePos, selectedObject.GetComponent<GetItem>().selectedTile);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
