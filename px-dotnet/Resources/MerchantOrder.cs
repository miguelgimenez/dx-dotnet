using MercadoPago.DataStructures.MerchantOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MercadoPago.Resources
{
    public class MerchantOrder : MPBase
    {
        #region Actions
        
        public static MerchantOrder Load(string id) 
        {
            return Load(id, WITHOUT_CACHE, null);
        }

        [GETEndpoint("/merchant_orders/:id")]
        public static MerchantOrder Load(string id, bool useCache, MPRequestOptions requestOptions)
        {
            return (MerchantOrder)ProcessMethod<MerchantOrder>(typeof(MerchantOrder), "Load", id, useCache, requestOptions);
        }

        public MerchantOrder Save()
        {
            return Save(null);
        }
        
        [POSTEndpoint("/merchant_orders")]
        public MerchantOrder Save(MPRequestOptions requestOptions) 
        {
            return (MerchantOrder)ProcessMethod<MerchantOrder>("Save", WITHOUT_CACHE, requestOptions);
        }

        public MerchantOrder Update()
        {
            return Update(null);
        }
        
        [PUTEndpoint("/merchant_orders/:id")]
        public MerchantOrder Update(MPRequestOptions requestOptions)
        {
            return (MerchantOrder)ProcessMethod<MerchantOrder>("Update", WITHOUT_CACHE, requestOptions);
        }

        #endregion

        #region Properties

        [StringLength(500)]
        private string notificationUrl;
        [StringLength(600)]
        private string additionalInfo;
        [StringLength(256)]
        private string externalReference;
        [StringLength(256)]
        private string marketplace;

        #endregion

        #region Accessors
       
        public string ID
        {
            get;
            set;
            //This Accessor must be removed after testing approvement.
        }

        public string PreferenceId { get; set; }

        public DateTime? DateCreated { get; set; }


        public DateTime? LastUpdate { get; set; }

        public string ApplicationId { get; set; }

        public string Status { get; set; }

        public string SiteId { get; set; }

        public Payer Payer { get; set; }

        public Collector Collector { get; set; }

        public long? SponsorId { get; set; }

        public List<MerchantOrderPayment> Payments { get; set; }

        public decimal? PaidAmount { get; set; }

        public decimal? RefundedAmount { get; set; }

        public decimal? ShippingCost { get; set; }

        public bool? Cancelled { get; set; }

        public List<Item> Items { get; set; }

        public void AppendItem(Item item)
        {
            if (Items == null)
            {
                Items = new List<Item>();
            }
            Items.Add(item);            
        }        

        public List<Shipment> Shipments { get; set; }

        public void AppendShipment(Shipment shipment)
        {
            if (Shipments == null)
            {
                Shipments = new List<Shipment>();
            }
            Shipments.Add(shipment);            
        }

        public string NotificationUrl
        {
            get { return notificationUrl; }
            set { notificationUrl = value; }
        }

        public string AdditionalInfo
        {
            get { return additionalInfo; }
            set { additionalInfo = value; }
        }

        public string ExternalReference
        {
            get { return externalReference; }
            set { externalReference = value; }
        }

        public string Marketplace
        {
            get { return marketplace; }
            set { marketplace = value; }
        }
       
        public decimal? TotalAmount { get; set; }

        #endregion
    }
}
