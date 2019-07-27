using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompilationForUI : MonoBehaviour {

    public LinkedList<ProductComp> productComp_List;
    public LinkedList<ProductStatic> productStatic_List;

    public void mass_add_day(int id_product, float mass)
    {

        recal_ProductComp_day(find_ProductComp_fromList_byId(id_product), mass, find_ProductStatic_fromList_byId(id_product));
    }

    public void mass_add_night(int id_product, float mass)
    {

    }

    void Awake()
    {
        productStatic_List.AddLast(new ProductStatic(1, "Жидкий  свиной навоз", 0.1f, 0.9f, 0.65f, 0.4f));
        productStatic_List.AddLast(new ProductStatic(2, "Силос", 0.2f, 0.8f, 0.85f, 0.25f));
        productStatic_List.AddLast(new ProductStatic(3, "Cыворотка", 0.02f, 0.98f, 0.8f, 0.62f));

        productComp_List.AddLast(new ProductComp(1));
        productComp_List.AddLast(new ProductComp(2));
        productComp_List.AddLast(new ProductComp(3));
    }
    // Use this for initialization
    void Start() {

        mass_add_day(1, 100);

    }

    // Update is called once per frame
    void Update() {

    }

    private void recal_ProductComp_day(ProductComp productComp, float mass_ForAdd, ProductStatic productStatic)
    {
        productComp.dayMass += mass_ForAdd;
        productComp.dayMassSV = productComp.dayMass * productStatic.coffSV;
        productComp.dayMassWater = productComp.dayMass * productStatic.coffWater;
        productComp.metanDay = productComp.dayMassSV * productStatic.coffMetana * productStatic.valueGas;

        Debug.Log(productComp.toString());

    }



    private void recal_ProductComp_night(ProductComp productComp, float mass_ForAdd)
    {

    }

    private ProductComp find_ProductComp_fromList_byId(int id)
    {
        foreach (ProductComp product in productComp_List)
       {
            if (id == product.get_fk_id())
            {
                return product;
            };
        }

        //Exception
        return null;
    }

    private ProductStatic find_ProductStatic_fromList_byId(int id)
    {
        foreach (ProductStatic product in productStatic_List)
        {
            if (id == product.getUniqueProduct_id())
            {
                return product;
            };
        }

        //Exception
        return null;
    }
}
