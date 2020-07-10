using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    //velocidade do laser.
    private float _speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //fazendo o laser se mover p cima.
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //verifica se a posicao do laser eh maior/igual a 6.
        if (transform.position.y >= 6)
        {
            //destroi este objeto (laser).
            Destroy(this.gameObject);
        }
    }
}
