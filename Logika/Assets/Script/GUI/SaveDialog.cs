using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.IO;
using System;

public class SaveDialog : MonoBehaviour {

    public InputField fName;
    public Dropdown directory;
    public Button saveButton;
    public Button cancelButton;
    public GameObject dialogObj;
    public GameObject mainFrame;
    private string curDir = Directory.GetCurrentDirectory();
    private TerrainHandler terrainHandler;

    private static int NB_FORMULE = 30;
    private static string FORMULE_PATH = "Canvas/PanelDessous/FormulePan/ScrollParent/LargePanel/";

    void Start ()
    {
        terrainHandler = (TerrainHandler)GameObject.Find("Plane").GetComponent<TerrainHandler>();
    }

    public void setUp()
    {
        mainFrame.SetActive(true);
        dialogObj.SetActive(true);

        saveButton.onClick.RemoveAllListeners();
        saveButton.onClick.AddListener(save);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(ClosePanel);

        updatePath(".");
        directory.onValueChanged.AddListener(delegate {
            updateDropDown();
        });
        fName.gameObject.SetActive(true);
        directory.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        dialogObj.SetActive(false);
        mainFrame.SetActive(false);
    }

    void updatePath(string path)
    {
        string newPath = "";
        directory.options.Clear();
        directory.options.Add(new Dropdown.OptionData() { text = "." });
        directory.options.Add(new Dropdown.OptionData() { text = ".." });
        if (path == ".")
        {
            newPath = curDir;
        }
        else if (path == "..")
        {
            newPath = Directory.GetParent(curDir).ToString();
        }
        else
        {
            newPath = path;
        }

        foreach (string dir in Directory.GetDirectories(newPath))
        {
            directory.options.Add(new Dropdown.OptionData() { text = dir });
        }

        directory.captionText.text = newPath;
        fName.text = newPath;
        curDir = newPath;
    }

    void updateDropDown()
    {
        Dropdown.OptionData opt = directory.options.ToArray()[directory.value];
        updatePath(opt.text);
        directory.value = 0;
    }

    void save()
    {
        /*JARDIN*/
        Jardin jardin = terrainHandler.getJardin();
        string path = fName.text;

        if (path.Length == 0)
        {
            /* Invalid path */
            Debug.Log("Invalid path");
            return;
        }

        try
        {
            StreamWriter sw = new StreamWriter(@path);

            /*Write formules */
            sw.WriteLine("#FORMULES");
            for (int i = 1; i <= NB_FORMULE; i++)
            {
                InputField iField = (InputField)GameObject.Find(FORMULE_PATH + "Form_" + i).GetComponent<InputField>();
                if (iField.text.Length == 0) continue;
                sw.WriteLine(iField.text);
            }

            /*Write jardin*/
            sw.WriteLine("#JARDIN");
            foreach (Element elem in jardin.GetElements())
            {
                sw.WriteLine(elem.ToString());
            }

            Debug.Log("sauvegarder dans : " + path);
            sw.Close();
            ClosePanel();
        }
        catch (Exception)
        {
            Debug.Log("Error while writing file path="+path);
            fName.text = "Dossier \"" + path + "\" introuvable !";
            return;
        }
    }
    
}
