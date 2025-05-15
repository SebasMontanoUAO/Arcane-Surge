using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float fadeDuration = 0.5f;
    public Transform popupPosition;

    private Transform mainCamera;
    private TextMeshProUGUI textMesh;
    private Vector3 startingPosition;
    private float timer = 0f;

    void Awake()
    {
        mainCamera = Camera.main.transform;
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Movimiento hacia arriba
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Fade out
        timer += Time.deltaTime;
        if (timer <= fadeDuration)
        {
            float alpha = 1 - (timer / fadeDuration);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
        }
        else
        {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1);
            transform.position = popupPosition.position;
        }
        transform.LookAt(mainCamera.position.ToIso());
    }
}
