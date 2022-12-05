using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private WaveSpawner m_waveSpawner;
    private BuildingSystem m_buildingSystem;

    [SerializeField] private Text timerText;

    [Header("Fill Images")]
    [SerializeField] private Image attackFillImage;
    [SerializeField] private Image dashFillImage;
    [Header("Turrets")]
    [SerializeField] private List<Image> turretsImageList;
    [SerializeField] private List<Sprite> turretsIconList;
    [SerializeField] private List<string> turretsNameList;
    [SerializeField] private Text currentTurretNameText;

    private Player m_player;

    void Start()
    {
        m_waveSpawner = FindObjectOfType<WaveSpawner>();
        m_buildingSystem = FindObjectOfType<BuildingSystem>();
        m_player = FindObjectOfType<Player>();

        //Fill abilities UI
        attackFillImage.fillAmount = m_player.GetAttackCooldown;
        dashFillImage.fillAmount = m_player.GetDashCooldown;
    }

    void Update()
    {
        //Wave Timer
        if (m_waveSpawner.WavesLeft != 0)
        { timerText.text = "Next wave in: " + string.Format("{00:00:00}", m_waveSpawner.NextWaveTime); }
        else { timerText.text = "Survive!"; timerText.alignment = TextAnchor.MiddleCenter; }

        //Abilities Fill
        attackFillImage.fillAmount = m_player.GetAttackCooldown;
        dashFillImage.fillAmount = m_player.GetDashCooldown;

        //Turrets Selection Icons
        int turretNum = m_buildingSystem.GetCurrentStructureNum;
        for (int i = 0; i < turretsImageList.Count; i++)
        {
            turretsImageList[i].sprite = turretsIconList[(turretNum + i) % turretsIconList.Count];
        }
        currentTurretNameText.text = turretsNameList[turretNum];
    }
}