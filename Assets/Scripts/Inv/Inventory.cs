using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Variables Eskeetit
    [Header("Inventory")]
    public List<Item> inv = new List<Item>();
    public Item selectedItem;
    [Header("Bools And Values")]
    public bool showInv;
    public int money;
    [Header("References And Locations")]
    public Vector2 scrollPos = Vector2.zero;
    public MouseLook mainCam, playerCam;
    public Movement playerMove;
    public Transform wHandler, hHandler;
    public GameObject helm, weapon;
    #endregion

    void Start()
    {
        mainCam = Camera.main.GetComponent<MouseLook>();
        playerCam = GetComponent<MouseLook>();
        playerMove = GetComponent<Movement>();
        wHandler = GameObject.FindGameObjectWithTag("WeaponHandler").GetComponent<Transform>();
        hHandler = GameObject.FindGameObjectWithTag("HeadHandler").GetComponent<Transform>();

        // Add all items here lol
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(1));
        inv.Add(ItemGen.CreateItem(100));
        inv.Add(ItemGen.CreateItem(101));
        inv.Add(ItemGen.CreateItem(102));
        inv.Add(ItemGen.CreateItem(103));
        inv.Add(ItemGen.CreateItem(200));
        inv.Add(ItemGen.CreateItem(201));
        inv.Add(ItemGen.CreateItem(202));
        inv.Add(ItemGen.CreateItem(203));
        inv.Add(ItemGen.CreateItem(204));
        inv.Add(ItemGen.CreateItem(205));
        inv.Add(ItemGen.CreateItem(206));
        inv.Add(ItemGen.CreateItem(207));
        inv.Add(ItemGen.CreateItem(208));
        inv.Add(ItemGen.CreateItem(209));
        inv.Add(ItemGen.CreateItem(210));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
        inv.Add(ItemGen.CreateItem(0));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInv();
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            inv.Add(ItemGen.CreateItem(0));
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            inv.Remove(inv[inv.Count]);
        }

    }

    public bool ToggleInv()
    {
        if (showInv)
        {
            showInv = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mainCam.enabled = true;
            playerCam.enabled = true;
            playerMove.enabled = true;
            return (false);
        }
        else
        {
            showInv = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainCam.enabled = false;
            playerCam.enabled = false;
            playerMove.enabled = false;
            return (true);
        }
    }

    void OnGUI()
    {
        if (showInv)
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;

            // BG labelled Inventory
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory");

            // IF we have less than or 35 items then no scroll view
            if (inv.Count <= 35)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scrW, 0.25f * scrH + i * (0.25f * scrH), 3 * scrW, 0.25f * scrH), inv[i].Name))
                    {
                        selectedItem = inv[i];
                        Debug.Log(selectedItem.Name);
                    }
                }
            }
            // ELSE IF we have more than 35 items we can scroll
            else
            {
                scrollPos = GUI.BeginScrollView(new Rect(0, 0, 6 * scrW, 9 * scrH), scrollPos, new Rect(0, 0, 0, 9 * scrH + ((inv.Count - 35) * 0.25f * scrH)), false, true);
                for (int i = 0; i < inv.Count; i++)
                {
                    if (GUI.Button(new Rect(0.5f * scrW, 0.25f * scrH + i * (0.25f * scrH), 3 * scrW, 0.25f * scrH), inv[i].Name))
                    {
                        selectedItem = inv[i];
                        Debug.Log(selectedItem.Name);
                    }
                }
                GUI.EndScrollView();
            }
            if (selectedItem != null)
            {
                if (selectedItem.Type == ItemType.Food)
                {
                    GUI.Box(new Rect(8 * scrW, 5 * scrH, 8 * scrW, 3 * scrH), selectedItem.Name + "\n" + selectedItem.Description + "\n" + "Value: $" + selectedItem.Value);
                    GUI.DrawTexture(new Rect(11 * scrW, 1.5f * scrH, 2 * scrW, 2 * scrH), selectedItem.Icon);
                    if (GUI.Button(new Rect(15 * scrW, 8.25f * scrH, scrW, 0.25f * scrH), "Eat"))
                    {
                        Debug.Log("Yummy" + selectedItem.Name);
                        inv.Remove(selectedItem);
                        selectedItem = null;
                    }
                }
                else if (selectedItem.Type == ItemType.Weapon)
                {
                    GUI.Box(new Rect(8 * scrW, 5 * scrH, 8 * scrW, 3 * scrH), selectedItem.Name + "\n" + selectedItem.Description + "\n" + "Value: $" + selectedItem.Value);
                    GUI.DrawTexture(new Rect(11 * scrW, 1.5f * scrH, 2 * scrW, 2 * scrH), selectedItem.Icon);
                    if (GUI.Button(new Rect(15 * scrW, 8.25f * scrH, scrW, 0.25f * scrH), "Equip"))
                    {
                        Debug.Log("Weapon" + selectedItem.Name);
                        if (weapon != null)
                        {
                            Destroy(weapon);
                        }
                        weapon = Instantiate(SpawnItem(selectedItem.Name), wHandler.position, wHandler.rotation, wHandler);
                        selectedItem = null;
                    }
                }
                else if (selectedItem.Type == ItemType.Apparel)
                {
                    GUI.Box(new Rect(8 * scrW, 5 * scrH, 8 * scrW, 3 * scrH), selectedItem.Name + "\n" + selectedItem.Description + "\n" + "Value: $" + selectedItem.Value);
                    GUI.DrawTexture(new Rect(11 * scrW, 1.5f * scrH, 2 * scrW, 2 * scrH), selectedItem.Icon);
                    if (GUI.Button(new Rect(15 * scrW, 8.25f * scrH, scrW, 0.25f * scrH), "Equip"))
                    {
                        Debug.Log("Helmet" + selectedItem.Name);
                        if (weapon != null)
                        {
                            Destroy(helm);
                        }
                        helm = Instantiate(SpawnItem(selectedItem.Name), hHandler.position, hHandler.rotation, hHandler);
                        selectedItem = null;
                    }
                }
            }
        }//inv goes above this
    }
    public GameObject SpawnItem(string ItemName)
    {
        GameObject spawn = Resources.Load("Prefabs/" + selectedItem.Name) as GameObject;
        return spawn;
    }
    void Mesh(string meshName)
    {
        Resources.Load("Prefabs/" + selectedItem.Name);
    }
}
