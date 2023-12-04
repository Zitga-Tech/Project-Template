using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace AssetLoad
{
    public sealed class LoadResourceService
    {
        private readonly Dictionary<string, Object> _resourceCached = new();
        
        #region Helper

        private T LoadResourceFromAddressable<T>(string resourcePath) where T : Object
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));

            if (_resourceCached.TryGetValue(resourcePath, out Object value))
            {
                return value as T;
            }

            try
            {
                var handler = Addressables.LoadAssetAsync<T>(resourcePath);
                handler.WaitForCompletion();

                var result = handler.Result;
                if (result == null)
                {
                    return null;
                }

                _resourceCached.TryAdd(resourcePath, result);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private async UniTask<T> LoadResourceFromResource<T>(string resourcePath) where T : Object
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));

            if (_resourceCached.TryGetValue(resourcePath, out Object value))
            {
                return value as T;
            }

            var result = await Resources.LoadAsync<T>(resourcePath);
            if (result == null)
                return null;

            _resourceCached.TryAdd(resourcePath, result);
            return result as T;
        }
        
        #endregion
    }
}

