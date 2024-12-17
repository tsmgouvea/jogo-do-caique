using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    public Color symbolColor; // Cor associada ao s�mbolo
    private PuzzleController puzzleController;

    private void Start()
    {
        // Substitua FindObjectOfType por FindFirstObjectByType para localizar o PuzzleController
        puzzleController = Object.FindFirstObjectByType<PuzzleController>();

        if (puzzleController == null)
        {
            Debug.LogError("PuzzleController n�o encontrado na cena!");
        }
    }

    public void OnSymbolClicked()
    {
        // Notifica o controlador do puzzle quando um s�mbolo � clicado
        puzzleController.RegisterInput(symbolColor);
    }
}