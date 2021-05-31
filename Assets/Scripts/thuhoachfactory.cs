using UnityEngine;

public class thuhoachfactory : MonoBehaviour
{
    public GameObject factory;

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            factory.GetComponent<LandFactory>().OnThuHoach(factory.GetComponent<LandFactory>().idFactory);
        }
    }
}
