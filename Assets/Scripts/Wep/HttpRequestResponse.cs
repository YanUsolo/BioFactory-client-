using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class ProductDTO
{
    public string uniqueProd_id;
    public string name;
    public string coffSV;
    public string coffWater;
    public string coffMetana;
    public string valueGas;

    public ProductDTO(int id, string name, float coffSV, float coffWater, float coffMetana, float valueGas)
    {
        uniqueProd_id = id + "";
        this.name = name;
        this.coffSV = coffSV + "";
        this.coffWater = coffWater + "";
        this.coffMetana = coffMetana + "";
        this.valueGas = valueGas + "";
    }

    public ProductDTO(Product prod)
    {
        this.uniqueProd_id = prod.getUniqueProduct_id() + "";
        this.name = prod.getNameProduct();
        this.coffSV = prod.getCoffSV() + "";
        this.coffWater = prod.getCoffWater() + "";
        this.coffMetana = prod.getCoffMetana() + "";
        this.valueGas = prod.getValueGas() + "";

    }
}




    [Serializable]
    public class AllDayDTO
    {
        //Вся загруженная масса за день
        public string allMass;
        //Всего с.в. из загруженных тонн за день и по сменам
        public string dayMassSV;
        public string nightMassSV;
        public string allMassSV;
        //Всего воды из за груженных тонн за день и по сменам
        public string dayMassWater;
        public string nightMassWater;
        public string allMassWater;
        //relation - отношение,рассчёт отношения массы с.в. и воды
        public string relationDaySV;
        public string relationNightSV;
        public string relationAllSV;

        public string serovodorodDay;
        public string serovodorodNight;




        public string GPA;

        public string date;

        public AllDayDTO(AllDay allDay)
        {
            this.allMass = allDay.getAllMass() + "";
            this.dayMassSV = allDay.getDayMassSV() + "";
            this.nightMassSV = allDay.getNightMassSV() + "";
            this.allMassSV = allDay.getAllMassSV() + "";
            this.dayMassWater = allDay.getDayMassWater() + "";
            this.nightMassWater = allDay.getNightMassWater() + "";
            this.allMassWater = allDay.getAllMassWater() + "";
            this.relationDaySV = allDay.getRelationDaySV() + "";
            this.relationNightSV = allDay.getRelationNightSV() + "";
            this.relationAllSV = allDay.getRelationAllSV() + "";
            this.serovodorodDay = allDay.getSerovodorotDay() +"";
            this.serovodorodNight = allDay.getSerovodorotNight() + "";
            this.GPA = allDay.getGPA() + "";
            this.date = allDay.getDateTime().ToString("yyyy.MM.dd");
        }
    }

    [Serializable]
    public class ProductCompDTO
    {
        //загруженно тонн
        public string dayMass;
        public string nightMass;
        //с.в. из загруженных тонн
        public string dayMassSV;
        public string nightMassSV;
        //воды их этих тонн
        public string dayMassWater;
        public string nightMassWater;
        //метана из этих тонн
        public string metanDay;
        public string metanNight;

        public string uniqueId_Product;

        public ProductCompDTO(ProductComputationyInDay prodComp)
        {
            this.uniqueId_Product = prodComp.getUniqueId() + "";
            this.dayMass = prodComp.getDayMass() + "";
            this.nightMass = prodComp.getNightMass() + "";
            this.dayMassSV = prodComp.getDayMassSV() + "";
            this.nightMassSV = prodComp.getNightMassSV() + "";
            this.dayMassWater = prodComp.getDayMassWater() + "";
            this.nightMassWater = prodComp.getNightMassWater() + "";
            this.metanDay = prodComp.getMetanDay() + "";
            this.metanNight = prodComp.getMetanNight() + "";

        }

    }






    public class HttpRequestResponse : MonoBehaviour
    {


        void Start()
        {
            // StartCoroutine(POST_testMthod());
            viewURLserver();
        }


        public string urlServer = "http://localhost:8080";
        public string urlServerDefault = "http://localhost:8080";
        public Text testHolder_URLserver;
        public InputField inputField_URLserver;

        public BlockerAndExceptionMessegeScript debugAndValidation;

        public void testMethod()
        {
            StartCoroutine(POST_testMthod());
        }

        public IEnumerator POST_SaveProdsJson(WWWForm dataProductDTOs)
        {
            var Data = dataProductDTOs;

        

            var Query = new WWW(urlServer + "/saveProducts", Data);
        
            yield return Query;

            if (Query.error != null)
            {
                Debug.Log("Server does not respond : " + Query.error);
            }

            else
            {
                if (Query.text == "response") // в основном HTML код которым ответил сервер
                {
                    Debug.Log("Server responded correctly");

                }
                else
                {
                    Debug.Log("Server responded : " + Query.text.ToString() + " Size\n" + Query.bytesDownloaded);


                }
                debugAndValidation.setRequestFlag_product(true);

            }
            Query.Dispose();
        }

        public IEnumerator POST_SaveAllDayJson(WWWForm allDay)
        {
            var Data = allDay;

            var Query = new WWW(urlServer + "/saveAllDay", Data);
            yield return Query;

            if (Query.error != null)
            {
                Debug.Log("Server does not respond : " + Query.error);
            }

            else
            {
                if (Query.text == "response") // в основном HTML код которым ответил сервер
                {
                    Debug.Log("Server responded correctly");

                }
                else
                {
                    Debug.Log("Server responded : " + Query.text.ToString() + "Size\n" + Query.bytesDownloaded);


                }
                debugAndValidation.setRequestFlag_AllDay(true);
            }
            Query.Dispose();
        }

        public WWWForm parseAllDay(AllDay allDay)
        {
            var data = new WWWForm();

            LinkedList<ProductComputationyInDay> tempProdComps = allDay.getProductCompInDays();
            AllDayDTO allDayDTO = new AllDayDTO(allDay);

            int count = 1;
            data.AddField("AllDay", JsonUtility.ToJson(allDayDTO).ToString());
            ProductCompDTO productCompDTO = null;

            foreach (ProductComputationyInDay prodComp in tempProdComps)
            {
                productCompDTO = new ProductCompDTO(prodComp);
                data.AddField("ProductComp_" + count, JsonUtility.ToJson(productCompDTO).ToString());
                count++;
            }
            //data.add
            /*
            foreach (Product prod in products)
            {
                tempProdDto = new ProductDTO(prod.uniqueProduct_id, prod.name, prod.coffSV, prod.coffWater, prod.coffMetana, prod.valueGas);
                data.AddField("Product_" + count, JsonUtility.ToJson(tempProdDto).ToString());
                count++;
            }
            */

            return data;
        }

        public WWWForm parseAllDayWithProduct(AllDay allDay, LinkedList<Product> products)
        {
            var data = new WWWForm();
       // UTF8Encoding utf8 = new UTF8Encoding();
        //ToDo fix
        LinkedList<ProductComputationyInDay> tempProdComps = allDay.getProductCompInDays();
            AllDayDTO allDayDTO = new AllDayDTO(allDay);

            int count = 1;
            data.AddField("AllDay", JsonUtility.ToJson(allDayDTO).ToString());
            ProductCompDTO productCompDTO = null;
            ProductDTO productDTO = null;

            foreach (ProductComputationyInDay prodComp in tempProdComps)
            {
                productCompDTO = new ProductCompDTO(prodComp);
          //  byte[] encodedBytes = utf8.GetBytes(JsonUtility.ToJson(productCompDTO).ToString());
              data.AddField("ProductComp_" + count, JsonUtility.ToJson(productCompDTO).ToString());
           // data.AddField("ProductComp_" + count, encodedBytes.ToString());

            count++;
            }

            count = 1;
            foreach (Product prod in products)
            {
                productDTO = new ProductDTO(prod);
                data.AddField("Product_" + count, JsonUtility.ToJson(productDTO).ToString());
                count++;
            
            }

            //data.add
            /*
            foreach (Product prod in products)
            {
                tempProdDto = new ProductDTO(prod.uniqueProduct_id, prod.name, prod.coffSV, prod.coffWater, prod.coffMetana, prod.valueGas);
                data.AddField("Product_" + count, JsonUtility.ToJson(tempProdDto).ToString());
                count++;
            }
            */

            return data;
        }


        public WWWForm parseProduct(LinkedList<Product> products)
        {
            var data = new WWWForm();

            ProductDTO tempProdDto;

            int count = 1;
            //data.add

            foreach (Product prod in products)
            {
                tempProdDto = new ProductDTO(prod.uniqueProduct_id, prod.name, prod.coffSV, prod.coffWater, prod.coffMetana, prod.valueGas);
                data.AddField("Product_" + count, JsonUtility.ToJson(tempProdDto).ToString());
                count++;
            }

            return data;
        }

        public IEnumerator POST_testMthod()
        {
            var Data = new WWWForm();


            var Query = new WWW(urlServer + "/testMethod", Data);


            yield return Query;

            if (Query.error != null)
            {
                Debug.Log("Server does not respond : " + Query.error);
            }

            else
            {
                if (Query.text == "response") // в основном HTML код которым ответил сервер
                {
                    Debug.Log("Server responded correctly");
                }
                else
                {
                    Debug.Log("Server responded : " + Query.text.ToString() + "Size\n" + Query.bytesDownloaded);
                    //   praseJsonToFilm(Query.text.ToString());parseJsonToStringByFilm
                    //  parseJsonsToFilms(Query.text.ToString());

                }
            }
            Query.Dispose();
        }




        public IEnumerator GET()
        {
            string data1 = "Текст 1";
            string data2 = "Текст 2";
            WWW Query = new WWW("http://127.0.0.1/game.php?variable1=" + data1 + "&variable2=" + data2);
            yield return Query;
            if (Query.error != null)
            {
                Debug.Log("Server does not respond : " + Query.error);
            }
            else
            {
                if (Query.text == "response") // что нам должен ответить сервер на наши данные
                {
                    Debug.Log("Server responded correctly");
                }
                else
                {
                    Debug.Log("Server responded : " + Query.text);
                }
            }
            Query.Dispose();
        }



        public IEnumerator POST_testConnection()
        {
            var Data = new WWWForm();


            var Query = new WWW(urlServer + "/testConnection", Data);


            yield return Query;

            if (Query.responseHeaders == null)
            {
                Debug.Log("Server does not respond : " + Query.error);
                // return false;
                debugAndValidation.messageTestConnection(false, urlServer);
            }

            if (Query.error != null)
            {
                Debug.Log("Server does not respond : " + Query.error);
                // return false;
                debugAndValidation.messageTestConnection(false, urlServer);
            }

            else
            {
                if (Query.text == "response") // в основном HTML код которым ответил сервер
                {
                    Debug.Log("Server responded correctly");
                    debugAndValidation.messageTestConnection(true, urlServer);
                }
                else
                {
                    //Debug.Log("Server responded : " + Query.text.ToString() + "Size\n" + Query.bytesDownloaded);
                    //   praseJsonToFilm(Query.text.ToString());parseJsonToStringByFilm
                    //  parseJsonsToFilms(Query.text.ToString());
                    debugAndValidation.messageTestConnection(true, urlServer);

                }
            }
            Query.Dispose();
        }

        string fixJson(string value)
        {
            value = "{\"Items\":[" + value + "]}";
            return value;
        }


        public void setNewUrl(string url)
        {
            if (!url.Equals(""))
            {
                this.urlServer = url;
                viewURLserver();
            }
        }

        public void viewURLserver()
        {
            testHolder_URLserver.text = urlServer;
            clearInputField(inputField_URLserver);
        }

        public void testConection()
        {
            StopCoroutine(POST_testConnection());
        }

        public void setDafaultURL()
        {
            urlServer = urlServerDefault;
            viewURLserver();
        }

        public static void clearInputField(InputField inputfield)
        {
            inputfield.Select();
            inputfield.text = "";
        }
    }



















    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }



    }
