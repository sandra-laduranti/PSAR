using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Logique0._1.grammaire.tools;
using PerCederberg.Grammatica.Runtime;
using System;
using IronPython.Runtime.Exceptions;

public class VerifButton : MonoBehaviour {
    private Button myButton;
    private InputField iField;
    private Text output;
    private static int NB_FORMULE = 30;
    private static string PATH = "Canvas/PanelDessous/FormulePan/ScrollParent/LargePanel/";
    private TerrainHandler terrainHandler;
    private static string SCRIPT_PYTHON = "psar_python.py";

    void Awake()
    {
        myButton = GetComponent<Button>(); // <-- you get access to the button component here
        myButton.onClick.AddListener(VerifFormule);  // <-- you assign a method to the button OnClick event here
        terrainHandler = (TerrainHandler)GameObject.Find("Plane").GetComponent<TerrainHandler>();
    }

    public void VerifFormule()
    {

        Jardin jardin = terrainHandler.getJardin();
        Communicate comm = new Communicate(SCRIPT_PYTHON, jardin);
        //lance le script
        comm.GetSource().Execute(comm.GetScope());
        for (int i = 1; i <= NB_FORMULE; i++)
        {
            iField = (InputField)GameObject.Find(PATH + "Form_" + i).GetComponent<InputField>();
            output = (Text)GameObject.Find(PATH + "VerifPan_" + i + "/verif_resultat_" + i).GetComponent<Text>();

            if (iField.text == "")
            {
                continue;
            }
            try
            {
                Formule formule = FormuleFactory.parse(iField.text);
                
                
                foreach (Element el in jardin.GetElements())
                {
                    Debug.Log(el);
                }

                object pythonForm = comm.HandleForm(formule);
                Func<Jardin, object> unity_my_interp_formul = comm.GetScope().GetVariable<Func<Jardin, object>>("unity_my_interp_formul");
                object pythonJardin = unity_my_interp_formul(jardin);
                Func<object, object, object> unity_eval_one_form = comm.GetScope().GetVariable<Func<object, object, object>>("unity_eval_one_form");
                var res = unity_eval_one_form(pythonJardin, pythonForm);

                //Affiche résultat
                if ((bool)res == true)
                {
                    output.color = Color.green;
                    output.text = "Vrai";
                }
                else
                {
                    output.color = Color.red;
                    output.text = "Faux";
                }
            }
            catch (ParserLogException)
            {
                output.color = Color.red;
                output.text = "Erreur";
                Debug.Log("Erreur formule");
            }
            catch (ValueErrorException)
            {
                output.color = new Color32(255, 128, 0, 255);
                output.text = "Var libre";
            }
            catch (Exception)
            {
                output.color = Color.red;
                output.text = "Erreur imprévue";
            }
        }
    }
}
