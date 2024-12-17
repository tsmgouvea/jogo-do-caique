using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogText; // Refer�ncia para o texto do di�logo
    public TMP_Text characterNameText;
    public Button nextButton; // Bot�o para avan�ar o di�logo
    public Image characterImage;
    public GameObject dialoguePanel;
    private string[] dialogueLines; // Linhas de di�logo
    private string[] characterNames;
    private Sprite[] characterSprites;
    private int currentLine = 0; // �ndice da linha atual
    private bool isTyping = false;
    public Image dialogueBackground;

    public float typingSpeed = 0.05f;

    void Start()
    {

        // Inicialmente, o texto estar� vazio
        dialogText.text = "";
        characterNameText.text = "";
        nextButton.gameObject.SetActive(false);
        nextButton.interactable = true;
        dialogueBackground.gameObject.SetActive(false);
        characterImage.gameObject.SetActive(false);

        // Configurar o bot�o para avan�ar
        nextButton.onClick.AddListener(NextDialogueLine);
        dialoguePanel.SetActive(false);
        GameState.IsModalActive = false; // Atualiza o estado global
        ToggleInteractions(true); // Reativa bot�es
    }

    // Fun��o chamada para iniciar o di�logo
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
        ToggleInteractions(false); // Desativa bot�es
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


    // Fun��o que exibe o pr�ximo texto do di�logo
    void DisplayDialogueLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            // Para a digita��o anterior, se estiver acontecendo
            StopAllCoroutines();
            dialogText.text = ""; // Limpa o texto antes de iniciar a digita��o da nova linha
            characterNameText.text = "";

            StartCoroutine(TypeText(dialogueLines[currentLine])); // Inicia a anima��o de digita��o para a nova linha

            // Altere o personagem baseado no �ndice da linha (ou um array de personagens para mais complexidade)
            if (currentLine < characterSprites.Length)
            {
                characterImage.sprite = characterSprites[currentLine]; // Exibe a imagem correspondente
            }
            else
            {
                characterImage.sprite = null; // Se n�o houver imagem para essa linha, limpa a imagem
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
            yield return new WaitForSeconds(typingSpeed); // Aguarda um tempo antes de adicionar a pr�xima letra
        }
        isTyping = false;
    }

    // Fun��o para avan�ar para a pr�xima linha de di�logo
    void NextDialogueLine()
    {
        if (!isTyping) // Se n�o estiver digitando, avan�a para a pr�xima linha
        {
            currentLine++; // Avan�a para a pr�xima linha
            DisplayDialogueLine(); // Atualiza o texto e a imagem
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false); // Desativa o painel de di�logo
        GameState.IsModalActive = false; // Atualiza o estado global
        ToggleInteractions(true); // Reativa bot�es

        characterImage.sprite = null; // Limpa a imagem do personagem
        characterNameText.text = ""; // Limpa o nome do personagem
        dialogText.text = ""; // Limpa o texto do di�logo
        nextButton.gameObject.SetActive(false);
        dialogueBackground.gameObject.SetActive(false);
    }
}
