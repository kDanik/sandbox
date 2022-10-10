using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ElementGridFileUtil : MonoBehaviour
{
    public String fileNameForSaving;

    public TextAsset fileForReading;

    public void SaveElementGridToFile(ElementGrid elementGrid)
    {
        WriteToFile(SerializeElementGridElements(elementGrid));
    }

    private string SerializeElementGridElements(ElementGrid elementGrid) {
        JArray elementsJson = new JArray();

        for (int y = 0; y < elementGrid.GetHeight(); y++)
        {
            for (int x = 0; x < elementGrid.GetWidth(); x++)
            {
                BaseElement baseElement = elementGrid.GetElementUnsafe(x, y);

                if (baseElement == null) continue;



                JObject elementJson = new JObject();

                elementJson.Add("elementId", baseElement.elementTypeId);
                elementJson.Add("x", x);
                elementJson.Add("y", y);

                elementsJson.Add(elementJson);
            }
        }

        return elementsJson.ToString();
    }


    public void LoadElementGridFromFile(ElementGrid elementGrid)
    {
        if (fileForReading == null) return;

        JArray elements = JArray.Parse(fileForReading.text);

        foreach (JToken element in elements.Children()) {

            elementGrid.SetElement(element.Value<int>("x"), element.Value<int>("y"), Activator.CreateInstance(Elements.elementsTypes[element.Value<int>("elementId")]) as BaseElement);
        }
    }

    private void WriteToFile(string content) {
        string filePath = Application.dataPath + "/ElementGridSaves/" + fileNameForSaving;
        File.Delete(filePath);
     
        StreamWriter outStream = File.CreateText(filePath);
        
        outStream.Write(content);

        outStream.Close();
    }
}
