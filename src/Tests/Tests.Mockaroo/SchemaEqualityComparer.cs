using Gigobyte.Mockaroo;
using System;
using System.Collections.Generic;

namespace Tests.Mockaroo
{
    public class SchemaEqualityComparer : IEqualityComparer<Schema>
    {
        public bool Equals(Schema x, Schema y)
        {
            //return x.ToJson() == y.ToJson();
            int i = 0, n = x.Count;
            bool areSame = x.Count == y.Count;
            while (areSame && (i < n))
            {
                areSame =
                    x[i].Name == y[i].Name &&
                    x[i].Type == y[i].Type &&
                    x[i].Formula == y[i].Formula &&
                    x[i].BlankPercentage == y[i].BlankPercentage;

                i++;
            }
            return areSame;
        }

        public int GetHashCode(Schema obj)
        {
            throw new NotImplementedException();
        }
    }
}