using UnityEngine;

    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// An abstract class that grants that one object has exactly one instance
        /// <para>The object can be acessed with the GetInstance method, the prefab must be at Resources/Singleton and have the same name as the class
        /// </summary>


        static T Instance;
        public bool ResetInstanceOnDestroy;

        protected virtual void Init()
        {
            DontDestroyOnLoad(gameObject);
        }

        public static T GetInstance()
        {
            if (!Instance)
            {
                var prefab = Resources.Load("Singletons/" + typeof(T).Name) as GameObject;
                if (!prefab)
                {
                    prefab = Resources.Load<T>("Singletons/") as GameObject;
                    if (!prefab)
                    {
                        Debug.LogError("Missing Singleton at Resources/Singletons\n Prefab: " + typeof(T).Name);
                        return null;
                    }
                }

                if (prefab.GetComponent<T>() == null)
                {
                    Debug.LogError("Prefab does not contain script " + typeof(T).Name);
                }

                Instance = Instantiate(prefab).GetComponent<T>();
                if (!Instance)
                {
                    Debug.LogError("No singleton found on resources folder at: " + "Singletons/" + typeof(T).Name + ".asset");
                }
                else
                {
                    Instance.Init();
                }
            }

            return Instance;
        }
    }