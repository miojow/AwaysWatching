﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ItemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();

	void Start () {
        items.Add(new Item("I_Key01", 1, "Chave marrom misteriosa.", 0, Item.ItemType.Key));
        items.Add(new Item("I_Key02", 2, "Chave prateada quadrada.", 0, Item.ItemType.Key));
        items.Add(new Item("I_Scroll", 3, "Carta endereçada a Karen.", 0, Item.ItemType.Document));
        items.Add(new Item("Cano", 4, "Cano de metal simples.",1, Item.ItemType.Weapon));
	}
	

}