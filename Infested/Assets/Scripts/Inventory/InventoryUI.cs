using UnityEngine;
using TMPro;
using System.Text;

public class InventoryUI : MonoBehaviour
{

    // Reference to the player's inventory
    public Inventory inventory;

    // Reference to the TextMeshPro UI element that displays the inventory Text
    public TMP_Text inventoryText;

    void Update()
    {

        // Update the inventory display every frame
        UpdateInventoryDisplay();
    }

    void UpdateInventoryDisplay()
    {

        // Exit early if either the inventory or the UI text reference is missing
        if (inventory == null || inventoryText == null)
            return;

        // Use a StringBuilder to build the inventory string
        StringBuilder sb = new StringBuilder();
        sb.AppendLine();

        // Add each item's name to the display string
        foreach (var item in inventory.items)
        {
            sb.AppendLine(item.itemName);
        }

        // Assign the built string to the UI text component
        inventoryText.text = sb.ToString();
    }
}
