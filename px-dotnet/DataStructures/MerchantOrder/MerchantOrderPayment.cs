using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MercadoPago.Common;

namespace MercadoPago.DataStructures.MerchantOrder
{
    public struct MerchantOrderPayment
    {
        #region Properties

        //public enum OperationTypeEmun
        //{
        //    RegularPayment = 0,
        //    PaymentAddition = 1
        //}

        #endregion

        #region Accessors

        public string Id { get; set; }

        public decimal TransactionAmount { get; set; }

        public decimal TotalPaidAmount { get; set; }

        public decimal ShippingCost { get; set; }

        //[StringLength(3)]
        public string CurrencyId { get; set; }

        public string Status { get; set; }

        public string StatusDetail { get; set; }

        public string OperationType { get; set; }

        public DateTime? DateApproved { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastModified { get; set; }

        public decimal AmountRefunded { get; set; }

        #endregion
    }
}
