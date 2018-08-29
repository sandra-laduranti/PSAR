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

public class UnityTest : MonoBehaviour {

    public Jardin jardin1(){
         Jardin jardin = new Jardin();
        jardin.Add(new Element(-3,2,"tulipe","moyen","rose","a"));
        jardin.Add(new Element(2,3,"rose","petit","blanc",null));
        jardin.Add(new Element(-2,-1,"rose","grand","rouge",null));
        jardin.Add(new Element(2,1,"paquerette","moyen","blanc","d"));
        return jardin;
    }

    //constante en moins pour test
    public Jardin jardin2()
    {
        Jardin jardin = new Jardin();
        jardin.Add(new Element(-3, 2, "tulipe", "moyen", "rose", "a"));
        jardin.Add(new Element(2, 3, "rose", "petit", "blanc", null));
        return jardin;
    }

    public object formules(Communicate comm)
    {
        String[] formuleTab = new string[]
            {
                "Rose(d)",
                "∀xRose(x)",
                "∃xRose(x)",
                "∀x(est_blanc(x) ⇒ ∃y(plus_petit_que(x,y) ∧ a_l_est_de(y,x)))"
            };

        object pythonArray = comm.pythonEmptyArray();

        foreach (String str in formuleTab)
        {
            comm.pythonAddInArray(pythonArray, comm.HandleForm(FormuleFactory.parse(str)));
        }
        return pythonArray;

    }

    void Start()
    {
        FormuleTest ft = new FormuleTest();
        Jardin jardin = jardin2();
        Communicate comm = new Communicate("psar_python.py", jardin);
        //lance le script
        comm.GetSource().Execute(comm.GetScope());

        ft.test_formules(comm, jardin);     
    }

    void Update()
    {

    }
}
