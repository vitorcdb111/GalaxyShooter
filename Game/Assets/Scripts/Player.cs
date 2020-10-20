using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Declarando uma variavel:
    //identificador, publico(qualquer outro script pode acessar) ou privado(apenas esse script).
    //uma boa pratica é utilizar o underline na frente da declaracao da variavel p saber q ele eh privada sem precisar olhar o topo do codigo.
    //tipo de dado, oq essa variavel vai manter, int, float, etc...
    //escolhendo um nome para a variavel.
    //(opcional) - pode se declarar um valor inicial ao componente.

    //Quando usar gameobject ou transform:
    //todo objeto do unity é considerado um gameobject, e todo gameobject tem um transform. Um transform é um game object. Ambos funcionam bem.
    //A dica é sempre usar gameobject quando se esta instanciando coisas.
    //o RigidBody é mais utilizado em quem é responsavel pela colisao, mas nao faz tanta diferenca.

    //SerializeField é um atributo que permite a variavel aparecer no inspetor(unity), msm sendo privada. Processo chamado de [Sereializacao].
    //ao usar um float com casa decimal, tem q incluir um f no final(determina q eh um valor decimal).


    [SerializeField]
    //criando a variavel do laser.
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;

    //criando as variaveis p poder dar o proximo tiro.
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    //variavel da velocidade do jogador.
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shielGameObject;

    public bool canTripleShot = false;
    //um padrao eh definir uma bool como uma pergunta isSpeedBostActive
    public bool canSpeedBost = false;
    public bool shieldActive = false;

    public int lives = 3;

    //manipulador para o uimanager
    private UIManager _uiManager;
    //manipulando a tela de titulo
    private GameManager _gameManager;

    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    private void Start()
    {      
        //passando os valores dos eixos "x, y, z" para 0, no objeto.
        //current pos = new pos.
        transform.position = new Vector3(0, 0, 0);        

        //estou acessando no inspetor do player p o canvas, assim consigo acessar o script uimanager q esta la.
        //parecido como se faz no  powerup.
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();        

        //verificando se encontrou o ui manager
        if(_uiManager != null)
        {
            //acessando o metodo updatelives do UIManager
            //dentro do () informe a vida atual(curentlive(lives))
            _uiManager.UpdateLives(lives);
        }

        //pegando a referencia do script GameManager
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();



        //ativando o spawn tanto dos inimigos quanto dos powerUps  
        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
    }


    // Update is called once per frame
    private void Update()
    {
        //inicializando o metodo(funcao) Movement.
        //a cada frame que chamarmos o metodo, todo o codigo dele sera lido.
        Movement();

        //Ao abrir o gerenciador de inputs.
        //e escolher oq mantem a tecla precionada.
        //escolhemos o codigo e a tecla.
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }     

    }

    //private pq a movimentacao eh uma funcao especifica do player.
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
        //definimos horizontal para inputs horizontais.
        //ao multiplicar a variavel q recebe o input, definimos p qual lado o objeto vai se mexer.
        //por padrao, o valor do input vem como 0, ao clicar em uma tecla, ele recebe um valor.

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(canSpeedBost == true)
        {
            transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            //vector3 com 1 no eixo x (1, 0, 0), velocidade, direcao apertada, tempo real.
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);

            //vector3 com 1 no eixo y (0, 1, 0), velocidade, direcao apertada, tempo real.
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        

        //se o player estiver em y e for maior q 0.
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

    private void Shoot()
    {
        //Quaternions é como o Unity lidacom entradas rotacionais.
        //time.time é a quantidade de tempo que o jogo esta sendo executado(tempo em segundos desde o inicio do game).
        //quando clicar em play, time.time sera = 0.

        //verificando se o tempo de jogo é maior que o tempo do firerate.
        //se for, posso atirar.
        if (Time.time > _canFire)
        {
            if(canTripleShot == true)
            {
                //center
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }

            else
            {
                //instancia o laser.
                //quando digitamos transform estamos obtendo o objeto ao qual este script esta conectado(player).
                //nome do prefab, posicao do objeto, rotacao.
                //Quartenion identity siginifica q nao queremos alterar a rotacao do objeto(sem rotacao ou rotacao padrao).
                //adicionado um vector3 p somar a posicao q quero q sai o laser da posicao do objeto(player).
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.92f, 0), Quaternion.identity);
            }

            //apos sair o tiro, precisamo re-atribuir o valor do canFire para que a condicao nao seja verdadeira ate passar o tempo correto do firerate.
            _canFire = Time.time + _fireRate;
        }
    }

    //metodo p ativar o powerup.
    public void TripleShotPowerupOn()
    {
        //a partir do powerup, definimos q cantripleShot = true;
        canTripleShot = true;
        //rotina q mantem o poder ativo por algum tempo.
        StartCoroutine(TripleShotDownRoutine());
    }

    //couroutine nos permite esperar um tempo em segundos e depois executar uma acao.
    //IEnumerator é obrigatorio para criar uma corotina.
    //comeca a corotina criada no script player.
    //se destruirmos o objeto q chama a corotina, ele nunca sera habilitado.
    //melhor maneira de corrigir isso é criar um metodo(no player) q ativa o powerup.

    //couroutine.
    public IEnumerator TripleShotDownRoutine()
    {
        //faz esperar por um tempo em segundos.
        yield return new WaitForSeconds(10.0f);

        //atribuir p falso o tripleshot p desativar o powerup.
        canTripleShot = false;
    }

    //metodo p ativar o powerup.
    public void SpeedBoostPowerupOn()
    {
        canSpeedBost = true;

        StartCoroutine(SpeedBostDownRoutine());
    }

    //couroutine.
    public IEnumerator SpeedBostDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);

        canSpeedBost = false;
    }

    public void Damage()
    {
        if(shieldActive == true)
        {
            shieldActive = false;
            _shielGameObject.SetActive(false);
            // para n continuar a funcao, chamamos return.
            //basicamente ela diz p voltar ao topo, voltar ao metodo.
            //ele interrompe o programa.
            return;
        }
        else
        {
            lives--;

            //atualizando o hud de vida.
            _uiManager.UpdateLives(lives);

            if (lives < 1)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _gameManager.gameOver = true;
                _uiManager.ShowTitleScreen();
                Destroy(this.gameObject);
            }
        }       
    }

    public void ShieldPowerupOn()
    {
        shieldActive = true;
        //habilitando o gameobject shield, q é child do player.
        _shielGameObject.SetActive(true);
    }
}
