using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
Met à jour la position du curseur de l'inputfield de formule
*/
public class CaretPos : MonoBehaviour {
    private InputField iField;
    private int caretPos;

    void Awake()
    {
        iField = GetComponent<InputField>();
    }

    public void updateOnClick() /* quand l'utilisateur clique quelque part dans l'inputfield */
    {
        caretPos = iField.caretPosition;
    }

    public void updateOnChanged() /* quand l'utilisateur ajouter ou supprime du texte au clavier */
    {
        if (iField.caretPosition == 0) return; /* Necessaire car la routine qui redonne le focus à l'inputfield trigger cet event avec caretPosition à 0*/
        caretPos = iField.caretPosition;
    }

    public int getCaretPos()
    {
        return caretPos;
    }

    public void setCaretPos(int pos)
    {
        caretPos = pos;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
