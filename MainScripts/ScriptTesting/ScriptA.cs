using UnityEngine;
public class ScriptA : MonoBehaviour
{
    public ScriptB other;

    void Update()
    {
        other.DoSomething();
    }
}