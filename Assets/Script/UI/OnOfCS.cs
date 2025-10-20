using UnityEngine;

public class OnOfCS : MonoBehaviour
{
    public static void  OnOf(GameObject gameObject) 
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            Debug.Log("Ç¬Ç¢ÇΩ");
        }
        else
        {
            gameObject.SetActive(false);
            Debug.Log("è¡Ç¶ÇΩ");
        }
        
    }
}
