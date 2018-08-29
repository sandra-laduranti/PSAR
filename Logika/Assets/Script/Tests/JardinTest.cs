using UnityEngine;
using System.Collections;

public class JardinTest
{
    public JardinTest()
    {
    }

    public Jardin jardin1()
    {
        Jardin jardin = new Jardin();
        jardin.Add(new Element(-3, 2, "tulipe", "moyen", "rose", "a"));
        jardin.Add(new Element(2, 3, "rose", "petit", "blanc", null));
        jardin.Add(new Element(-2, -1, "rose", "grand", "rouge", null));
        jardin.Add(new Element(2, 1, "paquerette", "moyen", "blanc", "d"));
        return jardin;
    }

    public Jardin jardin2()
    {
        Jardin jardin = new Jardin();
        jardin.Add(new Element(-3, 2, "tulipe", "moyen", "rose", "a"));
        jardin.Add(new Element(2, 3, "rose", "petit", "blanc", null));
        jardin.Add(new Element(-2, -1, "rose", "grand", "rouge", null));
        jardin.Add(new Element(2, 1, "paquerette", "moyen", "blanc", "d"));
        return jardin;
    }
}
