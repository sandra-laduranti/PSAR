using UnityEngine;
using System.Collections;
using System.Globalization;

public class Flower : MonoBehaviour {
    public enum Espece { Rose, Paquerette, Tulipe };
    public enum Couleur { Rose, Rouge, Blanche };
    public enum Taille { Petite, Grande, Moyenne };

    private Espece _espece;
    private Couleur _couleur;
    private  Taille _taille;
    private string _const;

    private Element _element;

 

    //si valeurs à -42: pas encore attribué à une tuile
    public float _x; 
    public float _y;

    GameObject objToSpawn;

    public void setFlower(Espece espece, Couleur couleur, Taille taille, string constante)
    {
        _espece = espece;
        _couleur = couleur;
        _taille = taille;
        _const = constante;
        _x = -42;
        _y = -42;
        Element = null;
        objToSpawn = null;

    }

    public void setFlowerFromElem(Element elem)
    {
        string espece = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(elem.GetFleur().ToLower());
        string couleur = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(elem.GetCouleur().ToLower());
        string taille = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(elem.GetTaille().ToLower());
        _const = elem.GetNom();
        Element = elem;
        //_x = (float)elem.GetX();
        //_y = (float)elem.GetY();
        _x = -42;
        _y = -42;


        if (elem.GetNom() == null)
        {
            _const = "none";
        }
        if (string.Compare(couleur, "Blanc") == 0)
            couleur = "Blanche";
        switch (taille)
        {
            case "Grand":
                taille = "Grande";
                break;
            case "Moyen":
                taille = "Moyenne";
                break;
            case "Petit":
                taille = "Petite";
                break;
        }
        _espece = (Espece)System.Enum.Parse(typeof(Espece), espece);
        _couleur = (Couleur)System.Enum.Parse(typeof(Couleur), couleur);
        _taille = (Taille)System.Enum.Parse(typeof(Taille), taille);
    }

    public void setXY(float x, float y)
    {
        _x = x;
        _y = y;
    }

    //si x ou y sont à -42 c'est que l'element n'a pas de coordonées 
    public bool isAttribuate(){
        if (-_x == -42 || _y ==-42)
            return false;
        return true;
    }

    public void desattribuate()
    {
        _x = -42;
        _y = -42;
    }

    void SetTransformPos(float x, float y, float z)
    {
        objToSpawn.transform.position = new Vector3(x, y, z);
    }

    //retourne un tableau de taille en fonction de l'espece
    //[petit, moyen, grand]
    float[] getScale(Espece espece)
    {
        float[] tab = new float[3];

        switch (espece)
        {
            case Espece.Paquerette:
                tab[0] = 10;
                tab[1] = 12;
                tab[2] = 15;
                break;
            case Espece.Rose:
                tab[0] = 3;
                tab[1] = 4;
                tab[2] = 5;
                break;
            case Espece.Tulipe:
                tab[0] = 1;
                tab[1] = 1.5f;
                tab[2] = 2;
                break;
        }
        return tab;
    }

    void SetSize()
    {
        float sizeScale = 0f ;
        float[] tabSize = getScale(_espece);

        switch (_taille)
        {
            case Taille.Petite:
                sizeScale = tabSize[0];
                break;
            case Taille.Moyenne:
                sizeScale = tabSize[1];
                break;
            case Taille.Grande:
                sizeScale = tabSize[2];
                break;
        }
      /* if (_espece == Espece.Tulipe)
             objToSpawn.transform.localScale += new Vector3(0.1F,0.1F,0.1F);
        else*/
             objToSpawn.transform.localScale = new Vector3(sizeScale, sizeScale, sizeScale);
        
    }

    public GameObject spawnFlower(float x, float y, float z)
    {
        string objFleur = _espece.ToString() + _couleur.ToString();
       objToSpawn = Instantiate(Resources.Load(objFleur)) as GameObject;
        objToSpawn.transform.position = new Vector3(x, y, z);
        SetSize();
        Renderer rend = objToSpawn.GetComponent<Renderer>();
        if (string.Compare(_const,"none") != 0)
            objToSpawn.GetComponent<TextMesh>().text = _const;
        else
            objToSpawn.GetComponent<TextMesh>().text = "";
        return objToSpawn;
    }

    //convert Flower to Element
    public Element copyToElement()
    {
        string fleur = _espece.ToString().ToLower();
        string taille = _taille.ToString().ToLower();
        string couleur = _couleur.ToString().ToLower();
        string nom = _const.ToString().ToLower();
        int x = (int)_x;
        int y = (int)_y;
        if (string.Compare(nom, "none") == 0)
            nom = null;
        //correction des feminins
        if (string.Compare(couleur, "blanche") == 0)
            couleur = "blanc";
        switch (taille)
        {
            case "grande":
                taille = "grand";
                break;
            case "moyenne":
                taille = "moyen";
                break;
            case "petite":
                taille = "petit";
                break;
        }
        Element = new Element(x,y, fleur, taille, couleur, nom);
        return Element;
    }

    public void destroyFlower()
    {
        _x = -42;
        _y = -42;
        if (objToSpawn)
            Destroy(objToSpawn);
           
    }

    public void destroyFlower(Jardin jardin)
    {
        _x = -42;
        _y = -42;
        if (objToSpawn)
            Destroy(objToSpawn);
        if (Element != null)
        {
            jardin.Remove(Element);
            Element = null;
        }
    }

    public Couleur couleur
    {
        get { return _couleur; }
        set { _couleur = value; }
    }

    public Taille taille
    {
        get { return _taille; }
        set { _taille = value; }
    }

    public Espece espece
    {
        get { return _espece; }
        set { _espece = value; }
    }

    public string constante
    {
        get { return _const; }
        set { _const = value; }
    }

    public Element Element
    {
        get { return _element; }
        set { _element = value; }
    }
}
