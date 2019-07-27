using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class SaveLoad : MonoBehaviour {

    //-------------------------------DTO---------------------------------------
    [Serializable]
    public class ProductDTO
    {
        public bool visable;
        public int uniqueProd_id;
        public string name;
        public float coffSV;
        public float coffWater;
        public float coffMetana;
        public float valueGas;

        public ProductDTO(bool visable, int id, string name, float coffSV, float coffWater, float coffMetana, float valueGas)
        {
            this.visable = visable;
            this.uniqueProd_id = id;
            this.name = name;
            this.coffSV = coffSV;
            this.coffWater = coffWater;
            this.coffMetana = coffMetana;
            this.valueGas = valueGas;
            /*  uniqueProd_id = id + "";
              this.name = name;
              this.coffSV = coffSV + "";
              this.coffWater = coffWater + "";
              this.coffMetana = coffMetana + "";
              this.valueGas = valueGas + "";*/
        }
    }

    [Serializable]
    public class AllDayDtoRead
    {
        //Вся загруженная масса за день
        public float allMass = 0;

        //Всего с.в. из загруженных тонн за день и по сменам
        public float dayMassSV;
        public float nightMassSV;
        public float allMassSV;

        //Всего воды из за груженных тонн за день и по сменам
        public float dayMassWater;
        public float nightMassWater;
        public float allMassWater;


        //relation - отношение,рассчёт отношения массы с.в. и воды
        public float relationDaySV;
        public float relationNightSV;
        public float relationAllSV;

        //Метан за сутки
        public float allDayMetan = 0;

        //Сероводород
        public float serovodorodDay;
        public float serovodorotNight;

        //ГПА
        public float coefForGPA;
        public float GPAvalue;
        public string date;

        public AllDayDtoRead(AllDay allDay_Now)
        {
            this.allMass = allDay_Now.getAllMass();

            this.dayMassSV = allDay_Now.getDayMassSV();
            this.nightMassSV = allDay_Now.getNightMassSV();
            this.allMassSV = allDay_Now.getAllMassSV();

            this.dayMassWater = allDay_Now.getDayMassWater();
            this.nightMassWater = allDay_Now.getNightMassWater();
            this.allMassWater = allDay_Now.getAllMassWater();

            this.relationDaySV = allDay_Now.getRelationDaySV();
            this.relationNightSV = allDay_Now.getRelationNightSV();
            this.relationAllSV = allDay_Now.getRelationAllSV();
            this.allDayMetan = 0;
            this.GPAvalue = allDay_Now.getGPA();
            serovodorodDay = allDay_Now.getSerovodorotDay();
            serovodorotNight = allDay_Now.getSerovodorotNight();
            this.coefForGPA = 0;
            this.date = allDay_Now.getDateTime().ToString("yyyy.MM.dd");
        }

    }

    [Serializable]
    public class ProductCompDto
    {
        //int ProductCompInDay_id;
        public string nameProduct;

        //по этапно заргуженные тонны

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

        public ProductCompDto(ProductComputationyInDay prodComp)
        {
            nameProduct = prodComp.getNameProduct();



            dayMass = prodComp.getDayMass();

            nightMass = prodComp.getNightMass();

            dayMassSV = prodComp.getDayMassSV();

            nightMassSV = prodComp.getNightMassSV();

            dayMassWater = prodComp.getDayMassWater();

            nightMassWater = prodComp.getNightMassWater();

            metanDay = prodComp.getMetanDay();

            metanNight = prodComp.getMetanNight();

        }
    }

    // Use this for initialization

    private void Awake()
    {
     
    }

    //--------------------------------Ссылки на компоненты UI------------------------------------------------------
    public InputField inputField_AllDayRead;
    public InputField inputField_Products;
    public InputField inputField_ProductComp;


    //--------------------------------Прямые ссылки на данные котопрые нужно сохранить------------------------------------------------------
    public AllDay allDay_ObjectForSaveOrLoad;


    //---------------Save and Load Product----------------------------------------
    public string pathProduct;
    public string pathProductDefault = "Files\\Product\\Products";

    
    public void LoadProducts(string path)
    {
       

        if (!File.Exists(path))
        {
            //  return false;
        }
        if (contProducts.GetComponent<Product>() != null)
        {
            //    return false;
        }

        string[] content = File.ReadAllLines(path);

        foreach (string str in content)
        {
            addProductOnCont(JsonUtility.FromJson<ProductDTO>(str));
        }

        //   return true;
    }
    public void SaveProduct_InFile()
    {
        SaveProduct_InFile(contProducts.GetComponentsInChildren<Product>(), DateTime.Now.ToString("dd.MM.yyyy"), pathProduct);
    }

    public void SaveProduct_InFile(Product[] prod, string date, bool defaultOrNot)
    {
        if (defaultOrNot)
        {
            SaveProduct_InFile(prod, date, pathProductDefault + "(" + date + ").json");
        }
        else
        {
            SaveProduct_InFile(prod, date, pathAllDayRead);
        }
    }

    public void SaveProduct_InFile(Product[] prod,string date,string pathProduct)
    {
        string tempPath = Path.Combine(Application.dataPath, pathProduct);
        Debug.Log(tempPath);
        if (!File.Exists(tempPath))
        {
            //  return false;
            Debug.Log("Shit!!!!!!");
        }

        if (prod.Length == 0)
        {
            //  return false;
        }


        LinkedList<ProductDTO> productDtos = parseProductToDto(prod);

        string[] contentForFile = new string[productDtos.Count];
        int i = 0;
        foreach (ProductDTO prodDTO in productDtos)
        {
            contentForFile[i] = JsonUtility.ToJson(prodDTO);
            i++;
        }

        File.WriteAllLines(tempPath, contentForFile);


        //   return true;
    }
    private LinkedList<ProductDTO> parseProductToDto(Product[] products)
    {

        LinkedList<ProductDTO> productDtoList = new LinkedList<ProductDTO>();
        foreach (Product prod in products)
        {
            //  tempProdDto = new ProductDTO(prod.uniqueProduct_id, prod.name, prod.coffSV, prod.coffWater, prod.coffMetana, prod.valueGas);
            productDtoList.AddLast(new ProductDTO(prod.getVisable(), prod.uniqueProduct_id, prod.name, prod.coffSV, prod.coffWater, prod.coffMetana, prod.valueGas));
        }

        return productDtoList;
    }

    private void addProductOnCont(ProductDTO prodDto)
    {
        GameObject newProduct = Instantiate(prefab_ProductG0, contProducts.transform, true);

        newProduct.GetComponent<Product>().setParamsInProduct(prodDto.visable, prodDto.uniqueProd_id, prodDto.name, prodDto.coffSV, prodDto.coffWater, prodDto.coffMetana, prodDto.valueGas);
    }

    public void setPathProducts(string path)
    {
        string tempPath = Path.Combine(Application.dataPath, path);

        if (!File.Exists(tempPath))
        {
            //  return false;
            Debug.Log("Shit!!!!!!");
            //Exception!!!
        }

        pathProduct = path;
        inputField_Products.placeholder.GetComponent<Text>().text = pathProduct;

    }





    //---------------Save and Load AllDayDtoRead----------------------------------------
    public string pathAllDayRead = "";
    public string pathAllDayReadDefault = "Files\\WorkPastDayRead\\SaveAllPastDayRead";

    //For beta ver 1
    public void Load_AllDay()
    {

        Load_AllDay(pathAllDayRead);
    }

    public void Load_AllDay(string path)
    {
        string tempPath = Path.Combine(Application.dataPath, path);


        if (!File.Exists(tempPath))
        {
            //  return false;
            //Exceprion

        }
        else
        {
            string[] content = File.ReadAllLines(tempPath);

            if (content == null)
            {
                //    return false;
                //Exceprion
            }

            foreach (string str in content)
            {
                Debug.Log(JsonUtility.FromJson<AllDayDtoRead>(str));
            }

            //   return true;
        }
    }

    public void SaveAllDayRead_inFile()
    {
        SaveAllDayRead_inFile(allDay_ObjectForSaveOrLoad, DateTime.Now.ToString("dd.MM.yyyy"), pathAllDayRead);
    }

    public void SaveAllDayRead_inFile(AllDay allDay, string date, bool defaultOrNot)
    {
        if (defaultOrNot) {
            SaveAllDayRead_inFile(allDay, date, pathAllDayReadDefault + "(" + date + ").json");
        }
        else
        {
            SaveAllDayRead_inFile(allDay, date, pathAllDayRead);
        }
    }

    public void SaveAllDayRead_inFile(AllDay allDay, string date, string pathAllDayRead)
    {
        string tempPath = Path.Combine(Application.dataPath, pathAllDayRead);
        Debug.Log(tempPath);
        if (!File.Exists(tempPath))
        {
            //  return false;
            Debug.Log("Shit!!!!!!");
            //Validate
        }




        AllDayDTO allDayDTO = new AllDayDTO(allDay);

        string[] contentForFile = { JsonUtility.ToJson(allDayDTO) };
        File.WriteAllLines(tempPath, contentForFile);

        //   return true;
    }

    public void setPathAllDayRead(string path)
    {
        string tempPath = Path.Combine(Application.dataPath, path);

        if (!File.Exists(tempPath))
        {
            //  return false;
            Debug.Log("Shit!!!!!!");
            //Exception!!!
        }

        pathAllDayRead = path;
        inputField_AllDayRead.placeholder.GetComponent<Text>().text = pathAllDayRead;

    }






    //---------------Save and Load ProductComps----------------------------------------
    public GameObject contProducts;
    public string pathProductComps = "";
    public string pathProductCompsDefault = "Files\\WorkDayNow\\ProductsComps";
    public GameObject prefab_ProductG0;

    //For beta ver 1

    public void SaveProductComp_inFile()
    {
        SaveProductComp_inFile(allDay_ObjectForSaveOrLoad.getProductCompInDays(), DateTime.Now.ToString("dd.MM.yyyy"), pathProductComps);
    }

    public void SaveProductComp_inFile(LinkedList<ProductComputationyInDay> prodComp, string date, bool defaultOrNot)
    {
        if (defaultOrNot)
        {
            SaveProductComp_inFile(prodComp, date, pathProductCompsDefault + "(" + date + ").json");
        }
        else
        {
            SaveProductComp_inFile(prodComp, date, pathProductComps);
        }
    }

    public void SaveProductComp_inFile(LinkedList<ProductComputationyInDay> prodComp, string date,string pathProd)
    {
        string tempPath = Path.Combine(Application.dataPath, pathProd);
        Debug.Log(tempPath);
        if (!File.Exists(tempPath))
        {
            //  return false;
            Debug.Log("Нет файла!!!!!!");
        }

        if (prodComp.Count != 0)
        {
            //  return false;
        }


        LinkedList<ProductCompDto> productDtos = parseProductCompToDto(prodComp);

        string[] contentForFile = new string[productDtos.Count];
        int i = 0;
        foreach (ProductCompDto prodDTO in productDtos)
        {
            contentForFile[i] = JsonUtility.ToJson(prodDTO);
            i++;
        }

        File.WriteAllLines(tempPath, contentForFile);


        //   return true;
    }

  


    public void LoadProductComps(string path)
    {
        string[] content = File.ReadAllLines(path);


        if (!File.Exists(path))
        {
            //  return false;
        }
        //Зачем это я написал?
        if (contProducts.GetComponent<Product>() != null)
        {
            //    return false;
        }

        foreach (string str in content)
        {

          
            


        }
        Debug.Log("Загрузка продукт");

        //   return true;
    }

    private LinkedList<ProductCompDto> parseProductCompToDto(LinkedList<ProductComputationyInDay> products)
    {

        LinkedList<ProductCompDto> productDtoList = new LinkedList<ProductCompDto>();
        foreach (ProductComputationyInDay prodComp in products)
        {
            //  tempProdDto = new ProductDTO(prod.uniqueProduct_id, prod.name, prod.coffSV, prod.coffWater, prod.coffMetana, prod.valueGas);
            productDtoList.AddLast(new ProductCompDto(prodComp));
        }

        return productDtoList;
    }

    public void setPathProductsComp(string path)
    {
        string tempPath = Path.Combine(Application.dataPath, path);

        if (!File.Exists(tempPath))
        {
            //  return false;
            Debug.Log("Shit!!!!!!");
            //Exception!!!
        }

        pathProductComps = path;
        inputField_ProductComp.placeholder.GetComponent<Text>().text = pathProductComps;

    }




    //Дописать ОКНО ИНФОРМАЦИИ
    // GameObject newProduct = Instantiate(prefab_ProductG0, containerProductGO.transform, true);



    /*
    public void SaveProducts_inFile(string date)
    {
        Product[] tempProducts = contProducts.GetComponentsInChildren<Product>();
        pathProductComp = Path.Combine(Application.dataPath, "Files\\Products\\Products (" + date + ").json");
        if (!File.Exists(pathProduct))
        {
            //  return false;
        }
        if (tempProducts == null)
        {
            //    return false;
        }

        LinkedList<ProductDTO> productDtos = parseProductToDto(tempProducts);

        string[] contentForFile = new string[productDtos.Count];
        int i = 0;
        foreach (ProductDTO prodDTO in productDtos)
        {
            contentForFile[i] = JsonUtility.ToJson(prodDTO);
            i++;
        }

        File.WriteAllLines(pathProduct, contentForFile);
        //   return true;
    }
    */



}
