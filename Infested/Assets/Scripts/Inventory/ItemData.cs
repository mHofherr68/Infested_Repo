using UnityEngine;

// Create a new item asset from the Unity editor via the "Assets > Create > Inventory > Item" menu
[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects/Inventory/Item")]
public class ItemData : ScriptableObject
{

    // The display name of the item
    public string itemName;

    // A unique identifier for the item
    public int itemID;

    // Optional: an icon for the item to be used in the UI
    // public Sprite icon;
}
