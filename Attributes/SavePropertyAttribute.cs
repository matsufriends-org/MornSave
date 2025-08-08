using System;

namespace MornSave
{
    /// <summary>
    /// 自動生成するプロパティのアクセスレベル
    /// </summary>
    public enum SavePropertyAccessLevel
    {
        /// <summary>getterのみ公開</summary>
        GetterOnly,
        /// <summary>getter/setterのみ公開（購読なし）</summary>
        GetterSetter,
        /// <summary>getter/setter/購読の全てを公開</summary>
        Full
    }

    /// <summary>
    /// このフィールドに対してプロパティとSubjectを自動生成する
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SavePropertyAttribute : Attribute
    {
        /// <summary>
        /// 生成するプロパティのアクセスレベル
        /// </summary>
        public SavePropertyAccessLevel AccessLevel { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="accessLevel">アクセスレベル（デフォルト: Full）</param>
        public SavePropertyAttribute(SavePropertyAccessLevel accessLevel = SavePropertyAccessLevel.Full)
        {
            AccessLevel = accessLevel;
        }
    }
}