using System;

namespace MornSave
{
    /// <summary>
    /// このクラスにSaveProperty属性付きフィールドのプロパティを自動生成する
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GenerateSavePropertiesAttribute : Attribute
    {
    }
}