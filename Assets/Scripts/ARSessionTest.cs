using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARSessionTest : MonoBehaviour
{
    [SerializeField]
    ARSession m_Session;

    [SerializeField] 
    private Text _text;

    void Update()
    {
        _text.text = ARSession.state.ToString();
    }
}