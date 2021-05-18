using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        //DataGlobal.instance.ArrayLand = GameObject.FindGameObjectsWithTag("Land");
        //DataGlobal.instance.ArrayLandFactory = GameObject.FindGameObjectsWithTag("LandFactory");
        //DataGlobal.instance.ArrayHaveOwnedItem = new int[3];
        DataGlobal.instance.ArrayAmount = new int[20];
        //DataGlobal.instance.ArrayChuong = GameObject.FindGameObjectsWithTag("Chuong");
        UIManager.instance.WhereHouse = GameObject.FindGameObjectWithTag("House");
        DataGlobal.instance.levelCurrentOfFactory = new int[DataGlobal.instance.ArrayLandFactory.Length];
    }
}
