using UnityEngine;

public class TutorialCanvasController : MonoBehaviour
{
    [SerializeField] CanvasesController canvasesController;
    [SerializeField] Canvas[] tutorialCanvases;



    void Start()
    {
        foreach (Canvas c in tutorialCanvases)
            canvasesController.UpdateCanvasToDisplay(c);
    }

    void Update()
    {

    }

}
