using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    Queue<GameObject> transmittedObjects;
    // Start is called before the first frame update
    void Start()
    {
        transmittedObjects = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision coll)
    {
        
        if (transmittedObjects.Contains(coll.gameObject)) return;

        if (coll.gameObject.CompareTag("Contagious"))
        {
            GameObject bacInstance = Instantiate(this.gameObject, coll.contacts[0].point, Quaternion.identity);
            bacInstance.transform.SetParent(coll.gameObject.transform);
            bacInstance.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            transmittedObjects.Enqueue(coll.gameObject);    
        }
    }
}
