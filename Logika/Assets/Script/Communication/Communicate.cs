using UnityEngine;
using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//partie formules
using Logique0._1.grammaire.tools;
using PerCederberg.Grammatica.Runtime;
// make us use Python
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;

public class Communicate {

    private ScriptEngine engine;
    private ScriptSource script;
    private ScriptScope scope;
    private Jardin jardin;

    public Communicate(string python, Jardin jardin)
    {
        //création du lien avec le script python
        engine = Python.CreateEngine();
        script = engine.CreateScriptSourceFromFile(python);
        scope = engine.CreateScope();
        this.jardin = jardin;
    }

    public Jardin GetJardin()
    {
        return jardin;
    }

    public void SetJardin(Jardin jardin)
    {
        this.jardin = jardin;
    }

    public ScriptScope GetScope()
    {
        return scope;
    }

    public ScriptSource GetSource()
    {
        return script;
    }

    public ScriptEngine GetEngine()
    {
        return engine;
    }

    public object pythonEmptyArray()
    {        
        Func< object> unity_array_empty = scope.GetVariable<Func< object>>("unity_array_empty");
        return unity_array_empty();
    }

    public object pythonCreateArray(object elem)
    {
        Func<object, object> unity_create_array = scope.GetVariable<Func<object, object>>("unity_create_array");
        return unity_create_array(elem);
    }

    public object pythonAddInArray(object array, object elem)
    {
        Func<object,object, object> unity_put_in_array = scope.GetVariable<Func<object,object ,object>>("unity_put_in_array");
        return unity_put_in_array(array, elem);
    }

    public object pythonUnaire(String className, object f){
       object typePython = scope.GetVariable(className); // Get class
       object classPython = engine.Operations.CreateInstance(typePython, f);
       return classPython;
    }


    public object pythonBinaire(String className, object sub1, object sub2)
    {
        object typePython = scope.GetVariable(className); // Get class
        object classPython = engine.Operations.CreateInstance(typePython, sub1, sub2);
        return classPython;
    }



    /*
     * Récupère récursivement les classes python correspondants aux différents éléments de la formule
     * Le cas d'arrêt est le fait de ne plus avoir d'éléments dans la liste Args ce qui signifie qu'on
     * a atteint les feuilles de l'ast.
     * Retourne un objet correspondant à une formule python
     */
    public object HandleForm(Formule f) 
    {
        object pythonForm = null;
        object[] sub = new object[3];
        int i = 0;

        //Debug.Log(f.ToString());
        if (f.getArgs() != null)
        {
            foreach (Formule form in f.getArgs())
            {
               /* Debug.Log("i: " + i + "type " + form.getType() );
                if (form.getArgs() != null)
                    Debug.Log("i: " + i + "args = " + form.getArgs().Count());
                else
                    Debug.Log("i: " + i + "args = 0");*/
                sub[i] = HandleForm(form);
                i++;
            }
        }


        Formule.Formule_Type type = f.getType();
        switch (type)
        {
            case Formule.Formule_Type.constante:
                if (!jardin.GetConstantes().Contains(f.getNom()))
                    throw new CommException("Error: La constante " + f.getNom() + " n'existe pas dans le jardin.");
                if (f.getArgs() == null)
                    sub[0] = pythonEmptyArray();
                pythonForm = pythonBinaire("Cons_term", f.getNom(), sub[0]);
               // Debug.Log(" Constante " + f.getNom());
                break;
            case Formule.Formule_Type.variable:
               pythonForm = pythonUnaire("Var_term", f.getNom());
                //Debug.Log(" Variable " + f.getNom());
                break;
           case Formule.Formule_Type.predicat:
                object array = pythonCreateArray(sub[0]);
                for (int j = 1; j < i; j++)
                    array = pythonAddInArray(array, sub[j]);
                pythonForm = pythonBinaire("F1_Atom", f.getNom(), array);
               // Debug.Log(" predicat " + f.getNom());
                break;
           case Formule.Formule_Type.neg:
                pythonForm = pythonUnaire("F1_Neg", sub[0]);
               // Debug.Log(" neg");
                break;
           case Formule.Formule_Type.and:
                pythonForm = pythonBinaire("F1_And", sub[0], sub[1]);
               // Debug.Log("and");
                break;
           case Formule.Formule_Type.or:
                pythonForm = pythonBinaire("F1_Or", sub[0], sub[1]);
               // Debug.Log("or");
                break;
           case Formule.Formule_Type.impl:
                pythonForm = pythonBinaire("F1_Impl", sub[0], sub[1]);
               // Debug.Log("impl");
                break;
           case Formule.Formule_Type.forall:
                pythonForm = pythonBinaire("F1_Forall", f.getArgs().First().getNom(), sub[1]);
               // Debug.Log("forall");
                break;
           case Formule.Formule_Type.exists:
                pythonForm = pythonBinaire("F1_Exists", f.getArgs().First().getNom(), sub[1]);
               // Debug.Log("exists");
                break;
            default:
                Debug.Log("Error");
                break;
        }

        return pythonForm;
    }



	
}

