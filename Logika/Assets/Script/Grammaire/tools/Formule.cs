using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logique0._1.grammaire.tools
{
    /* ARBRE SYNTAXIQUE D'UNE FORMULE

    Structure recursive, une formule est composé de :
    ** Un type pour savoir si la formule courante est une constante, variable, predicat, connecteur ou quantificateur
    ** Un nom, inutile pour les connecteur (neg, and, or impl) et quantificateur (exists) 
    il définit le nom de la variable ou de la constante ou le nom du prédicat
    ** Une liste de formule qui correspond aux arguements, varie selon le type :
        Une constante ou variable n'a pas d'argument ce sont les feuilles de l'arbre
        Un prédicat peut avoir 1, 2 ou 3 arguments qui seront forcement des constante ou variable
        Le connecteur neg (negation) prend une formule en argument
        Les connecteurs and, or, impl prennent deux formules en arguments
        Les quantificateurs forall et exists prennent deux arguments : une variable et une formule
    
    Quand on parcours une formule recursivement, le cas d'arret peut être au choix quand on a une formule_type égal à constante ou variable
    ou quand la List<Formule> args est null
    */
        
    public class Formule
    {
        public enum Formule_Type { constante, variable, predicat, neg, and, or, impl, forall, exists };
        private Formule_Type type;
        private String nom; //sert pour variable, constante, predicat
        private List<Formule> args;

        public Formule(Formule_Type type, String nom, List<Formule> args)
        {
            this.type = type;
            this.nom = nom;
            this.args = args;
        }


        public Formule_Type getType()
        {
            return this.type;
        }

        public String getNom()
        {
            return this.nom;
        }

        public List<Formule> getArgs()
        {
            return this.args;
        }

        /* encapsuleNot est utilisé quand on a formule = not pred1 AND pred2
        Par defaut le parseur analyse cette formule de cette façon not(pred1 AND pred2)
        EncapsuleNot recupère le premier fils du not et l'inverse afin d'obtenir : not(pred1) AND pred2

        Rq: Si les parenthèse sont correctement spécifié comme dans not(pred1 AND pred2) alors le parenthésage est respecté
        */
        public Formule encapsuleNot()
        {
            if(args == null)
            {
                Console.WriteLine("Erreur encapsuleNot");
                return this;
            }
            Formule tmp = args[0];
            List<Formule> list = new List<Formule>();
            list.Add(tmp);
            args[0] = new Formule(Formule_Type.neg, "empty", list);
            return this;
        }

        public override string ToString()
        {
            return myToString(0);
        }

        public String myToString(int nbIndent)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typeToString(type) + " nom=" + nom + " args:\n");
            if (args == null)
                return sb.ToString();
            nbIndent++;
            foreach(Formule f in args)
            {
                for (int i = 0; i < nbIndent; i++)
                    sb.Append("\t");
                sb.Append(f.myToString(nbIndent));
            }
            return sb.ToString();
        }

        private String typeToString(Formule_Type type)
        {
            switch (type)
            {
                case Formule_Type.constante:
                    return "constante";
                case Formule_Type.variable:
                    return "variable";
                case Formule_Type.predicat:
                    return "predicat";
                case Formule_Type.neg:
                    return "negation";
                case Formule_Type.and:
                    return "and";
                case Formule_Type.or:
                    return "or";
                case Formule_Type.impl:
                    return "imply";
                case Formule_Type.forall:
                    return "forall";
                case Formule_Type.exists:
                    return "exists";
                default:
                    return "error typeToString";
            }
        }
    }
}
