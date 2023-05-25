using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public GameObject player;
    private PlayerHealthScript pHS;

    public virtual void Use()
    {
        pHS = player.GetComponent<PlayerHealthScript>();

        // Use item
        // Something may happen
        if (itemName == "Health Potion")
        {
            pHS.Heal(10);
        }

        Debug.Log("Using " + name);

    }

}
