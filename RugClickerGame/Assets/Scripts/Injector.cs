using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RugPart")
        {
            if (other.transform.GetComponent<MeshRenderer>())
            {
                other.transform.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
