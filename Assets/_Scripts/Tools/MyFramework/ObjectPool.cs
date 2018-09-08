using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEngine;

namespace MyFramework
{
	public class ObjectPool
	{
		
		//objpool
		private Dictionary<string, List<GameObject>> pools;
		//prefabs
		private Dictionary<string, GameObject> prefabs;


		#region --singleton--

		

		
		private static ObjectPool instance;
		// Use this for initialization
		private ObjectPool()
		{
			pools=new Dictionary<string, List<GameObject>>();
			prefabs=new Dictionary<string, GameObject>();
		}

		public static ObjectPool GetInstance()
		{
			if (instance==null)
			{
				instance=new ObjectPool();
			}

			return instance;
		}
		
		#endregion


		public GameObject GetObj(string objName)
		{
			GameObject result = null;

			if (pools.ContainsKey(objName))
			{
				Debug.Log("contains prefab	"+objName);
				if (pools[objName].Count > 0)
				{
					result = pools[objName][0];
					
					result.SetActive(true);

					pools[objName].Remove(result);

					return result;
				}
			}

			GameObject prefab = null;

			if (prefabs.ContainsKey(objName))
			{
				Debug.Log("contains "+objName);
				prefab = prefabs[objName];
			}
			else
			{
				prefab = Resources.Load<GameObject>("Prefabs/" + objName);
				prefabs.Add(objName,prefab);
				//Debug.Log(prefabs.Count);
				Debug.Log("added new prefab to dic "+prefabs[objName]);
				pools.Add(objName,new List<GameObject>(){prefab});
				Debug.Log("added new pool to pool");
				
				
			}

			//result = pool[objName][0];
			
			result = UnityEngine.Object.Instantiate(prefab);
			Debug.Log("instantiated new "+prefab.name);

			result.name = objName;
			
			
			return result;
		}
		//recycle existed obj
		public void RecycleObj(GameObject obj)
		{
			obj.SetActive(false);
			Debug.Log("recycling");

			if (pools.ContainsKey(obj.name))
			{
				pools[obj.name].Add(obj);
				Debug.Log("added "+obj.name);
			}
			else
			{
				pools.Add(obj.name, new List<GameObject>() {obj});
				Debug.Log("created pool "+obj.name);
			}
		}

		//initial a specific pool
		public void InitPool(string objName, int count)
		{
			GameObject prefab = null;
			
			if (prefabs.ContainsKey(objName))
			{
				Debug.Log("contains key");
				return;
			}
			else
			{
				prefab = Resources.Load<GameObject>("Prefabs/" + objName);
				prefabs.Add(objName,prefab);
				pools.Add(objName,new List<GameObject>(count));
				for (int i = 0; i < count; i++)
				{
					pools[objName].Add(prefab);
					//Debug.Log("has"+pool[objName][i].name);
				}
				
				
				//pool[objName].Add(prefab);
			}
		}
	}
	

}

