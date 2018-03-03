using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private void Awake()
    {
        PlayerInventory.Instance.OnInvetoryItemAdded.AddListener(OnInventoryChanged);
        PlayerInventory.Instance.OnInvetoryItemRemoved.AddListener(OnInventoryChanged);

        UITool.Get<Text>(transform, "txtTemplate").enabled = false;
    }
    
    private void GenerateList()
    {
        var items = PlayerInventory.Instance.GetInventoryItems();
        var container = transform.Find("Items");
        var template = UITool.Get<Text>(container.transform, "txtTemplate");

        foreach (var item in items)
        {
            var newItem = Instantiate(template.gameObject, container);
            var text = newItem.GetComponent<Text>();
            text.text = item;
            text.enabled = true;
        }
    }

    public void OnInventoryChanged(string item)
    {
        GenerateList();
    }
}