using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AideDialog : MonoBehaviour {

    public GameObject dialogObj;
    public Button buttonClose;

	// Use this for initialization
	void Start () {
        dialogObj.SetActive(false);
        buttonClose.onClick.AddListener(ClosePanel);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void setUp()
    {
        dialogObj.SetActive(true);
    }

    void ClosePanel()
    {
        dialogObj.SetActive(false);
    }
}
