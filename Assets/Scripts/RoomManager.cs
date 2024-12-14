using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public GameObject roomContainer; // Referência ao contêiner das salas
    public float roomWidth = 1920f;  // Largura de cada sala (ajuste conforme o tamanho da sua sala)
    public int totalRooms = 4;       // Número total de salas no jogo
    public Button leftButton;        // Botão de retroceder
    public Button rightButton;       // Botão de avançar

    private int currentRoomIndex = 0; // Índice da sala atual

    void Start()
    {
        // Garante que a posição inicial do RoomContainer está correta
        if (roomContainer != null)
        {
            roomContainer.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("RoomContainer não foi atribuído no Inspector!");
        }

        // Atualiza os botões para refletir a sala inicial
        UpdateButtons();
    }

    // Função chamada para mudar de sala
    public void ChangeRoom(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < totalRooms)
        {
            currentRoomIndex = roomIndex;

            // Calcula a nova posição X do RoomContainer
            float targetPositionX = -roomWidth * roomIndex;

            // Move o RoomContainer para a nova posição
            roomContainer.transform.localPosition = new Vector3(targetPositionX, 0, 0);

            // Atualiza os botões para refletir a nova sala
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
        ChangeRoom(currentRoomIndex + 1);
    }

    // Função chamada para retroceder para a sala anterior
    public void PreviousRoom()
    {
        ChangeRoom(currentRoomIndex - 1);
    }

    // Função para atualizar os botões com base na sala atual
    public void UpdateButtons()
    {
        leftButton.interactable = currentRoomIndex > 0; // Ativa o botão apenas se não estiver na primeira sala
        rightButton.interactable = currentRoomIndex < totalRooms - 1; // Ativa o botão apenas se não estiver na última sala
    }
}