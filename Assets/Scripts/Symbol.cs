using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    public Color symbolColor; // Cor associada ao símbolo
    private PuzzleController puzzleController;

    private void Start()
    {
        // Substitua FindObjectOfType por FindFirstObjectByType para localizar o PuzzleController
        puzzleController = Object.FindFirstObjectByType<PuzzleController>();

        if (puzzleController == null)
        {
            Debug.LogError("PuzzleController não encontrado na cena!");
        }
    }

    public void OnSymbolClicked()
    {
        // Notifica o controlador do puzzle quando um símbolo é clicado
        puzzleController.RegisterInput(symbolColor);
    }
}