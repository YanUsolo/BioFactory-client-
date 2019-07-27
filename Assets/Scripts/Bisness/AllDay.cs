using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDay : MonoBehaviour
{
   // public Product[] products_array = null;
    public LinkedList<Product> products = new LinkedList<Product>();
    private LinkedList<ProductComputationyInDay> productCompInDays;
    // Use this for initialization

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

    //ГПА
    private float coefForGPA;
    private float GPAvalue;

    private void Awake()
    {
        //   parseArrayToList_products();
     //   products_array = new Product[20];


    }

    void Start()
    {
        // parseArrayToList_products();
        //   debugProductsList();
        // initProductComp(
       // initProductComp(products);
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void initNewAllDay(LinkedList<Product> prods,DateTime date,float GPAvalue)
    {
        setProductsOnStartProject(prods);
        dateTime = date;
        setNewValue_coefForGPA(GPAvalue);
    }
   
 

    //Список названий продуктов
    public LinkedList<string> getNameProducts()
    {
        // string[] tempArray = new string[10];
        LinkedList<string> tempList = new LinkedList<string>();
        foreach (ProductComputationyInDay prod in productCompInDays)
        {
            tempList.AddLast(prod.getNameProduct());
        }

        return tempList;
    }

    //При запуске приложение инит стандартных продуктов 
    //Удалить лишнее
       public void initProductComp(LinkedList<Product> products)
       {
           productCompInDays = new LinkedList<ProductComputationyInDay>();
           int i = 0;
           foreach (Product prod in products)
           {
              // products_array[i] = prod;
               productCompInDays.AddLast(new ProductComputationyInDay(prod));
               i++;
           }
       }


    /*

       //parse!
       public void parseArrayToList_products()
       {

           products = new LinkedList<Product>();

           if (products != null)
           {
               products.Clear();
           }

           if (products_array != null)
           {

               foreach (Product prod in products_array)
               {
                   products.AddLast(prod);
               }

           }

       }*/

    private DateTime dateTime; 

    public void debugProductsList()
    {
        foreach (Product prod in products)
        {
            Debug.Log("Linked list element from AllDay : " + prod.name);
        }

    }


 

    public void recal_allParametrs()
    {
        recal_ProdCompParams();
        recal_allMass();
        recal_allMassSV();
        recal_dayMassSV();
        recal_nightMassSV();
        recal_allMassWater();
        recal_dayMassWater();
        recal_nightMassWater();
        recal_relationDaySV();
        recal_relationNightSV();
        recal_relationAllSV();
        recal_allDayMetan();
        recal_GPA();
       // debugAllProd();
    }

    public void  recal_ProdCompParams()
    {
        foreach (ProductComputationyInDay prod in productCompInDays)
        {
            prod.recal_AllCompulationly();
        }
    }

    public void debugAllProd()
    {
       
        foreach (ProductComputationyInDay prodComp in productCompInDays)
        {

            Debug.Log("---- Product Computationy ----");
            Debug.Log(prodComp.getNameProduct() + " Название");
            Debug.Log(prodComp.getDayMassList() + " масс лист");
            Debug.Log(prodComp.getDayMassSV() + " масс св(день)");
            Debug.Log(prodComp.getSummAllMass() + "вся масса(масса св)");
            Debug.Log(getRelationDaySV() + " Отношение сухих веществ(день)");
            Debug.Log(getRelationNightSV() + " Отношение сухих веществ(ночь)");
            Debug.Log(getRelationAllSV() + " Отношение сухих веществ");
            Debug.Log(getGPA() + " ГПА");


        }
    }

    public void deleteProductComp(Product prodOnDelete)
    {
        /*
        foreach (ProductComputationyInDay prodComp in productCompInDays)
        {
            if (prodComp.getNameProduct().Equals(prodOnDelete.getNameProduct()))
            {
                productCompInDays.Remove(prodComp);
            }
        }*/

        LinkedListNode<ProductComputationyInDay> tempItem = productCompInDays.First;
        for (int i = 0; i < productCompInDays.Count; i++)
        {
            if (tempItem.Value.getNameProduct().Equals(prodOnDelete.getNameProduct()))
            {
                productCompInDays.Remove(tempItem);
            }

            if (tempItem.Next == null)
            {
                i = 100;
            }
            else{
                tempItem = tempItem.Next;
            }
        }

        recal_allParametrs();
    }


    public AllDay(LinkedList<Product> products)
    {
        this.products = products;
    }

    //сумма всей массы продуктов за сутки
    public void recal_allMass()
    {
        allMass = 0;
        foreach (ProductComputationyInDay prodCom in productCompInDays)
        {
            allMass += prodCom.getSummAllMass();
        }
    }

    //сумма массы с.в. за сутки всех продуктов
    public void recal_allMassSV()
    {
        allMassSV = 0;
        foreach (ProductComputationyInDay prodCom in productCompInDays)
        {
            allMassSV += prodCom.getDayMassSV() + prodCom.getNightMassSV();
        }
    }

    public void recal_dayMassSV()
    {
        dayMassSV = 0;
        foreach (ProductComputationyInDay prodCom in productCompInDays)
        {
            dayMassSV += prodCom.getDayMassSV();
        }
    }

    public void recal_nightMassSV()
    {
        nightMassSV = 0;
        foreach (ProductComputationyInDay prodCom in productCompInDays)
        {
            nightMassSV += prodCom.getNightMassSV();
        }
    }

    //Рассчёт мыссы воды за сутки
    public void recal_allMassWater()
    {
        allMassWater = 0;
        foreach (ProductComputationyInDay prodCom in productCompInDays)
        {
            allMassWater += prodCom.getDayMassWater() + prodCom.getNightMassWater();
        }
    }

    public void recal_dayMassWater()
    {
        dayMassWater = 0;
        foreach (ProductComputationyInDay prodCom in productCompInDays)
        {
            dayMassWater += prodCom.getDayMassWater();
        }
    }

    public void recal_nightMassWater()
    {
        nightMassWater = 0;
        foreach (ProductComputationyInDay prodCom in productCompInDays)
        {
            nightMassWater += prodCom.getNightMassWater();
        }
    }

    //Relation recalpulation
    public void recal_relationDaySV()
    {

        //Need Fix
        float tempCalculation = dayMassWater;
        if (dayMassWater == 0)
        {
            tempCalculation = 1;
        }

        relationDaySV = (dayMassSV * 100) / tempCalculation;
    }

    public void recal_relationNightSV()
    {
        //Need Fix
        float tempCalculation = nightMassWater;
        if (nightMassWater == 0)
        {
            tempCalculation = 1;
        }

        relationNightSV = (nightMassSV * 100) / tempCalculation;
    }

    public void recal_relationAllSV()
    {
        float tempCalculation = dayMassWater + nightMassWater;

        //Need Fix
        if (tempCalculation == 0)
        {
            tempCalculation = 1;
        }

        relationAllSV = ((dayMassSV + nightMassSV) * 100) / (tempCalculation);
    }

    public void recal_allDayMetan()
    {
        allDayMetan = 0;
        foreach (ProductComputationyInDay prodCom in productCompInDays)
        {
            allDayMetan += prodCom.getMetanDay();
            allDayMetan += prodCom.getMetanNight();
        }
    }

    public void recal_GPA()
    {
        this.GPAvalue = allDayMetan * coefForGPA;
    }

    


 


   
    //Для тестирование окна оператора
    public void initProductForTest()
    {
        float[] digitalsDay_1 = { 123.9f };
        float[] digitalsDay_2 = { 120f };
        float[] digitalsDay_3 = { 23f, 14f, 15 };
        float[] digitalsDay_4 = { 7.5f };

        float[] digitalsNight_1 = { };
        float[] digitalsNight_2 = { };
        float[] digitalsNight_3 = { 15f, 15f, 15f };
        float[] digitalsNight_4 = { };


        LinkedListNode<ProductComputationyInDay> prod = productCompInDays.First;
        prod.Value.setMassByArray(digitalsDay_1, true);
        prod.Value.setMassByArray(digitalsNight_1, false);
        prod.Value.recal_AllCompulationly();
        prod = prod.Next;

        prod.Value.setMassByArray(digitalsDay_2, true);
        prod.Value.setMassByArray(digitalsNight_2, false);
        prod.Value.recal_AllCompulationly();
        prod = prod.Next;

        prod.Value.setMassByArray(digitalsDay_3, true);
        prod.Value.setMassByArray(digitalsNight_3, false);
        prod.Value.recal_AllCompulationly();
        prod = prod.Next;

        prod.Value.setMassByArray(digitalsDay_4, true);
        prod.Value.setMassByArray(digitalsNight_4, false);
        prod.Value.recal_AllCompulationly();
        prod = prod.Next;
    }


    public void setProductCompInDays(LinkedList<ProductComputationyInDay> prodList)
    {
        
    }
    //ИНИТ ПРОД. В ДНЕ НА НАЧАЛЕ ЗАПУСКА ПРОГРАММЫ
    public void setProductsOnStartProject(LinkedList<Product> prod) {
        this.products = prod;
        initProductComp(products);

    }
    public LinkedList<ProductComputationyInDay> getProductCompInDays()
    {

        return productCompInDays;
    }

    public LinkedList<ProductComputationyInDay> getProductCompInDaysByVisable()
    {
        LinkedList<ProductComputationyInDay> tempList = new LinkedList<ProductComputationyInDay>();

        foreach (ProductComputationyInDay prod in productCompInDays)
        {
            if (prod.isVisable()) {
                tempList.AddLast(prod);
            }
        }

        return tempList;
    }


    public void addProductInAllDay(Product newProd)
    {
        products.AddLast(newProd);
        productCompInDays.AddLast(new ProductComputationyInDay(newProd));
        //recal_allParametrs();
    }

    public void setNewValue_coefForGPA(float coefForGPA)
    {
        this.coefForGPA = coefForGPA;
        recal_GPA();
    }

  

    public void setSerovodorotNight(float num)
    {
        this.serovodorotNight = num;
    }

    public void setSerovodorotDay(float num)
    {
        this.serovodorodDay = num;
    }






    public float getAllMass() {
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
