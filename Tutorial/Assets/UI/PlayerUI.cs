using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] PlayerStats playerStats;
    public GameObject gameUI;
    public GameObject dialogueInterface;
    [SerializeField] GameObject pauseMenuObject;
    [SerializeField] GameObject menuObject;

    public GameObject speak;
    public GameObject reply;
    
    [SerializeField] GameObject inventoryTab;
    [SerializeField] GameObject statsTab;

    public TextMeshProUGUI strength;
    public TextMeshProUGUI melee;
    public TextMeshProUGUI unarmed;

    public TextMeshProUGUI dexterity;
    public TextMeshProUGUI accuracy;
    public TextMeshProUGUI ranged;
    public TextMeshProUGUI sleightOfHand;
    public TextMeshProUGUI stealth;

    public TextMeshProUGUI constitution;
    public TextMeshProUGUI defense;
    public TextMeshProUGUI willpower;

    public TextMeshProUGUI intelligence;
    public TextMeshProUGUI crafting;
    public TextMeshProUGUI investigation;
    public TextMeshProUGUI magic;
    public TextMeshProUGUI medicine;

    public TextMeshProUGUI charisma;
    public TextMeshProUGUI barter;
    public TextMeshProUGUI deception;
    public TextMeshProUGUI intimidation;
    public TextMeshProUGUI persuasion;

    [SerializeField] GameObject questsTab;

    [SerializeField] GameObject mapTab;
    
    List<GameObject> menuTabs;
    int menuTab = 1;

    public MouseLook mouseLook;

    [SerializeField] Image healthBar;
    [SerializeField] Image magicBar;
    [SerializeField] Image staminaBar;

    [SerializeField] float lerpDuration = 1f;

    private void Awake()
    {
        menuTabs = new List<GameObject>();
        menuTabs.Add(inventoryTab);
        menuTabs.Add(statsTab);
        menuTabs.Add(questsTab);
        menuTabs.Add(mapTab);
    }

    public void Start()
    {
        mouseLook.ToggleMouseLock();

        pauseMenuObject.SetActive(false);
        menuObject.SetActive(false);
        dialogueInterface.SetActive(false);

        gameManager.SetGameState(GameManager.GameState.Running);

        inventoryTab.SetActive(false);
        statsTab.SetActive(false);
        questsTab.SetActive(false);
        mapTab.SetActive(false);
        for(int i = 0; i < menuTabs.Count; i++)
        {
            menuTabs[i].SetActive(false);
        }
        
    }

    //Dialogue Interface
    public void ToggleDialogue()
    {
        gameUI.SetActive(false);
        dialogueInterface.SetActive(true);
        speak.SetActive(true);
        reply.SetActive(false);
        gameManager.SetGameState(GameManager.GameState.Dialogue);
        mouseLook.ToggleMouseLock();
    }
    public void ToggleSpeak()
    {
        if (speak.activeSelf) {speak.SetActive(false); reply.SetActive(true);}
        else if (reply.activeSelf) {speak.SetActive(true); reply.SetActive(false);}
    }

    //Menu
    public void TogglePauseMenu()
    {
        if (pauseMenuObject.activeSelf) {pauseMenuObject.SetActive(false); gameManager.SetGameState(GameManager.GameState.Running);}
        else if (!menuObject.activeSelf) {pauseMenuObject.SetActive(true); gameManager.SetGameState(GameManager.GameState.PauseMenu);}
        else return;
        mouseLook.ToggleMouseLock();
    }
    public void ToggleMenu()
    {
        if (menuObject.activeSelf) {menuObject.SetActive(false); gameManager.SetGameState(GameManager.GameState.Running);}
        else if (!pauseMenuObject.activeSelf) {menuObject.SetActive(true); SetMenuTab(0); gameManager.SetGameState(GameManager.GameState.TabMenu);}
        else return;
        mouseLook.ToggleMouseLock();
    }
    public void SetMenuTab(int newTab) // 0 = Inventory, 1 = Stats, 2 = Quests, 3 = Map
    {
        if (newTab == menuTab) return;
        menuTabs[menuTab].SetActive(false);
        menuTabs[newTab].SetActive(true);
        menuTab = newTab;

    }
    public void SetStatsText()
    {
        strength.SetText("Strength: " + playerStats.strength.GetValue());
        melee.SetText("Melee: " + playerStats.skill_Melee.GetValue());
        unarmed.SetText("Unarmed: " + playerStats.skill_Unarmed.GetValue());

        dexterity.SetText("Dexterity: " + playerStats.dexterity.GetValue());
        accuracy.SetText("Accuracy: " + playerStats.skill_Accuracy.GetValue());
        ranged.SetText("Ranged: " + playerStats.skill_Ranged.GetValue());
        sleightOfHand.SetText("Sleight Of Hand: " + playerStats.skill_SleightOfHand.GetValue());
        stealth.SetText("Stealth: " + playerStats.skill_Stealth.GetValue());

        constitution.SetText("Constitution: " + playerStats.constitution.GetValue());
        defense.SetText("Defense: " + playerStats.skill_Defense.GetValue());
        willpower.SetText("Willpower: " + playerStats.skill_Willpower.GetValue());

        intelligence.SetText("Intelligence: " + playerStats.intelligence.GetValue());
        crafting.SetText("Crafting: " + playerStats.skill_Crafting.GetValue());
        investigation.SetText("Observation: " + playerStats.skill_Observation.GetValue());
        magic.SetText("Magic: " + playerStats.skill_Magic.GetValue());
        medicine.SetText("Medicine: " + playerStats.skill_Medicine.GetValue());

        charisma.SetText("Charisma: " + playerStats.charisma.GetValue());
        barter.SetText("Barter: " + playerStats.skill_Barter.GetValue());
        deception.SetText("Deception: " + playerStats.skill_Deception.GetValue());
        intimidation.SetText("Intimidation: " + playerStats.skill_Intimidation.GetValue());
        persuasion.SetText("Persuasion: " + playerStats.skill_Persuasion.GetValue());
    }

    //UI Bars
    public void SetHealth(float amount)
    {
        amount = 1 / amount;
        StartCoroutine(BarLerp(healthBar, healthBar.fillAmount, amount));
    }
    public void SetMagic(float amount)
    {
        amount = 1 / amount;
        StartCoroutine(BarLerp(magicBar, magicBar.fillAmount, amount));
    }
    public void SetStamina(float amount)
    {
        amount = 1 / amount;
        StartCoroutine(BarLerp(staminaBar, staminaBar.fillAmount, amount));
    }
    IEnumerator BarLerp(Image valueToLerp, float startValue, float endValue)
    {
        float timeElapsed = 0;

        while (timeElapsed < lerpDuration)
        {
            valueToLerp.fillAmount = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        valueToLerp.fillAmount = endValue;
    }

}
