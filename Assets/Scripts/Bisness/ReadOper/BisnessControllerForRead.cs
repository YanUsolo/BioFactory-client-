using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BisnessControllerForRead : MonoBehaviour {

    //ОБЪЕТЫ ДЛЯ РАБОТЫ КЛАССА
    public AllDay allDayNow;


    //Объкты для отобрадение на панели past day
    public GameObject allDayForRead_GO;
    public AllDayForRead allDayForRead;

    //Для отбрадение парамеитров на панели инфо:
    //Масса за сутки
    public Text allMassSV;
    //отночегие всей массы за сутки
    public Text relationAllSV;
    //отобразить дату в окне оператора
    public Text Date;
    //отобразить GPA в окне оператора
    public Text GPA;

    //Элементы ui для сероводорода

    public Text SerovodorodDay_text;
    public Text SerovodorodnNight_text;

    //Для отбражение массы продуктов:
    public GameObject contentDay;
    public LinkedList<FieldMassScript> textContentDay = new LinkedList<FieldMassScript>();
    public GameObject[] itemsContentDay;

    public GameObject contentNight;
    public LinkedList<FieldMassScript> textContentNight = new LinkedList<FieldMassScript>();
    public GameObject[] itemsContentNight;

    //Для save in file
    public SaveLoad saveLoad;

   

    public void initPastDayForRead()
    {
        allDayForRead.initAllDayForRead(allDayNow);

        initTextListForDay(contentDay);
        initTextListForNight(contentNight);

        update_showInfoDayProduct_onViewScroll();
        update_showInfoNightProduct_onViewScroll();
        update_showInfo_onPanelInfo();
        update_ViewSerovodorot();
        //update_showInfo_onPanelInfo();

    }


    public void update_showInfo_onPanelInfo()
    {
        this.allMassSV.text = "Масса веществ за сутки : " + allDayForRead.getAllMassSV();
        this.relationAllSV.text = "% сухих вешеств : " + allDayForRead.getRelationAllSV();
        this.Date.text = "Дата : " + allDayForRead.getDateTime().ToString("dd.MM.yyyy");
        this.GPA.text = "Ожидаемая мощность генерации, МВт : " + allDayForRead.getGPA();

    }

    public void update_ViewSerovodorot()
    {
        SerovodorodDay_text.text = "День : " + allDayForRead.getSerovodorotDay();

        SerovodorodnNight_text.text = "Ночь : " + allDayForRead.getSerovodorotNight() ;

    }

    public void initTextListForDay(GameObject content)
    {

        float hightContent = 0f;

        initDefaultTextList(content, true);

        for (int i = content.transform.childCount - 1; i != allDayForRead.getProductCompForReads().Count; i--)
        {
            itemsContentDay[i].SetActive(false);

        }

        textContentDay.Clear();
        for (int i = 0; i < allDayForRead.getProductCompForReads().Count + 1; i++)
        {

            textContentDay.AddLast(itemsContentDay[i].GetComponent<FieldMassScript>());
            hightContent += 40;
        }

        RectTransform rt = content.GetComponent<RectTransform>();
        //rt.sizeDelta = new Vector2(-467, hightContent);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hightContent);
    }

    public void initTextListForNight(GameObject content)
    {

        float hightContent = 0f;

        initDefaultTextList(content, false);

        for (int i = content.transform.childCount - 1; i != allDayForRead.getProductCompForReads().Count; i--)
        {
            itemsContentNight[i].SetActive(false);

        }

        textContentNight.Clear();
        for (int i = 0; i < allDayForRead.getProductCompForReads().Count + 1; i++)
        {

            textContentNight.AddLast(itemsContentNight[i].GetComponent<FieldMassScript>());
            hightContent += 40;
        }

        RectTransform rt = content.GetComponent<RectTransform>();
        //   rt.sizeDelta = new Vector2(0, hightContent);
        // rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, other.rect.width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hightContent);

    }

    public void initDefaultTextList(GameObject content, bool flag)
    {
        if (!flag)
        {
            for (int i = 0; i < content.transform.childCount; i++)
            {
                itemsContentNight[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 1; i < content.transform.childCount - 1; i++)
            {
                itemsContentDay[i].SetActive(true);
            }
        }
    }

    public void update_showInfoDayProduct_onViewScroll()
    {
        LinkedList<AllDayForRead.ProductCompForRead> tempProductList = allDayForRead.getProductCompForReads();

        LinkedListNode<FieldMassScript> textList = textContentDay.First;

        if (tempProductList.Count == textContentDay.Count - 1)
        {
            textList.Value.nameProduct.text = "Заргуженна за день:";
            textList = textList.Next;

            foreach (AllDayForRead.ProductCompForRead prod in tempProductList)
            {
               // textList.Value.nameProduct.runInEditMode = true;
                textList.Value.nameProduct.text = "" + prod.getNameProduct();
                textList.Value.summMass.text = "" + prod.getDayMass();
                //    Debug.Log("!!!!!!!!!!!!!!!!!!" + prod.getDayMass());
                // textList.Value.summMass.
                textList.Value.setMassListInDropDown(prod.getDayMassList());

                textList = textList.Next;
            }
        }

    }

    //Отрисовка значений на окне оператора(scrollView Night)
    public void update_showInfoNightProduct_onViewScroll()
    {
        LinkedList<AllDayForRead.ProductCompForRead> tempProductList = allDayForRead.getProductCompForReads();

        LinkedListNode<FieldMassScript> textList = textContentNight.First;

        if (tempProductList.Count == textContentNight.Count - 1)
        {
            textList.Value.nameProduct.text = "Заргуженна за ночь:";
            textList = textList.Next;

            foreach (AllDayForRead.ProductCompForRead prod in tempProductList)
            {

              //  textList.Value.nameProduct.runInEditMode = true;
                textList.Value.nameProduct.text = "" + prod.getNameProduct();
                textList.Value.summMass.text = "" + prod.getNightMass();
                //    Debug.Log("!!!!!!!!!!!!!!!!!!" + prod.getDayMass());
                // textList.Value.summMass.
                textList.Value.setMassListInDropDown(prod.getNightMassList());

                textList = textList.Next;
            }
        }

    }




    //Методы

    /*



        //ToDo  !!!!!!!!!!!!!

  


        //Отрисовка значений на окне оператора(scrollView Day)
        public void update_showInfoDayProduct_onViewScroll()
        {
            LinkedList<ProductComputationyInDay> tempProductList = allDayForRead.getProductCompInDaysByVisable();

            LinkedListNode<FieldMassScript> textList = textContentDay.First;

            if (tempProductList.Count == textContentDay.Count - 1)
            {
                textList.Value.nameProduct.text = "Заргуженна за день:";
                textList = textList.Next;

                foreach (ProductComputationyInDay prod in tempProductList)
                {
                    textList.Value.nameProduct.runInEditMode = true;
                    textList.Value.nameProduct.text = "" + prod.getNameProduct();
                    textList.Value.summMass.text = "" + prod.getDayMass();
                    //    Debug.Log("!!!!!!!!!!!!!!!!!!" + prod.getDayMass());
                    // textList.Value.summMass.
                    textList.Value.setMassListInDropDown(prod.getDayMassList());

                    textList = textList.Next;
                }
            }

        }

        //Отрисовка значений на окне оператора(scrollView Night)
        public void update_showInfoNightProduct_onViewScroll()
        {
            LinkedList<ProductComputationyInDay> tempProductList = allDayForRead.getProductCompInDaysByVisable();

            LinkedListNode<FieldMassScript> textList = textContentNight.First;

            if (tempProductList.Count == textContentNight.Count - 1)
            {
                textList.Value.nameProduct.text = "Заргуженна за ночь:";
                textList = textList.Next;

                foreach (ProductComputationyInDay prod in tempProductList)
                {

                    textList.Value.nameProduct.runInEditMode = true;
                    textList.Value.nameProduct.text = "" + prod.getNameProduct();
                    textList.Value.summMass.text = "" + prod.getNightMass();
                    //    Debug.Log("!!!!!!!!!!!!!!!!!!" + prod.getDayMass());
                    // textList.Value.summMass.
                    textList.Value.setMassListInDropDown(prod.getNightMassList());

                    textList = textList.Next;
                }
            }

        }

        //Отрисовка значений на окне оператора(Panel Info)
        public void update_showInfo_onPanelInfo()
        {
            this.allMassSV.text = "Масса веществ за сутки : " + allDayForRead.getAllMassSV();
            this.relationAllSV.text = "% сухих вешеств : " + allDayForRead.getRelationAllSV();
            this.Date.text = "Дата : " + allDayForRead.getDateTime().ToString("dd.MM.yyyy");
            this.GPA.text = "Ожидаемая мощность генерации, МВт : " + allDayForRead.getGPA();

        }

        //ToDo
        public void updateNameProduct_dropdown()
        {
            dropDown_nameProduct.options.Clear();

            LinkedList<string> nameProducts = allDayForRead.getNameProductByVisable();
            foreach (string str in nameProducts)
            {
                dropDown_nameProduct.options.Add(new Dropdown.OptionData(str));
            }

            dropDown_nameProduct.RefreshShownValue();
        }

        void resetAll()
        {

          //  allDayForRead.recal_allParametrs();
            update_showInfoDayProduct_onViewScroll();
            update_showInfoNightProduct_onViewScroll();
            update_showInfo_onPanelInfo();

        }

        // Use this for initialization
        void Start () {

        }

        // Update is called once per frame
        void Update () {

        }

        public void clearInputField()
        {
            InputField_addMass.Select();
            InputField_addMass.text = "";
        }

        public void clearSerovodorotInputField()
        {
            SerovodorodDayAndNight_InputField.Select();
            SerovodorodDayAndNight_InputField.text = "";
        }*/
}
