using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FocusHandler : MonoBehaviour {

    private InputField currentFormule;
    private Text currentVerif;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateFormule(InputField formule)
    {
        currentFormule = formule;
    }

    public void updateVerif(Text verif)
    {
        currentVerif = verif;
    }

    public InputField getCurrentFormule()
    {
        return currentFormule;
    }

    public Text getCurrentVerif()
    {
        return currentVerif;
    }
}
