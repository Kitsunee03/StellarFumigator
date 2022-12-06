using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour
{
	public static int Gems;
	[SerializeField] private int m_startGems = 10;

	public static int CoreHealth;
    [SerializeField] private int m_coreHealth = 20;

	public static int Rounds;

	void Start()
	{
		Gems = m_startGems;
		CoreHealth = m_coreHealth;

		Rounds = 0;
	}
}