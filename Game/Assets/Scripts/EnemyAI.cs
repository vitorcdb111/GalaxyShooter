using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    private UIManager _uiManager;

    //instanciando um clip especifico.
    [SerializeField]
    private AudioClip _audioClip;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6.5f)
        {
            //random.range gera um numero aleatorio entre (0.0f (limite inferior), 1.0f(limite superior)).
            float randomX = Random.Range(-7f, 7f);

            transform.position = new Vector3(randomX, 6.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);        
            if (_uiManager != null)
            {
                _uiManager.UpdateScore();
            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            //esse metodo nos permite instanciar um clip de audio q esta prestes a ser destruido.
            //mas ele precisa saber qual clip vai instanciar.
            //clipAtPoint, toca em uma determinada posicao no espaco.
            //Quando passamos a posicao, ele pega a posicao 3D, para deixar o som
            //mais proximo ao usuario, passamos a Main Camera.
            //o ultimo parametro é o volume.
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);            
        }

        else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            if (_uiManager != null)
            {
                _uiManager.UpdateScore();
            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);            
        }
    }
}
