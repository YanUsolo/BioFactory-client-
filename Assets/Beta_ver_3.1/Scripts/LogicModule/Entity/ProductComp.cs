using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductComp : MonoBehaviour {

    int fk_id_product;

    string id_productComp ;
    

    //по этапно заргуженные тонны
    LinkedList<float> dayMassList = new LinkedList<float>();
    LinkedList<float> nightMassList = new LinkedList<float>();

    //загруженно тонн
    public float dayMass;
    public float nightMass;

    //с.в. из загруженных тонн
    public float dayMassSV;
    public float nightMassSV;

    //воды их этих тонн
    public float dayMassWater;
    public float nightMassWater;

    //метана из этих тонн
    public float metanDay;
    public float metanNight;

    public ProductComp(int id_product)
    {
        this.fk_id_product = id_product;
        this.id_productComp = id_product + "_2";
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public int get_fk_id()
    {
        return this.fk_id_product; 
    }

    public string toString()
    {
        return "ProductComp {\n" +
            "fk_id_product : " + fk_id_product + "\n" +
            "-----Day-----\n" +
            "dayMass" + dayMass + "\n" +
            "dayMassSV" + dayMassSV + "\n" +
            "dayMassWater" + dayMassWater + "\n" +
            "metanDay" + metanDay + "\n" +
            "-----Night-----\n" +
            "nightMassList" + nightMassList + "\n" +
            "nightMass" + nightMass + "\n" +
            "nightMassSV" + nightMassSV + "\n" +
            "nightMassWater" + nightMassWater + "\n" +
            "metanNight" + metanNight + "\n";
    }


}
