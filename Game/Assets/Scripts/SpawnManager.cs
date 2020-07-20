using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    //criar um array p armazenar todos powerups em um so lugar.
    private GameObject[] _powerups;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    //coroutine p geracao do inimigo.
    IEnumerator EnemySpawnRoutine()
    {        
        //criar um loop q gere a cada tantos segundos o inimigo.
        //enquanto a condicao for verdadeira.
        while (true)
        {
            //gerando num valor aleatorio entre -7 e 7 e acima do campo de visao.
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while(true)
        {
            //variavel p armazenar um valor aleatorio entre 0 e 2.
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerUp], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(8.0f);
        }        
    }
}
