using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

namespace GUIWork
{
    [AddComponentMenu("Character Setup/Character Handler")]
    public class CharacterHandler : MonoBehaviour
    {
        #region Variables
        [Header("Character")]
        public string charName;
        public string charClass;

        [Header("Health")]
        public float maxHealth;
        public float curHealth;

        [Header("Mana")]
        public float maxMana;
        public float curMana;

        [Header("Stamina")]
        public float maxStamina;
        public float curStamina;
        public FirstPersonController fPSController;

        [Header("GUI Section")]
        public GUIStyle healthTexture;
        public GUIStyle manaTexture, staminaTexture, damageFlashTexture;
        public bool showGUI = false;
        #endregion

        #region Start
        void Start()
        {
            StartCoroutine(addStamina()); // Starting Coroutine to add stamina overtime
            maxHealth = GetComponent<CustomisationGet>().health;            // setting health component to equal health variable
            maxMana = GetComponent<CustomisationGet>().mana;                // setting mana component to equal mana variable
            maxStamina = GetComponent<CustomisationGet>().stamina;          // setting stamina component to equal stamina variable
            charName = GetComponent<CustomisationGet>().name;               // setting name component to equal name variable
            charClass = GetComponent<CustomisationGet>().myClass;           // setting charClass to equal myClass variable
            curHealth = maxHealth;                                          // setting current health to max
            curMana = maxMana;                                              // setting current mana to max
            curStamina = maxStamina;                                        // setting current stamina to max
            fPSController = GetComponent<FirstPersonController>();
        }
        #endregion

        #region Update
        void Update()
        {
            if (curStamina == 0) // IF curStamina is set to to 0
            {
                fPSController.enabled = false; // Disable FPSController 
            }
        
            if (Input.GetKeyDown(KeyCode.E)) // IF you press E once
            {
                curHealth++; // Health stat will add 1 value from currentHealth
                curMana++; // Mana stat will add 1 value from currentMana
                curStamina++; // Stamina stat will add 1 value from currentStamina
            }

            if (Input.GetKeyDown(KeyCode.Q)) // IF you press Q once
            {
                showGUI = !showGUI; // showGUI eqauls to not showGUI
                curHealth--; // Health stat will remove 1 value from currentHealth
                curMana--; // Mana stat will remove 1 value from currentMana
            }

            if (Input.GetKey(KeyCode.LeftShift)) // IF you hold down shift 
            {
                curStamina--; // curStamina will reduce rapidly (don't know exact amount per second)
            }


            if (Input.GetKeyUp(KeyCode.Q)) // IF you release the Q button
            {
                showGUI = !showGUI; // showGUI will equal to not show GUI
            }
        }
        #endregion

        #region LateUpdate
        void LateUpdate()
{
    #region CappingHealth
    // IF our current health is greater than our max health
    if (curHealth > maxHealth)
    {
        // Then cap currrent health so it doesn't go above the max health
        curHealth = maxHealth;
    }

    // IF our current health is less than 0
    if (curHealth < 0)
    {
        // Then cap current health so it doesn't go below 0
        curHealth = 0;
    }
    #endregion
    #region CappingMana
    // IF our current mana is greater than our max mana
    if (curMana > maxMana)
    {
        // Then cap current mana so it doesn't go above the max mana
        curMana = maxMana;
    }

    // IF our current mana is less than 0
    if (curMana < 0)
    {
        // Then cap current mana so it doesn't go below 0
        curMana = 0;
    }
    #endregion
    #region CappingStamina
    // IF our current stamina is greater than our max stamina
    if (curStamina > maxStamina)
    {
        // Then cap current stamina so it doesn't go above the max stamina
        curStamina = maxStamina;
    }

    // IF our current stamina is less than 0
    if (curStamina < 0)
    {
        // Then cap current stamina so it doesn't go below 0
        curStamina = 0;
    }
    #endregion
}
#endregion

#region OnGUI
void OnGUI()
{
    // set up our aspect ratio for the GUI elements
    float scrW = Screen.width / 16;    //scrW - 16
    float scrH = Screen.height / 9;    //scrH - 9

    GUI.Box(new Rect(0.5f * scrW, 0.25f * scrH, 2.5f * scrW, 0.5f * scrH), charName + ""); // Display the name and class of the player's character

    // GUI Box on screen for the healthbar background
    GUI.Box(new Rect(6 * scrW, 0.25f * scrH, 4 * scrW, 0.5f * scrH), ""); // background

    // GUI Box for current health that moves in same place as the background bar
    // current Health divided by the posistion on screen and timesed by the total max health
    GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.5f * scrH), "", healthTexture); // Moving health bar

    // GUI Box on screen for the mana background
    GUI.Box(new Rect(6 * scrW, 0.75f * scrH, 4 * scrW, 0.5f * scrH), ""); // background

    GUI.Box(new Rect(6 * scrW, 0.75f * scrH, curMana * (4 * scrW) / maxMana, 0.5f * scrH), "", manaTexture);    // Moving mana bar

    // GUI Box on screen for the stamina background
    GUI.Box(new Rect(6 * scrW, 1.25f * scrH, 4 * scrW, 0.5f * scrH), ""); // background

    GUI.Box(new Rect(6 * scrW, 1.25f * scrH, curStamina * (4 * scrW) / maxStamina, 0.5f * scrH), "", staminaTexture);    // Moving stamina bar

    GUI.Box(new Rect(6 * scrW, 0.25f * scrH, scrW * 4, 0.5f * scrH), curHealth + "/" + maxHealth); // Health stats

    GUI.Box(new Rect(6 * scrW, 0.75f * scrH, scrW * 4, 0.5f * scrH), curMana + "/" + maxMana); // Mana stats

    GUI.Box(new Rect(6 * scrW, 1.25f * scrH, scrW * 4, 0.5f * scrH), curStamina + "/" + maxStamina); // Stamina stats

    if (!showGUI) // IF not showGUI is true
    {
        return; // Return true
    }

    GUI.Box(new Rect(0 * scrW, 0 * scrH, 16.5f * scrW, 9.5f * scrH), "", damageFlashTexture); // Red flash GUIBox
}
#endregion

IEnumerator addStamina() // Creating an IEnumerator so I can yield the result 
{
    while (true) // while this condition is true
    {
        if (curStamina < maxStamina) // IF curStamina is less than maxStamina
        {
            curStamina += 1; // Increase curStamina by 1 IN 0.1 seconds
            yield return new WaitForSeconds(0.0500f);
        }
        else
        {
            yield return null; // IF curStamina has reached maxStamina, yield and return null
        }
    }

}
    }

}




