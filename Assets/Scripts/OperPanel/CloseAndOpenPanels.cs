using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAndOpenPanels : MonoBehaviour {

    public GameObject Panel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenPanel()
    {
        this.transform.SetAsLastSibling();
        gameObject.GetComponent<Animator>().SetBool("isOpen",true);
       
    }

    public void ClosePanel()
    {
        gameObject.GetComponent<Animator>().SetBool("isOpen", false);
    }
}
