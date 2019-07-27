using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BlockerAndExceptionMessegeScript : MonoBehaviour {

    public Text titel;
    public Text message;

    public GameObject button_OkGO;
    public GameObject button_YesGO;
    public GameObject button_NoGO;


    //flag for save allDay on request
    public bool requestFlag_AllDay = false;
    public bool requestFlag_product = false;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public bool validateOnCreateNewDay(DateTime allDay_Date, DateTime nowDate)
    {
        string nowDateStr = nowDate.ToString("yyyy.MM.dd");

        

        Debug.Log(allDay_Date.ToString("HH") + "Час рабочего дня" );
        Debug.Log(nowDate.ToString("HH") + "Час на данный момент дня");
        Debug.Log("Check : " + int.Parse(nowDate.ToString("hh")));
        //Проверка что наступил новый день и прогло 6 ч до конца рабочего дня работы
        if ( !(allDay_Date.ToString("yyyy.MM.dd").Equals(nowDateStr)) && (int.Parse(nowDate.ToString("hh")) >= 6 ) )
        {

            string message = "Рабочий день(" + allDay_Date + ") сохранён.Новый день(" + nowDate + ") готов к работе";

            message_activeExceptionWindow(message);

            button_OkGO.SetActive(true);

            gameObject.SetActive(true);

            return true;
           
        }
        else
        {
            string message = "Рабочий день не сохранён,он ещё не прошёл.";

            warning_activeExceptionWindow(message);

            button_OkGO.SetActive(true);

            gameObject.SetActive(true);

            return false;
        }

       
    }

    public void errorSaveAllDay()
    {
        string message = "При сохранении данных произошла ошибка.Проверьте состояние сервера и БД";

        warning_activeExceptionWindow(message);
        button_OkGO.SetActive(true);
    
        gameObject.SetActive(true);
    }

    public void allRightSaveAllDay(string date)
    {
        string message = "Сохранение("+ date + ") прошло успешно";

        warning_activeExceptionWindow(message);
        button_OkGO.SetActive(true);

        gameObject.SetActive(true);
    }

    public void warning_activeExceptionWindow(string message)
    {
        this.titel.text = "Warning Message:";

        this.message.text = message;

    }

    public void correction_activeExceptionWindow(string message)
    {
        this.titel.text = "Сorrection Message:";

        this.message.text = message;

    }

    public void message_activeExceptionWindow(string message)
    {
        this.titel.text = "Message:";

        this.message.text = message;

    }


    public void setRequestFlag_AllDay(bool flag)
    {
        this.requestFlag_AllDay = flag;
    }

    public void setRequestFlag_product(bool flag)
    {
        this.requestFlag_product = flag;
    }

    public bool getRequestFlag_AllDay()
    {
        return requestFlag_AllDay;
    }
    public bool getRequestFlag_product()
    {
        return requestFlag_product;
    }
    //----------Сами проверки данных-----------

    //Проверка кофф сухого вещества
    public bool validateCoffSV(float digital)
    {
        if (0 <= digital && digital <= 1)
        {
            return true;
        }

        string message = "Значение коэффициента сухого вещества должно быть от 0 до 1.";

        warning_activeExceptionWindow(message);
        gameObject.SetActive(true);

        return false;
    }

    public void ClosedAndOpenPanel()
    {
        if (gameObject.activeSelf)
        {

            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }




    //предупреждение при вводе coffWater;

    public void warningAboutCoffWater()
    {
        string message = "Изменения будут приняты и приведут к неожиданным последствиям";

        warning_activeExceptionWindow(message);
        gameObject.SetActive(true);


    }

    //
    public bool validateCoffWater(float digital)
    {
        if (0 <= digital && digital <= 1)
        {
            return true;
        }

        string message = "Значение коэффициента воды вещества должно быть от 0 до 1.";

        warning_activeExceptionWindow(message);
        gameObject.SetActive(true);

        return false;
    }

    public bool validateApplyNewProduct(Product editProd)
    {

        string message = "";
        if (editProd.getNameProduct().Equals("") || editProd.getNameProduct().Length >= 30)
        {
            message = "Имя продукта либр не вписано, либо больше 30 символов./nПродукт не будет добавлен.";
            warning_activeExceptionWindow(message);
            gameObject.SetActive(true);

            return false;
        }
        else
        {
            if ((editProd.getCoffSV() == 0) || (editProd.getCoffMetana() == 0) || (editProd.getValueGas() == 0))
            {
                message = "Изначально все параметры равны 0, если вы не вписали новые.";
                warning_activeExceptionWindow(message);
                gameObject.SetActive(true);

            }

            return true;
        }
              
    }

    public void messageTestConnection(bool goodConection,string url)
    {
        string messageTrue = "Связь с сервером " + url + " успешно установлена!";
        string messageFalse = "Связь с сервером " + url + " не установлена!";
        if (goodConection)
        {
            warning_activeExceptionWindow(messageTrue);
            gameObject.SetActive(true);
        }
        else
        {
            warning_activeExceptionWindow(messageFalse);
            gameObject.SetActive(true);
        }
        

    }

    //Подверждение удаления продута
    public void applyDeleteProdMessage(string nameProd)
    {
        string message = "Вы действительно хотите удалить выбранный продукт(" + nameProd + ") из списка?";

        correction_activeExceptionWindow(message);
        button_OkGO.SetActive(false);
        button_YesGO.SetActive(true);
        button_NoGO.SetActive(true);

        gameObject.SetActive(true);

    }

    //при выборе одного из ответатов закрытие окна
    public void closeYesOrNoButton()
    {
        button_NoGO.SetActive(false);
        button_YesGO.SetActive(false);
        button_OkGO.SetActive(true);
        gameObject.SetActive(false);
    }

    //Valitate для SaveLoad класс
    //public void 

}
