using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public GameObject roomContainer; // Container das salas
    public float roomWidth = 1920f;  // Largura de cada sala
    public float roomHeight = 1080f; // Altura de cada sala
    public int totalRooms = 6;       // N�mero total de salas
    public int roomsPerRow = 3;      // N�mero de salas por linha (para grade)


    private int currentRoomIndex = 0; // �ndice da sala atual

    void Start()
    {
        if (roomContainer != null)
        {
            roomContainer.transform.localPosition = Vector3.zero;

        }
        else
        {
            Debug.LogError("RoomContainer n�o foi atribu�do no Inspector!");
        }
    }

    // Fun��o para mudar diretamente para uma sala espec�fica
    public void ChangeRoom(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < totalRooms)
        {
            currentRoomIndex = roomIndex;

            // Calcula a posi��o correta na grade
            int row = roomIndex / roomsPerRow;  // Descobre a linha
            int col = roomIndex % roomsPerRow;  // Descobre a coluna

            float targetPositionX = -roomWidth * col;
            float targetPositionY = roomHeight * row;  // Movendo para baixo (positivo)

            // Move o RoomContainer para a nova posi��o
            roomContainer.transform.localPosition = new Vector3(targetPositionX, targetPositionY, 0);
        }
        else
        {
            Debug.LogWarning("�ndice da sala � inv�lido!");
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
