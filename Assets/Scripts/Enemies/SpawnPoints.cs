using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
	public static Transform[] Spawns;

	void Awake()
	{
		Spawns = new Transform[transform.childCount];
		for (int i = 0; i < Spawns.Length; i++)
		{
			Spawns[i] = transform.GetChild(i);
		}
	}
}