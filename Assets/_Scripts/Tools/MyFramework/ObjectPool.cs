using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
				Debug.Log("Pool "+objName+" Existed");
				if (pools[objName] != null)
				{

					for (int i = 0; i < pools[objName].Count; i++)
					{
						if (!pools[objName][i].activeInHierarchy)
						{
							Debug.Log("setactive existed "+pools[objName][i]);
							//pools[objName][i].SetActive(true);
							return pools[objName][i];
							
						}
						else
						{
							Debug.Log("continue;");
							//BE AWARE OF BREAK & CONTINUE!!!!!!!!
							continue;  
							
						}
					}
					//no enough inactive objects
					Debug.Log("no enough inactive objs");
					pools[objName].Add(prefabs[objName]);
					
					
					Debug.Log(pools[objName].Count);
					GameObject instance = Object.Instantiate(pools[objName][pools[objName].Count-1]);
					instance.name += instance.GetInstanceID().ToString();
					pools[objName][pools[objName].Count-1] = instance;
					return instance;
					//result = pools[objName][0];
				}
				else
				{
					Debug.Log("pool is empty!");
				}
			}
			else
			{
				GameObject prefab;
				if (prefabs.ContainsKey(objName))
				{
					pools.Add(objName,new List<GameObject>());
					Debug.Log("Added New Pool for"+objName);
				}
				else
				{
					prefab = Resources.Load<GameObject>("Prefabs/" + objName);//load to mem
					prefabs.Add(objName,prefab);
				}
			}

			/*
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
				prefab = Resources.Load<GameObject>("Prefabs/" + objName);//load to mem
				prefabs.Add(objName,prefab);//add to prefab dic
				//Debug.Log(prefabs.Count);
				Debug.Log("added new prefab to dic "+prefabs[objName]);
				
				//pools.Add(objName,new List<GameObject>(){prefab});
				Debug.Log("added new pool to pool");
				
				
			}

			//result = pool[objName][0];
			
			//result = UnityEngine.Object.Instantiate(prefab);
			//result.GetInstanceID()
			//result.SetActive(true);
			Debug.Log("instantiated new "+prefab.name);

			result.name = objName;
			
			*/
			
			
			return result;
		}
		//recycle existed obj
		public void RecycleObj(GameObject obj)
		{
			obj.SetActive(false);
			Debug.Log("recycling "+obj.name);

			/*
			if (pools.ContainsKey(obj.name))
			{
				pools[obj.name].Add(obj);
				Debug.Log("Recycled "+obj.name);
			}
			else
			{
				pools.Add(obj.name, new List<GameObject>() {obj});
				Debug.Log("created pool "+obj.name);
			}
			*/
		}


		private void RecheckPool()
		{
			
		}

		//initial a specific pool
		public void InitPool(string objName, int count, GameObject parent)
		{
			GameObject prefab = null;
			GameObject instance = null;
			
			if (prefabs.ContainsKey(objName))
			{
				Debug.Log("contains key");
				return;
			}
			else
			{
				prefab = Resources.Load<GameObject>("Prefabs/" + objName);//load to mem
				
				prefabs.Add(objName,prefab);
				pools.Add(objName,new List<GameObject>(count));
				for (int i = 0; i < count; i++)
				{
					pools[objName].Add(prefab);
					
					instance = UnityEngine.Object.Instantiate(pools[objName][i]);//instantiate to scene
					instance.name += instance.GetInstanceID().ToString();
					//pools[objName].Add(instance);
					pools[objName][i] = instance;
					instance.transform.SetParent(parent.transform,true);
					instance.SetActive(false);
					//Debug.Log("has"+pool[objName][i].name);
				}
				
				
				//pool[objName].Add(prefab);
			}
		}



		public List<GameObject> GetPool(string poolName)
		{
			if (pools.ContainsKey(poolName))
			{
				Debug.Log("get "+poolName);
				return pools[poolName];
				
			}
			//return pools[poolName];
			else
			{
				Debug.Log("get null");
				return null;
				
				
			}
		}
	}
	

}

