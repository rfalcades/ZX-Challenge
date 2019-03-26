using System;
using System.Collections.Generic;
using System.Text;

namespace ZX.Model.Api
{
    public class CoverageArea
    {
        public string type { get; set; }
        public List<List<List<List<double>>>> coordinates { get; set; }
    }

    public class Address
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class PdvRaw
    {
        public PdvRaw()
        {
            this.coverageArea = new CoverageArea();
            this.address = new Address();
        }

        public string id { get; set; }
        public string tradingName { get; set; }
        public string ownerName { get; set; }
        public string document { get; set; }
        public CoverageArea coverageArea { get; set; }
        public Address address { get; set; }     
    }

    public class PdvRawCollection
    {
        public PdvRawCollection()
        {
            this.pdvs = new List<PdvRaw>();
        }

        public void Add(PdvRaw pdv)
        {
            this.pdvs.Add(pdv);
        }

        public List<PdvRaw> pdvs { get; set; }    
    } 
}
