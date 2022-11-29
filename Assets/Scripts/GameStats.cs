using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour
{
	public static int m_money;
	public int startMoney = 400;

	public static int Lives;
	public int startLives = 20;

	public static int Rounds;

	void Start()
	{
		m_money = startMoney;
		Lives = startLives;

		Rounds = 0;
	}
}