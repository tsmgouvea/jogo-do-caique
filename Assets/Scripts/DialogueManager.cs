using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogText; // Referência para o texto do diálogo
    public TMP_Text characterNameText;
    public Button nextButton; // Botão para avançar o diálogo
    public Image characterImage;
    public GameObject dialoguePanel;
    private string[] dialogueLines; // Linhas de diálogo
    private string[] characterNames;
    private Sprite[] characterSprites;
    private int currentLine = 0; // Índice da linha atual
    private bool isTyping = false;
    public Image dialogueBackground;

    public float typingSpeed = 0.05f;

    void Start()
    {

        // Inicialmente, o texto estará vazio
        dialogText.text = "";
        characterNameText.text = "";
        nextButton.gameObject.SetActive(false);
        nextButton.interactable = true;
        dialogueBackground.gameObject.SetActive(false);
        characterImage.gameObject.SetActive(false);

        // Configurar o botão para avançar
        nextButton.onClick.AddListener(NextDialogueLine);
        dialoguePanel.SetActive(false);
        GameState.IsModalActive = false; // Atualiza o estado global
        ToggleInteractions(true); // Reativa botões
    }

    // Função chamada para iniciar o diálogo
    public void StartDialogue(string[] lines, string[] names, Sprite[] sprites)
    {
        dialogueLines = lines;
        characterNames = names;
        characterSprites = sprites;
        currentLine = 0;
        nextButton.gameObject.SetActive(true);
        dialogueBackground.gameObject.SetActive(true);
        characterImage.gameObject.SetActive(true);

        // Exibir a primeira linha
        dialoguePanel.SetActive(true);
        GameState.IsModalActive = true; // Atualiza o estado global
        ToggleInteractions(false); // Desativa botões
        DisplayDialogueLine();
    }
    private void ToggleInteractions(bool enable)
    {
        var buttons = FindObjectsByType<UnityEngine.UI.Button>(FindObjectsSortMode.None);

        foreach (var button in buttons)
        {
            if (button == nextButton)
                continue; // Ignora desativar o `nextButton`

            button.interactable = enable;
        }
    }


    // Função que exibe o próximo texto do diálogo
    void DisplayDialogueLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            // Para a digitação anterior, se estiver acontecendo
            StopAllCoroutines();
            dialogText.text = ""; // Limpa o texto antes de iniciar a digitação da nova linha
            characterNameText.text = "";

            StartCoroutine(TypeText(dialogueLines[currentLine])); // Inicia a animação de digitação para a nova linha

            // Altere o personagem baseado no índice da linha (ou um array de personagens para mais complexidade)
            if (currentLine < characterSprites.Length)
            {
                characterImage.sprite = characterSprites[currentLine]; // Exibe a imagem correspondente
            }
            else
            {
                characterImage.sprite = null; // Se não houver imagem para essa linha, limpa a imagem
            }

            if (currentLine < characterNames.Length)
            {
                characterNameText.text = characterNames[currentLine]; // Exibe o nome do personagem
            }
        }
        else
        {
            EndDialogue();
        }
    }

    // Coroutine que vai exibir o texto letra por letra
    IEnumerator TypeText(string line)
    {
        isTyping = true;
        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter; // Adiciona uma letra ao texto
            yield return new WaitForSeconds(typingSpeed); // Aguarda um tempo antes de adicionar a próxima letra
        }
        isTyping = false;
    }

    // Função para avançar para a próxima linha de diálogo
    void NextDialogueLine()
    {
        if (!isTyping) // Se não estiver digitando, avança para a próxima linha
        {
            currentLine++; // Avança para a próxima linha
            DisplayDialogueLine(); // Atualiza o texto e a imagem
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false); // Desativa o painel de diálogo
        GameState.IsModalActive = false; // Atualiza o estado global
        ToggleInteractions(true); // Reativa botões

        characterImage.sprite = null; // Limpa a imagem do personagem
        characterNameText.text = ""; // Limpa o nome do personagem
        dialogText.text = ""; // Limpa o texto do diálogo
        nextButton.gameObject.SetActive(false);
        dialogueBackground.gameObject.SetActive(false);
    }
}
