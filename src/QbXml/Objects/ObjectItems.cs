﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QbSync.QbXml.Objects
{
    internal class ObjectItems
    {
        private readonly object instance;
        private readonly string[] nameOrder;
        private readonly PropertyInfo itemsProperty;
        private readonly object[] itemsValue;
        private readonly Dictionary<System.Type, string> typeMapping;
        private List<ObjectItemValue> propertyList;

        public ObjectItems(object instance, string name, string[] nameOrder, Dictionary<System.Type, string> typeMapping)
        {
            this.instance = instance;
            this.nameOrder = nameOrder;
            this.typeMapping = typeMapping;
            propertyList = new List<ObjectItemValue>();

            itemsProperty = instance.GetType().GetProperty(name);
            itemsValue = itemsProperty.GetValue(instance, null) as object[];

            Initialize();
        }

        private void Initialize()
        {
            if (itemsValue != null)
            {
                for (var i = 0; i < itemsValue.Length; i++)
                {
                    propertyList.Add(new ObjectItemValue
                    {
                        Name = GetMappingName(itemsValue[i].GetType()),
                        Value = itemsValue[i]
                    });
                }
            }
        }

        private string GetMappingName(System.Type type)
        {
            foreach (var kvp in typeMapping)
            {
                if (type.Equals(kvp.Key) || type.IsSubclassOf(kvp.Key))
                {
                    return kvp.Value;
                }
            }

            return null;
        }

        public void SetItem<T>(string name, T value)
        {
            RemoveItems(name);

            propertyList.Add(new ObjectItemValue
            {
                Name = name,
                Value = value
            });

            SetItemsOnInstance();
        }

        public void SetItems<T>(string name, T[] values)
        {
            RemoveItems(name);

            for (var i = 0; i < values.Length; i++)
            {
                propertyList.Add(new ObjectItemValue
                {
                    Name = name,
                    Value = values[i]
                });
            }

            SetItemsOnInstance();
        }

        public T GetItem<T>(string name)
        {
            return GetItems<T>(name).FirstOrDefault();
        }

        public IEnumerable<T> GetItems<T>(string name)
        {
            return propertyList.Where(m => m.Name == name).Select(m => m.Value).Cast<T>();
        }

        private void RemoveItems(string name)
        {
            propertyList = propertyList.Where(m => m.Name != name).ToList();
        }

        private void SetItemsOnInstance()
        {
            if (nameOrder != null)
            {
                propertyList = propertyList
                               .OrderBy(m => Array.FindIndex(nameOrder, n => n == m.Name))
                               .ToList();
            }

            var itemsValue = propertyList
                .Select(m => m.Value);

            if (itemsValue.Count() == 0)
            {
                itemsValue = null;
            }

            if (itemsValue != null)
            {
                itemsProperty.SetValue(instance, itemsValue.ToArray(), null);
            }
        }
    }
}
