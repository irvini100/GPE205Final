using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public Text myTextBox;
    public Text theText;
    

    // Use this for initialization
    void Start() {
        myTextBox.text = "UATanks!";
    }

    // Update is called once per frame
    void Update() {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ChangeText()
    {
        myTextBox.text = "Jump";
    }

    public void Save()
    {
        PlayerPrefs.SetString("PlayerData", theText.text);
        PlayerPrefs.Save();
    }

    public void Load ()
    {
        theText.text = PlayerPrefs.GetString("PlayerData");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
