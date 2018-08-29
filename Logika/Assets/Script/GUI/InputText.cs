using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class InputText : MonoBehaviour {

    public GameObject mainInputField;

	// Use this for initialization
	void Start () {
        mainInputField.GetComponent<InputField>().lineType = InputField.LineType.MultiLineNewline;
	}

	//When you press a button, this method is called.
	public void ChangeInputField(int type)
	{
		if (type == 0)
		{
			//Change the input field to "Single Line" line type.
			//mainInputField.GetComponent<InputField> ().lineType = InputField.LineType.SingleLine;
		}
		else if (type == 1) 
		{
			//Change the input field to "MultiLine Newline" line type.
			//mainInputField.GetComponent<InputField> ().lineType = InputField.LineType.MultiLineNewline;
		}
		else if (type == 2) 
		{
			//Change the input field to "MultiLine Submit" line type.
			//mainInputField.GetComponent<InputField> ().lineType = InputField.LineType.MultiLineSubmit;
		}
	}
}