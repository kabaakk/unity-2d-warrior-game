using UnityEngine;

public class TutorialSign : MonoBehaviour
{
    public TextMesh myText;

    public string tutorialText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myText.text = tutorialText;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        myText.text = null;
    }
}
