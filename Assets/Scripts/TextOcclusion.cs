using UnityEngine;
using TMPro;
public class TextOcclusion : MonoBehaviour
{
    public Transform canvasTransform;
    public Transform labelsTransform;
    public TextMeshProUGUI labelText;

    public Vector2 lowRange = new Vector2(-1, -.75f); //opposite directions
    public Vector2 midRange = new Vector2(-.25f, .25f); //0 = perpindicular
    public Vector2 highRange = new Vector2(.75f, 1); //pointing in same directions
    private void Update()
    {
        Vector3 canvasForward = -canvasTransform.forward.normalized;
        Vector3 labelForward = labelsTransform.up.normalized;
        Vector3 camCanvasDif = labelsTransform.position.normalized - canvasTransform.position.normalized;

        float dotProduct = Vector3.Dot(canvasForward, labelForward);

        if(dotProduct >= highRange.x && dotProduct <= highRange.y)
        {
            labelText.gameObject.SetActive(true);
        }
        else
        {
            labelText.gameObject.SetActive(false);
        }
        //Debug.Log(dotProduct);
    }
}
