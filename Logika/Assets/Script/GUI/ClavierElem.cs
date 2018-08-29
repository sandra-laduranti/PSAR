using UnityEngine;
using System.Collections;
using System.ComponentModel;
using UnityEngine.UI;
using System;
using System.Reflection;
using UnityEngine.EventSystems;

public class ClavierElem : MonoBehaviour {
    public string symb;
    private InputField backupField;
    private InputField iField;
    private Button myButton;
    private CaretPos caret; /* Script chargé par le formulaire sauvegardant la position du curseur */
    private FocusHandler focusHandler; /* Script gérant le formulaire actuellement focus */
    private int caretPos; /* Variable locale position du curseur */

    void Awake()
    {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here
        myButton.onClick.AddListener(CompleteIField);  // <-- you assign a method to the button OnClick event here
        focusHandler = (FocusHandler)GameObject.Find("Canvas/PanelDessous/FormulePan").GetComponent<FocusHandler>();
        backupField = (InputField)GameObject.Find("Canvas/PanelDessous/FormulePan/ScrollParent/LargePanel/Form_1").GetComponent<InputField>();
    }

    //rempli le champ imput avec le symbole selectionné
    public void CompleteIField()
    {
        iField = focusHandler.getCurrentFormule();
        if (iField == null)
        {
            iField = backupField;
        }
        caret = (CaretPos)iField.GetComponent(typeof(CaretPos));

        ClavierEnum clavierEnum = (ClavierEnum)Enum.Parse(typeof(ClavierEnum), symb, true);
        String realSymb = GetEnumDescription(clavierEnum);
        caretPos = caret.getCaretPos();
        try
        {
            iField.text = iField.text.Substring(0, caretPos) + realSymb + iField.text.Substring(caretPos);
            caretPos += realSymb.Length;
            caret.setCaretPos(caretPos); /* On a ajouter un caractère */
        }
        catch (ArgumentOutOfRangeException) /* Utile si l'utilisateur fait une selection et la supprime */
        {
            iField.text += realSymb;
            caretPos = iField.text.Length;
            caret.setCaretPos(caretPos);
        }
        iField.Select();
        iField.ActivateInputField();
        StartCoroutine(MoveTextEnd());
    }

    IEnumerator MoveTextEnd()
    {
        yield return 0;
        iField.MoveTextEnd(false);
        iField.caretPosition = caretPos; /* repositionne le curseur juste après l'ajout */
    }

    //Récupère la description dans l'enumeration s'il y en a une (par exemple pour les symboles
    //Renvoie la description s'il y en a une, sinon renvoie le String de la valeur
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   /* 
    public string symb;
    private InputField iField;
    private Button myButton;
    private CaretPos caret; // Script chargé par le formulaire sauvegardant la position du curseur 
    private FocusHandler focusHandler; //Script gérant le formulaire actuellement focus 
    private int caretPos; //Variable locale position du curseur 
     void Awake()
    {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here
        myButton.onClick.AddListener(CompleteIField);  // <-- you assign a method to the button OnClick event here
        focusHandler = (FocusHandler)GameObject.Find("Canvas/PanelDessous/FormulePan").GetComponent<FocusHandler>();
    }

    //rempli le champ imput avec le symbole selectionné
    public void CompleteIField()
    {
        iField = focusHandler.getCurrentFormule();
        caret = (CaretPos)iField.GetComponent(typeof(CaretPos));

        ClavierEnum clavierEnum = (ClavierEnum)Enum.Parse(typeof(ClavierEnum), symb, true);
        String realSymb = GetEnumDescription(clavierEnum);
        caretPos = caret.getCaretPos();
        try {
            iField.text = iField.text.Substring(0, caretPos) + realSymb + iField.text.Substring(caretPos);
            caretPos += realSymb.Length;
            caret.setCaretPos(caretPos); // On a ajouter un caractère 
        }
        catch(ArgumentOutOfRangeException) // Utile si l'utilisateur fait une selection et la supprime 
        {
            iField.text += realSymb;
            caretPos = iField.text.Length;
            caret.setCaretPos(caretPos);
        }
        iField.Select();
        iField.ActivateInputField();
        StartCoroutine(MoveTextEnd());
    }

    IEnumerator MoveTextEnd()
    {
        yield return 0;
        iField.MoveTextEnd(false);
        iField.caretPosition = caretPos; // repositionne le curseur juste après l'ajout 
    }

    //Récupère la description dans l'enumeration s'il y en a une (par exemple pour les symboles
    //Renvoie la description s'il y en a une, sinon renvoie le String de la valeur
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}*/
}
