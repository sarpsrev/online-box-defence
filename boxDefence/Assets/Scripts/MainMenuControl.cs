using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuControl : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;
    public InputField usernameString;
    public Text selectedUsername;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("usernamess")){
           firstPanel.SetActive(true);
        } else{
        secondPanel.SetActive(true);
        selectedUsername.text = PlayerPrefs.GetString("usernamess");
            
        }
        
    }

    public void saveUsername(){

        
        PlayerPrefs.SetString("usernamess",usernameString.text);
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
        selectedUsername.text = PlayerPrefs.GetString("usernamess");



    }


}
