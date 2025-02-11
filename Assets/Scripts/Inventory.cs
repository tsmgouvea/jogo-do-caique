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
    public GameObject clickableArea;
    public Button inventoryButton;
    public Image itemPreview;          // Imagem de pré-visualização do item
    public TextMeshProUGUI itemDescription; // Texto para descrição do item
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
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);
        GameState.IsModalActive = isActive; // Atualiza o estado global
        ToggleInteractions(!isActive); // Desativa ou ativa os botões
    }

    private void ToggleInteractions(bool enable)
    {
        var buttons = FindObjectsByType<UnityEngine.UI.Button>(FindObjectsSortMode.None);

        clickableArea.SetActive(enable);

        foreach (var button in buttons)
        {
            
            // Verifica se o botão é o InventoryButton
            if (button == inventoryButton) // Assumindo que você tem uma referência pública do InventoryButton
                continue; // Não desativa o botão do inventário

            // Verifica se o botão é de um slot ou deve permanecer ativo
            bool isSlotButton = slots.Exists(slot => button == slot.GetComponent<Button>());

            if (isSlotButton)
                continue; // Ignora a desativação dos botões de slots

            button.interactable = enable;
        }

        // Desativa objetos clicáveis com BoxCollider2D
        var colliders2D = FindObjectsByType<BoxCollider2D>(FindObjectsSortMode.None);
        foreach (var collider in colliders2D)
        {
            collider.enabled = enable; // Ativa ou desativa o BoxCollider2D
        }
    }

    // Função para adicionar um item ao primeiro slot vazio
    public void AddItem(string itemName, Sprite newItemIcon, string itemDescriptionText)
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
                        slotButton.onClick.RemoveAllListeners();
                        slotButton.onClick.AddListener(() =>
                        {
                            ShowItemDetails(new Item { name = itemName, description = itemDescriptionText, icon = newItemIcon });
                        });

                    }

                    return; // Sai após preencher o primeiro slot vazio
                }
            }

            Debug.LogWarning("Inventário cheio!");
        }
    }

    // Função para exibir os detalhes de um item
    public void ShowItemDetails(Item item)
    {
        if (item != null)
        {
            itemPreview.sprite = item.icon;
            itemPreview.enabled = true; // Ativa a imagem do ícone
            itemDescription.text = item.description;
        }
        else
        {
            itemPreview.sprite = null;
            itemPreview.enabled = false; // Oculta a imagem do ícone
            itemDescription.text = "Slot vazio";
        }
    }

    // Função chamada ao usar o item (a lógica pode ser expandida conforme necessário)
    void UseItem(string itemName)
    {
        Debug.Log("Usando o item: " + itemName);
        // Aqui você pode adicionar a lógica de efeito do item, como alterar o avatar, etc.
    }
}


[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite icon;
}