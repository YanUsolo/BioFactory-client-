using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool visible;

    public int product_id;

    public int uniqueProduct_id;

    public string name = "";

    //  public bool status = true;

    public Product(string name, float coffSV, float coffMetana, float valueGas)
    {
        this.name = name;
        this.coffSV = coffSV;
        recal_coffWater();
        this.coffMetana = coffMetana;
        this.valueGas = valueGas;
    }

    public Product(int id, string name, float coffSV, float coffMetana, float valueGas)
    {
        this.product_id = id;
        this.name = name;
        this.coffSV = coffSV;
        recal_coffWater();
        this.coffMetana = coffMetana;
        this.valueGas = valueGas;

    }

    public void setParamsInProduct(bool visable, int id, string name, float coffSV, float coffWater, float coffMetana, float valueGas)
    {
        this.visible = visable;
        uniqueProduct_id = id;
        this.name = name;
        this.coffSV = coffSV;
        this.coffWater = coffWater;
        this.coffMetana = coffMetana;
        this.valueGas = valueGas;
    }
    public Product()
    {

    }


    public float coffSV = 0.0f;//Кофф С.В.(сухого вещества)
    public float coffWater = 0.0f;// Кофф воды(1 - с.в.)

    public float coffMetana = 0.0f;//Вводится техником.
    public float valueGas = 0.0f;//Объем газа м3/т вводится техником 







    // private float AOMOP;//ToDo  //Общий выход мутана из 1 сырья м3/т.Состоит из кофф.Метана и Объёма газа на м3/т. 
    public void recal_coffWater()
    {//перевычисление кофф. воды

        this.coffWater = 1 - coffSV;
    }

    public float[] COMD = { };//ToDo //кофф выхода метана по суткам

    public void setId(int id)
    {
        this.product_id = id;
    }

    public int getId()
    {
        return this.product_id;
    }
    public void setUniqueProduct_id(int id) {
        this.uniqueProduct_id = id;
    }

    public int getUniqueProduct_id()
    {
        return this.uniqueProduct_id;
    }
    public string getNameProduct()
    {
        return this.name;
    }

    public float getCoffSV()
    {
        return this.coffSV;
    }

    //Check Code!!!!!!!!!!!!!!!!!!!!!
    public float getCoffWater()
    {
       // recal_coffWater();
        return this.coffWater;
    }

    public float getCoffMetana()
    {
        return this.coffMetana;
    }

    public float getValueGas()
    {
        return this.valueGas;
    }

    public void setParamProd(string name, float coffSV, float coffMetana, float valueGas)
    {
        this.name = name;
        this.coffSV = coffSV;
        recal_coffWater();
        this.coffMetana = coffMetana;
        this.valueGas = valueGas;
    }

    public bool getVisable()
    {
        return this.visible;
    }

    public void setVisible(bool param) {

        this.visible = param;
    }

    public void setNameProduct(string nameProd) {
        this.name = nameProd;
    }

    public void setCoffSV(float param)
    {
        this.coffSV = param;
    }

    public void setCoffSVandWater(float param)
    {
        this.coffSV = param;
        this.coffWater = 1 - param;
    }

    public void setCoffWater(float param)
    {
        this.coffWater = param;
    }

   // public void setCoffWa

    public void setCoffMetana(float param)
    {
        this.coffMetana = param;
    }

    public void setValueGas(float param)
    {
        this.valueGas = param;
    }
  
}
