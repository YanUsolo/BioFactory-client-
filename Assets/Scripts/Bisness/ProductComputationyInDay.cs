using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProductComputationyInDay
{
    //int ProductCompInDay_id;
    Product prod;
    string nameProduct;

    //по этапно заргуженные тонны
    LinkedList<float> dayMassList = new LinkedList<float>();
    LinkedList<float> nightMassList = new LinkedList<float>();

    //загруженно тонн
    float dayMass = 0;
    float nightMass = 0;

    //с.в. из загруженных тонн
    float dayMassSV = 0;
    float nightMassSV = 0;

    //воды их этих тонн
    float dayMassWater;
    float nightMassWater;

    //метана из этих тонн
    float metanDay;
    float metanNight;

    public void recal_AllCompulationly()
    {
        recal_dayAndNighttMass();
        recal_dayAndNightMassSV();
        recal_dayAndNightMassWater();
        recal_dayAndNightMetan();

    }

    public ProductComputationyInDay(Product prod)
    {

        this.nameProduct = prod.getNameProduct();
        this.prod = prod;
    }

    //Рассчёт
    //расчёт массы продукта 
    public void recal_dayAndNighttMass()
    {

        this.dayMass = 0;
        this.nightMass = 0;
        foreach (float dayMass in dayMassList)
        {
            this.dayMass += dayMass;
        }
        foreach (float nightMass in nightMassList)
        {
            this.nightMass += nightMass;
        }
    }

    //рассчёт массы с.в. продукта
    public void recal_dayAndNightMassSV()
    {
        float coffSV = prod.getCoffSV();

        //float allMass = this.dayMass + this.nightMass;

        this.dayMassSV = dayMass * coffSV;

        this.nightMassSV = nightMass * coffSV;

    }

    //рассчёт массы воды продукта
    public void recal_dayAndNightMassWater()
    {
        float coffWater = prod.getCoffWater();

        this.dayMassWater = dayMass * coffWater;

        this.nightMassWater = nightMass * coffWater;
    }



    public void recal_dayAndNightMetan()
    {
        float coffMetana = prod.getCoffMetana();
        float valueGas = prod.getValueGas();
        this.metanDay = dayMassSV * coffMetana * valueGas;
        this.metanNight = nightMassSV * coffMetana * valueGas;
    }

    public float getSummAllMass()
    {
        return this.dayMass + this.nightMass;
    }

    public float getDayMassSV()
    {
        return this.dayMassSV;
    }

    public float getNightMassSV()
    {
        return this.nightMassSV;
    }

    public float getDayMassWater()
    {
        return this.dayMassWater;
    }

    public float getNightMassWater()
    {
        return this.nightMassWater;
    }

    public string getNameProduct() {

        return prod.name;
    }

    public int getUniqueId()
    {
        return this.prod.getUniqueProduct_id();
    }

    public float getDayMass()
    {
        return this.dayMass;
    }

    public float getMetanDay() {
        return metanDay;
    }

    public float getMetanNight()
    {
        return metanNight;
    }

    public float getNightMass()
    {
        return this.nightMass;
    }

    public LinkedList<float> getDayMassList() {

        return this.dayMassList;
    }

    public LinkedList<float> getNightMassList()
    {

        return this.nightMassList;
    }

    public bool isVisable()
    {
        return prod.getVisable();
    }

    //ToDo
    public void addMass(float digital, bool dayOrNight)
    {
        if (dayOrNight)
        {
            dayMassList.AddLast(digital);
        }
        else {
            nightMassList.AddLast(digital);
        }

        recal_AllCompulationly();
    }

    public bool deleteMass(float digital, bool dayOrNight)
    {
        if (dayOrNight)
        {
            //dayMassList.AddLast(digital);
            // dayMassList.Find(digital);
            if (dayMassList.Remove(digital))
            {
                recal_AllCompulationly();
                return true;
            }
        }
        else
        {
            if (nightMassList.Remove(digital))
            {
                recal_AllCompulationly();
                return true;
            }
        }

        return false;
    }

    //Check code when something happend!!!!!
    public void setMassByArray(float[] array, bool dayOrNight)
    {
        if (dayOrNight) {
            for (int i = 0; i < array.Length; i++) {
                dayMassList.AddLast(array[i]);
                
            }
        }
        if(!dayOrNight) {
            for (int i = 0; i < array.Length; i++)
            {
                nightMassList.AddLast(array[i]);
                
            }
        }
        recal_AllCompulationly();
    }

    
    //End Product Calculation
}

