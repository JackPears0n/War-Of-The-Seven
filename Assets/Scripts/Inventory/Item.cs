using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    private PlayerHealthScript pHS;

    public virtual void Use(GameObject player)
    {
        // Use item
        // Something may happen
        if (itemName == "Health Potion")
        {
            pHS = player.GetComponent<PlayerHealthScript>();
            pHS.Heal(15);
        }

        Debug.Log("Using " + itemName);

    }

}
