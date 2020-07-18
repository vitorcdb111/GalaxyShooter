using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int powerUpID; //0 = triple shot / 1 = speed boost / 2 = shield.

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    // other é o objeto q colidiu com o objeto deste script(powerup).
    private void OnTriggerEnter2D(Collider2D other)
    {

        //utilize a comparacao de tag, p evitar erros.
        //a tag é configurada no inspetor.
        if(other.tag == "Player")
        {
            //manipulador do componente(acessar outro script):
            //acessar a classe que queremos (Player).
            //nome p variavel.
            //pegamos o obejto que colidimos q esta armazenado em other.
            //a partir dai, podemos pegar o componente q queremos.
            //depois "()".
            //a classe tem q ser identica ao q esta dentro do maior menor(<>).
            Player player = other.GetComponent<Player>();
            //sempre verificar se o componente é nulo.
            if(player != null)
            {
                //definimos no inspetor os ids, assim, cada powerup tem seu proprio id.
                if(powerUpID == 0)
                {
                    //so vai acessar variaveis publicas.
                    //chama o metodo q ativa o powerup, assim n tera problemas com o objeto sendo destruido.
                    player.TripleShotPowerupOn();
                }
                else if(powerUpID == 1)
                {
                    player.SpeedBoostPowerupOn();
                }
                else if(powerUpID == 2)
                {
                    //enable shield.
                }

            }

            //destroi nos msm.
            Destroy(this.gameObject);
        }

    }
}
