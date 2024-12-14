using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public GameObject roomContainer; // Refer�ncia ao cont�iner das salas
    public float roomWidth = 1920f;  // Largura de cada sala (ajuste conforme o tamanho da sua sala)
    public int totalRooms = 4;       // N�mero total de salas no jogo
    public Button leftButton;        // Bot�o de retroceder
    public Button rightButton;       // Bot�o de avan�ar

    private int currentRoomIndex = 0; // �ndice da sala atual

    void Start()
    {
        // Garante que a posi��o inicial do RoomContainer est� correta
        if (roomContainer != null)
        {
            roomContainer.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("RoomContainer n�o foi atribu�do no Inspector!");
        }

        // Atualiza os bot�es para refletir a sala inicial
        UpdateButtons();
    }

    // Fun��o chamada para mudar de sala
    public void ChangeRoom(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < totalRooms)
        {
            currentRoomIndex = roomIndex;

            // Calcula a nova posi��o X do RoomContainer
            float targetPositionX = -roomWidth * roomIndex;

            // Move o RoomContainer para a nova posi��o
            roomContainer.transform.localPosition = new Vector3(targetPositionX, 0, 0);

            // Atualiza os bot�es para refletir a nova sala
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
        ChangeRoom(currentRoomIndex + 1);
    }

    // Fun��o chamada para retroceder para a sala anterior
    public void PreviousRoom()
    {
        ChangeRoom(currentRoomIndex - 1);
    }

    // Fun��o para atualizar os bot�es com base na sala atual
    public void UpdateButtons()
    {
        leftButton.interactable = currentRoomIndex > 0; // Ativa o bot�o apenas se n�o estiver na primeira sala
        rightButton.interactable = currentRoomIndex < totalRooms - 1; // Ativa o bot�o apenas se n�o estiver na �ltima sala
    }
}