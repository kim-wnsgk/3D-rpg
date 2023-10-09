
using UnityEngine;
public class Shop : MonoBehaviour
{
    public GameObject popupObject;

    void Start()
    {
        popupObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popupObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popupObject.SetActive(false);
        }
    }
}