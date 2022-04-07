using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{

    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject slot;
    public Inventory inventory;
    public Slot[] inventorySlots;
    public int selectedItem = 0;

    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] Image iconDisplay;
    [SerializeField] TextMeshProUGUI itemInfoText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;
    [SerializeField] TextMeshProUGUI carryWeightText;

    void OnEnable(){
        DisplayInventory();
    }

    private void OnDisable()
    {
        Clear();
    }

    void InventoryToDisplay(Inventory _inventory){
        inventory = _inventory;
    }

    public void DisplayInventory()
    {
        Clear();
        for (int i = 0; i < inventory.inventory.Length; i++)
        {
            GameObject newSlot = Instantiate(slot, transform);
        }

        inventorySlots = GetComponentsInChildren<Slot>();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            ItemInstance instance;

            if (inventory.GetItem(i, out instance))
                inventorySlots[i].SetItem(instance);
            if (i == selectedItem)
                SelectSlot(inventorySlots[i]);
        }
        carryWeightText.SetText(inventory.TotalWeight() + "/" + playerStats.carryCapacity.GetValue());
    }

    public void SelectSlot(Slot _slot)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].Deselect();
            if (inventorySlots[i] == _slot)
                selectedItem = i;
        }
        
        _slot.Select();

        //Set the selected item display info
        itemNameText.SetText ( _slot.itemInstance.GetItemName() );
        itemDescriptionText.SetText(_slot.itemInstance.GetItemDescription());
        iconDisplay.sprite = _slot.icon.sprite;
        iconDisplay.SetNativeSize();
        RectTransform rt = iconDisplay.transform.GetComponent<RectTransform>();
        float width = rt.sizeDelta.x / rt.sizeDelta.y;
        rt.sizeDelta = new Vector2(width * 128, 128);

        //Equip if possible
        playerStats.EquipItem(_slot.itemInstance);

    }

    public void Clear()
    {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
        inventorySlots = new Slot[0]; //creates an array with no items
    }

}
