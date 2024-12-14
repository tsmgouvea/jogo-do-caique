using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public Image backgroundImage; // Refer�ncia � imagem de fundo
    public Sprite[] roomSprites; // Lista de imagens das salas
    public Button leftButton;  // Bot�o de retroceder
    public Button rightButton; // Bot�o de avan�ar
    public GameObject clickableArea; // Refer�ncia ao ClickableArea
    public GameObject chave;
    public GameObject faca;

    private int currentRoomIndex = 0; // �ndice da sala atual

    void Start()
    {
        // Certifique-se de que a sala inicial est� sendo exibida
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

        // Atualize os bot�es para refletir a sala inicial
        UpdateButtons();
    }

    // Fun��o chamada para mudar de sala
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

                // Ativa o ClickableArea somente na sala 1 (ou a sala que voc� escolher)
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

            // Atualiza os bot�es para refletir o novo estado da sala
            UpdateButtons();
        }
        else
        {
            Debug.LogWarning("�ndice da sala � inv�lido!");
        }
    }

    // Fun��o chamada para ir para a pr�xima sala
    public void NextRoom()
    {
        // Avan�a para a pr�xima sala, se poss�vel
        ChangeRoom(currentRoomIndex + 1);
    }

    // Fun��o chamada para retroceder para a sala anterior
    public void PreviousRoom()
    {
        // Retrocede para a sala anterior, se poss�vel
        ChangeRoom(currentRoomIndex - 1);
    }

    // Fun��o para atualizar os bot�es com base na sala atual
    public void UpdateButtons()
    {
        leftButton.interactable = currentRoomIndex > 0; // Ativa o bot�o apenas se n�o estiver na primeira sala
        rightButton.interactable = currentRoomIndex < roomSprites.Length - 1; // Ativa o bot�o apenas se n�o estiver na �ltima sala
    }
}
