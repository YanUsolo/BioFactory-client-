using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompParametrs : MonoBehaviour {


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







    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  //  private void productComp_create_add(int id, )
   
}
