using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //se a tecla a ou seta esquerda estiver pressionada
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _anim.SetBool("TurnLeft", true);
            // para n se ter conflito, de tentar pressionar a tecla A e D ao msm tempo, passamos o outro estado p falso
            _anim.SetBool("TurnRight", false);
        }

        //quando soltar a tecla a ou seta esquerda
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _anim.SetBool("TurnRight", true);
            _anim.SetBool("TurnLeft", false);
        }

        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _anim.SetBool("TurnRight", false);
            _anim.SetBool("TurnLeft", false);
        }

    }
}
