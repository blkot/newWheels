using UnityEngine;

[AddComponentMenu("_Scripts/SeeThrough/SetRenderQueue")]

public class SetRenderQueue : MonoBehaviour
{

    [SerializeField]
    protected int[] m_queues = new int[] { 3000 };

    protected Renderer[] _renderers;

    protected void Awake()
    {
        _renderers = this.GetComponents<Renderer>();
        //Material[] materials = new Material[_renderers.Length];
        
        for (int i = 0; i < _renderers.Length && i < m_queues.Length; ++i)
        {
            _renderers[i].material.renderQueue = m_queues[i];
        }
    }
}