using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lares.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public GameObject PlayerReference;
        public List<InventoryNode> InventoryData;
        public ScriptableInventory InventoryList;

        public bool IsInInventory(Guid id) => InventoryData.Any(i => new Guid(i.Item.ItemData.ItemId) == id);
        public bool IsValidID(Guid id) => InventoryList.ItemList.Any(i => new Guid(i.ItemData.ItemId) == id);
        public InventoryItem GetInventoryItemById(Guid id) => InventoryList.ItemList.Find(i => new Guid(i.ItemData.ItemId) == id);

        private void OnEnable()
        { 
            if (InventoryList.ItemList.Count == 0) { Debug.Log("InventoryList is empty!"); }
            InventoryData = new List<InventoryNode>();
            AddToInventory(new Guid(InventoryList.ItemList[0].ItemData.ItemId), 3);
            AddToInventory(new Guid(InventoryList.ItemList[1].ItemData.ItemId));
        }

        public bool AddToInventory(Guid id, int count = 1)
        {
            if (IsInInventory(id))
            {
                InventoryData.Find(i => new Guid(i.Item.ItemData.ItemId) == id).ItemCount += count;
                return true;
            }

            if (!IsValidID(id)) { return false; }

            InventoryData.Add(new InventoryNode(GetInventoryItemById(id), count));
            return true;
        }

        public bool RemoveFromInventory(Guid id, int count = 1)
        {
            InventoryNode item = InventoryData.Find(i => new Guid(i.Item.ItemData.ItemId) == id);
            if (item is null) { return false; }
            
            item.ItemCount -= count;
            if (item.ItemCount <= 0)
                InventoryData.Remove(item);

            return true;
        }

        public int GetItemCountInInventory(Guid id)
        {
            InventoryNode item = InventoryData.Find(i => new Guid(i.Item.ItemData.ItemId) == id);
            return item?.ItemCount ?? 0;
        }
    }

    public class InventoryNode
    {
        public InventoryItem Item;
        public int ItemCount;

        public InventoryNode(InventoryItem item, int count)
        {
            Item = item;
            ItemCount = count;
        }
    }
}
