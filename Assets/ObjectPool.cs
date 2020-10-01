using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ObjectPool", menuName = "Addressable Object")]
public class ObjectPool : ScriptableObject
{
    public Sprite[] Sprites;
    public AudioClip[] Audio;
    public GameObject[] prefabs;
}
