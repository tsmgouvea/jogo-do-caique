using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Image backgroundImage; // Referência à imagem de fundo
    public Sprite[] roomSprites; // Lista de imagens das salas
    public Button leftButton;  // Botão de retroceder
    public Button rightButton; // Botão de avançar
    public GameObject clickableArea; // Referência ao ClickableArea
    public GameObject chave;
    public GameObject faca;

    private int currentRoomIndex = 0; // Índice da sala atual

    void Start()
    {
        // Certifique-se de que a sala inicial está sendo exibida
        if (roomSprites.Length > 0)
        {
            backgroundImage.sprite = roomSprites[currentRoomIndex];
        }

        // Inicialmente, desativa o ClickableArea
        if (clickableArea != null)
        {
            clickableArea.SetActive(true); // Desativa o ClickableArea
            chave.SetActive(true);
            faca.SetActive(false);
        }

        // Atualize os botões para refletir a sala inicial
        UpdateButtons();
    }

    // Função chamada para mudar de sala
    public void ChangeRoom(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < roomSprites.Length)
        {
            currentRoomIndex = roomIndex;
            backgroundImage.sprite = roomSprites[roomIndex];

            // Controla a visibilidade do ClickableArea baseado na sala
            if (clickableArea != null)
            {
                // Desativa o ClickableArea antes de mudar de sala
                clickableArea.SetActive(false);
                chave.SetActive(false);
                faca.SetActive(false);

                // Ativa o ClickableArea somente na sala 1 (ou a sala que você escolher)
                if (currentRoomIndex == 0) // Exemplo: Ativa o ClickableArea apenas na sala 1
                {
                    clickableArea.SetActive(true); // Ativa o ClickableArea
                    chave.SetActive(true);
                }

                if (currentRoomIndex == 1)
                {
                    faca.SetActive(true);
                }
            }

            // Atualiza os botões para refletir o novo estado da sala
            UpdateButtons();
        }
        else
        {
            Debug.LogWarning("Índice da sala é inválido!");
        }
    }

    // Função chamada para ir para a próxima sala
    public void NextRoom()
    {
        // Avança para a próxima sala, se possível
        ChangeRoom(currentRoomIndex + 1);
    }

    // Função chamada para retroceder para a sala anterior
    public void PreviousRoom()
    {
        // Retrocede para a sala anterior, se possível
        ChangeRoom(currentRoomIndex - 1);
    }

    // Função para atualizar os botões com base na sala atual
    public void UpdateButtons()
    {
        leftButton.interactable = currentRoomIndex > 0; // Ativa o botão apenas se não estiver na primeira sala
        rightButton.interactable = currentRoomIndex < roomSprites.Length - 1; // Ativa o botão apenas se não estiver na última sala
    }
}
