using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisableCamera : MonoBehaviour, IPointerEnterHandler
{
    MaxCamera camera;
    bool camState = true;

	// Use this for initialization
	void Start () {
        if (GameObject.FindWithTag("MainCamera"))
        {
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().GetComponent<MaxCamera>();
        }
	}
	
	// Update is called once per frame
	void Update () {

	
	}

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (camera && camState)
            {
                camera.setCamera(false);
                camState = false;
            }

        }
        else
        {
            if (camera && camState == false)
            {
                camera.setCamera(true);
                camState = true;
            }
        }
    }

    public void enableCamera()
    {
        Debug.Log("enable");
    }

    public void disableCamera()
    {
        Debug.Log("disable");
    }

    //void OnMouseEnter()
    //{
    //    //camera = GameObject.Find("MaxCamera").GetComponent<Camera>();
    //   // camera.enabled = false;
    //    Debug.Log("onmouseEnter: false\n");
    //}

    //void OnMouseExit()
    //{
    //    //camera = GameObject.Find("MaxCamera").GetComponent<Camera>();
    //    //camera.enabled = true;
    //    Debug.Log("onMouseExit: true\n");
    //}

    public void OnMouseEnter()
    {
        print("There is a mouse on me!");
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer enter");
    }
}
