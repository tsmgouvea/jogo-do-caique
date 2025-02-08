using UnityEngine;

public class ClickableDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager; // Referência ao sistema de diálogo
    public string[] dialogueLines; // Linhas de diálogo associadas ao clique
    public string[] characterNames;
    public Sprite[] characterSprites; // Sprites dos personagens que aparecerão durante o diálogo
    public RoomManager roomManager;
    public int valorSala;

    // Detecta o clique no objeto
    void OnMouseDown()
    {
        if (dialogueManager != null)
        {
            if (roomManager != null)
            {
                roomManager.ChangeRoom(valorSala);
            }
            // Inicia o diálogo passando as linhas e os sprites dos personagens
            dialogueManager.StartDialogue(dialogueLines, characterNames, characterSprites);
        }
    }


}