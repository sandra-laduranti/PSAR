using UnityEngine;
using System.Collections;

public class EasterPlant : MonoBehaviour
{

    public GameObject plant;
      Ray ray;
      RaycastHit hit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseOver(){
        if (Input.GetMouseButtonDown(0))
        {
            if (plant != null)
                plant.SetActive(false);
        }
    }
}
