using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private BuildingSystem m_buildingSystem;
    private WaveSpawner m_waveSpawner;

    [SerializeField] private Text timerText;

    [Header("Fill Images")]
    [SerializeField] private Image attackFillImage;
    [SerializeField] private Image dashFillImage;
    [Header("Turrets")]
    [SerializeField] private List<Image> turretsImageList;
    [SerializeField] private List<Sprite> turretsIconList;
    [SerializeField] private List<string> turretsNameList;
    [SerializeField] private Text currentTurretNameText;
    [SerializeField] private Text currentTurretPriceText;

    [Header("Gems")]
    [SerializeField] private Text gemsText;

    private Player m_player;

    void Start()
    {
        m_buildingSystem = FindObjectOfType<BuildingSystem>();
        m_waveSpawner = FindObjectOfType<WaveSpawner>();
        m_player = FindObjectOfType<Player>();

        //Fill abilities UI
        attackFillImage.fillAmount = m_player.GetAttackCooldown;
        dashFillImage.fillAmount = m_player.GetDashCooldown;
    }

    void Update()
    {
        //Wave Timer
        if (m_waveSpawner.WavesLeft != 0)
        {
            timerText.text = "Next wave in: " + string.Format("{00:00:00}", m_waveSpawner.NextWaveTime);
        }
        else if (m_waveSpawner.EnemiesAlive > 0) { timerText.text = "Survive!"; timerText.alignment = TextAnchor.MiddleCenter; }
        else { timerText.text = "Return to base!"; timerText.alignment = TextAnchor.MiddleCenter; }

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
        //Turret price
        int turretPrice = m_buildingSystem.GetCurrentStructure.GetComponent<Turret>().TurretPrice;
        currentTurretPriceText.text = turretPrice.ToString();
        if (turretPrice > GameStats.Gems) { currentTurretPriceText.color = Color.red; }
        else { currentTurretPriceText.color = Color.white; }

        //Set Gems text
        gemsText.text = GameStats.Gems.ToString();
    }
}