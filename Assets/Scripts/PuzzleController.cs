using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleController : MonoBehaviour
{
    public List<Button> symbols; // Bot�es ou objetos interativos dos s�mbolos
    public List<Color> correctSequence; // Sequ�ncia correta de cores
    public List<Color> inputSequence; // Sequ�ncia que o jogador insere
    public GameObject panelOrder; // Painel com a sequ�ncia de cores na cena
    public TextMeshProUGUI resultText; // Texto para mostrar se acertou ou errou
    public GameObject door; // Ativa o gameobject
    public GameObject doorDialogue; // desativa o dialogo da porta
    public AudioSource incorrectSound;
    private void Start()
    {
        // Garante que a sequ�ncia de entrada comece vazia
        inputSequence = new List<Color>();

        // Exibe a sequ�ncia correta no painel (opcional)
        DisplaySequence();
    }

    public void RegisterInput(Color color)
    {
        inputSequence.Add(color);

        // Verifica se a sequ�ncia j� foi completada
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

        // Aqui voc� pode adicionar l�gica para prosseguir no jogo
    }

    private void DisplaySequence()
    {
        // Mostra a sequ�ncia correta no painel de cores
        for (int i = 0; i < correctSequence.Count; i++)
        {
            panelOrder.transform.GetChild(i).GetComponent<Image>().color = correctSequence[i];
        }
    }
}
