using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClavierButton : MonoBehaviour {
    public GameObject clavierGameObj;
    private ClavierHandler clavier;
    private Button myButton;

    void Awake()
    {
        if (clavierGameObj != null)
        {
            clavier = clavierGameObj.GetComponent<ClavierHandler>();
        }
        myButton = GetComponent<Button>(); // <-- you get access to the button component here
        myButton.onClick.AddListener(ChangeClavier);  // <-- you assign a method to the button OnClick event here
    }

    void ChangeClavier()
    {
        Debug.Log("click");
        if (clavier != null)
        {
            Debug.Log("switch");
            clavier.SwitchClavier();
        }
    }

    void ShowClavier()
    {
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
