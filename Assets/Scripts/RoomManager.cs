using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public GameObject roomContainer; // Container das salas
    public float roomWidth = 1920f;  // Largura de cada sala
    public float roomHeight = 1080f; // Altura de cada sala
    public int totalRooms = 6;       // Número total de salas
    public int roomsPerRow = 3;      // Número de salas por linha (para grade)


    private int currentRoomIndex = 0; // Índice da sala atual

    void Start()
    {
        if (roomContainer != null)
        {
            roomContainer.transform.localPosition = Vector3.zero;

        }
        else
        {
            Debug.LogError("RoomContainer não foi atribuído no Inspector!");
        }
    }

    // Função para mudar diretamente para uma sala específica
    public void ChangeRoom(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < totalRooms)
        {
            currentRoomIndex = roomIndex;

            // Calcula a posição correta na grade
            int row = roomIndex / roomsPerRow;  // Descobre a linha
            int col = roomIndex % roomsPerRow;  // Descobre a coluna

            float targetPositionX = -roomWidth * col;
            float targetPositionY = roomHeight * row;  // Movendo para baixo (positivo)

            // Move o RoomContainer para a nova posição
            roomContainer.transform.localPosition = new Vector3(targetPositionX, targetPositionY, 0);
        }
        else
        {
            Debug.LogWarning("Índice da sala é inválido!");
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
