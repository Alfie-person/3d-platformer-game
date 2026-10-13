using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    int coins = 0;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            Debug.Log("Coins: " + coins);
        }
    }
}
// ep 7 timestamp: 20:31 of 31:08