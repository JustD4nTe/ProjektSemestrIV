using System;
using System.Data;

namespace ProjektSemestrIV.DAL.Entities
{
    interface IBaseEntity : ICloneable
    {
        void SetData(IDataReader dataReader);
    }
}
