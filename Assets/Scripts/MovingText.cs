using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace MercenariesProject
{
    public class MovingText : MonoBehaviour
    {
        public TMP_Text textComponent;
        public List<string> _randomTitle;
        private void Start()
        {
            int randomNb = (int)Random.Range(0f, _randomTitle.Count - 1);
           
            textComponent.text = _randomTitle[randomNb];
        }
        void Update()
        {
            textComponent.ForceMeshUpdate();
            var textInfo = textComponent.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible)
                {
                    continue;
                }
                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; j++)
                {
                    var orig = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 4f + orig.x * 0.01f) * 10f, 0);
                }


            }
            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                var mesInfo = textInfo.meshInfo[i];
                mesInfo.mesh.vertices = mesInfo.vertices;
                textComponent.UpdateGeometry(mesInfo.mesh, i);
            }
        }
    }
}

