using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUIWork
{
    public class CustomisationGet : MonoBehaviour
    {

        [Header("Character")]
        public Renderer character;
        public string myClass;

        [Header("Character Base Stats")]
        public int strength;
        public int constitution;
        public int intelligence;
        public int dexterity;
        public int wisdom;
        public int charisma;

        [Header("Character Stats")]
        public float health;
        public float mana;
        public float stamina;

        #region Start
        void Start()
        {
            //our character reference connected to the Skinned Mesh Renderer via finding the Mesh
            //Run the function LoadTexture	
            character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
            LoadTexture();
            LoadCharStats();
            SetChanges();
        }
        #endregion

        public void LoadCharStats()
        {
            gameObject.name = PlayerPrefs.GetString("CharacterName");  // grab the gameObject in scene that is our character and set its Object name to the Characters name
            myClass = PlayerPrefs.GetString("ClassName");
            strength = PlayerPrefs.GetInt("StrengthPts");
            constitution = PlayerPrefs.GetInt("ConstitutionPts");
            intelligence = PlayerPrefs.GetInt("IntelligencePts");
            dexterity = PlayerPrefs.GetInt("DexterityPts");
            wisdom = PlayerPrefs.GetInt("WisdomPts");
            charisma = PlayerPrefs.GetInt("CharismaPts");
        }

        #region LoadTexture Function
        public void LoadTexture()
        {
            if (!PlayerPrefs.HasKey("CharacterName"))       // check to see if PlayerPrefs (our save location) HasKey (has a save file...you will need to reference the name of a file)
            {
                SceneManager.LoadScene("CustomSet");        // if it doesnt then load the CustomSet level
            }

            // if it does have a save file then load and SetTexture Skin, Hair, Mouth and Eyes from PlayerPrefs
            SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
            SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
            SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
            SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
            SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
            SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
        }
        #endregion

        // Create a function that is called SetTexture it should contain a string and int
        // the string is the name of the material we are editing, the int is the direction we are changing
        #region SetTexture
        public void SetTexture(string type, int dir)
        {
            //we need variables that exist only within this function
            //these are int material index and Texture2D array of textures
            Texture2D tex = null;
            int matIndex = 0;

            //inside a switch statement that is swapped by the string name of our material
            //case skin
            switch (type)
            {
                case "Skin":
                    tex = Resources.Load("Character/Skin_" + dir.ToString()) as Texture2D;      // textures is our Resource.Load Character Skin save index we loaded in set as our Texture2D
                    matIndex = 1;                                                               // material index element number is 1
                    break;                                                                      // break
                case "Hair":                                                                    // now repeat for each material
                    tex = Resources.Load("Character/Hair_" + dir.ToString()) as Texture2D;
                    matIndex = 2;                                                               // hair is 2
                    break;
                case "Mouth":
                    tex = Resources.Load("Character/Mouth_" + dir.ToString()) as Texture2D;
                    matIndex = 3;                                                               // mouth is 3
                    break;
                case "Eyes":
                    tex = Resources.Load("Character/Eyes_" + dir.ToString()) as Texture2D;
                    matIndex = 4;                                                               // eyes are 4
                    break;
                case "Armour":
                    tex = Resources.Load("Character/Armour_" + dir.ToString()) as Texture2D;
                    matIndex = 5;                                                               // armour is 5
                    break;
                case "Clothes":
                    tex = Resources.Load("Character/Clothes_" + dir.ToString()) as Texture2D;
                    matIndex = 6;                                                               // clothes are 6
                    break;
            }

            Material[] mats = character.materials;                                              // Material array is equal to our characters material list
            mats[matIndex].mainTexture = tex;                                                   // our material arrays current material index's main texture is equal to our texture arrays current index
            character.materials = mats;                                                         // our characters materials are equal to the material array
        }
        #endregion

        #region SetChanges
        public void SetChanges()
        {
            health = (10 * strength) + (12 * constitution); // If I add strength or constition my health stat will be multiplied by the value given
            mana = (4 * wisdom) + (5 * intelligence); // If I add wisdom or intelligence my mana stat will be multiplied by the value given
            stamina = (20 * charisma) + (30 * dexterity) + (5 * constitution); // If I add charisma, dexterity or constitution my stamina stat will be multiplied by the value given
        }
        #endregion

    }
}