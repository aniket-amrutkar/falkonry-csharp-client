﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace falkonry_csharp_client.helper.models
{
    class Eventbuffer
    {
        public string id
        {
            get; set;
        }

        public string sourceId
        {
            get; set;
        }

        public string name
        {
            get; set;
        }

        public string tenant
        {
            get; set;
        }

        public int createTime
        {
            get; set;
        }

        public string createdBy
        {
            get; set;
        }

        public int updateTime
        {
            get; set;
        }

        public string updatedBy
        {
            get; set;
        }

        public string getSchemaList
        {
            get; set;
        }

        public List<Subscription> subscriptionList
        {
            get;
            set
            {
                List<Subscription> subscriptionL = new List<Subscription>();
                foreach(object eachSubscription in value)
                {
                    if (eachSubscription is Subscription)
                    {
                        subscriptionL.Add((Subscription)eachSubscription);
                    }
                }
                this.subscriptionList=subscriptionL;
            }
        }

        public string toJSON()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

    }
}