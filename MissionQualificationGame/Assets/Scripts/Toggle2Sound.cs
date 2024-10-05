using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Toggle2Sound : MonoBehaviour
{
    public Toggle m_Toggle2;

    void Start()
    {
        if (m_Toggle2 == null)
        {
            m_Toggle2 = FindObjectOfType<Toggle>();
            if (m_Toggle2 == null)
            {
                Debug.LogError("Toggle2 bileþeni bulunamadý.");
            }
        }

        m_Toggle2.onValueChanged.AddListener(OnToggleValueChanged);
        OnToggleValueChanged(m_Toggle2);

        // Baþlangýç durumu için ToggleValueChanged metodunu çaðýrmak
    }

    // Toggle deðeri deðiþtiðinde çaðrýlacak metod
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Toggle acildi");
        }
        else
        {
            Debug.Log("Toggle kapandi!");
        }
    }

    public bool AcikMi(bool isOn)
    {
        if (isOn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
