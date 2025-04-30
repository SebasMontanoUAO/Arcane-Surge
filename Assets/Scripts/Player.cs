using UnityEngine;

public class Player : MonoBehaviour
{
    public static Transform Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this.transform;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
