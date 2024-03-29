﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Base.Domain.Common
{
    public abstract class Enumeration : IComparable
    {

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; set; }

        #region ctor
        protected Enumeration()
        { }

        protected Enumeration(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        #endregion

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static bool CheckValueValid<T>(int value) where T : Enumeration
        {
            var t = FromValue<T>(value);
            return t != null;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static string GetDisplayName<T>(int value) where T : Enumeration
        {
            var t = FromValue<T>(value);
            return t != null ? t.Name : string.Empty;
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            //if (matchingItem == null)
            //    throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public void Translate(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
