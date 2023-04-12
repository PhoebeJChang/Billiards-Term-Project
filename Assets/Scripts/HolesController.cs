using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolesController : MonoBehaviour
{
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Balls"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = new Vector3((float)0, (float)0.5, (float)0);
            count = count - 1;
            SetCountText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
