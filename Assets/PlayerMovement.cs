using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 5, 0);
        }
    }
}
// video is in 32:45 of 39:14, making my first 3d game unity ep 2