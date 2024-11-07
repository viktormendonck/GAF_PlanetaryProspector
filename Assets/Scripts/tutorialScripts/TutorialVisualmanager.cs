using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVisualmanager : MonoBehaviour
{
    [System.Serializable]
    public struct VisualInfo
    {
        public string Text;
        public bool UsesVisualObject;
        public GameObject VisualObject;
    }

    [SerializeField] List<VisualInfo> TutorialPages = new List<VisualInfo>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(TutorialPages[0].Text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
