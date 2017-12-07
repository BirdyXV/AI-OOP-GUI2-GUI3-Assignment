using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DragAndDropChest : MonoBehaviour
{
    #region Variables
    [Header("Inventory")]
    public bool showChest;
    public List<Item> inventory = new List<Item>();
    public int slotX, slotY;
    private Rect inventorySize;

    [Header("Dragging")]
    public bool dragging;
    public Item draggedItem;
    public int draggedFrom;
    public GameObject droppedItem;

    [Header("Tool Tip")]
    public int toolTipItem;
    public bool showToolTip;
    private Rect toolTipRect;
    [Header("References and Locations")]
    public FirstPersonController playerMove;
    private float scrW, scrH;
    DragAndDropInventory inv;

    #endregion
    #region Clamp to Screen
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
    #endregion
    #region Add Item
    public void AddItem(int ID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Name == null)
            {
                inventory[i] = ItemGen.CreateItem(ID);
                Debug.Log(inventory[i].Name + " was added");
                return;
            }
        }
    }
    #endregion
    #region Drop Item
    public void DropItem(int ID)
    {
        droppedItem = Resources.Load("Prefabs/" + ItemGen.CreateItem(ID).Mesh) as GameObject;
        Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity);
        return;
    }
    #endregion
    #region Draw Item
    void DrawItem(int windowID)
    {
        if (draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scrW * 0.5f, scrH * 0.5f), draggedItem.Icon);
        }
    }
    #endregion
    #region Tool Tip
    #region Tool Tip Content
    private string ToolTipContent(int ID)
    {
        string toolTipString =
            "Name: " + inventory[ID].Name +
            "\nDescription: " + inventory[ID].Description +
            "\nType: " + inventory[ID].Type +
            "\nID: " + inventory[ID].ID;
        return toolTipString;
    }
    #endregion
    #region Tool Tip Window
    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scrW * 2, scrH * 3), ToolTipContent(toolTipItem));
    }
    #endregion
    #endregion
    #region Drag Inventory
    void InvetoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, 0.25f * scrH, 6 * scrW, 0.5f * scrH), "Banner");
        GUI.Box(new Rect(0, 4.25f * scrH, 6 * scrW, 0.5f * scrH), "Gold and Exp");
        showToolTip = false;

        #region Nested For Loop
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(0.125f * scrW + x * (scrW * 0.75f), 0.75f * scrH + y * (scrH * 0.65f), scrW * 0.75f, scrH * 0.65f);
                GUI.Box(slotLocation, "");

                #region Pickup Item
                if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !dragging && inventory[i].Name != null)
                {
                    draggedItem = inventory[i];
                    inventory[i] = new Item();
                    dragging = true;
                    draggedFrom = i;
                    Debug.Log("Dragging: " + draggedItem.Name);
                }
                #endregion
                #region Swap Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && dragging && inventory[i].Name != null)
                {
                    Debug.Log("Swapping: " + draggedItem.Name + "With: " + inventory[i].Name);
                    inventory[draggedFrom] = inventory[i];
                    inventory[i] = draggedItem;
                    dragging = false;
                    draggedItem = new Item(); ;
                }
                #endregion
                #region Place Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && dragging && inventory[i].Name == null)
                {
                    Debug.Log("Place: " + draggedItem.Name + "Into: " + i);
                    inventory[i] = draggedItem;
                    dragging = false;
                    draggedItem = new Item(); ;
                }
                #endregion
                #region Return Item
                if (e.button == 0 && e.type == EventType.MouseUp && i == ((slotX * slotY) - 1) && dragging)
                {
                    Debug.Log("Return: " + draggedItem.Name + "Into: " + draggedFrom);
                    inventory[draggedFrom] = draggedItem;
                    dragging = false;
                    draggedItem = new Item(); 
                }
                #endregion
                #region Draw Item Icon
                if (inventory[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inventory[i].Icon);
                    #region Set ToolTip on Mouse
                    if (slotLocation.Contains(e.mousePosition) && !dragging & showChest)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }

        #endregion
        #region Drag Window
        GUI.DragWindow(new Rect(0 * scrW, 0 * scrH, 6 * scrW, 0.5f * scrH));
        GUI.DragWindow(new Rect(0 * scrW, 0.5f * scrH, 0.25f * scrW, 3.5f * scrH));
        GUI.DragWindow(new Rect(5.5f * scrW, 0.5f * scrH, 0.25f * scrW, 3.5f * scrH));
        GUI.DragWindow(new Rect(0 * scrW, 4 * scrH, 0.25f * scrW, 3.5f * scrH));
        #endregion
    }
    #endregion
    #region Start
    void Start()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        playerMove = GetComponent<FirstPersonController>();
        inventorySize = new Rect(10 * scrW, scrH, 6 * scrW, 4.5f * scrH);
        for (int i = 0; i < slotX * slotY; i++)
        {
            inventory.Add(new Item());
        }
        AddItem(300);
        AddItem(301);
        AddItem(400);
        AddItem(401);
        AddItem(500);

    }
    #endregion
    #region Toggle Inventory and Control
    public bool ToggleInv()
    {
        if (showChest)
        {
            showChest = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            playerMove.enabled = false;
            return (false);
        }
        else
        {
            showChest = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerMove.enabled = true;
            return (true);
        }
    }
    #endregion
    #region Update
    void Update()
    {

        //if our interact key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            //create a ray
            Ray interact;
            //this ray is shooting out from the main cameras screen point center of screen
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info
            RaycastHit hitinfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interact, out hitinfo, 10))
            {
                #region Chest
                //and that hits info is tagged NPC
                if (hitinfo.collider.CompareTag("Chest"))
                {
                    ToggleInv();
                }
                #endregion
            }
        }
    }
    #endregion
    #region OnGUI
    void OnGUI()
    {
        Event e = Event.current;
        #region Draw Inventory if showInv is true
        if (showChest)
        {
            inventorySize = ClampToScreen(GUI.Window(4, inventorySize, InvetoryDrag, "My Inventory"));
        }
        #endregion
        #region Draw ToolTip
        if (showToolTip && showChest)
        {
            toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.01f, scrW * 2, scrH * 3);
            GUI.Window(6, toolTipRect, DrawToolTip, "");
        }

        #endregion
        #region Drop Item on not show Inventory and Mouse is up
        if (e.button == 0 && e.type == EventType.MouseUp && dragging)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Incase inventory closes drop dragged item
        if (e.button == 0 && e.type == EventType.MouseUp && dragging && !showChest)
        {
            DropItem(draggedItem.ID);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Draw Item on Mouse
        if (dragging)
        {
            if (draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scrW * 0.5f, scrH * 0.5f);
                GUI.Window(5, mouseLocation, DrawItem, "");
            }
        }
        #endregion
    }
    #endregion
}
