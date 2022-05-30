using TMPro;
using UnityEngine;

public class TextRandomizer : MonoBehaviour
{
    [SerializeField] string[] phrases;
    TextMeshProUGUI currentText;
    int counter = 0;
    private void Awake()
    {
        currentText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void UpdateText()
    {
        currentText.text = phrases[counter];
        if (counter == phrases.Length - 1)
            counter = 0;
        else counter++;
    }
}
