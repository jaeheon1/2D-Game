using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{

    bool hasPackage;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1,1);
    [SerializeField] Color32 hasPackageColor1 = new Color32(2, 2, 2, 2);

    private void Start()
    {
       sprite.GetComponent<SpriteRenderer>();

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collison");
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (the thing we trigger is ther package
        if(other.tag=="Package"&&!hasPackage)
        {
            
            Debug.Log("Package is collison");
            hasPackage = true;
            sprite.color = hasPackageColor;
            Destroy(other.gameObject, 0.5f);

            


        }
        if(other.tag=="Customer"&& hasPackage==true)
        {
            Debug.Log("Package Delivered");
            hasPackage = false;
            sprite.color = hasPackageColor1;


        }
    }
}
