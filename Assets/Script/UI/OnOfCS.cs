using UnityEngine;

public class OnOfCS : MonoBehaviour
{
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
