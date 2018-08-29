using UnityEngine;
using System.Collections;

public class ClavierHandler : MonoBehaviour {

    public GameObject clavier1, clavier2;

    //initialise en ne montrant que le premier clavier
    void Start()
    {
        clavier1.SetActive(true);
        clavier2.SetActive(false);
    }

    public void SwitchClavier()
    {
        Debug.Log("in switchClavier");
        if (clavier1.activeSelf)
        {
            clavier1.SetActive(false);
            clavier2.SetActive(true);
            Debug.Log("switch to 2");
        }
        else
        {
            clavier1.SetActive(true);
            clavier2.SetActive(false);
            Debug.Log("switch to 1");
        }
    }

}
