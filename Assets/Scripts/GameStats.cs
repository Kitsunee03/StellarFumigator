using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour
{
	private static int m_gems;
	[SerializeField] private int m_startGems = 10;

	public static int CoreHealth;
    [SerializeField] private int m_coreHealth = 20;

	public static int Rounds;

	void Start()
	{
		m_gems = m_startGems;
		CoreHealth = m_coreHealth;

		Rounds = 0;
	}

    #region Accessors
	public static int Gems { get { return m_gems; } set { m_gems = value; } }
    #endregion
}