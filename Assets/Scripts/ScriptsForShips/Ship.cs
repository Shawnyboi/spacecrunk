using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private List<ModuleSlot> m_Slots = new List<ModuleSlot>(4);
    private Dictionary<Collider, ModuleSlot> m_ColliderToModuleDictionary = new Dictionary<Collider, ModuleSlot>();
    [SerializeField]
    private List<Collider> m_ModuleCollider = new List<Collider>(4);
    // Start is called before the first frame update

    private void Start()
    {
        m_ColliderToModuleDictionary = new Dictionary<Collider, ModuleSlot>();
        for (int i = 0; i < 4; i++) {
            m_ColliderToModuleDictionary.Add(m_ModuleCollider[i], m_Slots[i]);
        }
    }

    public ModuleSlot GetModuleSlotAtCollider(Collider c)
    {
        return m_ColliderToModuleDictionary[c];
    }

    public bool AddModule(Collider c, Module m)
    {
        if (m_ColliderToModuleDictionary[c] == null)
        {
            m_ColliderToModuleDictionary[c].AddModule(m);
            return true;
        }
        else
        {
            return false;
        }
    }    

    public Module RemoveModuleAtCollider(Collider c)      
    {
        ModuleSlot moduleToRemove = m_ColliderToModuleDictionary[c];
        m_ColliderToModuleDictionary[c] = null;
        return moduleToRemove.RemoveModule();
    }

}


public class ModuleSlot
{
    private Transform m_Transform;
    private Module m_Module = null;
    
    public void AddModule(Module m)
    {
        m_Module = m;
    }

    public Module RemoveModule()
    {
        Module m = m_Module;
        m_Module = null;
        return m;
    }

    public bool ModuleOccupied()
    {
        return m_Module != null;
    }

    public Quaternion GetDirection()
    {
        return m_Transform.rotation;
    }
}