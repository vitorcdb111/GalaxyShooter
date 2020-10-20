using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//acesso ao unity ui p manipular o elemento image// ele dara acesso a todos os elementos de ui do editor.
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    //esse sera o Player_Lives, objeto filho do Canvas.
    public Image livesImageDisplay;
    public int score;
    public Text scoreText;
    public GameObject titleScreen;

    //metodo personalizado parecido com o collider other
    //criar uma variavel p saber quantas vidas o player tem
    public void UpdateLives(int currentLives)
    {
        //acesso o objeto que quero + a imagem que quero procurar(os sprites(sourceimage do inspetor)) e pega um novo sprite p armazenar no array.
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }


    //funcoes q serao chamadas no GamaManager.
    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        //rezetando de maneira simples a pontucao do score.
        scoreText.text = "Score: ";
        score = 0;
    }
}
