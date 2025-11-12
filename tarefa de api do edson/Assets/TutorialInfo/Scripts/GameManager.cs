using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    APIManager api;

    void Start()
    {
        api = GetComponent<APIManager>();

        // Teste inicial: buscar todos os players
        StartCoroutine(api.GetPlayers());

        // Teste: adicionar um player novo
        Player novo = new Player()
        {
            id = 3,
            vida = 95,
            quantidadeItens = 4,
            posicaoX = 5,
            posicaoY = 0,
            posicaoZ = -2
        };

        StartCoroutine(api.AddPlayer(novo));
    }
}
