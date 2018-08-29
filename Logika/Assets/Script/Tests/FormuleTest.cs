using UnityEngine;
using System.Collections;
using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//partie formules
using Logique0._1.grammaire.tools;
using PerCederberg.Grammatica.Runtime;

public class FormuleTest {

    public FormuleTest()
    {
    }

	public  void testSyntaxe(){
         String[] formuleTab = new string[]
            {
                "Rose(d)",
                "∀xRose(x)",
                "∃xRose(x)",
                "∀x(est_blanc(x) ⇒ ∃y(plus_petit_que(x,y) ∧ a_l_est_de(y,x)))",
                "∀x(a_l_est(x) ∨ a_l_ouest(x) ∨ au_sud(x) ∨ au_nord(x))",
                "(∀x(est_grand(x) ⇒ est_rouge(x)) ∧ ¬∃x(est_blanc(x) ∧ ∃y(est_rouge(y) ∧ au_sud_de(x,y))))",
                "∃x(¬est_rouge(x) ∧ au_nord_de(x,g))",
                "∃x((Rose(x) ∧ est_rouge(x)) ∧ (∀y(Rose(y) ∧ est_rouge(y)) ⇒ egal(x, y)))"
            };

         List<Formule> formules = new List<Formule>();
        foreach (String str in formuleTab)
        {
            Console.WriteLine("formule=" + str);
            Console.WriteLine(FormuleFactory.parse(str) + "\n\n");
            formules.Add(FormuleFactory.parse(str));
        }
    }

    public void test_one_form(String form, int name, Communicate comm, Jardin jardin)
    {
        Debug.Log("====================" + "f" + name + "====================");
        Formule f = FormuleFactory.parse(form);

        try
        {
            object pythonForm = comm.HandleForm(f);
            Debug.Log(name + " = " + pythonForm.ToString());

            Func<Jardin, object> unity_my_interp_formul = comm.GetScope().GetVariable<Func<Jardin, object>>("unity_my_interp_formul");
            object pythonJardin = unity_my_interp_formul(jardin);
            Func<object, object, object> unity_eval_one_form = comm.GetScope().GetVariable<Func<object, object, object>>("unity_eval_one_form");

            Debug.Log("f" + name + " = " + unity_eval_one_form(pythonJardin, pythonForm));
            Debug.Log("===========================================================");
        }
        catch (CommException c)
        {
            Debug.Log(c);
        }    
    }

    public void test_formules(Communicate comm, Jardin jardin)
    {

        String[] formuleTab = new string[]
            {
                "Rose(d)",
                "∀xRose(x)",
                "∃xRose(x)",
                "∀x(est_blanc(x) ⇒ ∃y(plus_petit_que(x,y) ∧ a_l_est_de(y,x)))",
                "∀x(a_l_est(x) ∨ a_l_ouest(x) ∨ au_sud(x) ∨ au_nord(x))",
                "(∀x(est_grand(x) ⇒ est_rouge(x)) ∧ ¬∃x(est_blanc(x) ∧ ∃y(est_rouge(y) ∧ au_sud_de(x,y))))",
                "∃x(est_rouge(x) ∧ au_nord_de(x,d))",
                "∃x((Rose(x) ∧ est_rouge(x)) ∧ (∀y((Rose(y) ∧ est_rouge(y)) ⇒ au_sud_de(x, y))))"
                
            };

        List<Formule> formules = new List<Formule>();
        int i = 1;
        foreach (String str in formuleTab)
        {
            test_one_form(str, i, comm, jardin);
            i++;
        }
    }
}
