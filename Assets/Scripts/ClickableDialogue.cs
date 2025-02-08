using UnityEngine;

public class ClickableDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager; // Refer�ncia ao sistema de di�logo
    public string[] dialogueLines; // Linhas de di�logo associadas ao clique
    public string[] characterNames;
    public Sprite[] characterSprites; // Sprites dos personagens que aparecer�o durante o di�logo
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
            // Inicia o di�logo passando as linhas e os sprites dos personagens
            dialogueManager.StartDialogue(dialogueLines, characterNames, characterSprites);
        }
    }


}