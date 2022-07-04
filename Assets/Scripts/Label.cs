using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace label
{
 public class Label: MonoBehaviour
    {  [Tooltip("L,W,H in inches. Weight in pounds")]
       [Range(1, 100)] public int weight;
       [Range(1, 100)] public int length; 
       [Range(1, 100)] public int width;
       [Range(1, 100)] public int height;

        public bool isDamaged;
    }
  public enum BoxSize
    {
        Small,
        Medium,
        Large
    }

    public enum UniqueCharacteristics
    {
        None,
        IrregularShape,
        WrappedInPlastic,
        Strapped

    }
}