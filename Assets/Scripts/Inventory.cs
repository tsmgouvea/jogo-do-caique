using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Inventory : MonoBehaviour
{
    // Referências ao painel do inventário e ao painel interno dos slots
    public GameObject inventoryPanel; // Painel principal do inventário
    public GameObject slotsPanel;     // Painel interno onde os slots serão colocados
    public GameObject slotPrefab;     // Prefab do slot vazio
    public Image avatarImage;         // Avatar do jogador
    public int numberOfSlots = 6;    // Número total de slots no inventário

    private List<GameObject> slots = new List<GameObject>(); // Lista de slots
    private List<string> collectedItems = new List<string>(); // Lista de itens coletados

    void Start()
    {
        // Inicialmente o inventário está oculto
        inventoryPanel.SetActive(false);

        // Criar os slots vazios no painel interno
        CreateEmptySlots();
    }

    // Função para criar os slots vazios no painel interno
    private void CreateEmptySlots()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotsPanel.transform); // Slots no painel interno
            slots.Add(newSlot);
        }
    }

    // Função para alternar o estado de visibilidade do inventário
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    // Função para adicionar um item ao primeiro slot vazio
    public void AddItem(string itemName, Sprite newItemIcon)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);

            foreach (GameObject slot in slots)
            {
                // Verifica se o slot está vazio (sem sprite)
                Image itemImage = slot.transform.Find("ItemIcon").GetComponent<Image>();
                if (itemImage.sprite == null)
                {
                    // Preenche o slot com o ícone do item
                    itemImage.sprite = newItemIcon;
                    itemImage.enabled = true;

                    // Atualiza o texto do slot, se necessário
                    TextMeshProUGUI slotText = slot.GetComponentInChildren<TextMeshProUGUI>();
                    if (slotText != null)
                    {
                        slotText.text = itemName;
                    }

                    // Adiciona ação ao botão do slot
                    Button slotButton = slot.GetComponent<Button>();
                    if (slotButton != null)
                    {
                        slotButton.onClick.AddListener(() => UseItem(itemName));
                    }

                    return; // Sai após preencher o primeiro slot vazio
                }
            }

            Debug.LogWarning("Inventário cheio!");
        }
    }

    // Função chamada ao usar o item (a lógica pode ser expandida conforme necessário)
    void UseItem(string itemName)
    {
        Debug.Log("Usando o item: " + itemName);
        // Aqui você pode adicionar a lógica de efeito do item, como alterar o avatar, etc.
    }
}
