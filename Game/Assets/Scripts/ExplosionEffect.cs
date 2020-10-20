using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //para adicionar um tempo de atraso nesse destroy, basta colocar uma virgula.
        Destroy(this.gameObject, 4f);
    }

}
