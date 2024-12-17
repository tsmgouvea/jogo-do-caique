using UnityEngine;

public class CollectItem : MonoBehaviour
{
    // Nome do item e a refer�ncia ao script Inventory
    public string itemName;
    public Sprite itemIcon;
    public string itemText;
    public Inventory inventory;

    // Quando o jogador clicar no item
    void OnMouseDown()
    {
        // Adiciona o item ao invent�rio
        inventory.AddItem(itemName, itemIcon, itemText);

        // Destroi o objeto ap�s a coleta (opcional)
        Destroy(gameObject);
    }
}
