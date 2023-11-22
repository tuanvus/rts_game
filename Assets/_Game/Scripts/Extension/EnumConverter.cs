using System;
using UnityEngine;

    public class EnumConverter
    {
        public static TEnum StringToEnum<TEnum>(string value) where TEnum : struct
        {
          //  Debug.Log($"_Log TEnum => <b><color=green>{value}</color></b>");
            if (Enum.TryParse<TEnum>(value, out TEnum result))
            {
                return result;
            }
            else
            {
                Debug.Log($"Invalid enum value => <b><color=red>{value}</color></b>");
            }

            return default(TEnum);
        }

        public static string EnumToString<TEnum>(TEnum value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }

        public static int EnumToInt<TEnum>(TEnum value)
        {
            return (int)(object)value;
        }

        public static Enum GetEnumByString(Type enumType, string value)
        {
            if (value == "")
            {
                Array enumValues = Enum.GetValues(enumType);
                if (enumValues.Length > 0)
                {
                    return (Enum)enumValues.GetValue(0);
                }
                else
                {
                    Debug.Log("Enum type has no values.");
                }
            }
            else
            {
          
                bool check = Enum.IsDefined(enumType, value);
                if (check)
                {
                    return (Enum)Enum.Parse(enumType, value);
                }
                else
                {
                    Array enumValues = Enum.GetValues(enumType);
                    if (enumValues.Length > 0)
                    {
                        return (Enum)enumValues.GetValue(0);
                    }
                }
            }

            return null;
        }

        public static bool TryGetEnumByString<T>(string value, out T result) where T : struct
        {
            return Enum.TryParse(value, out result);
        }
    }
