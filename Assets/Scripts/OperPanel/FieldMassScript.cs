using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldMassScript : MonoBehaviour {

    public Text nameProduct;
    public Text summMass;
    public Dropdown massList;
    public Text massList_label;

    public GameObject self;

    public InputField addMass_inputField;
    // Use this for initialization
    public void Awake()
    {
        self = gameObject;
       // Debug.Log(gameObject.ToString());
    }

    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setMassListInDropDown(LinkedList<float> digitals)
    {
        massList.options.Clear();

        massList.options.Add(new Dropdown.OptionData("None"));
        foreach (float digital in digitals)
        {
            massList.options.Add(new Dropdown.OptionData(digital + ""));
        }
        massList.RefreshShownValue();

    }

    public void setMassInInputField()
    {
        if (massList.value != 0)
        {
            addMass_inputField.text = massList_label.text;
        }
        else
        {
            addMass_inputField.text = "";
        }
        
    }
}
