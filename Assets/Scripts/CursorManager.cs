using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;

    private Player m_player;

    [SerializeField] private Texture2D m_weaponCursor;
    [SerializeField] private Texture2D m_architectCursor;
    private Vector2 m_weaponHtSpt;
    private Vector2 m_architectHtSpt;

    private void Start()
    {
        m_player = FindObjectOfType<Player>();
        instance = this;
        m_weaponHtSpt = new Vector2(m_weaponCursor.width / 2.5f, m_weaponCursor.height / 2.5f);
        m_architectHtSpt = new Vector2(m_architectCursor.width / 2.5f, m_architectCursor.height / 2.5f);
    }

    void Update()
    {
        if(m_player.CurrentMode == PLAYER_MODE.WEAPON)
        {
            Cursor.SetCursor(m_weaponCursor, m_weaponHtSpt, CursorMode.Auto);
        }
        else if (m_player.CurrentMode == PLAYER_MODE.ARCHITECT) 
        {
            Cursor.SetCursor(m_architectCursor, m_architectHtSpt, CursorMode.Auto);
        }
    }
}