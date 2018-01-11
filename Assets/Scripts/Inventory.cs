using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	int rupee_count = 0;

	public void AddRupees(int num_rupees)
	{
		rupee_count += num_rupees;
	}

	public int GetRupees()
	{
		return rupee_count;
	}
}
