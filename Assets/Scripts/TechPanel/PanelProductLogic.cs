using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PanelProductLogic : MonoBehaviour {


    public BlockerAndExceptionMessegeScript debugAndValidation;


   
    //DropDown выбора продукта для изменения 
    public Dropdown dropdown_ProductsList;

    //Выбранный продукт для редактирования
    public Product productEdit;
    //DropDown param product.
    public Dropdown dropdown_coffProd;
    public Text inputField_paramProd;
    public Text inputField_nameProd;
   
    public Text text_viewParam;

    //Контэйнер базавых продуктов
    public GameObject containerProductGO;
    //Продукты из конт.
    public LinkedList<Product> products;
    

    //Отображать у оператора?Visable toggle
    public Toggle toggle_visableProduct;

    //ИНФО MESSAGE
    public Text text_infoMessage;

    //ИНФО MESSAGE в окне оператора от технолога
    public Text text_infoMessageForOperator;

    public bool statusAddProd = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      //  viewParam();

    }
    public void setInfoMessageForOperator(string value)
    {
        text_infoMessageForOperator.text = value;
    }
    //------------Param Product------------------------
    public void viewParam()
    {
            text_viewParam.text = "Параметры продукта : \n" +
            "Кофф. сухого вещества : " + productEdit.getCoffSV() + "\n" +
            "Кофф. воды : " + productEdit.getCoffWater() + "\n" +
            "Кофф. метана : " + productEdit.getCoffMetana() + "\n" +
            "Кофф. объём газа : " + productEdit.getValueGas() + "\n";
    }

    public void viewNameOnEdit()
    {
        inputField_nameProd.text = "";

    }

    //------------Param Product End--------------------

    //инит продукта для редактирования
    public void selectProduct(int value)
    {
        //условие создание нового продукта
        if (statusAddProd)
        {
            if (value == dropdown_ProductsList.options.Count - 1)
            {
               
                statusAddProd = false;
                text_infoMessage.text = "Для созранение продукта впишите имя";
                //containerProductGO
            }
        }

        if (statusAddProd)
        {
            productEdit = getProductByIndex(value);
            viewParam();
            viewNameOnEdit();
            viewVisableProduct();
        }
    }

    //------------Products List------------------------
    public void resetProductInDropDown(LinkedList<Product> products,int index)
    {

     
        dropdown_ProductsList.ClearOptions();
        // getSotrProduct(products);

        dropdown_ProductsList.AddOptions(getNameProduct(products));

        dropdown_ProductsList.RefreshShownValue();

        dropdown_ProductsList.value = index;

        selectProduct(dropdown_ProductsList.value);

    }

    public LinkedList<Product> getSotrProduct(LinkedList<Product> product)
    {

        LinkedList<Product> list = new LinkedList<Product>();


        /*  foreach (Product prod in product)
          {
              if (prod.getVisable() == true)
              {
                  list.AddLast(prod);
              }
          }

          foreach (Product prod in product)
          {
              if (prod.getVisable() == false)
              {
                  list.AddLast(prod);
              }
          }
          */

        foreach (Product prod in product)
        {
           
            list.AddLast(prod);
            
        }

        return list;
    }

    public List<string> getNameProduct(LinkedList<Product> product)
    {
        List<string> list = new List<string>();

        foreach (Product prod in product)
        {

            list.Add(prod.getNameProduct());

        }

        if (statusAddProd)
        {
            list.Add("Новый продукт");
        }
        return list;
    }
    //------------Products List END------------------------

    public void setProducts(LinkedList<Product> prod) {

        this.products = prod;
        resetProductInDropDown(products,0);
    }

    //инит продукта для редактирования
    public Product getProductByIndex(int index) {

        int temp = 0;
        

        foreach (Product prod in products)
        {
            if (temp == index) {
                return prod;
            }

            temp++;
        }
        return null;
    }

    //Ред. папаметров продукта
    public void editByParam()
    {
        float param = float.Parse(inputField_paramProd.text);

        switch (dropdown_coffProd.value)
        {
          
            case 0:
                if (debugAndValidation.validateCoffSV(param))
                {
                    productEdit.setCoffSVandWater(param);
                }
                break;
            case 1:
                if (debugAndValidation.validateCoffWater(param))
                {
                    debugAndValidation.warningAboutCoffWater();
                    productEdit.setCoffWater(param);
                }

                break;
            case 2:

               
                 productEdit.setCoffMetana(param);
                           
                break;
            case 3:
                productEdit.setValueGas(param);
                break;
        }

        viewParam();
        viewNameOnEdit();
        viewVisableProduct();
    }

    public void editByName()
    {

        if (!inputField_nameProd.text.Equals(""))
        {
            productEdit.setNameProduct(inputField_nameProd.text);

            if (productEdit.getId() == 0)
            {
                text_infoMessage.text = "Продукт добавлен и сохранён.";
            }
        }



        resetProductInDropDown(products,0);

    }

    //Измение ид при изменениях его значений
    public void setNewUniqID()
    {
        string temp = "";

        DateTime localDateTime = DateTime.Now;

        string newId = localDateTime + "";
        newId = newId.Replace("/", "");
        newId = newId.Replace(":", "");
        newId = newId.Replace(" ", "");
        newId = newId.Substring(0, newId.Length - 7);
        newId = newId + productEdit.getId();
        //  localDateTime.GetDateTimeFormats()
        productEdit.setUniqueProduct_id(Int32.Parse(newId));
        Debug.Log("" + localDateTime);
        Debug.Log("" + newId);
    }


    //Добавление нового продута в конт. проудктов
    public void setNewProd(Product prod)
    {
        productEdit = prod;
      //  products.AddLast(prod);
        
      //  resetProductInDropDown(products);
        viewParam();
        viewNameOnEdit();
        viewVisableProduct();
    }


    //action для кнопки добавления
    //No use
    public void applyNewProduct()
    {
        if (productEdit.getId() == 0) {
            if (debugAndValidation.validateApplyNewProduct(productEdit))
            {






                text_infoMessage.text = "Продукт добавлен и сохранён.";
                //Todo
            }
        }
        else {
        }
    }

    //Отображать или не отображать у оператора
    public void changeVisableProduct(bool value)
    {
        productEdit.setVisible(value);
    }

    //Усьановить значение продукта в тогл
    public void viewVisableProduct()
    {
        toggle_visableProduct.isOn = productEdit.getVisable();
    }

    public Product getProdEidt() {

        return this.productEdit;
    }

    //Удаление продукта
    public void deleteProductOnButton()
    {
        debugAndValidation.applyDeleteProdMessage(productEdit.getNameProduct());

    }



    public void setStatusAdd(bool status) {
        this.statusAddProd = status;
    }


    public void clearInputField(InputField inputField)
    {
        inputField.Select();
        inputField.text = "";
    }



}
