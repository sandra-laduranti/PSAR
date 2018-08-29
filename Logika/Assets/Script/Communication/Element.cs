using UnityEngine;
using System.Collections;

public class Element  {

	private string fleur;
    private string taille;
    private string couleur;
    private string nom;

    private int x;
    private int y;

    public Element()
    {
    }

    public Element(int x, int y, string fleur, string taille, string couleur, string nom)
    {
        this.fleur = fleur;
        this.taille = taille;
        this.couleur = couleur;
        this.nom = nom;
        this.x = x;
        this.y = y;
    }

    public string GetFleur()
    {
        return fleur;
    }

    public void SetFleur(string fleur)
    {
        this.fleur = fleur;
    }

    public string GetTaille()
    {
        return taille;
    }

    public void SetTaille(string taille)
    {
        this.taille = taille;
    }

    public string GetCouleur()
    {
        return couleur;
    }

    public void SetCouleur(string couleur)
    {
        this.couleur = couleur;
    }

       public string GetNom()
    {
        return nom;
    }

     public void SetNom(string nom)
      {
           this.nom = nom;
      }

    public int GetX()
    {
        return x;
    }

    public void SetX(int x)
    {
        this.x = x;
    }

    public int GetY()
    {
        return y;
    }

    public void SetY(int y)
    {
        this.y = y;
    }

    public override string ToString()
    {
        return x + " " + y + " " + fleur + " " + taille + " " + couleur + " " + nom;
    }


}
