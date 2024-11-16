using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEngine.ParticleSystem;
using System.Linq;

namespace Lares.Inventory
{
    public enum ItemTypes
    {
        RECOVERY,
        MATERIAL,
        KEY
    }

    [Serializable]
    public struct InventoryItem
    {
        public int itemID;
        public string itemName;
        public string itemDescription;
        public Image itemImage;
        public ItemTypes itemType;
        public onUseFunctions onUse;

        public InventoryItem(int _itemID, string _itemName, string _itemDes, Image _itemimage, ItemTypes _itemType, onUseFunctions _onUse)
        {
            itemID = _itemID;
            itemName = _itemName;
            itemDescription = _itemDes;
            itemType = _itemType;
            itemImage = _itemimage;
            itemType = _itemType;
            onUse = _onUse;
        }

        public static bool operator == (InventoryItem a, InventoryItem b) { return a.itemID == b.itemID; }
        public static bool operator != (InventoryItem a, InventoryItem b) { return a.itemID != b.itemID; }

        public override bool Equals(System.Object obj)
        {
            if (obj == null || !(obj is InventoryItem))
                return false;
            else
                return itemID == ((InventoryItem)obj).itemID;
        }
    }

    public class Inventory : MonoBehaviour
    {
        public List<Pair<int, int>> InventoryData;
        [SerializeField] ScriptableInventory Inventorylist;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InventoryData = new();
        }

        public bool AddToInventory(int ID)
        {
            //check if ID is already in the list
            if (IsInInventory(ID))
            {
                InventoryData.Find(i => i.Item1 == ID).Item2++;
                return true;
            }

            //check if ID is a valid item
            if (!IsValidItemID(ID))
                return false;

            //Add to inventory if not already 
            InventoryData.Add(new Pair<int, int>(ID, 1));
            return true;
        }

        //TO DO:
        public bool RemoveFromInventory(int ID)
        {
            if (IsInInventory(ID))
            {
                int index = InventoryData.FindIndex(i => i.Item1 == ID);
                InventoryData[index].Item2-= 1;
                if (InventoryData[index].Item2 <= 0)
                {
                    InventoryData.RemoveAt(index);
                    return true;
                }
            }
            return false;
        }


        public InventoryItem ReturnItemDataByID(int ID)
        {
            return Array.Find<InventoryItem>(Inventorylist.inventoryItems, i => i.itemID == ID);
        }


        public bool IsInInventory(int ID)
        {
            return InventoryData.Any(i => i.Item1 == ID);
        }

        public int GetItemCountInInventory(int ID)
        {
            if (IsInInventory(ID))
            {
                return InventoryData.Find(i => i.Item1 == ID).Item2;
            }
            return 0;
        }

        
        public int GetInventoryListIndex(int ID)
        {
            return Array.FindIndex<InventoryItem>(Inventorylist.inventoryItems, i => i.itemID == ID);
        }


        public int GetInventoryDataIndex(int ID)
        {
            return InventoryData.FindIndex(i => i.Item1 == ID);
        }


        public bool IsValidItemID(int ID)
        {
            return Inventorylist.inventoryItems.Any<InventoryItem>(i => i.itemID == ID); 
        }

        public List<Pair<InventoryItem, int>> ReturnInventoryAsInventoryItems()
        {
            List<Pair<InventoryItem, int>> tempInv = new();
            foreach (Pair<int, int> item in InventoryData)
            {
                tempInv.Add(new Pair<InventoryItem, int>(ReturnItemDataByID(item.Item1), item.Item2));
            }
            return tempInv;
        }

        public class Pair<T1, T2>
        {
            public T1 Item1 { get; set; }
            public T2 Item2 { get; set; }

            public Pair(T1 item1, T2 item2)
            {
                Item1 = item1;
                Item2 = item2;
            }
        }
    }
}
