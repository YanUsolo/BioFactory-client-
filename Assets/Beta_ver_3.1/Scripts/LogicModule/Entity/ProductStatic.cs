using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductStatic : MonoBehaviour {

    public int uniqueProduct_id;

    public string namePorduct = "";

    public float coffSV = 0.0f;//Кофф С.В.(сухого вещества)
    public float coffWater = 0.0f;// Кофф воды(1 - с.в.)

    public float coffMetana = 0.0f;//Вводится техником.
    public float valueGas = 0.0f;//Объем газа м3/т вводится техником 


    public ProductStatic(int id, string name, float coffSV,float coffWater,float coffMetana, float valueGas)
    {
        this.uniqueProduct_id = id;
        this.namePorduct = name;
        this.coffSV = coffSV;
        this.coffWater = coffWater;
        this.coffMetana = coffMetana;
        this.valueGas = valueGas;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getUniqueProduct_id()
    {
        return this.uniqueProduct_id;
    }

    public string toString()
    {
        return "ProductStatic {\n" +
            "uniqueProduct_id : " + uniqueProduct_id + "\n"+
            "namePorduct" + namePorduct + "\n" +
            "coffSV" + coffSV + "\n" +
            "coffWater" + coffWater + "\n" +
            "coffMetana" + coffMetana + "\n" +
            "valueGas" + valueGas + "\n"; 
    }
}
