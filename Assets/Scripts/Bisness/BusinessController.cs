using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BusinessController : MonoBehaviour {

    public BlockerAndExceptionMessegeScript debugAndValidation;

    public GameObject allDay_NowGO;
    public AllDay allDay_Now;

    // Use this for initialization
    public Text exceptionMessege;

    // --------------Operator Panel-----------------


    //Для добавление массы к продукту:
    public Dropdown dropDown_nameProduct;
    public Text labelDropdown_nammeProduct;

    public Dropdown dropDown_dayOrNight;
    public Text labelDropDown_dayOrNight;


    public InputField InputField_addMass;
    public Text inputField_addMass;

    //Для отбражение массы продуктов:
    [SerializeField]
    public GameObject contentDay;
    public LinkedList<FieldMassScript> textContentDay = new LinkedList<FieldMassScript>();
    public GameObject[] itemsContentDay = new GameObject[21];

    [SerializeField]
    public GameObject contentNight;
    public LinkedList<FieldMassScript> textContentNight = new LinkedList<FieldMassScript>();
    public GameObject[] itemsContentNight;

    //Для отбрадение парамеитров на панели инфо:
    //Масса за сутки
    public Text allMassSV;
    //отночегие всей массы за сутки
    public Text relationAllSV;
    //отобразить дату в окне оператора
    public Text Date;
    //отобразить GPA в окне оператора
    public Text GPA;

    //отображение значения на окне технолога
    public Text placeHolder_coefForGPA;
    public Text inputField_coefForGPA;

    //Элементы ui для сероводорода
    public Toggle SerovodorodDay_toggle;
    public Toggle SerovodorodNight_toggle;
    public Text SerovodorodDay_text;
    public Text SerovodorodnNight_text;
    public InputField SerovodorodDayAndNight_InputField;

    // -------------Tech Panel----------------
    //dropdown для выбора продуктов
    public PanelProductLogic panelProductLogic;

    //Префаб для создание нового продукта
    public GameObject prefab_ProductG0;

    //Container Product 
    public ContainerLogic contLogic;
    //public ContainerProductLogic contProd;
    //Container logic для работы с проуктами
    public GameObject containerProductGO;

    public float coefForGPA = 0.2f;

    //рАБОТА С ФАЙЛАМИ
    public SaveLoad saveLoad;
    
 



    //КОЕФ. для вычисления гпа 
    //новое значение для расчёта
    public void setNewValue_CoefForGPA(string text)
    {
        if (!text.Equals(""))
        {
            coefForGPA = float.Parse(text);
            allDay_Now.setNewValue_coefForGPA(coefForGPA);
            viewCoefForGPA();
        }
    }

    public float getCoefForGPA()
    {
        return this.coefForGPA;
    }

    public void viewCoefForGPA()
    {
        placeHolder_coefForGPA.text = "Равен " + coefForGPA;
        inputField_coefForGPA.text = "";
    }

    public bool statusAddProd = true;

    private void Awake()
    {
        //allDay_Now.setProducts(contProd.getProducts());
        //инит. комп.
        dropDown_nameProduct = dropDown_nameProduct.GetComponent<Dropdown>();
        allDay_Now = allDay_NowGO.GetComponent<AllDay>();

        //Установление связей с конт. прод.
        contLogic = new ContainerLogic(containerProductGO);
        //contLogic.setContainerProduct(containerProductGO);
        // contLogic.

        //получение базовых продуктов для формирование расчётных продукьтов
        //
        //allDay_Now.setProductsOnStartProject(contLogic.getProducts());
        allDay_Now.initNewAllDay(contLogic.getProducts(),DateTime.Now.AddDays(-1), coefForGPA);

        //формирование выводов параметров в окне оператора.Переделать или сделать кастыль

        Debug.Log("Check - " + GetComponentsInChildren<FieldMassScript>().ToString());

       // Invoke("initTextListForDay",1f);
        initTextListForDay(contentDay);
        initTextListForNight(contentNight);

        //itemsContentDay = get
    }

    private void OnEnable()
    {
        // allDay_Now.setProducts(contProd.getProducts());

    }
    void Start()
    {
        //Список имён для панели оператора
        updateNameProduct_dropdown();
        //перерасчёт данных продуктов у оператора и обновление визуально
        resetAll();
        //получение базавых продуктов для окна технолога
        panelProductLogic.setProducts(contLogic.getProducts());
        viewCoefForGPA();
    }

    //Создание нового для
    public void createNewDay()
    {
        DateTime localDateTime = DateTime.Now;

       // string date = localDateTime.ToString("yyyy.MM.dd");
       
       // date = date.Replace("/",".");

      //  Debug.Log("Date " + date);

        if (debugAndValidation.validateOnCreateNewDay(allDay_Now.getDateTime(), localDateTime))
        {
            allDay_Now.initNewAllDay(contLogic.getProducts(), DateTime.Now, coefForGPA);

            allDay_Now.recal_allParametrs();

            resetAll();
        }
    }

    //Save allDay на сераер !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public void saveAllDay_ButtonAction()
    {

        //StartCoroutine(fix_SaveAllDay());
        //if()
        // saveOrUpdateProductsInBD();

        // saveOrUpdateAllDayInBD();
        saveOrUpdateAllDayWithProductInBD();


    }

    public IEnumerator fix_SaveAllDay()
    {

        if (debugAndValidation.getRequestFlag_AllDay() && debugAndValidation.getRequestFlag_product())
        {
            debugAndValidation.allRightSaveAllDay(allDay_Now.getDateTime().ToString("dd.MM.yyyy"));
            debugAndValidation.setRequestFlag_AllDay(false);
            debugAndValidation.setRequestFlag_product(false);
        }
        else
        {
            debugAndValidation.errorSaveAllDay();
            debugAndValidation.setRequestFlag_AllDay(false);
            debugAndValidation.setRequestFlag_product(false);
        }

        yield return new WaitForSeconds(5f);    
    }
    //---------------------------------------------------------WEB----------------------------------------------------------------------------------------------------------
    //HTTP ЗАПРОСЫ ДЛЯ СОХРАНЕНИЯ В БД
    public HttpRequestResponse httpRequestResponse;

    public void saveOrUpdateProductsInBD()
    {
        // prodForSave
        WWWForm data = httpRequestResponse.parseProduct(contLogic.getProducts());

        StartCoroutine(httpRequestResponse.POST_SaveProdsJson(data));

        //  StartCoroutine(POST_forSaveProdJson(prodDTO));


    }

    //Переписать как длделаю сервер.Важно!!!
    public void saveOrUpdateAllDayWithProductInBD()
    {

        WWWForm data = httpRequestResponse.parseAllDayWithProduct(allDay_Now,contLogic.getProducts());

        StartCoroutine(httpRequestResponse.POST_SaveAllDayJson(data));
    }

    public void saveOrUpdateAllDayInBD()
    {

        WWWForm data = httpRequestResponse.parseAllDay(allDay_Now);

        StartCoroutine(httpRequestResponse.POST_SaveAllDayJson(data));
    }
    //---------------------------------------------------------WEB----------------------------------------------------------------------------------------------------------
  
        
        
        //no use
    public void updateNameProduct_dropdown_Fix()
    {
        dropDown_nameProduct.RefreshShownValue();
    }

    //Работа с Container Product
    public void viewAllProducts_dropdown()
    {

    }


    //Rewrite
    public void resetContent(GameObject content) {

        int count = content.transform.GetChildCount();

        int temp = count - 1;
        for (; temp != 0; temp--) {
            Destroy(content.transform.GetChild(temp).gameObject);
        }
    }



    //ToDo  !!!!!!!!!!!!!

    public void initTextListForDay(GameObject content)
    {

        float hightContent = 0f;

        initDefaultTextList(content,true);

        for (int i = content.transform.childCount - 1; i != allDay_Now.getProductCompInDaysByVisable().Count; i--)
        {
            itemsContentDay[i].SetActive(false);

        }

        textContentDay.Clear();
        for (int i = 0; i < allDay_Now.getProductCompInDaysByVisable().Count + 1; i++)
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

        initDefaultTextList(content,false);

        for (int i = content.transform.childCount - 1; i != allDay_Now.getProductCompInDaysByVisable().Count; i--)
        {
            itemsContentNight[i].SetActive(false);

        }

        textContentNight.Clear();
        for (int i = 0; i < allDay_Now.getProductCompInDaysByVisable().Count + 1; i++)
        {

            textContentNight.AddLast(itemsContentNight[i].GetComponent<FieldMassScript>());
            hightContent += 40;
        }

        RectTransform rt = content.GetComponent<RectTransform>();
        //   rt.sizeDelta = new Vector2(0, hightContent);
       // rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, other.rect.width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, hightContent);

    }

    public void initDefaultTextList(GameObject content,bool flag)
    {
        if (!flag)
        {
            for (int i = 0; i < content.transform.childCount; i++) {            
                itemsContentNight[i].SetActive(true);
            }
        }else
        {
            for (int i = 1; i < content.transform.childCount - 1; i++) {
               // Debug.Log(content.transform.childCount + " | " + i + " -------  " + itemsContentDay.Length);

              //  Debug.Log(itemsContentDay[i].ToString() + "-------- to string");
                itemsContentDay[i].SetActive(true);
      
            }
        }
    }


    //Отрисовка значений на окне оператора(scrollView Day)
    public void update_showInfoDayProduct_onViewScroll()
    {
        LinkedList<ProductComputationyInDay> tempProductList = allDay_Now.getProductCompInDaysByVisable();

        LinkedListNode<FieldMassScript> textList = textContentDay.First;

        if (tempProductList.Count == textContentDay.Count - 1)
        {
            textList.Value.nameProduct.text = "Заргужено за день:";
            textList = textList.Next;

            foreach (ProductComputationyInDay prod in tempProductList)
            {
                //textList.Value.nameProduct.runInEditMode = true;
                
              textList.Value.nameProduct.text = "" + prod.getNameProduct();
                textList.Value.summMass.text = "" + prod.getDayMass();
            //    Debug.Log("!!!!!!!!!!!!!!!!!!" + prod.getDayMass());
               // textList.Value.summMass.
                textList.Value.setMassListInDropDown( prod.getDayMassList());
               
                textList = textList.Next;
            }
        }

    }

    //Отрисовка значений на окне оператора(scrollView Night)
    public void update_showInfoNightProduct_onViewScroll()
    {
        LinkedList<ProductComputationyInDay> tempProductList = allDay_Now.getProductCompInDaysByVisable();

        LinkedListNode<FieldMassScript> textList = textContentNight.First;

        if (tempProductList.Count == textContentNight.Count - 1)
        {
            textList.Value.nameProduct.text = "Заргужено за ночь:";
            textList = textList.Next;

            foreach (ProductComputationyInDay prod in tempProductList)
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

    //Отрисовка значений на окне оператора(Panel Info)
    public void update_showInfo_onPanelInfo()
    {
        this.allMassSV.text = "Масса веществ за сутки : " + allDay_Now.getAllMassSV();
        this.relationAllSV.text = "% сухих вешеств : " + allDay_Now.getRelationAllSV();
        this.Date.text = "Дата : " + allDay_Now.getDateTime().ToString("dd.MM.yyyy");
        this.GPA.text = "Ожидаемая мощность генерации, МВт : " + allDay_Now.getGPA();

    }

    //ToDo
    public void updateNameProduct_dropdown()
    {
        dropDown_nameProduct.options.Clear();

        LinkedList<string> nameProducts = contLogic.getNameProductByVisable();
        foreach (string str in nameProducts)
        {
            dropDown_nameProduct.options.Add(new Dropdown.OptionData(str));
        }

        dropDown_nameProduct.RefreshShownValue();
    }


    //Добавдение массы к продукту
    public void addMassProduct()
    {
        bool flag = true;

        if (labelDropdown_nammeProduct.text == "Выбирете продукт!")
        {
            exceptionMessege.text = "Неверное заполнение";
            flag = false;
        }
        if (inputField_addMass.text == "")
        {
            exceptionMessege.text = "Неверное заполнение";
            flag = false;
        }
        if (flag)
        {
            bool dayOrNight = true;
            float digital = 0.0f;
            float.TryParse(inputField_addMass.text, out digital);

            if (!labelDropDown_dayOrNight.text.Equals("День")) {
                dayOrNight = false;
            }

            getProductByName(labelDropdown_nammeProduct.text).addMass(digital, dayOrNight);

            if (dayOrNight)
            {
                update_showInfoDayProduct_onViewScroll();
            }
            else {
                update_showInfoNightProduct_onViewScroll();
            }

            allDay_Now.recal_allParametrs();
            update_showInfo_onPanelInfo();
        }
    }

    //Удаление массы продукта
    public void deleteMassProduct()
    {
        bool flag = true;

        if (labelDropdown_nammeProduct.text == "Выбирете продукт!")
        {
            exceptionMessege.text = "Неверное заполнение";
            flag = false;
        }
        if (inputField_addMass.text == "")
        {
            exceptionMessege.text = "Неверное заполнение";
            flag = false;
        }
        if (flag)
        {
            bool dayOrNight = true;
            float digital = 0.0f;
            float.TryParse(inputField_addMass.text, out digital);

            if (!labelDropDown_dayOrNight.text.Equals("День"))
            {
                dayOrNight = false;
            }

            if (getProductByName(labelDropdown_nammeProduct.text).deleteMass(digital, dayOrNight))
            {
                exceptionMessege.text = "Масса удалена";
            }
            else {
                exceptionMessege.text = "Такая масса не найдена";
            }
            

            if (dayOrNight)
            {
                update_showInfoDayProduct_onViewScroll();
            }
            else
            {
                update_showInfoNightProduct_onViewScroll();
            }

            allDay_Now.recal_allParametrs();
            update_showInfo_onPanelInfo();
        }
    }

 //   private void deleteMassOnProduct() { }

    void resetAll() {

        allDay_Now.recal_allParametrs();
        update_showInfoDayProduct_onViewScroll();
        update_showInfoNightProduct_onViewScroll();
        update_showInfo_onPanelInfo();

    }

    public void recal_AllDay() {
    }

    public ProductComputationyInDay getProductByName(string nameProduct)
    {
        LinkedList<ProductComputationyInDay> products = allDay_Now.getProductCompInDays();
        foreach(ProductComputationyInDay prod in products)
        {
            if (prod.getNameProduct().Equals(nameProduct)) {
                return prod;
            }
            
        } 
        return null;
    }

   

	// Update is called once per frame
	void Update () {
		
	}


    //
    public void setAllDay() {

    }

    public void checkOnCreate(int value)
    {

     //   Debug.Log(value + " - " + contLogic.getCountProducts());
        if (value == contLogic.getCountProducts())
        {
            if (statusAddProd)
            {
                Product temp = createNewProduct();
                panelProductLogic.setNewProd(temp);
                //allDay_Now.addProductInAllDay(temp);
                statusAddProd = false;
            }
        }

    }

    //Создание нового продукта в конт.
    public Product createNewProduct()
    {
        GameObject newProduct = Instantiate(prefab_ProductG0, containerProductGO.transform, true);

      //  panelProductLogic.setProducts(contLogic.getProducts());

        return newProduct.GetComponent<Product>();
    }

    //инит нового продукта для вычислений
   


    public class ContainerLogic
    {

        private GameObject containerProductGO;
        private LinkedList<Product> products = new LinkedList<Product>();

        public ContainerLogic(GameObject cont)
        {
            this.containerProductGO = cont;
            initProductsInCont();
         //   th
        }


        //Создание нового продукта в конт.
        public Product createNewProd() {
            return null;

        }

        public void initProductsInCont()
        {
            Product[] tempProducts = containerProductGO.GetComponentsInChildren<Product>();
            products.Clear();

            foreach (Product prod in tempProducts)
            {
                products.AddLast(prod);
                //   Debug.Log("" + prod.getNameProduct());
            }

            // Debug.Log("" + products.ToString());
            //  debugProducts();
        }

        public int getCountProducts()
        {
            int count = 0;
            foreach (Product prod in products)
            {
                count++;
            }

            return count;
        }

        public LinkedList<string> getNameProduct()
        {
            LinkedList<string> tempList = new LinkedList<string>();

            foreach (Product prod in products)
            {
                tempList.AddLast(prod.getNameProduct());
            }
            return tempList;
        }

        public LinkedList<string> getNameProductByVisable()
        {
            LinkedList<string> tempList = new LinkedList<string>();

            foreach (Product prod in products)
            {
                if (prod.visible)
                {
                    tempList.AddLast(prod.getNameProduct());
                }
            }
            return tempList;
        }



        public LinkedList<Product> getProducts()
        {
            return products;
        }

        private void debugProducts()
        {
            foreach (Product prod in products)
            {
                Debug.Log(prod.getNameProduct() + " - " + prod.getId());
            }

        }

        public void setContainerProduct(GameObject cont) {
            this.containerProductGO = cont;
        }

        //Обновление id после удаления или добавления
        public void resetIdProducts()
        {
            Product[] tempProducts = containerProductGO.transform.GetComponentsInChildren<Product>(false);




            for (int i = 0; i < tempProducts.Length; i++)
            {
                tempProducts[i].setId(i + 1);

            }


        }
        //не работает как надо
        public GameObject getProductById(int id)
        { 
            return containerProductGO.transform.GetChild(id-1).gameObject;
        }



    }

    public void initNewProduct(string nameProd)
    {
        Product tempProd = panelProductLogic.getProdEidt();
        if (!nameProd.Equals(""))
        {
            if (tempProd.getId() == 0)
            {
                tempProd.setId(contLogic.getProducts().Count + 1);
                allDay_Now.addProductInAllDay(tempProd);

                panelProductLogic.statusAddProd = true;
                statusAddProd = true;
                
                contLogic.resetIdProducts();
                contLogic.initProductsInCont();
                panelProductLogic.resetProductInDropDown(contLogic.getProducts(), contLogic.getProducts().Count -1);

                updateNameProduct_dropdown();

                initTextListForNight(contentNight);
                initTextListForDay(contentDay);
                resetAll();
                // panelProductLogic.resetProductInDropDown(contLogic.getProducts());
            }
        }
    }

    public void setVisableUpdate()
    {

        updateNameProduct_dropdown();

        initTextListForNight(contentNight);
        initTextListForDay(contentDay);

        resetAll();
    }

    //Удаление продукта
    public void deleteProductOnButtonByYes()
    {
        //
        Product prodOnDelete = panelProductLogic.getProdEidt();
       // GameObject tempFix = getProductById
        GameObject prodOnDeleteGO = contLogic.getProductById(prodOnDelete.getId());


        //prodOnDeleteGO.dest

        //  prodOnDeleteGO.GetComponent<Product>().
        allDay_Now.deleteProductComp(prodOnDelete);

        prodOnDeleteGO.SetActive(false);
        //Destroy(prodOnDeleteGO.GetComponent<Product>());
    //    Destroy(prodOnDeleteGO);


        contLogic.resetIdProducts();
        contLogic.initProductsInCont();


        panelProductLogic.setProducts(contLogic.getProducts());

        updateNameProduct_dropdown();

        //  initDefaultTextList()

        initTextListForNight(contentNight);
        initTextListForDay(contentDay);

        resetAll();
       
       // initDefaultTextList();
        //update_showInfoNightProduct_onViewScroll();

        //ToDo operator action
    }

    public void changedToggleDay(bool value)
    {
        if (value == true)
        {
            SerovodorodNight_toggle.isOn = false;
        }
        else {
            SerovodorodNight_toggle.isOn = true;
        }
    }

    public void changedToggleNight(bool value)
    {
        if (value == true)
        {
            SerovodorodDay_toggle.isOn = false;
        }
        else
        {
            SerovodorodDay_toggle.isOn = true;
        }
    }
    public void setSerovodorot(string value)
    {
        if (!value.Equals("")) {
            if (SerovodorodDay_toggle.isOn == false)
            {
                allDay_Now.setSerovodorotDay(float.Parse(value));
                SerovodorodDay_text.text = "День : " + value;
            }
            else
            {
                allDay_Now.setSerovodorotNight(float.Parse(value));
                SerovodorodnNight_text.text = "Ночь : " + value;
            }
        }
    }

    public void deleteProductOnButtonByNo()
    {
        //

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
    }

    //-------------------------------Работа с файлами-----------------
   

    public void saveAllDayReadInFile()
    {
        //  Debug.Log("" + allDayNow.ToString() + "\n" + allDayNow.getProductCompInDays());

        // saveLoad.SaveProductComp(allDayNow.getProductCompInDays());
        //For beta ver 1 
        //    saveLoad.SaveProductComp(allDayNow.getProductCompInDays(), DateTime.Now.ToString("dd.MM.yyyy"));
        saveLoad.SaveAllDayRead_inFile(allDay_Now, DateTime.Now.ToString("dd.MM.yyyy"),true);
    }

    public void saveProductCompInFile()
    {
        //  Debug.Log("" + allDayNow.ToString() + "\n" + allDayNow.getProductCompInDays());

        // saveLoad.SaveProductComp(allDayNow.getProductCompInDays());
        //For beta ver 1 
        //    saveLoad.SaveProductComp(allDayNow.getProductCompInDays(), DateTime.Now.ToString("dd.MM.yyyy"));
        saveLoad.SaveProductComp_inFile(allDay_Now.getProductCompInDays(), DateTime.Now.ToString("dd.MM.yyyy"),true);
    }

    public void saveProductInFile()
    {

        saveLoad.SaveProduct_InFile(containerProductGO.GetComponentsInChildren<Product>(), DateTime.Now.ToString("dd.MM.yyyy"),true);
    }


}
