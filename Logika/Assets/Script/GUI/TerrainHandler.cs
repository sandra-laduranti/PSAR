using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Globalization;

public class TerrainHandler : MonoBehaviour
{
    public ToggleGroup myToggleGroup;
    public Dropdown buttonFlower;
    public Dropdown buttonColor;
    public Dropdown buttonSize;
    public Dropdown buttonConstante;
    public Button buttonClean;
    public Text errorText;
    public enum ToggleType { Create, Delete, Modify, View };


    private Jardin _jardin;
    private ToggleType mode;
    private ToggleType lastMode;
    private Flower _flower; //Dernière fleur manipulée
    private Flower _lastModifFlower; //Dernière fleur modifiée
    private GameObject _previousGameObject;
    public bool _onModify; // si jamais on est en train de modifier une fleur

    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        _jardin = new Jardin();    //initialisation d'un jardin vide
        mode = ToggleType.View;  //le mode est en défault au départ
        lastMode = mode;
        _flower = null;
        _previousGameObject = null;
        buttonClean.onClick.AddListener(cleanJardin);
    }

    //permet de récupérer le bon type de l'enum via le nom du toggle selectionné
    //set le mode du logiciel
    void setToggleType()
    {
        if (myToggleGroup)
        {
            Toggle activeToggle = myToggleGroup.ActiveToggles().FirstOrDefault();
            mode = (ToggleType)Enum.Parse(typeof(ToggleType), activeToggle.name);
        }

    }

    //reviens en mode view après une modification
    public void setToggleActive(string name)
    {
        if (myToggleGroup)
        {

            myToggleGroup.SetAllTogglesOff();
            GameObject.Find(name).GetComponent<Toggle>().isOn = true;
           // myToggleGroup.NotifyToggleOn(GameObject.Find("View").GetComponent<Toggle>());
            setToggleType();
        }
    }

    //test si la constante choisie dans le dropdown est déjà utilisée
    //si c'est le cas on set le message d'erreur du menu
    bool testConstante()
    {
        String constante = buttonConstante.options.ElementAt(buttonConstante.value).text;

        if (_jardin.existConst(constante))
        {
            errorText.text = "Constante déjà utilisée";
            return false;
        }
        errorText.text = "";
        return true;
    }

    //Création d'une fleur par récupération du contenu des dropdown
    //On supprime la fleur si jamais elle n'est pas attribuée à une tuile pour
    // pouvoir la reconstruire avec les nouvelles valeurs des dropdown
    void CreateFlower(GameObject tile)
    {
        if (_flower != null)
            if (!_flower.isAttribuate() )
            {
                _flower.destroyFlower();
            }

        Flower.Couleur couleur = (Flower.Couleur)Enum.Parse(typeof(Flower.Couleur), buttonColor.options.ElementAt(buttonColor.value).text);
        Flower.Espece espece = (Flower.Espece)Enum.Parse(typeof(Flower.Espece), buttonFlower.options.ElementAt(buttonFlower.value).text);
        Flower.Taille taille = (Flower.Taille)Enum.Parse(typeof(Flower.Taille), buttonSize.options.ElementAt(buttonSize.value).text);
        String constante = buttonConstante.options.ElementAt(buttonConstante.value).text;

        //check si la constante n'est pas déjà utilisée, si c'est le cas on stop la création
        if (_jardin.existConst(constante))
        {
            errorText.text = "Constante déjà utilisée";
            return;
        }
        //crée la fleur
        _flower = tile.AddComponent<Flower>();
        _flower.setFlower(espece, couleur, taille, constante);
        hit.collider.gameObject.GetComponent<Tiles>().setFlower(_flower);
    }

    //Modification d'une fleur, set les dropdown avec les valeurs de la fleur récupérée
    void ModifyFlower(GameObject tile)
    {
        //Debug.Log("modif flower");
        if (Input.GetMouseButtonDown(0))
        {
            Flower flower = tile.GetComponent<Tiles>().getFlower();
            if (_flower == null)
                return;

            _flower = flower;
            buttonColor.value = buttonColor.options.FindIndex(a => a.text == _flower.couleur.ToString());
            buttonFlower.value = buttonFlower.options.FindIndex(a => a.text == _flower.espece.ToString());
            buttonSize.value = buttonSize.options.FindIndex(a => a.text == _flower.taille.ToString());
            buttonConstante.value = buttonConstante.options.FindIndex(a => a.text == _flower.constante);
            buttonColor.RefreshShownValue();
            buttonFlower.RefreshShownValue();
            buttonSize.RefreshShownValue();
            buttonConstante.RefreshShownValue();
            //on supprime la constante des listes pour que ce ne soit pas bloquant quand on passera dans create.
            if (_flower.Element != null)
            {
                _jardin.Remove(_flower.Element);
                _flower.Element = null;
            }
            //on désattribue la fleur de sa tuile
            _flower.desattribuate();
            setToggleActive("Create");

        }
    }



    // Update is called once per frame
    //check le type selectionné pour effectuer les différentes actions correspondantes
    //Récupère l'objet pointé par la souris et l'analyse si jamais c'est une tuile
    void Update()
    {
        setToggleType();
        //update du type
        if (lastMode != mode)
        {
            //clean le terrain si on change de mode pour supprimer la fleur restante
            if (lastMode == ToggleType.Create && _flower)
            {
                Debug.Log("last Mode && flower\n");
                if (!_flower.isAttribuate())
                    _flower.destroyFlower();
            }
            if (lastMode == ToggleType.Modify && _flower)
            {
                if (!_flower.isAttribuate())
                    _flower.destroyFlower();
            }
            lastMode = mode;
        }
        //on récupère l'objet pointé par la souris
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (_previousGameObject == null)
                _previousGameObject = hit.collider.gameObject;

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Tiles"))
            {
                if (mode == ToggleType.Create)
                {
                    if (_previousGameObject != hit.collider.gameObject && testConstante())
                    {
                        CreateFlower(hit.collider.gameObject);
                    }
                }
                if (mode == ToggleType.Modify )
                {
                    errorText.text = "";
                    ModifyFlower(hit.collider.gameObject);
                }
                if (mode == ToggleType.View || mode == ToggleType.Delete)
                    errorText.text = "";
            }
        }
    }

    //restore un jardin depuis une sauvegarde
    public void setJardin(Jardin newJardin)
    {
        _jardin = newJardin;

        GameObject[] plane = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject gameTile in plane) // Loop through List with foreach.
        {
            //clean de l'ancien jardin
            Tiles tuile = gameTile.GetComponent<Tiles>();
            if (tuile.getFlower())
            {
                tuile.getFlower().destroyFlower();
                Debug.Log("tuile(" + tuile.x + "," + tuile.y + ") destroy flower");
            }
            //ajout d'une fleur si jamais il y en a une à ces coordonnées
            Element elem = _jardin.existFlowerOnTile(tuile.x, tuile.y);
            if (elem != null)
            {
                Debug.Log(" elem x: " + elem.GetX() + " elem_y " + elem.GetY() + " non null!");
                _flower = gameTile.AddComponent<Flower>();
                _flower.setFlowerFromElem(elem);
                tuile.setFlower(_flower);
                tuile.attributeFlower();
            }

        }
    }

    public void cleanJardin()
    {
        GameObject[] plane = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject gameTile in plane) // Loop through List with foreach.
        {
            //clean de l'ancien jardin
            Tiles tuile = gameTile.GetComponent<Tiles>();
            if (tuile.getFlower())
            {
                 Element elem = _jardin.existFlowerOnTile(tuile.x, tuile.y);
                 if (elem != null)
                 {
                     _jardin.Remove(elem);
                 }
                tuile.getFlower().destroyFlower();
            }
        }
    }

    //récupération du jardin pour les formules
    public Jardin getJardin()
    {
        return _jardin;
    }

    public ToggleType getMode()
    {
        return mode;
    }


}

