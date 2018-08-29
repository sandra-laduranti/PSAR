using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class OpenDialog : MonoBehaviour {

    public Dropdown fName;
    public Dropdown directory;
    public Button openButton;
    public Button cancelButton;
    public GameObject dialogObj;
    public GameObject mainFrame;
    private string curDir = Directory.GetCurrentDirectory();
    private TerrainHandler terrainHandler;


    private static int NB_FORMULE = 30;
    private static string FORMULE_PATH = "Canvas/PanelDessous/FormulePan/ScrollParent/LargePanel/";

    void Start()
    {
        terrainHandler = (TerrainHandler)GameObject.Find("Plane").GetComponent<TerrainHandler>();
    }

    public void setUp()
    {
        mainFrame.SetActive(true);
        dialogObj.SetActive(true);

        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(open);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(ClosePanel);

        updateDirPath(".");
        directory.onValueChanged.AddListener(delegate {
            updateDirDropDown();
        });
        updateFilePath(curDir);
        fName.onValueChanged.AddListener(delegate
        {
            updateFileDropDown();
        });
        fName.gameObject.SetActive(true);
        directory.gameObject.SetActive(true);
        openButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        dialogObj.SetActive(false);
        mainFrame.SetActive(false);
    }

    void updateFilePath(string path)
    {
        fName.options.Clear();

        foreach (string file in Directory.GetFiles(path))
        {
            fName.options.Add(new Dropdown.OptionData() { text = file });
        }

        fName.value = 0;
        if(fName.options.Count > 0)
        {
            fName.captionText.text = fName.options.ToArray()[0].text;
        }
    }

    void updateDirPath(string path)
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
        curDir = newPath;
    }

    void updateDirDropDown()
    {
        Dropdown.OptionData opt = directory.options.ToArray()[directory.value];
        updateDirPath(opt.text);
        updateFilePath(curDir);
        directory.value = 0;
    }

    void updateFileDropDown()
    {
        fName.captionText.text = fName.options.ToArray()[fName.value].text;
    }

    void open()
    {

        string path = fName.options.ToArray()[fName.value].text;

        if (path.Length == 0)
        {
            /* Invalid path */
            Debug.Log("Invalid path");
            return;
        }

        try
        {
            StreamReader sr = new StreamReader(@path);
            string line = "";
            int formCpt = 1;
            InputField iField;
            Jardin jardin = new Jardin();

            //Lecture des eventuelles commentaire en amont
            while (line != "#FORMULES" && !sr.EndOfStream)
            {
                line = sr.ReadLine();
            }

            if (sr.EndOfStream)
            {
                sr.Close();
                ClosePanel();
                return;
            }
            //Lecture formules
            line = sr.ReadLine();
            while(line != "#JARDIN" && !sr.EndOfStream)
            {
                iField = (InputField)GameObject.Find(FORMULE_PATH + "Form_" + formCpt).GetComponent<InputField>();
                iField.text = line;
                formCpt++;
                if(formCpt > NB_FORMULE)
                {
                    break;
                }
                line = sr.ReadLine();
            }

            if (sr.EndOfStream)
            {
                sr.Close();
                ClosePanel();
                return;
            }
            //Lecture jardin
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] splitted = line.Split();
                jardin.Add(Int32.Parse(splitted[0]), Int32.Parse(splitted[1]),
                    splitted[2], splitted[3], splitted[4],
                    splitted[5].Length == 0 ? null : splitted[5]);
            }
            terrainHandler.setJardin(jardin);

            sr.Close();
            ClosePanel();
        }
        catch (Exception)
        {
            Debug.Log("Error while opening file path=" + path);
            return;
        }
    }
}
