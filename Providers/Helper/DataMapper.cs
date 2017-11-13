using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Providers.Helper
{
    public class DataMapper
    {
        public List<T> MapData<T>(IDataReader dr) where T : new()
        {

            Type businessEntityType = typeof(T);

            List<T> entitys = new List<T>();

            Hashtable hashtable = new Hashtable();

            PropertyInfo[] properties = businessEntityType.GetProperties();

            foreach (PropertyInfo info in properties)
            {

                hashtable[info.Name.ToUpper()] = info;

            }

            while (dr.Read())
            {

                T newObject = new T();

                for (int index = 0; index < dr.FieldCount; index++)
                {

                    PropertyInfo info = (PropertyInfo)

                                        hashtable[dr.GetName(index).ToUpper()];

                    if ((info != null) && info.CanWrite)
                    {
                        try
                        {
                            info.SetValue(newObject, dr.GetValue(index), null);
                        }
                        catch { }

                    }

                }

                entitys.Add(newObject);

            }

            dr.Close();

            return entitys;

        }
    }
}
