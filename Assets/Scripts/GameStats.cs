using UnityEngine;

public class GameStats : MonoBehaviour
{
	private static int m_gems;
	[SerializeField] private int m_startGems = 10;

	public static int CoreHealth;
	public static int CoreMaxHealth;
    [SerializeField] private int m_coreHealth = 20;

	private void Awake()
	{
		m_gems = m_startGems;
        CoreMaxHealth = m_coreHealth;
		CoreHealth = CoreMaxHealth;
	}

	private void Update()
	{
		//Gems clamp
		m_gems = Mathf.Clamp(m_gems, 0, 5000);
    }

	#region Accessors
	public static int Gems { get { return m_gems; } set { m_gems = value; } }
    #endregion
}