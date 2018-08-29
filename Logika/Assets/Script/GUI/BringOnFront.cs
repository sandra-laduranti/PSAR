using UnityEngine;
using System.Collections;

public class BringOnFront : MonoBehaviour {

    void OnEnable ()
    {
        transform.SetAsLastSibling();
    }
}
