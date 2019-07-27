using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AllDayForRead : MonoBehaviour {

    //public LinkedList<ProductCompDTO> productCompDto = new LinkedList<ProductCompDTO>();
    public LinkedList<ProductCompForRead> productCompForReads = new LinkedList<ProductCompForRead>();

    //Дата работы
    private DateTime dateTime;

    //Вся загруженная масса за день
    private float allMass = 0;

    //Всего с.в. из загруженных тонн за день и по сменам
    private float dayMassSV;
    private float nightMassSV;
    private float allMassSV;

    //Всего воды из за груженных тонн за день и по сменам
    private float dayMassWater;
    private float nightMassWater;
    private float allMassWater;


    //relation - отношение,рассчёт отношения массы с.в. и воды
    private float relationDaySV;
    private float relationNightSV;
    private float relationAllSV;

    //Метан за сутки
    private float allDayMetan = 0;

    //Сероводород
    private float serovodorodDay;
    private float serovodorotNight;


    private float coefForGPA;
    private float GPAvalue;

    public void  initAllDayForRead(AllDay allDayOrigin)
    {
           allMass = 0;

    //Всего с.в. из загруженных тонн за день и по сменам
    dayMassSV = allDayOrigin.getDayMassSV();
    nightMassSV = allDayOrigin.getNightMassSV();
    allMassSV = allDayOrigin.getAllMassSV();

    //Всего воды из за груженных тонн за день и по сменам
     dayMassWater = allDayOrigin.getDayMassWater();
     nightMassWater= allDayOrigin.getNightMassWater();
     allMassWater = allDayOrigin.getAllMassWater();

        serovodorodDay = allDayOrigin.getSerovodorotDay();
        serovodorotNight = allDayOrigin.getSerovodorotNight();

        dateTime = allDayOrigin.getDateTime();


        //relation - отношение,рассчёт отношения массы с.в. и воды
        relationDaySV = allDayOrigin.getRelationDaySV() ;
     relationNightSV = allDayOrigin.getRelationNightSV() ;
    relationAllSV = allDayOrigin.getRelationAllSV();

    //Метан за сутки
     //allDayMetan = allDayOrigin.Meta;
     GPAvalue = allDayOrigin.getGPA();

        foreach (ProductComputationyInDay prodRead in allDayOrigin.getProductCompInDaysByVisable())
        {
            productCompForReads.AddLast(new ProductCompForRead(prodRead));
        }

}

    public LinkedList<ProductCompForRead> getProductCompForReads()
    {
        return productCompForReads;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public class ProductCompForRead
    {
        //int ProductCompInDay_id;
        string nameProduct;

        //по этапно заргуженные тонны
        LinkedList<float> dayMassList = new LinkedList<float>();
        LinkedList<float> nightMassList = new LinkedList<float>();
        //загруженно тонн
        float dayMass;
        float nightMass;

        //с.в. из загруженных тонн
        float dayMassSV;
        float nightMassSV;

        //воды их этих тонн
        float dayMassWater;
        float nightMassWater;

        //метана из этих тонн
        float metanDay;
        float metanNight;

        public ProductCompForRead(ProductComputationyInDay productCompOrigin)
        {
            nameProduct = productCompOrigin.getNameProduct();

            dayMassList = productCompOrigin.getDayMassList();

            nightMassList = productCompOrigin.getNightMassList();

            dayMass = productCompOrigin.getDayMass();

            nightMass = productCompOrigin.getNightMass();

            dayMassSV = productCompOrigin.getDayMassSV();

            nightMassSV = productCompOrigin.getNightMassSV();

            dayMassWater = productCompOrigin.getDayMassWater();

            nightMassWater = productCompOrigin.getNightMassWater();

            metanDay = productCompOrigin.getMetanDay();

            metanNight = productCompOrigin.getMetanNight();

        }

        public LinkedList<float> getDayMassList()
        {
            return dayMassList;
        }

        public LinkedList<float> getNightMassList()
        {
            return nightMassList;
        }

        public string getNameProduct()
        {

            return this.nameProduct;
        }

        public float getDayMass()
        {
            return this.dayMass;
        }

        public float getMetanDay()
        {
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

     

    }

    public void setSerovodorotNight(float num)
    {
        this.serovodorotNight = num;
    }

    public void setSerovodorotDay(float num)
    {
        this.serovodorodDay = num;
    }






    public float getAllMass()
    {
        return this.allMass;
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

    public float getAllMassWater()
    {
        return this.allMassWater;
    }

    public float getRelationDaySV()
    {
        return this.relationDaySV;
    }

    public float getSerovodorotDay()
    {
        return this.serovodorodDay;
    }
    public float getSerovodorotNight()
    {
        return this.serovodorotNight;
    }

    public float getRelationNightSV()
    {
        return this.relationNightSV;
    }

    public float getRelationAllSV()
    {
        return this.relationAllSV;
    }

    public float getAllMassSV()
    {
        return this.allMassSV;
    }

    public DateTime getDateTime()
    {
        return this.dateTime;
    }

    public void setDate(DateTime date)
    {
        this.dateTime = date;
    }

    public float getGPA()
    {
        return this.GPAvalue;
    }
}
