using System;
using TD.DAL;

namespace TD.QSModel
{
    /// <summary>
    ///	{||CnName||}
    /// </summary>
    [Serializable]
    public class {||ClassName||} : IChange
    {
	    #region 私有成员

		private bool m_IsChanged;
        private bool m_IsDeleted;
		
		{||FieldListPrivate||}
        #endregion

        #region 默认( 空 ) 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public {||ClassName||}()
        {
{||FieldListInit||}
        }
        #endregion

        #region 公有属性

      {||FieldListPublic||}
	  
        /// <summary>
        /// 对象的值是否被改变
        /// </summary>
        public virtual bool IsChanged
        {
            get { return m_IsChanged; }
        }

        /// <summary>
        /// 对象是否已经被删除
        /// </summary>
        public virtual bool IsDeleted
        {
            get { return m_IsDeleted; }
        }

        #endregion

        #region 公有函数

        /// <summary>
        /// 标记对象已删除
        /// </summary>
        public virtual void MarkAsDeleted()
        {
            m_IsDeleted = true;
            m_IsChanged = true;
        }


        #endregion

        #region 重写Equals和HashCode
        /// <summary>
        /// 用唯一值实现Equals
        /// </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if ((obj == null) || (obj.GetType() != GetType())) return false;
            {||ClassName||} castObj = ({||ClassName||})obj;
            return (castObj != null) &&
                ({||m_ID||} == castObj.{||ID||});
        }

        /// <summary>
        /// 用唯一值实现GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 57;
            hash = 27 * hash * {||m_ID||}.GetHashCode();
            return hash;
        }
        #endregion

        #region IChange 成员

        public void MakeAsDefault()
        {
            m_IsChanged = false;
        }

        #endregion
	}
}
