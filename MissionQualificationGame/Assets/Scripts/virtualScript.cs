using Cinemachine;
using UnityEngine;

public class VirtualScript : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private static VirtualScript _instance;
    public static VirtualScript Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<VirtualScript>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("VirtualScript");
                    _instance = go.AddComponent<VirtualScript>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        vcam = GetComponent<CinemachineVirtualCamera>();
    }


}