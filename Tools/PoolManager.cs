using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public List<Poolable> poolableList;
    [SerializeField]
    private List<Poolable> currentActivePoolables;

    private Dictionary<string, Stack<Poolable>> poolDictionary;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        poolDictionary = new Dictionary<string, Stack<Poolable>>();

        foreach(var poolObj in poolableList)
        {
            if (poolObj == null)
                continue;
            poolDictionary.Add(poolObj.id, new Stack<Poolable>());

            for (int i = 0; i < poolObj.createCountInAwake; i++)
            {
                Poolable clone = Instantiate(poolObj.gameObject, transform).GetComponent<Poolable>();
                clone.gameObject.SetActive(false);
                poolDictionary[poolObj.id].Push(clone);
            }
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    /* 프리팹 또는 같은 오브젝트를 파라미터로 받아서
     * 풀러블 오브젝트를 찾아줌
     */
    public Poolable Pop(Poolable poolObj)
    {
        // 풀에 존재한다면 찾음
        if(poolDictionary.ContainsKey(poolObj.id))
        {
            Poolable returnObj = null;
            while (poolDictionary[poolObj.id].Count > 0 && returnObj == null)
            {
                returnObj = poolDictionary[poolObj.id].Pop();
                if (returnObj.isUsing)
                {
                    returnObj = null;
                    continue;
                }

                returnObj.isUsing = true;
                returnObj.onPop?.Invoke();                
            }

            if (returnObj == null)
            {
                returnObj = Instantiate(poolObj.gameObject, transform).GetComponent<Poolable>();
                returnObj.gameObject.SetActive(false);
                returnObj.isUsing = true;
                returnObj.onPop?.Invoke();
            }

            currentActivePoolables.Add(returnObj);
            return returnObj;
        }
        // 풀에 없다면 풀을 만들고 새로생성해서 반환해줌
        else
        {
            poolableList.Add(poolObj);
            poolDictionary.Add(poolObj.id, new Stack<Poolable>());
            Poolable clone = Instantiate(poolObj, transform).GetComponent<Poolable>();
            clone.gameObject.SetActive(false);
            clone.isUsing = true;
            if (clone.isAutoPooling)
                clone.ResetAutoPoolingTimer();
            clone.onPop?.Invoke();

            currentActivePoolables.Add(clone);
            return clone;
        }
    }

    public Poolable Pop(string id)
    {
        if(!poolDictionary.ContainsKey(id))
        {
            Debug.LogError("ID에 해당하는 풀링된 오브젝트 없음");
            return null;
        }

        // 생성한 풀러블 오브젝트가 있다면 탐색
        if (poolDictionary[id].Count > 0)
        {
            Poolable returnObj = null;
            while (poolDictionary[id].Count > 0 && returnObj == null)
            {
                returnObj = poolDictionary[id].Pop();
                if (returnObj.isUsing)
                {
                    returnObj = null;
                    continue;
                }

                returnObj.isUsing = true;
                returnObj.onPop?.Invoke();
            }

            // 미사용중인 풀러블 오브젝트가 없다면 새로생성
            if (returnObj == null)
            {
                returnObj = Instantiate(poolDictionary[id].Peek().gameObject, transform).GetComponent<Poolable>();
                returnObj.gameObject.SetActive(false);
                returnObj.isUsing = true;
                returnObj.onPop?.Invoke();
            }

            currentActivePoolables.Add(returnObj);
            return returnObj;
        }
        // 생성한 풀러블 오브젝트가 없다면
        else
        {
            // 프리팹 리스트에서 아이디로 프리팹을 찾아봄
            Poolable prefab = poolableList.Find(p => p.id == id);
            if (prefab == null)
            {
                Debug.LogError("ID에 해당하는 풀링된 오브젝트 없음");
                return null;
            }

            // 프리팹을 찾았다면 생성
            Poolable clone = Instantiate(prefab.gameObject, transform).GetComponent<Poolable>();
            clone.gameObject.SetActive(false);
            clone.isUsing = true;
            if (clone.isAutoPooling)
                clone.ResetAutoPoolingTimer();
            clone.onPop?.Invoke();
            currentActivePoolables.Add(clone);

            return clone;
        }
    }

    public void Push(Poolable poolObj)
    {
        if (!poolDictionary.ContainsKey(poolObj.id))
        {
            poolDictionary.Add(poolObj.id, new Stack<Poolable>());
            poolableList.Add(poolObj);
        }

        poolObj.onPush?.Invoke();
        poolObj.isUsing = false;
        poolDictionary[poolObj.id].Push(poolObj);
        poolObj.gameObject.SetActive(false);

        if (currentActivePoolables.Contains(poolObj))
            currentActivePoolables.Remove(poolObj);
    }

    public void PushAllActivePoolables()
    {
        if (currentActivePoolables.Count <= 0)
            return;

        List<Poolable> copyList = new List<Poolable>();

        foreach (var p in currentActivePoolables)
            copyList.Add(p);

        foreach (var p in copyList)
            Push(p);

        currentActivePoolables.Clear();
    }
}
