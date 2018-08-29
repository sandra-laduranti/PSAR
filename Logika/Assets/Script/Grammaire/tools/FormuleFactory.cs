using PerCederberg.Grammatica.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Logique0._1.grammaire.tools
{
    class FormuleFactory
    {
        private static string formule_str;

        public static Formule parse(String str)
        {
            formule_str = str;
            Node n = null;
            LogicalParser parser = new LogicalParser(new StringReader(str));
            try {
                n = parser.Parse();
            }
            catch(ParserLogException)
            {
                throw;
            }

            if (n == null)
                return null;
            return parseNode(n);
        }

        private static Formule parseNode(Node n)
        {
            if(String.Equals(n.Name, "FORMULE"))
            {

                Node child1 = n.GetChildAt(0);
                List<Formule> list = new List<Formule>();

                if (String.Equals(child1.Name, "NOT"))
                {
                    if (n.GetChildAt(1).GetChildAt(0).Name == "PREDICAT") /* Le not s'applique seulement au predicat qui le suit */
                    {
                        Formule f = parseNode(n.GetChildAt(1));
                        return f.encapsuleNot();
                    }
                    else /* Le not s'applique sur tout le reste de la formule */
                    {
                        list.Add(parseNode(n.GetChildAt(1)));
                        return new Formule(Formule.Formule_Type.neg, "empty", list);
                    }
                }
                else if(String.Equals(child1.Name, "FORALL"))
                {
                    Node varNode = n.GetChildAt(1);
                    list.Add(new Formule(Formule.Formule_Type.variable, ""+formule_str[varNode.StartColumn-1], null));
                    list.Add(parseNode(n.GetChildAt(2)));
                    return new Formule(Formule.Formule_Type.forall, "empty", list);
                }
                else if(String.Equals(child1.Name, "EXISTS"))
                {
                    Node varNode = n.GetChildAt(1);
                    list.Add(new Formule(Formule.Formule_Type.variable, "" + formule_str[varNode.StartColumn-1], null));
                    list.Add(parseNode(n.GetChildAt(2)));
                    return new Formule(Formule.Formule_Type.exists, "empty", list);
                }
                else if(String.Equals(child1.Name, "PREDICAT"))
                {
                    Node pred = child1.GetChildAt(0);
                    Formule formPred = formuleFromPred(pred);

                    if(n.GetChildCount() == 1)
                    {
                        return formPred;
                    }
                    else
                    {
                        Node child2 = n.GetChildAt(1);
                        Node connecteur = child2.GetChildAt(0);
                        Formule.Formule_Type type;
                        if (String.Equals(connecteur.Name, "AND"))
                            type = Formule.Formule_Type.and;
                        else if (String.Equals(connecteur.Name, "OR"))
                            type = Formule.Formule_Type.or;
                        else /* IMPLY */
                            type = Formule.Formule_Type.impl;
                        list.Add(formPred);
                        list.Add(parseNode(child2.GetChildAt(1)));
                        return new Formule(type, "empty", list);
                    }
                }
                else /* equals "(" */
                {
                    Node child3 = n.GetChildAt(2);
                    Node connecteur = child3.GetChildAt(0);

                    if(connecteur.Name == "RIGHT_PAREN")
                    {
                        return parseNode(n.GetChildAt(1));
                    }
                    /* Si connecteur */
                    Formule.Formule_Type type;
                    if (String.Equals(connecteur.Name, "AND"))
                        type = Formule.Formule_Type.and;
                    else if (String.Equals(connecteur.Name, "OR"))
                        type = Formule.Formule_Type.or;
                    else /* IMPLY */
                        type = Formule.Formule_Type.impl;
                    list.Add(parseNode(n.GetChildAt(1)));
                    list.Add(parseNode(child3.GetChildAt(1)));
                    return new Formule(type, "empty", list);
                }
            }
            Console.WriteLine("Erreur cas inconnu FormuleFactory");
            return null;
        }

        private static Formule formuleFromPred(Node pred)
        {
            Node actualPred = pred.GetChildAt(0);
            String strPred = formule_str.Substring(actualPred.StartColumn - 1, actualPred.EndColumn - actualPred.StartColumn + 1);
            List<Formule> args = new List<Formule>();

            if (String.Equals(pred.Name, "PRED1"))
            {
                Node arg1 = pred.GetChildAt(2).GetChildAt(0);

                args.Add(new Formule(
                    String.Equals(arg1.Name, "VAR") ? Formule.Formule_Type.variable : Formule.Formule_Type.constante,
                    "" + formule_str[arg1.StartColumn - 1], null));
                return new Formule(Formule.Formule_Type.predicat, strPred, args);
            }
            else if(String.Equals(pred.Name, "PRED2"))
            {
                Node arg1 = pred.GetChildAt(2).GetChildAt(0);
                Node arg2 = pred.GetChildAt(4).GetChildAt(0);


                args.Add(new Formule(
                    String.Equals(arg1.Name, "VAR") ? Formule.Formule_Type.variable : Formule.Formule_Type.constante,
                    "" + formule_str[arg1.StartColumn-1], null));
                args.Add(new Formule(
                    String.Equals(arg2.Name, "VAR") ? Formule.Formule_Type.variable : Formule.Formule_Type.constante,
                    "" + formule_str[arg2.StartColumn-1], null));
                return new Formule(Formule.Formule_Type.predicat, strPred, args);
            }
            else /* PRED3 */
            {
                Node arg1 = pred.GetChildAt(2).GetChildAt(0);
                Node arg2 = pred.GetChildAt(4).GetChildAt(0);
                Node arg3 = pred.GetChildAt(6).GetChildAt(0);

                args.Add(new Formule(
                   String.Equals(arg1.Name, "VAR") ? Formule.Formule_Type.variable : Formule.Formule_Type.constante,
                   "" + formule_str[arg1.StartColumn - 1], null));
                args.Add(new Formule(
                    String.Equals(arg2.Name, "VAR") ? Formule.Formule_Type.variable : Formule.Formule_Type.constante,
                    "" + formule_str[arg2.StartColumn - 1], null));
                args.Add(new Formule(
                    String.Equals(arg3.Name, "VAR") ? Formule.Formule_Type.variable : Formule.Formule_Type.constante,
                    "" + formule_str[arg3.StartColumn - 1], null));
                return new Formule(Formule.Formule_Type.predicat, strPred, args);

            }
        }
    }
}
