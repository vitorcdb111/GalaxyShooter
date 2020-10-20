using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();              
    }

    private void Update()
    {
        if(gameOver == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //vector3.zero = (0,0,0)
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                //nao posso trabalhar com um elemento direto da UIManager em outro script, por isso, crio as funcoes la e trago elas para ca.
                _uiManager.HideTitleScreen();
            }
        }
    }
}
