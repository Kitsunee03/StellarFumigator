using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour
{
    private Player m_player;
    private BuildingSystem m_buildingSystem;

    [Header("Abilities")]
    [SerializeField] private Image attackFillImage;
    [SerializeField] private Image dashFillImage;

    [Header("Core")]
    [SerializeField] private Image coreHealthFillImage;
    [SerializeField] private Text coreHealthText;

    [Header("Turrets")]
    [SerializeField] private List<Image> turretsImageList;
    [SerializeField] private List<Sprite> turretsIconList;
    [SerializeField] private List<string> turretsNameList;
    [SerializeField] private Text currentTurretNameText;
    [SerializeField] private Text currentTurretPriceText;

    [Header("Gems")]
    [SerializeField] private Text gemsText;

    void Start()
    {
        m_buildingSystem = FindObjectOfType<BuildingSystem>();
        m_player = FindObjectOfType<Player>();

        //Fill abilities UI
        attackFillImage.fillAmount = m_player.GetAttackCooldown;
        dashFillImage.fillAmount = m_player.GetDashCooldown;
        coreHealthFillImage.fillAmount = GameStats.CoreHealth / GameStats.CoreMaxHealth;
    }

    void Update()
    {
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

        //Set Core Health
        coreHealthText.text = GameStats.CoreHealth.ToString() + "/" + GameStats.CoreMaxHealth.ToString() + " HP";
        coreHealthFillImage.fillAmount = GameStats.CoreHealth / (float)GameStats.CoreMaxHealth;
    }
}