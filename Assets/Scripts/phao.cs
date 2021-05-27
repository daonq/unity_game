using UnityEngine;

public class phao : MonoBehaviour
{
    public GameObject stone;

    private void OnMouseDown()
    {
        if (DataGlobal.instance.AllowMouseDown)
        {
            stone.GetComponent<Stone>().no();
            gameObject.SetActive(false);
        }
    }
}
