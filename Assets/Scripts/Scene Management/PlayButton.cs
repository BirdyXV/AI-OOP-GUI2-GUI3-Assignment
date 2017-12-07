using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void LoadScene() // LoadScene is needed to load scenes with canvas
    {
        Application.LoadLevel("Game"); // Loads game scene
    }
}
