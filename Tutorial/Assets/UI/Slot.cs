using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public int index = 0;
    public ItemInstance itemInstance = null; //holds name weight and sprite, set in item.cs

    public Image panel;
    public Color notSelectedColor;
    public Color selectedColor;

    public TextMeshProUGUI nameBox;
    public TextMeshProUGUI weightBox;
    public TextMeshProUGUI lbsText;
    public TextMeshProUGUI valueBox;
    public Image icon;

    private void Awake()
    {
        icon.sprite = null;
    }

    public void SetItem(ItemInstance instance) //setting the slot to display an item
    {
        nameBox.text = instance.GetItemName();
        weightBox.text = instance.GetWeight().ToString();
        if (instance.GetWeight() == 1)
            lbsText.text = " lb";
        else
            lbsText.text = " lbs";
        valueBox.text = instance.GetValue().ToString();
        icon.sprite = instance.item.icon;

        itemInstance = instance;
        icon.SetNativeSize(); //stops squishing and stretching of icon
        RectTransform rt = icon.transform.GetComponent<RectTransform>();
        float width = rt.sizeDelta.x / rt.sizeDelta.y; //makes height consistant and width proportional between items
        rt.sizeDelta = new Vector2(width * 64, 64);
    }

    public void RemoveItem() //the slot will clear itself of the item it currently is storing
    {
        this.itemInstance = null;
        this.nameBox.text = null;
        this.icon.sprite = null;
    }

    public void Selected()
    {
        if (GetComponentInParent<InventoryDisplay>())
        {
            GetComponentInParent<InventoryDisplay>().SelectSlot(this);
        }
    }

    public void Select()
    {
        panel.color = selectedColor;
    }

    public void Deselect()
    {
        panel.color = notSelectedColor;
    }
}
