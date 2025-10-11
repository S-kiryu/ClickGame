using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]private GameObject GameObject;

    public void ShopOnOf() 
    {
        OnOfCS.OnOf(GameObject);
    }
}
