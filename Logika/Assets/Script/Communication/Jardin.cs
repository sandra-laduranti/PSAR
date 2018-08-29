using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class Jardin {

    private List<Element> elements;
    private List<string> constantes;

    public Jardin(){
        elements = new List<Element>();
        constantes = new List<string>();
    }

    public void Add(Element element)
    {
        elements.Add(element);
        string nom  = element.GetNom();
        if (nom != null)
        {
            constantes.Add(nom);
        }
    }

    public void Add(int x, int y, string fleur, string taille, string couleur, string nom)
    {
        elements.Add(new Element(x, y, fleur, taille, couleur, nom));
        if (nom != null )
        {
            constantes.Add(nom);
        }
    }

    public bool existConst(string constante)
    {
        return constantes.Contains(constante);
    }

    public Element existFlowerOnTile(int x, int y)
    {
        foreach (Element elem in elements)
        {
            if (elem.GetX() == x && elem.GetY() == y)
                return elem;
        }
        return null;
    }

    public void Remove(Element element)
    {
        if (element.GetNom() != null )
            constantes.Remove(element.GetNom());
        elements.Remove(element);
    }

    public void RemoveConstante(string constante)
    {
        constantes.Remove(constante);
    }

    public List<string> GetConstantes()
    {
        return constantes;
    }

    public List<Element> GetElements()
    {
        return elements;
    }

}