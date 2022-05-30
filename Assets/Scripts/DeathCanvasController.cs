using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathCanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionOfDeath;
    [SerializeField] CanvasesController canvasesController;

    void Start()
    {

    }

    void Update()
    {

    }

    public void FoundDeath(string textToDisplay)
    {
        descriptionOfDeath.text = textToDisplay;

        canvasesController.UpdateCanvasToDisplay(GetComponent<Canvas>());
    }


}
