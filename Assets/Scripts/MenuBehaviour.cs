using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuBehaviour : MonoBehaviour {
    GameObject[] menuItems;
    public bool menuShowing = false;
	// Use this for initialization
	void Start () {
        menuItems = GameObject.FindGameObjectsWithTag("Menu");
        ToggleMenu(menuShowing);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuShowing = !menuShowing;
            ToggleMenu(menuShowing);
        }
	}
    public void ToggleMenu(bool enabled)
    {
        foreach (GameObject item in menuItems)
        {
            item.SetActive(enabled);
        }
        menuShowing = enabled;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadLevel(int levelNo)
    {
        SceneManager.LoadScene(levelNo, LoadSceneMode.Single);
    }
}
