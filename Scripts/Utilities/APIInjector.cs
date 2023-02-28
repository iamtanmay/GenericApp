using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace AppStarter
{
    public class APIInjector : MonoBehaviour
    {
        public API API;
        public List<InstanceMethodInfo> APIMethods = new List<InstanceMethodInfo>();

        public List<UnityEvent<InstanceMethodInfo[]>> injections = new List<UnityEvent<InstanceMethodInfo[]>>();

        public object GetAsSystemObject()
        {
            return API;
        }

        public void Inject()
        {
            if (API == null)
                API = transform.GetComponent<API>();

            if (API == null)
                return;

            MethodInfo[] APIMethodArray = API.GetType().GetMethods();
            List<string> monobehaviorMethodNames = new List<string>();

            foreach (MethodInfo method in typeof(MonoBehaviour).GetMethods())
                monobehaviorMethodNames.Add(method.Name);

            foreach (MethodInfo method in APIMethodArray)
                if (!monobehaviorMethodNames.Contains(method.Name))
                {
                    InstanceMethodInfo newMethodInfo = new InstanceMethodInfo();
                    newMethodInfo.instance = API;
                    newMethodInfo.method = method;
                    APIMethods.Add(newMethodInfo);
                }

            foreach (UnityEvent<InstanceMethodInfo[]> injection in injections)
                injection.Invoke(APIMethods.ToArray());
        }
    }
}