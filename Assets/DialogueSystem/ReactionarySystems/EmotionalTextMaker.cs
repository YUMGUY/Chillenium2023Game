using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmotionalTextMaker : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text textComponent;
    
    public bool wavyText;
    public float waveStrength;

    public bool wiggleText;
    public float horizWiggleStrength;
    public float vertWiggleStrength;
    public float wiggleFrequency;

    public int startIndex;
    public int endIndex;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(wavyText) { MakeWavyText(); }
       else if(wiggleText) { MakeWiggleText(); }
    }

    public void MakeWavyText()
    {
        print("animating text");
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;
        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var characterInfo = textInfo.characterInfo[i];
            if (!characterInfo.isVisible)
            {
                continue;
            }
            var vertices = textInfo.meshInfo[characterInfo.materialReferenceIndex].vertices;
            // 4 vertices
            for (int j = 0; j < 4; ++j)
            {
                var orig = vertices[characterInfo.vertexIndex + j];
                vertices[characterInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * waveStrength, 0);

            }
        }
        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        } 
    }

    public void MakeWiggleText()
    {
        print("wiggling text");
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;
        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var characterInfo = textInfo.characterInfo[i];
            if (!characterInfo.isVisible)
            {
                continue;
            }
            var vertices = textInfo.meshInfo[characterInfo.materialReferenceIndex].vertices;
            // 4 vertices
            for (int j = 0; j < 4; ++j)
            {
                var orig = vertices[characterInfo.vertexIndex + j];
                vertices[characterInfo.vertexIndex + j] = orig + new Vector3(Mathf.Sin(Time.time * wiggleFrequency + orig.y * 0.01f) * (horizWiggleStrength + Random.Range(-1f, 1f)), vertWiggleStrength * Random.Range(-1f,1f), 0);

            }
        }
        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
