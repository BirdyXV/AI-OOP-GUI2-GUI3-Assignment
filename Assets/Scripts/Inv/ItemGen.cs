using UnityEngine;

public class ItemGen : MonoBehaviour
{
    public static Item CreateItem(int itemID)
    {
        Item temp = new Item();
        #region Variables
        string name = "";
        int value = 0;
        string description = "";
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Food;
        #endregion
        #region Switch Item Data
        switch (itemID)
        {
            #region Food 0-99
            case 0:
                name = "Apple";
                value = 5;
                description = "Delicious Sweet";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;
            case 1:
                name = "Meat";
                value = 10;
                description = "Tasty Steak";
                icon = "Meat";
                mesh = "Meat";
                type = ItemType.Food;
                break;
            #endregion
            #region Weapon 100-199
            case 100:
                name = "Bow And Arrow";
                value = 45;
                description = "Shoot Down Your Enemies From Afar";
                icon = "Bow And Arrow";
                mesh = "Bow And Arrow";
                type = ItemType.Weapon;
                break;
            case 101:
                name = "Sword";
                value = 50;
                description = "Slash Your Enemies";
                icon = "Sword";
                mesh = "Sword";
                type = ItemType.Weapon;
                break;
            case 102:
                name = "Shield";
                value = 30;
                description = "Block Brute Force";
                icon = "Shield";
                mesh = "Shield";
                type = ItemType.Weapon;
                break;
            case 103:
                name = "Axe";
                value = 30;
                description = "Axe";
                icon = "Axe";
                mesh = "Axe";
                type = ItemType.Weapon;
                break;
            #endregion
            #region Apparel 200-299
            case 200:
                name = "Armour";
                value = 100;
                description = "Defend Yourself";
                icon = "Armour";
                mesh = "Armour";
                type = ItemType.Apparel;
                break;
            case 201:
                name = "Belt";
                value = 20;
                description = "Keep Your Armoured Pants Up";
                icon = "Belt";
                mesh = "Belt";
                type = ItemType.Apparel;
                break;
            case 202:
                name = "Boots";
                value = 30;
                description = "Comfy Shoes";
                icon = "Boots";
                mesh = "Boots";
                type = ItemType.Apparel;
                break;
            case 203:
                name = "Brace";
                value = 15;
                description = "Protect Your Wrists";
                icon = "Brace";
                mesh = "Brace";
                type = ItemType.Apparel;
                break;
            case 204:
                name = "Cloak";
                value = 75;
                description = "Hide In The Shadows";
                icon = "Cloak";
                mesh = "Cloak";
                type = ItemType.Apparel;
                break;
            case 205:
                name = "Glove";
                value = 25;
                description = "Get A Better Grip On Your Weapons";
                icon = "Glove";
                mesh = "Glove";
                type = ItemType.Apparel;
                break;
            case 206:
                name = "Helmet";
                value = 80;
                description = "Protect Your Skull";
                icon = "Helmet";
                mesh = "Helmet";
                type = ItemType.Apparel;
                break;
            case 207:
                name = "Pants";
                value = 60;
                description = "Comfy Pants";
                icon = "Pants";
                mesh = "Pants";
                type = ItemType.Apparel;
                break;
            case 208:
                name = "Shoulder";
                value = 35;
                description = "Shoulder Barge Enemies";
                icon = "Shoulder";
                mesh = "Shoulder";
                type = ItemType.Apparel;
                break;
            #endregion
            #region Crafting 300-399
            case 300:
                name = "Diamond";
                value = 1000;
                description = "Shiny Diamond";
                icon = "Diamond";
                mesh = "Diamond";
                type = ItemType.Crafting;
                break;
            case 301:
                name = "Ingot";
                value = 500;
                description = "Upgrade Your Armour Immensely";
                icon = "Ingot";
                mesh = "Ingot";
                type = ItemType.Crafting;
                break;
            #endregion
            #region Quest 400-499
            case 400:
                name = "Ring";
                value = 500;
                description = "A Ring That Enhances Your Power";
                icon = "Ring";
                mesh = "Ring";
                type = ItemType.Quest;
                break;
            case 401:
                name = "Necklace";
                value = 1000;
                description = "A Necklace That Enhances Your Luck";
                icon = "Necklace";
                mesh = "Necklace";
                type = ItemType.Quest;
                break;
            #endregion
            #region Money 500-599
            case 500:
                name = "Coins";
                value = 1;
                description = "Save Up Coins To Make Pearls";
                icon = "Coins";
                mesh = "Coins";
                type = ItemType.Money;
                break;
            #endregion
            #region Ingridients 600-699
            case 600:
                name = "Book";
                value = 250;
                description = "A Book Of Potion Tutorials";
                icon = "Book";
                mesh = "Book";
                type = ItemType.Ingredients;
                break;
            #endregion
            #region Potions 700-799
            case 700:
                name = "HP";
                value = 500;
                description = "Refills Your Health";
                icon = "HP";
                mesh = "HP";
                type = ItemType.Potions;
                break;
            case 701:
                name = "MP";
                value = 250;
                description = "Refills Your Mana";
                icon = "MP";
                mesh = "MP";
                type = ItemType.Potions;
                break;
            #endregion
            #region Scrolls 800-899
            case 801:
                name = "Scroll";
                value = 20;
                description = "A Map Of The World";
                icon = "Scroll";
                mesh = "Scroll";
                type = ItemType.Scrolls;
                break;
            #endregion
            default:
                itemID = 0;
                name = "Apple";
                value = 5;
                description = "Delicious Sweet";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;
        }
        #endregion
        #region Temp Connect
        temp.ID = itemID;
        temp.Name = name;
        temp.Value = value;
        temp.Description = description;
        temp.Icon = Resources.Load("Icons/" + icon) as Texture2D;
        temp.Mesh = mesh;
        temp.Type = type;
        #endregion
        return temp;
    }
}
