using UnityEngine;

public class OnOfCS : MonoBehaviour
{
    [SerializeField]private GameObject _gameObject;

    public void ButtonOnOf() 
    {
        OnOf(_gameObject);
    }

    public static void  OnOf(GameObject gameObject)
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            Debug.Log("����");
        }
        else
        {
            gameObject.SetActive(false);
            Debug.Log("������");
        }  
    }
}
