using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlockStateSwitcher : MonoBehaviour {

    List<GameObject> blocks = new List<GameObject>();

    Transform Cell;

    private void OnEnable()
    {
        Cell = transform;

        for (int i = 0; i < Cell.childCount; i++)
        {
            Transform block = Cell.GetChild(i);
            if(block.tag == "Corruptable" || block.tag == "Passable")
            {
                blocks.Add(block.gameObject);
               
            }
            else if(block.tag == "Variant")
            {
                if(block.childCount > 0)
                {
                    for (int j = 0; j < block.childCount; j++)
                    {
                        Transform BlockChild = block.GetChild(j);
                        if(BlockChild.tag == "Corruptable" || block.tag == "Passable")
                        {
                            blocks.Add(BlockChild.gameObject);
                        }
                    }                    
                }
            }
            
        }

        ResetState();
    }

    void ResetState()
    {
        for (int i = 0; i < blocks.Count; i++)
        {
            if(blocks[i].GetComponent<SpriteRenderer>().color == Color.blue)
            {
                blocks[i].GetComponent<SpriteRenderer>().color = new Color32(5, 154, 0, 255);
            }
        }
    }
}
