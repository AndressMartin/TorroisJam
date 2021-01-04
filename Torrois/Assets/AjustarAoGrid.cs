using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjustarAoGrid : MonoBehaviour
{
    Transform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTransform.position = new Vector3(Mathf.Floor(myTransform.position.x) + 0.5f, Mathf.Floor(myTransform.position.y) + 0.5f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
