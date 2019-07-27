using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainActionScript : MonoBehaviour {

    private string passwordOfTech = "qwe";

    public GameObject techWindow;
    public GameObject operatorWindow;

    public Text passwordText;
    public GameObject passwordInputFieldGO;
    public GameObject mainWindow;
    public InputField passwordInputField;
   // Selectable.Transition transition =  new Selectable.Transition();

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}

  
    public void OpenTechPanel()
    {
        passwordInputFieldGO.SetActive(true);

        
    }
    public void OpenTechPanelInputExist()
    {
        if (passwordText.text.Equals(passwordOfTech))
        {
            passwordInputFieldGO.SetActive(false);

            clearInputField(passwordInputField);

            //passwordText.text.Remove(0, passwordOfTech.Length);
            techWindow.transform.SetAsLastSibling();
            techWindow.SetActive(true);

            //gameObjectSetActive(false);
            mainWindow.SetActive(false);
        }
    }

    public static void clearInputField(InputField inputfield)
    {
        inputfield.Select();
        inputfield.text = "";
    }


    public void changePassword(Text text)
    {
        if (!text.text.Equals(""))
        {
            passwordOfTech = text.text;
        }
      
    }

    public void viewPassword(Text placeHolder)
    {
        placeHolder.text = passwordOfTech;
    }

    public void OpenCloseOperatorWindow()
    {
        if (operatorWindow.activeSelf == false)
        {
            operatorWindow.SetActive(true);
            mainWindow.SetActive(false);
        }
        else {
            operatorWindow.SetActive(false);
            mainWindow.SetActive(true);
        }
    }

    public void OpenCloseTechWindow()
    {
        if (techWindow.activeSelf == false)
        {
            techWindow.SetActive(true);
            mainWindow.SetActive(false);
        }
        else
        {
            techWindow.SetActive(false);
            mainWindow.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
