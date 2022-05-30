using UnityEngine;

public class StoryCanvasManager : MonoBehaviour
{

    [SerializeField] CanvasesController canvasesController;
    [SerializeField] Canvas[] storyCanvases;
    bool triggerOnce;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOnce)
            return;
        triggerOnce = true;
        foreach (Canvas c in storyCanvases)
            canvasesController.UpdateCanvasToDisplay(c);
    }
}
