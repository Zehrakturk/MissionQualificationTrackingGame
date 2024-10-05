using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Toggle1Sound : MonoBehaviour
{
    public Toggle m_Toggle1;

    void Start()
    {
        if (m_Toggle1 == null)
        {
            m_Toggle1 = FindObjectOfType<Toggle>();
            if (m_Toggle1 == null)
            {
                Debug.LogError("Toggle1 bileþeni bulunamadý.");
            }
        }

        m_Toggle1.onValueChanged.AddListener(OnToggleValueChanged);
        OnToggleValueChanged(m_Toggle1);

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
        if(isOn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
