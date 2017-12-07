using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//you will need to change Scenes
using UnityEngine.SceneManagement;

namespace GUIWork
{
    public class CustomisationSet : MonoBehaviour
    {
        #region Variables
        [Header("Texture List")]
        // Texture2D List for armour, clothes, skin, hair, mouth, eyes
        public List<Texture2D> skin = new List<Texture2D>();
        public List<Texture2D> hair = new List<Texture2D>();
        public List<Texture2D> mouth = new List<Texture2D>();
        public List<Texture2D> eyes = new List<Texture2D>();
        public List<Texture2D> armour = new List<Texture2D>();
        public List<Texture2D> clothes = new List<Texture2D>();

        [Header("Index")]
        // Index numbers for our current armour, clothes, skin, hair, mouth, eyes textures
        public int skinIndex;
        public int hairIndex, mouthIndex, eyesIndex;
        public int armourIndex;
        public int clothesIndex;

        [Header("Renderer")]
        // Renderer for our character mesh so we can reference a material list
        public Renderer character;

        [Header("Max Index")]
        // max amount of skin, hair, mouth, eyes textures that our lists are filling with

        public int skinMax;
        public int hairMax, mouthMax, eyesMax, armourMax, clothesMax;

        [Header("Stats")]
        public int strPoints;
        public int conPoints, intPoints, dexPoints, wisPoints, chaPoints, skillPoints;

        private Vector2 scrollPosClass;
        private bool showClass;
        private int strPointsStart;
        private int conPointsStart, intPointsStart, dexPointsStart, wisPointsStart, chaPointsStart;

        // Bring in the PlayerClass structs - each class has a pre-allocated 30 pts total
        ClassTypes fire = new ClassTypes(8, 1, 10, 6, 4, 1, "Fire");
        ClassTypes water = new ClassTypes(10, 6, 4, 1, 8, 1, "Water");
        ClassTypes wind = new ClassTypes(5, 5, 5, 5, 5, 5, "Wind");
        ClassTypes earth = new ClassTypes(4, 3, 4, 3, 7, 7, "Earth");
        ClassTypes wood = new ClassTypes(6, 6, 6, 6, 4, 3, "Wood");
        ClassTypes ice = new ClassTypes(2, 10, 10, 5, 2, 1, "Ice");
        ClassTypes lightning = new ClassTypes(3, 3, 5, 5, 7, 8, "Lightning");
        ClassTypes rock = new ClassTypes(10, 10, 5, 2, 1, 2, "Rock");
        ClassTypes lightMagic = new ClassTypes(6, 2, 8, 8, 4, 2, "Light Magic");
        ClassTypes darkMagic = new ClassTypes(4, 2, 8, 8, 6, 2, "Dark Magic");
        ClassTypes poison = new ClassTypes(8, 8, 8, 6, 1, 1, "Poison");
        ClassTypes sand = new ClassTypes(10, 10, 1, 2, 5, 2, "Sand");

        public string myClass;

        [Header("Character Name")]
        //name of our character that the user is making
        public string charName = "Name Here";
        #endregion

        #region Start
        //in start we need to set up the following
        void Start()
        {
            SetDefaultPointsClass();
            #region for loop to pull textures from file
            //for loop looping from 0 to less than the max amount of skin textures we need
            for (int i = 0; i < skinMax; i++)
            {
                Texture2D temp = Resources.Load("Character/Skin_" + i.ToString()) as Texture2D;     //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
                skin.Add(temp);                                                                     //add our temp texture that we just found to the skin List
            }

            //for loop looping from 0 to less than the max amount of hair textures we need
            for (int i = 0; i < hairMax; i++)
            {
                Texture2D temp = Resources.Load("Character/Hair_" + i.ToString()) as Texture2D;     //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Hair_#
                hair.Add(temp);                                                                     //add our temp texture that we just found to the hair List
            }

            //for loop looping from 0 to less than the max amount of mouth textures we need
            for (int i = 0; i < mouthMax; i++)
            {
                Texture2D temp = Resources.Load("Character/Mouth_" + i.ToString()) as Texture2D;     //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Mouth_#
                mouth.Add(temp);                                                                     //add our temp texture that we just found to the mouth List
            }

            //for loop looping from 0 to less than the max amount of eye textures we need
            for (int i = 0; i < eyesMax; i++)
            {
                Texture2D temp = Resources.Load("Character/Eyes_" + i.ToString()) as Texture2D;     //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Eyes_#
                eyes.Add(temp);                                                                     //add our temp texture that we just found to the eyes List
            }

            //for loop looping from 0 to less than the max amount of armour textures we need
            for (int i = 0; i < armourMax; i++)
            {
                Texture2D temp = Resources.Load("Character/Armour_" + i.ToString()) as Texture2D;     //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Armour_#
                armour.Add(temp);                                                                     //add our temp texture that we just found to the armour List
            }

            //for loop looping from 0 to less than the max amount of clothes textures we need
            for (int i = 0; i < clothesMax; i++)
            {
                Texture2D temp = Resources.Load("Character/Clothes_" + i.ToString()) as Texture2D;    //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Clothes_#
                clothes.Add(temp);                                                                    //add our temp texture that we just found to the clothes List
            }
            #endregion
            character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();

            #region do this after making the function SetTexture
            //SetTexture skin, hair, mouth, eyes to the first texture 0
            SetTexture("Skin", 0);
            SetTexture("Hair", 0);
            SetTexture("Mouth", 0);
            SetTexture("Eyes", 0);
            SetTexture("Armour", 0);
            SetTexture("Clothes", 0);
            #endregion
        }
        #endregion

        #region SetTexture
        // Create a function that is called SetTexture it should contain a string and int
        // the string is the name of the material we are editing, the int is the direction we are changing
        void SetTexture(string type, int dir)
        {
            // We need variables that exist only within this function
            // these are ints index numbers, max numbers, material index and Texture2D array of textures
            int index = 0, max = 0, matIndex = 0;
            Texture2D[] textures = new Texture2D[0];

            //inside a switch statement that is swapped by the string name of our material

            #region Switch Material
            switch (type)
            {
                //case skin
                case "Skin":
                    index = skinIndex;//index is the same as our skin index
                    max = skinMax;//max is the same as our skin max
                    textures = skin.ToArray();//textures is our skin list .ToArray()
                    matIndex = 1;//material index element number is 1
                    break;//break

                //now repeat for each material
                //hair is 2
                case "Hair":
                    index = hairIndex;          //index is the same as our index  
                    max = hairMax;              //max is the same as our max
                    textures = hair.ToArray();  //textures is our list .ToArray()
                    matIndex = 2;               //material index element number is 2
                    break;                      //break

                case "Mouth":                   //mouth is 3
                    index = mouthIndex;         //index is the same as our index
                    max = mouthMax;             //max is the same as our max
                    textures = mouth.ToArray();                //textures is our list .ToArray()
                    matIndex = 3;               //material index element number is 3
                    break;                      //break

                case "Eyes":                //eyes are 4
                    index = eyesIndex;                //index is the same as our index
                    max = eyesMax;                //max is the same as our max
                    textures = eyes.ToArray();                //textures is our list .ToArray()
                    matIndex = 4;                //material index element number is 4
                    break;                //break

                case "Armour":                //armour is 5
                    index = armourIndex;                //index is the same as our index
                    max = armourMax;                //max is the same as our max
                    textures = armour.ToArray();                //textures is our list .ToArray()
                    matIndex = 5;                //material index element number is 5
                    break;                //break

                case "Clothes":                //Clothes are 6
                    index = clothesIndex;                //index is the same as our index
                    max = clothesMax;                //max is the same as our max
                    textures = clothes.ToArray();                //textures is our list .ToArray()
                    matIndex = 6;                //material index element number is 4
                    break;                //break
            }
            #endregion

            // outside our switch statement
            #region OutSide Switch 

            index += dir; //index plus equals our direction
                          //cap our index to loop back around if is is below 0 or above max take one

            if (index < 0)
            {
                index = max - 1;
            }

            if (index > max - 1)
            {
                index = 0;
            }

            Material[] mat = character.materials;               // Material array is equal to our characters material list
            mat[matIndex].mainTexture = textures[index];        // Our material arrays current material index's main texture is equal to our texture arrays current index
            character.materials = mat;                          // our characters materials are equal to the material array

            #endregion

            //create another switch that is goverened by the same string name of our material
            #region Set Material Switch
            switch (type)
            {
                //case skin
                case "Skin":
                    skinIndex = index;      // skin index equals our index
                    break;                  // break

                //case hair
                case "Hair":
                    hairIndex = index;      // hair index equals our index
                    break;                  // break

                //case mouth
                case "Mouth":
                    mouthIndex = index;     // mouth index equals our index
                    break;                  // break

                //case eyes
                case "Eyes":
                    eyesIndex = index;      // eyes index equals our index
                    break;                  // break

                //case armour
                case "Armour":
                    armourIndex = index;      // armour index equals our index
                    break;                  // break

                //case clothes
                case "Clothes":
                    clothesIndex = index;      // clothes index equals our index
                    break;                  // break

            }
            #endregion

        }

        #region Save
        void Save()//Function called Save this will allow us to save our indexes to PlayerPrefs
        {
            // SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex
            PlayerPrefs.SetInt("SkinIndex", skinIndex);
            PlayerPrefs.SetInt("HairIndex", hairIndex);
            PlayerPrefs.SetInt("MouthIndex", mouthIndex);
            PlayerPrefs.SetInt("EyesIndex", eyesIndex);
            PlayerPrefs.SetInt("ArmourIndex", armourIndex);
            PlayerPrefs.SetInt("ClothesIndex", clothesIndex);

            // SetInt for Strength, Constitution, Intelligence, Dexterity, Wisdom, Charisma
            PlayerPrefs.SetInt("StrengthPts", strPoints);
            PlayerPrefs.SetInt("ConstitutionPts", conPoints);
            PlayerPrefs.SetInt("IntelligencePts", intPoints);
            PlayerPrefs.SetInt("DexterityPts", dexPoints);
            PlayerPrefs.SetInt("WisdomPts", wisPoints);
            PlayerPrefs.SetInt("CharismaPts", chaPoints);

            // SetString CharacterName, ClassName
            PlayerPrefs.SetString("CharacterName", charName);
            PlayerPrefs.SetString("ClassName", myClass);
        }
        #endregion

        #region OnGUI
        //Function for our GUI elements
        void OnGUI()
        {
            //create the floats scrW and scrH that govern our 16:9 ratio
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;

            //create an int that will help with shuffling your GUI elements under eachother
            int i = 0;

            #region Skin
            if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))    //GUI button on the left of the screen with the contents <
            {
                SetTexture("Skin", -1); //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Skin");   //GUI Box or Label on the left of the screen with the contents Skin        
                                                                                                    //GUI button on the left of the screen with the contents >
            if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
            {
                SetTexture("Skin", 1); // when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            }
            //move down the screen with the int using ++ each grouping of GUI elements are moved using this
            i++;
            #endregion

            //set up same things for Hair, Mouth and Eyes
            #region Hair
            if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))    // GUI button on the left of the screen with the contents <
            {
                SetTexture("Hair", -1); //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Hair");   //GUI Box or Label on the left of the screen with the contents Hair        
                                                                                                    //GUI button on the left of the screen with the contents >
            if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
            {
                SetTexture("Hair", 1); // when pressed the button will run SetTexture and grab the Hair Material and move the texture index in the direction  1
            }
            //move down the screen with the int using ++ each grouping of GUI elements are moved using this
            i++;
            #endregion

            #region Mouth
            if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))    // GUI button on the left of the screen with the contents <
            {
                SetTexture("Mouth", -1); //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Mouth");   //GUI Box or Label on the left of the screen with the contents Mouth       
                                                                                                     //GUI button on the left of the screen with the contents >
            if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
            {
                SetTexture("Mouth", 1); // when pressed the button will run SetTexture and grab the Mouth Material and move the texture index in the direction  1
            }
            //move down the screen with the int using ++ each grouping of GUI elements are moved using this
            i++;
            #endregion

            #region Eyes
            if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))    // GUI button on the left of the screen with the contents <
            {
                SetTexture("Eyes", -1); //when pressed the button will run SetTexture and grab the Eyes Material and move the texture index in the direction  -1
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Eyes");   //GUI Box or Label on the left of the screen with the contents Eyes        
                                                                                                    //GUI button on the left of the screen with the contents >
            if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
            {
                SetTexture("Eyes", 1); // when pressed the button will run SetTexture and grab the Eyes Material and move the texture index in the direction  1
            }
            //move down the screen with the int using ++ each grouping of GUI elements are moved using this
            i++;
            #endregion

            #region Armour
            if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))    // GUI button on the left of the screen with the contents <
            {
                SetTexture("Armour", -1); //when pressed the button will run SetTexture and grab the Armour Material and move the texture index in the direction  -1
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Armour");   //GUI Box or Label on the left of the screen with the contents Armour        
                                                                                                      //GUI button on the left of the screen with the contents >
            if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
            {
                SetTexture("Armour", 1); // when pressed the button will run SetTexture and grab the Armour Material and move the texture index in the direction  1
            }
            //move down the screen with the int using ++ each grouping of GUI elements are moved using this
            i++;
            #endregion

            #region Clothes
            if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))    // GUI button on the left of the screen with the contents <
            {
                SetTexture("Clothes", -1); //when pressed the button will run SetTexture and grab the Clothes Material and move the texture index in the direction  -1
            }
            GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Clothes");   //GUI Box or Label on the left of the screen with the contents Clothes        
                                                                                                       //GUI button on the left of the screen with the contents >
            if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
            {
                SetTexture("Clothes", 1); // when pressed the button will run SetTexture and grab the Clothes Material and move the texture index in the direction  1
            }
            //move down the screen with the int using ++ each grouping of GUI elements are moved using this
            i++;
            #endregion

            #region Class
            GUI.Button(new Rect(10 * scrW, 0.5f * scrH, 5 * scrW, 0.5f * scrH), "Class"); // Class button



            if (GUI.Button(new Rect(10 * scrW, scrH * 1, 150, 30), "Fire")) // Creating button for warrior // Same for every other class type below
            {
                // Allocating points to a class type's points // Same for every other class type below
                strPoints = fire.strPoints;
                conPoints = fire.conPoints;
                intPoints = fire.intPoints;
                dexPoints = fire.dexPoints;
                wisPoints = fire.wisPoints;
                chaPoints = fire.chaPoints;
                SetStartingPoints();
                myClass = fire.className;
            }

            if (GUI.Button(new Rect(10 * scrW, scrH * 1.50f, 150, 30), "Water"))
            {
                strPoints = water.strPoints;
                conPoints = water.conPoints;
                intPoints = water.intPoints;
                dexPoints = water.dexPoints;
                wisPoints = water.wisPoints;
                chaPoints = water.chaPoints;
                SetStartingPoints();
                myClass = water.className;
            }

            if (GUI.Button(new Rect(10 * scrW, scrH * 2, 150, 30), "Wind"))
            {
                strPoints = wind.strPoints;
                conPoints = wind.conPoints;
                intPoints = wind.intPoints;
                dexPoints = wind.dexPoints;
                wisPoints = wind.wisPoints;
                chaPoints = wind.chaPoints;
                SetStartingPoints();
                myClass = wind.className;
            }

            if (GUI.Button(new Rect(10 * scrW, scrH * 2.5f, 150, 30), "Earth"))
            {
                strPoints = earth.strPoints;
                conPoints = earth.conPoints;
                intPoints = earth.intPoints;
                dexPoints = earth.dexPoints;
                wisPoints = earth.wisPoints;
                chaPoints = earth.chaPoints;
                SetStartingPoints();
                myClass = earth.className;
            }

            if (GUI.Button(new Rect(10 * scrW, scrH * 3, 150, 30), "Wood"))
            {
                strPoints = wood.strPoints;
                conPoints = wood.conPoints;
                intPoints = wood.intPoints;
                dexPoints = wood.dexPoints;
                wisPoints = wood.wisPoints;
                chaPoints = wood.chaPoints;
                SetStartingPoints();
                myClass = wood.className;
            }

            if (GUI.Button(new Rect(10 * scrW, scrH * 3.5f, 150, 30), "Ice"))
            {
                strPoints = ice.strPoints;
                conPoints = ice.conPoints;
                intPoints = ice.intPoints;
                dexPoints = ice.dexPoints;
                wisPoints = ice.wisPoints;
                chaPoints = ice.chaPoints;
                SetStartingPoints();
                myClass = ice.className;
            }

            if (GUI.Button(new Rect(12.5f * scrW, scrH * 1, 150, 30), "Lightning"))
            {
                strPoints = lightning.strPoints;
                conPoints = lightning.conPoints;
                intPoints = lightning.intPoints;
                dexPoints = lightning.dexPoints;
                wisPoints = lightning.wisPoints;
                chaPoints = lightning.chaPoints;
                SetStartingPoints();
                myClass = lightning.className;
            }

            if (GUI.Button(new Rect(12.5f * scrW, scrH * 1.5f, 150, 30), "Rock"))
            {
                strPoints = rock.strPoints;
                conPoints = rock.conPoints;
                intPoints = rock.intPoints;
                dexPoints = rock.dexPoints;
                wisPoints = rock.wisPoints;
                chaPoints = rock.chaPoints;
                SetStartingPoints();
                myClass = rock.className;
            }

            if (GUI.Button(new Rect(12.5f * scrW, scrH * 2, 150, 30), "Light Magic"))
            {
                strPoints = lightMagic.strPoints;
                conPoints = lightMagic.conPoints;
                intPoints = lightMagic.intPoints;
                dexPoints = lightMagic.dexPoints;
                wisPoints = lightMagic.wisPoints;
                chaPoints = lightMagic.chaPoints;
                SetStartingPoints();
                myClass = lightMagic.className;
            }

            if (GUI.Button(new Rect(12.5f * scrW, scrH * 2.5f, 150, 30), "Dark Magic"))
            {
                strPoints = darkMagic.strPoints;
                conPoints = darkMagic.conPoints;
                intPoints = darkMagic.intPoints;
                dexPoints = darkMagic.dexPoints;
                wisPoints = darkMagic.wisPoints;
                chaPoints = darkMagic.chaPoints;
                SetStartingPoints();
                myClass = darkMagic.className;
            }

            if (GUI.Button(new Rect(12.5f * scrW, scrH * 3, 150, 30), "Poison"))
            {
                strPoints = poison.strPoints;
                conPoints = poison.conPoints;
                intPoints = poison.intPoints;
                dexPoints = poison.dexPoints;
                wisPoints = poison.wisPoints;
                chaPoints = poison.chaPoints;
                SetStartingPoints();
                myClass = poison.className;
            }

            if (GUI.Button(new Rect(12.5f * scrW, scrH * 3.5f, 150, 30), "Sand"))
            {
                strPoints = sand.strPoints;
                conPoints = sand.conPoints;
                intPoints = sand.intPoints;
                dexPoints = sand.dexPoints;
                wisPoints = sand.wisPoints;
                chaPoints = sand.chaPoints;
                SetStartingPoints();
                myClass = sand.className;
            }



            i++;
            #endregion

            #region Random Reset
            //create 2 buttons one Random and one Reset
            //Random will feed a random amount to the direction
            if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Random"))
            {
                SetTexture("Skin", Random.Range(0, skinMax - 1));
                SetTexture("Hair", Random.Range(0, hairMax - 1));
                SetTexture("Mouth", Random.Range(0, mouthMax - 1));
                SetTexture("Eyes", Random.Range(0, eyesMax - 1));
                SetTexture("Armour", Random.Range(0, armourMax - 1));
                SetTexture("Clothes", Random.Range(0, clothesMax - 1));
            }

            //reset will set all to 0 both use SetTexture
            if (GUI.Button(new Rect(1.25f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Reset"))
            {
                // Stats reset
                SetDefaultPointsClass();

                SetTexture("Skin", skinIndex = 0);
                SetTexture("Hair", hairIndex = 0);
                SetTexture("Mouth", mouthIndex = 0);
                SetTexture("Eyes", eyesIndex = 0);
                SetTexture("Armour", armourIndex = 0);
                SetTexture("Clothes", clothesIndex = 0);
            }
            //move down the screen with the int using ++ each grouping of GUI elements are moved using this
            i++;
            #endregion

            #region Stats
            #region GUIStrength
            GUI.Box(new Rect(2.5f * scrW, scrH, 1.5f * scrW, 0.5f * scrH), "Strength"); // Creating a strength box
            if (GUI.Button(new Rect(4.0f * scrW, scrH, 0.5f * scrW, 0.5f * scrH), "<")) // IF button is active / creating a remove button (for skill points)
            {
                // IF strength points are greater than strength points at start
                if (strPoints > strPointsStart)
                {
                    // Remove 1 strength point
                    strPoints--;
                    // Add 1 skill point
                    skillPoints++;
                }
            }
            GUI.Box(new Rect(4.5f * scrW, scrH, 0.5f * scrW, 0.5f * scrH), strPoints.ToString()); // Allows you to convert integers into strings
            if (GUI.Button(new Rect(5.0f * scrW, scrH, 0.5f * scrW, 0.5f * scrH), ">")) // IF button is active / creating an add button (for skill points)
            {
                // IF skill points are less than 0
                if (skillPoints > 0)
                {
                    // Add 1 strength point
                    strPoints++;
                    // Remove 1 skill point
                    skillPoints--;
                }
            }
            #endregion
            // Commenting is the same for all other class types
            #region GUIConstitution
            GUI.Box(new Rect(2.5f * scrW, 1.5f * scrH, 1.5f * scrW, 0.5f * scrH), "Constitution");
            if (GUI.Button(new Rect(4.0f * scrW, 1.5f * scrH, 0.5f * scrW, 0.5f * scrH), "<"))
            {
                if (conPoints > conPointsStart)
                {
                    conPoints--;
                    skillPoints++;
                }
            }
            GUI.Box(new Rect(4.5f * scrW, 1.5f * scrH, 0.5f * scrW, 0.5f * scrH), conPoints.ToString());
            if (GUI.Button(new Rect(5.0f * scrW, 1.5f * scrH, 0.5f * scrW, 0.5f * scrH), ">"))
            {
                if (skillPoints > 0)
                {
                    conPoints++;
                    skillPoints--;
                }
            }
            #endregion
            #region GUIIntelligence
            GUI.Box(new Rect(2.5f * scrW, 2.0f * scrH, 1.5f * scrW, 0.5f * scrH), "Intelligence");
            if (GUI.Button(new Rect(4.0f * scrW, 2.0f * scrH, 0.5f * scrW, 0.5f * scrH), "<"))
            {
                if (intPoints > intPointsStart)
                {
                    intPoints--;
                    skillPoints++;
                }
            }
            GUI.Box(new Rect(4.5f * scrW, 2.0f * scrH, 0.5f * scrW, 0.5f * scrH), intPoints.ToString());
            if (GUI.Button(new Rect(5.0f * scrW, 2.0f * scrH, 0.5f * scrW, 0.5f * scrH), ">"))
            {
                if (skillPoints > 0)
                {
                    intPoints++;
                    skillPoints--;
                }
            }
            #endregion
            #region GUIDexterity
            GUI.Box(new Rect(2.5f * scrW, 2.5f * scrH, 1.5f * scrW, 0.5f * scrH), "Dexterity");
            if (GUI.Button(new Rect(4.0f * scrW, 2.5f * scrH, 0.5f * scrW, 0.5f * scrH), "<"))
            {
                if (dexPoints > dexPointsStart)
                {
                    dexPoints--;
                    skillPoints++;
                }
            }
            GUI.Box(new Rect(4.5f * scrW, 2.5f * scrH, 0.5f * scrW, 0.5f * scrH), dexPoints.ToString());
            if (GUI.Button(new Rect(5.0f * scrW, 2.5f * scrH, 0.5f * scrW, 0.5f * scrH), ">"))
            {
                if (skillPoints > 0)
                {
                    dexPoints++;
                    skillPoints--;
                }
            }
            #endregion
            #region GUIWisdom
            GUI.Box(new Rect(2.5f * scrW, 3.0f * scrH, 1.5f * scrW, 0.5f * scrH), "Wisdom");
            if (GUI.Button(new Rect(4.0f * scrW, 3.0f * scrH, 0.5f * scrW, 0.5f * scrH), "<"))
            {
                if (wisPoints > wisPointsStart)
                {
                    wisPoints--;
                    skillPoints++;
                }
            }
            GUI.Box(new Rect(4.5f * scrW, 3.0f * scrH, 0.5f * scrW, 0.5f * scrH), wisPoints.ToString());
            if (GUI.Button(new Rect(5.0f * scrW, 3.0f * scrH, 0.5f * scrW, 0.5f * scrH), ">"))
            {
                if (skillPoints > 0)
                {
                    wisPoints++;
                    skillPoints--;
                }
            }
            #endregion
            #region GUICharisma
            GUI.Box(new Rect(2.5f * scrW, 3.5f * scrH, 1.5f * scrW, 0.5f * scrH), "Charisma");
            if (GUI.Button(new Rect(4.0f * scrW, 3.5f * scrH, 0.5f * scrW, 0.5f * scrH), "<"))
            {
                if (chaPoints > chaPointsStart)
                {
                    chaPoints--;
                    skillPoints++;
                }
            }
            GUI.Box(new Rect(4.5f * scrW, 3.5f * scrH, 0.5f * scrW, 0.5f * scrH), chaPoints.ToString());
            if (GUI.Button(new Rect(5.0f * scrW, 3.5f * scrH, 0.5f * scrW, 0.5f * scrH), ">"))
            {
                if (skillPoints > 0)
                {
                    chaPoints++;
                    skillPoints--;
                }
            }
            #endregion
            // Creating skill points box
            GUI.Box(new Rect(2.5f * scrW, 4.5f * scrH, 1.5f * scrW, 0.5f * scrH), "Skill Points");
            GUI.Box(new Rect(4 * scrW, 4.5f * scrH, 0.5f * scrW, 0.5f * scrH), skillPoints.ToString());
            #endregion

            #region Character Name and Save & Play
            //name of our character equals a GUI TextField that holds our character name and limit of characters
            charName = GUI.TextField(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 12);
            //move down the screen with the int using ++ each grouping of GUI elements are moved using this
            i++;
            //GUI Button called Save and Play
            if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Save & Play"))
            {
                //this button will run the save function and also load into the game level
                if (skillPoints == 0 && name != "")
                {
                    Save(); // Call save 
                    SceneManager.LoadScene("Tutorial"); // Load tutorial scene
                }
            }
            #endregion
        }
        #endregion

        #region Setting Starting Points
        void SetStartingPoints()
        {
            // Setting how many skill points we have, to spend
            strPointsStart = strPoints;
            conPointsStart = conPoints;
            intPointsStart = intPoints;
            dexPointsStart = dexPoints;
            wisPointsStart = wisPoints;
            chaPointsStart = chaPoints;
            skillPoints = 10;
        }
        #endregion

        #region Setting Default Class
        void SetDefaultPointsClass()
        {
            // Setting the default class 
            skillPoints = 10;
            strPoints = fire.strPoints;
            conPoints = fire.conPoints;
            intPoints = fire.intPoints;
            dexPoints = fire.dexPoints;
            wisPoints = fire.wisPoints;
            chaPoints = fire.chaPoints;
            SetStartingPoints();
            myClass = fire.className;
        }
        #endregion
    }
    #endregion
}