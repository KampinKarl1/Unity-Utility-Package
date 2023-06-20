using UnityEngine;
using System.Collections;
/// <summary>
/// Can give an object a bit of variety by changing the active shown object at start. 
/// Example: Synty and PolyPerfect have characters with lots of gameobjects/character models; you can randomly choose those models with this script.
/// </summary>
public class MultiModelObject : MonoBehaviour
{
    [SerializeField] GameObject[] models = new GameObject[0];

    [SerializeField, Tooltip("Keep the currently shown gameobject as the active model")]
    private bool keepCurrentModel = false;

    void Start()
    {
        if (models.Length == 0 || keepCurrentModel) 
            return;

        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].activeSelf)
                models[i].SetActive(false);
        }

        ActivateModelAt(Random.Range(0, models.Length));

        Destroy(this);
    }

    private int currentActive = 0;
    public void ActivateModelAt(int modelIndex)
    {
        models[currentActive].SetActive(false);

        currentActive = modelIndex;
        models[modelIndex].SetActive(true);
    }

    private int RandomIndex() 
    {
      int index =   Random.Range(0, models.Length);
        if (index == currentActive && models.Length > 1)//dont create infinite loop
            return RandomIndex();
        return index;
    }
}
