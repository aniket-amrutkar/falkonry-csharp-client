﻿///
/// falkonry-csharp-client
/// Copyright(c) 2016 Falkonry Inc
/// MIT Licensed
///

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    public class Assessment
    {
        public string id
        {
            get;
            set;
        }
        public string sourceId
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string tenant
        {
            get;
            set;
        }
        public long createTime
        {
            get;
            set;
        }
        public string createdBy
        {
            get;
            set;
        }
        public long updateTime
        {
            get;
            set;
        }
        public string updatedBy
        {
            get;
            set;
        }
       /* public object input
        {
            get;
            set;
        }
        */
        public string toJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
        public string datastream
        {
            get;
            set;
        }

        public string live
        {
            get;
            set;
        }

        public string factsMeasurement
        {
            get;
            set;
        }

        public string production
        {
            get;
            set;
        }

        public string activeModel
        {
            get;
            set;
        }

    }
}
