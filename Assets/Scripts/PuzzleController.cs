using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleController : MonoBehaviour
{
    public List<Button> symbols; // Botões ou objetos interativos dos símbolos
    public List<Color> correctSequence; // Sequência correta de cores
    public List<Color> inputSequence; // Sequência que o jogador insere
    public GameObject panelOrder; // Painel com a sequência de cores na cena
    public TextMeshProUGUI resultText; // Texto para mostrar se acertou ou errou
    public GameObject door; // Ativa o gameobject
    public GameObject doorDialogue; // desativa o dialogo da porta
    public AudioSource incorrectSound;
    private void Start()
    {
        // Garante que a sequência de entrada comece vazia
        inputSequence = new List<Color>();

        // Exibe a sequência correta no painel (opcional)
        DisplaySequence();
    }

    public void RegisterInput(Color color)
    {
        inputSequence.Add(color);

        // Verifica se a sequência já foi completada
        if (inputSequence.Count == correctSequence.Count || GameState.IsModalActive)
        {
            CheckSequence();
        }
    }

    private void CheckSequence()
    {
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (inputSequence[i] != correctSequence[i])
            {
                resultText.text = "Errado! Tente novamente!";
                incorrectSound.Play();
                inputSequence.Clear();
                return;
            }
        }

        resultText.text = "Correto! Puzzle Resolvido!";
        door.SetActive(true);
        doorDialogue.SetActive(false);

        // Aqui você pode adicionar lógica para prosseguir no jogo
    }

    private void DisplaySequence()
    {
        // Mostra a sequência correta no painel de cores
        for (int i = 0; i < correctSequence.Count; i++)
        {
            panelOrder.transform.GetChild(i).GetComponent<Image>().color = correctSequence[i];
        }
    }
}
