using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public int currentExp = 0;
    public int currentCurrency = 0;
    public int maxCurrency = 500;

    public float charge = 50;
    public float maxCharge = 100;
    public float chargeDecay = 1;
    public float chargeRate = 4;

    Text currencyDisplay;

    public List<Item> itemList = new List<Item>();
    public bool hasFlashlight = false;

    public int goldGearsHeld = 0;

	// Use this for initialization
	void Start () {
        currencyDisplay = GameObject.Find("CurrencyText").GetComponent<Text>();
        currencyDisplay.text = currentCurrency.ToString();

        //hasFlashlight = true;
        CreateItem("Flashlight", 1);
        hasFlashlight = SearchInventory("Flashlight");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CurrencyPickup(int amount)
    {
        if (currentCurrency < maxCurrency)
        {
            currentCurrency += amount;
        }
        if(currentCurrency > maxCurrency)
        {
            currentCurrency = maxCurrency;
        }
        currencyDisplay.text = currentCurrency.ToString();
    }

    public bool SearchInventory(string itemToSearch)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i].itemName == itemToSearch)
            {
                return true;
            }
        }
        return false;
    }

    public void AddSimilarItem(string itemToAdd)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemName == itemToAdd)
            {
                itemList[i].quantity++;
            }
        }
    }

    public void RemoveItemFromList(string itemToRemove)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemName == itemToRemove)
            {
                if (itemList[i].quantity > 1)
                {
                    itemList[i].quantity--;
                }
                else
                {
                    itemList.Remove(itemList[i]);
                }
            }
        }
    }

    public void CreateItem(string _itemName, int _quantity)
    {
        Item createdItem = new Item();
        createdItem.itemName = _itemName;
        createdItem.quantity = _quantity;
        itemList.Add(createdItem);
    }
}
