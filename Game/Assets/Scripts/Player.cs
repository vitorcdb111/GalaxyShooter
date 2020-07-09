using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Declarando uma variavel:
    //identificador, publico(qualquer outro script pode acessar) ou privado(apenas esse script).
    //tipo de dado, oq essa variavel vai manter, int, float, etc...
    //escolhendo um nome para a variavel.
    //(opcional) - pode se declarar um valor inicial ao componente.

    //SerializeField é um atributo que permite a variavel aparecer no inspetor(unity), msm sendo privada. Processo chamado de [Sereializacao].
    [SerializeField]
    //ao usar um float com casa decimal, tem q incluir um f no final(determina q eh um valor decimal).
    private float speed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        //passando os valores dos eixos "x, y, z" para 0, no objeto.
        transform.position = new Vector3(0, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        //inicializando o metodo(funcao) Movement.
        //a cada frame que chamarmos o metodo, todo o codigo dele sera lido.
        Movement();
    }


    //private pq a movimentacao eh uma funcao especifica do player
    private void Movement()
    {
        //translate é a movimentacao q o obejto terá.

        // time.deltatime suaviza o frame, caso fosse so o vector3, seria 
        // movido a uma velocidade de 60m por segundo, enquanto com o deltatime
        //ele vai se mover na velocidade real, 1m por segundo.
        //ao multiplicar por um valor, dizemos qual velocidade por segundo queremos.
        //podemos atribuir a velocidade a uma variavel, e simplificar a manipulacao de velocidade.

        //Input é a entrada do teclado.
        //Get axis pegal qual botao vai usar, conforme os axes padroes.
        //definimos horizontal para inputs horizontais
        //ao multiplicar a variavel q recebe o input, definimos p qual lado o objeto vai se mexer.
        //por padrao, o valor do input vem como 0, ao clicar em uma tecla, ele recebe um valor.

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //vector3 com 1 no eixo x (1, 0, 0), velocidade, direcao apertada, tempo real.
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        //vector3 com 1 no eixo y (0, 1, 0), velocidade, direcao apertada, tempo real.
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        //se o player estiver em y e for maior q 0
        //passo a posicao y do player p 0.

        //restringindo as bordas q nao quero q o player passe:

        //em y:
        if (transform.position.y > 0)
        {
            //usando o vetor3 com transform em x, siginifca q mantemos a posicao dele em x, e mudamos a posicao "y,z" para 0.
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //em x:
        if (transform.position.x > 8.5f)
        {
            transform.position = new Vector3(8.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.5f)
        {
            transform.position = new Vector3(-8.5f, transform.position.y, 0);
        }

        //eh possivel fazer ele aparecer no outro lado, ao inves de trancar numa posicao.
        //bastar mudar os valores do if para 9.4 e -9.4, e a posicao do x no vetor3 com os respectivos valores(9.4f).
    }
}
