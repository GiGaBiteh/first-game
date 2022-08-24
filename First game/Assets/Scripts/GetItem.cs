using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class GetItem : MonoBehaviour
{
    public RuleTile selectedTile;
    public GameObject groundItem;
    public int stack;
    public Text whiteText, blackText;
    void Start()
    {
        whiteText.text = stack.ToString();
        blackText.text = stack.ToString();
    }
}
