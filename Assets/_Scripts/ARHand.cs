using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARHand : MonoBehaviour
{
    bool isInfected = false;
    public bool IsInfected
    {
        get { return isInfected; }
        set
        {
            isInfected = value;
            if (isInfected)
            {
                bacteriaOnHand.SetActive(true);
            }
        }
    }
    public GameObject bacteriaOnHand;
    public GameObject bacteriaPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(transform.position);
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("bacteria"))
        {
            IsInfected = true;
        }
        if (coll.gameObject.CompareTag("Contagious") && IsInfected)
        {
            GameObject bacInstance = Instantiate(bacteriaPrefab, coll.contacts[0].point, Quaternion.identity);
            bacInstance.transform.SetParent(coll.gameObject.transform.parent);
            bacInstance.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}
